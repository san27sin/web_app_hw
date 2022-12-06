using web_app_hw.Models.Dto;

namespace web_app_hw.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);
        public SessionDto GetSessionInfo(string sessionToken);
    }
}
