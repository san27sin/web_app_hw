using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Data
{
    [Table("EmployeeTypes")]
    public class TypeOfMembership
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Level { get; set; }

        [Column(TypeName = "date")]
        public DateTime Expired { get; set; }
        [Column(TypeName ="money")]
        public double Money { get; set; }

        [InverseProperty(nameof(Client.TypeOfMembership))]
        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();
    }
}
