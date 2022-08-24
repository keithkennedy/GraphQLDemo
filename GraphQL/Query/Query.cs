using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MSGraphQL.Data;
using MSGraphQL.Registry;

namespace MSGraphQL.Query
{
    public class Query
    {
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Organisation> GetOrganisations(DatabaseContext context)
        {
            return context.Organisations;
        }

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<MsStatement> GetStatements(DatabaseContext context)
        {
            return context.Statements;
        }
    }
}
