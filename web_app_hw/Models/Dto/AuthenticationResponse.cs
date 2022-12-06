namespace web_app_hw.Models.Dto
{
    public class AuthenticationResponse
    {
        public AuthenticationStatus Status { get; set; } //возвращаем какой-то статус
        public SessionDto Session { get; set; }
    }
}
