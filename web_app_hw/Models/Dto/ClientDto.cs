namespace web_app_hw.Models.Dto
{
    public class ClientDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDay { get; set; }        
        public int FitnessClubId { get; set; }
        public virtual int TypeOfMembershipId { get; set; }
    }
}
