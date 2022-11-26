using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Data
{
    //[Table("FitnessClubs")]
    public class FitnessClub
    {
        public int Id { get; set; }//автоматически

        public string Rank { get; set; }

        public string Location { get; set; }
                
        public virtual List<Client> Clients { get; set; } = new ();
        
    }
}
