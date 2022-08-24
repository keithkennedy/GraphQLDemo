using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSGraphQL.Registry
{
    [Table("msstatement")]
    public class MsStatement
    {
        [Key]
        public int Id { get; set; }
        [Column("org_id")]
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = null;
        
        public int Year { get; set; }
        [Column("Statement")]
        public string StatementText { get; set; } = default!;
        public string Policies { get; set; } = default!;
        public string Training { get; set; } = default!;

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string ApprovedBy { get; set; } = default!;
        public DateTime ApprovedOn { get; set; }
    }
}
