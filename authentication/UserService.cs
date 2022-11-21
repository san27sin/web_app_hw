using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace authentication
{
    public class UserService
    {
        private string SecretCode = "48dfgfdsd$jfgghdghdfgh34253453245dsafasdfsadfgfhfgh23542345fgdsfg!";

        private IDictionary<string, string> _users = new Dictionary<string, string>()
        {
            { "user1", "password" }, // 0
            { "user2", "password" }, // 1
            { "user3", "password" }, // 2
            { "user4", "password" }, // 3
            { "user5", "password" }, // 4
        };

        public string Authenticate(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) ||
            string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;

            }

            int i = 0;

            foreach (var usr in _users)
            {
                if (string.CompareOrdinal(usr.Key, user) == 0 &&
                string.CompareOrdinal(usr.Value, password) == 0)
                {
                    return GenerateJwtToken(i, usr.Key);
                }
                i++;
            }
            return string.Empty;

        }

        private string GenerateJwtToken(int id, string name)
        {
            //1)начинаем с создания класса
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            //2)создаем дескриптор 
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
            //3)заполняем объект определенными свойствами (настройками)
            //переводим ключ из массив байт
            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            securityTokenDescriptor.SigningCredentials =//закладывается алгоритм кодирование этого токина
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);//делаем настройки, первое наш массив байт(строки), второе алгоритм

            //указываем время действия токена, от нынешнего времени
            securityTokenDescriptor.Expires = DateTime.Now.AddMinutes(10);

            //в рамках claim - можно закодировать любую информацию, например идентификатор и пользователь
            securityTokenDescriptor.Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),//создали коидровку для айди с уже заготовленным форматом
                new Claim(ClaimTypes.Name, name)//создали кодировку для имени
            });

            //создаем токен
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            //переводим токен в строку и возвращаем в виде строки
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}
