

namespace web_app_hw.Models.Requests
{
    public class CreateClientRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string Membership { get; set; }

        public string club { get; set; }
    }
}
