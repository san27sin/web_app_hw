namespace web_app_hw.Models.Dto
{
    public class ClientDto
    {
        public int Id { get; set; } //автоматическое создание
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string Membership { get; set; }

        public FitnessClubDto club { get; set; }//автоматическое создание
    }
}
