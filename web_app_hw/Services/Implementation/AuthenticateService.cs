using FitnessClub.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using web_app_hw.Models.Dto;

namespace web_app_hw.Services.Implementation
{
    //Формирование контроллера для аутенфикации
    public class AutnenticateService : IAuthenticateService
    {

        public const string SecretKey = "48dfgfdsd$jfgghdghdfgh34253453245dsafasdfsadfgfhfgh23542345fgdsfg!";
        //коллекция сессий пользователей нашего пользователя
        private readonly Dictionary<string, SessionDto> _sessions = new Dictionary<string, SessionDto>();

        #region Services
        private IServiceScopeFactory _serviceScopeFactory;//динамическое создание и получение scope сервисов
        #endregion

        public AutnenticateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        ///нужен для проверки сессии которой владеет наш клиент
        public SessionDto GetSessionInfo(string sessionToken)
        {
            SessionDto sessionDto = null;

            lock (_sessions)//ограничивает вход потоков по одному, не дает использовать сразу нескольким потокам
            {
                _sessions.TryGetValue(sessionToken, out sessionDto);
            }

            if (sessionDto == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();

                FitnessClubDb context = scope.ServiceProvider.GetRequiredService<FitnessClubDb>();

                AccountSession session = context
                    .AccountSessions
                    .FirstOrDefault(s => s.SessionToken == sessionToken);//находим сессию 

                if (sessionDto == null)
                    return null;

                Account account = context.Accounts.FirstOrDefault(item => item.AccountId == session.AccountId);//сравниваем объект сессии и объект account 

                sessionDto = GetSessionDto(account, session);//склеиваем объект и получаем sessiondto

                if (sessionDto != null)
                {
                    lock (_sessions)
                    {
                        _sessions[sessionToken] = sessionDto;//помщяем сессию в коллекцию 
                    }
                }
            }

            return sessionDto;
        }

        /// <summary>
        /// Проверка учетной записи нашего пользователя
        /// </summary>
        /// <param name="authenticationRequest"></param>
        /// <returns></returns>
        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            //обращаемся к нашему контексту и запрашиваем
            FitnessClubDb context = scope.ServiceProvider.GetRequiredService<FitnessClubDb>();

            Account? account =
                !string.IsNullOrWhiteSpace(authenticationRequest.Login) ?
                FindAccountByLogin(context, authenticationRequest.Login) : null;

            if (account == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.UserNotFound,
                };
            }

            if (!PasswordUtils.VerifyPassword(authenticationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.InvalidPassword
                };
            }

            AccountSession session = new AccountSession
            {
                AccountId = account.AccountId,
                SessionToken = CreateSessionToken(account),
                TimeCreated = DateTime.Now,
                TimeClosed = DateTime.Now,
                IsClosed = false
            };

            context.AccountSessions.Add(session);
            context.SaveChanges();

            SessionDto sessionDto = GetSessionDto(account, session);

            lock (_sessions)
            {
                _sessions[session.SessionToken] = sessionDto;
            }

            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Success,
                Session = sessionDto
            };
        }

        public SessionDto GetSession(string sessionToken)
        {
            throw new NotImplementedException();
        }


        private SessionDto GetSessionDto(Account account, AccountSession accountSession)
        {
            return new SessionDto
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                Account = new AccountDto
                {
                    AccountId = account.AccountId,
                    EMail = account.EMail,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    SecondName = account.SecondName,
                    Locked = account.Locked,
                }

            };
        }


        private Account FindAccountByLogin(FitnessClubDb context, string login)
        {
            return context
                .Accounts
                .FirstOrDefault(account => account.EMail == login);
        }

        private string CreateSessionToken(Account account)
        {
            //1)начинаем с создания класса
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //переводим ключ из массив байт
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);

            //2)создаем дескриптор 
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
            //3)заполняем объект определенными свойствами (настройками)


            securityTokenDescriptor.SigningCredentials =//закладывается алгоритм кодирование этого токина
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);//делаем настройки, первое наш массив байт(строки), второе алгоритм

            //указываем время действия токена, от нынешнего времени
            securityTokenDescriptor.Expires = DateTime.Now.AddMinutes(10);

            //в рамках claim - можно закодировать любую информацию, например идентификатор и пользователь
            securityTokenDescriptor.Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),//создали коидровку для айди с уже заготовленным форматом
                new Claim(ClaimTypes.Name, account.EMail)//создали кодировку для имени
            });

            //создаем токен
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            //переводим токен в строку и возвращаем в виде строки
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}
