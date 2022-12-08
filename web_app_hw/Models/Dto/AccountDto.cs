namespace web_app_hw.Models.Dto
{
    /// <summary>
    /// копируем сюда нужную инфо из аккаунта в более сжатом виде
    /// </summary>
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string EMail { get; set; }
        public bool Locked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
    }
}
