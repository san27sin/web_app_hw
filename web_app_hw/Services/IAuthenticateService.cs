using web_app_hw.Models.Dto;
using web_app_hw.Models.Request;

namespace web_app_hw.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);
        public SessionDto GetSessionInfo(string sessionToken);
    }
}
