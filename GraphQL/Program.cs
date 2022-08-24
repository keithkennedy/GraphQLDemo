using MSGraphQL.Data;
using MSGraphQL.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPooledDbContextFactory<DatabaseContext>(o => 
        o.UseSqlite("Data Source=graphql.sqlite3"));

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .InitializeOnStartup() // loads schema on start - rather than when first query is issued
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterDbContext<DatabaseContext>(DbContextKind.Pooled);

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JwtKey"]));
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = "audience",
                ValidIssuer = "issuer",
                RequireSignedTokens = false,
                IssuerSigningKey = signingKey
            };
    });

builder.Services.AddControllers();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await DatabaseContext.CheckAndSeedDatabaseAsync(
        app.Services.GetRequiredService<IDbContextFactory<DatabaseContext>>()
        .CreateDbContext());
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(e =>
{
    e.MapGraphQL("/");
});

app.Run();
