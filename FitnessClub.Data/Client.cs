using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Data
{
    public class Client
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //автоматическое создание

        [ForeignKey(nameof(FitnessClub))]
        public int FitnessClubId { get; set; }

        [ForeignKey(nameof(TypeOfMembership))]
        public int TypeOfMembershipId { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Surname { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public DateTime BirthDay { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Membership { get; set; }

        public FitnessClub FitnessClub { get; set; }
        public TypeOfMembership TypeOfMembership { get; set; }
        
    }
}
