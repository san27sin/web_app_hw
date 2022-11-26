using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using web_app_hw.Models.Dto;
using web_app_hw.Models.Request;
using web_app_hw.Services;

namespace web_app_hw.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController] 
    public class AuthenticateController : ControllerBase
    {
        #region Servises
        private readonly IAuthenticateService _authenticateService;
        private readonly IValidator<AuthenticationRequest> _authenticationRequestValidator;
        #endregion

        public AuthenticateController(IAuthenticateService authenticateService, IValidator<AuthenticationRequest> authenticationRequestValidator)
        {
            _authenticateService = authenticateService;
            _authenticationRequestValidator = authenticationRequestValidator;
        }

        //делаем 2 метода
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]//задаем в атрибутах тот класс который вернется в случае определенного статуса
        public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            ValidationResult validationResult = _authenticationRequestValidator.Validate(authenticationRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());//транслирует список ошибок которые были выявлены

            //обращаемся к нашему сервесу с методом логин
            AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);
            //клиент прошел авторизацию
            if(authenticationResponse.Status == Models.AuthenticationStatus.Success)
            {
                //заголовок в рамках ответа
                Response.Headers.Add("X-Session-Token", authenticationResponse.Session.SessionToken);
            }
            return Ok(authenticationResponse);//возвращаем ответ
        }

        [HttpGet]
        [Route("session")]
        [ProducesResponseType(typeof(SessionDto),StatusCodes.Status200OK)]
        public IActionResult GetSession()
        {
            var authorizationHeader = Request.Headers[HeaderNames.Authorization];//получаем заголовок строкой
            if(AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                var scheme = headerValue.Scheme;//Bearer
                var sessionToken = headerValue.Parameter;//Token
                if (string.IsNullOrEmpty(sessionToken))
                    return Unauthorized();

                SessionDto sessionDto = _authenticateService.GetSessionInfo(sessionToken);

                if(sessionDto == null)
                    return Unauthorized();

                return Ok(sessionDto);//сессия существует и мы ее возвращаем
            }
            return Unauthorized();
        }
    }
}
