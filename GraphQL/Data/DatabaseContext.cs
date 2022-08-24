using Microsoft.EntityFrameworkCore;
using MSGraphQL.Registry;

namespace MSGraphQL.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public static async Task CheckAndSeedDatabaseAsync(DatabaseContext context)
        {
            if (await context.Database.EnsureCreatedAsync())
            {
                var organisations = Seed.GetOrganisations();
                if (context.Organisations != null)
                {
                    context.Organisations.AddRange(organisations);
                    await context.SaveChangesAsync();
                }

                var statements = Seed.GetStatements();
                if (context.Statements != null)
                {
                    context.Statements.AddRange(statements);
                    await context.SaveChangesAsync();
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organisation>()
                .HasMany(s => s.Statements);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Organisation> Organisations { get; set; } = null!;
        public DbSet<MsStatement> Statements { get; set; } = null!;
    }
}
