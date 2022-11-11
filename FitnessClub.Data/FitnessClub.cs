using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Data
{
    public class FitnessClub
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//автоматически
        [Column(TypeName = "nvarchar(128)")]
        public string Rank { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Location { get; set; }

        [InverseProperty(nameof(Client.FitnessClub))]
        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();
    }
}
