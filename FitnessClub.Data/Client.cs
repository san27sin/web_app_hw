using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FitnessClub.Data
{
    public class Client
    {
        public int Id { get; set; }//автоматическое создание
        public int FitnessClubId { get; set; } 
        public virtual int TypeOfMembershipId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime BirthDay { get; set; }              

        //объекты на связь с нашим объектом мы не помечаем для записи в таблице
        public FitnessClub? FitnessClub { get; set; }
        public TypeOfMembership? TypeOfMembership { get; set; }        
    }
}
