using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Data
{    
    public class TypeOfMembership
    {
        public int Id { get; set; }

        public string Level { get; set; }

        public DateTime Expired { get; set; }

        public double Money { get; set; }
        
        public virtual List<Client> Clients { get; set; } = new();        
    }
}
