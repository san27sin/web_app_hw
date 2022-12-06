namespace web_app_hw.Models.Dto
{
    public class SessionDto
    {
        //идентификатор сессии
        public int SessionId { get; set; }
        //инфо о токене
        public string SessionToken { get; set; }
        //инфо о аккаунте
        public AccountDto Account { get; set; }

    }
}
