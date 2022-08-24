using HotChocolate.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSGraphQL.Registry
{
    [Table("organisation")]
    public class Organisation 
    {
        [Key]
        public int Id { get; set; }
        
        // No one can query this
        [GraphQLIgnore]
        public string GovId { get; set; } = default!;
        public string CompanyId { get; set; } = default!;
        [Column("Name")]
        [GraphQLName("name")]
        public string OrganisationName { get; set; } = default!;
        
        // Only admins can query this
        [Authorize(Roles = new string[] { "Admin" })]
        public string Address { get; set; } = default!;
        public string Turnover { get; set; } = default!;

        public ICollection<MsStatement> Statements { get; set; } = new HashSet<MsStatement>();
    }
}
