namespace web_app_hw.models
{
    public class Client
    {
        public int Id { get; set; } //автоматическое создание
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string Membership { get; set; }

        public FitnessClub club { get; set; }//автоматическое создание
    }
}
