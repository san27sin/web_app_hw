using web_app_hw.models;

namespace web_app_hw.Models.Requests
{
    public class CreateClientRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string Membership { get; set; }

        public FitnessClub club { get; set; }
    }
}
