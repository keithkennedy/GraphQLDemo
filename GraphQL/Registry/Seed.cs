using Microsoft.AspNetCore.Identity;

namespace MSGraphQL.Registry
{
    public class Seed
    {
        public static IEnumerable<Organisation> GetOrganisations()
        {
            yield return new Organisation
            {
                Id = 1,
                OrganisationName = "Big House PLC.",
                CompanyId = "BH001",
                GovId = "cab204ba-1978-11ed-b61b-b42e99ed6ad5",
                Turnover = "Under 30 Million",
                Address = "123 Long Road"
            };
            yield return new Organisation
            {
                Id = 2,
                OrganisationName = "Fresh Food PLC.",
                CompanyId = "FF001",
                GovId = "cab204ba-1978-11ed-b61b-b42e99ed6ad6",
                Turnover = "Over 60 Million",
                Address = "1 Big Tower"
            };
        }

        public static IEnumerable<MsStatement> GetStatements()
        {
            yield return new MsStatement
            {
                Id = 1,
                Start = new DateTime(2020, 12, 01),
                End = new DateTime(2021, 12, 01),
                OrganisationId = 1,
                Policies = "All",
                Training = "All",
                Year = 2022,
                StatementText = "Generic text",
                ApprovedOn = new DateTime(2022, 05, 01),
                ApprovedBy = "Keith Kennedy"
            };

            yield return new MsStatement
            {
                Id = 2,
                Start = new DateTime(2019, 12, 01),
                End = new DateTime(2020, 12, 01),
                OrganisationId = 1,
                Policies = "All",
                Training = "Not all",
                Year = 2021,
                StatementText = "Generic text",
                ApprovedOn = new DateTime(2021, 05, 01),
                ApprovedBy = "Keith Kennedy"
            };

            yield return new MsStatement
            {
                Id = 3,
                Start = new DateTime(2020, 12, 01),
                End = new DateTime(2021, 12, 01),
                OrganisationId = 2,
                Policies = "All",
                Training = "All",
                Year = 2022,
                StatementText = "Generic text",
                ApprovedOn = new DateTime(2022, 05, 01),
                ApprovedBy = "Bob Bobby"
            };
        }
    }
}
