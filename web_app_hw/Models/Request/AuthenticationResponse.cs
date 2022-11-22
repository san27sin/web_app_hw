using web_app_hw.Models.Dto;

namespace web_app_hw.Models.Request
{
    public class AuthenticationResponse
    {
        public AuthenticationStatus Status { get; set; }
        public SessionDto Session { get; set; }
    }
}
