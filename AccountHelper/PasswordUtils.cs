using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountHelper
{
    public static class PasswordUtils
    {
        //создадим секретный ключ который будет прохониться только на защищенном сервере (в нашем случаи здесь)
        private const string SecretKey = "sdfdfSDDsverrev==11";

        public static (string passwordSalt, string passwordHash) CreatePasswordHash(string password)
        {
            //для создания хэша нужно 3 компонента:
            byte[] buffer = new byte[16];
            //преднозначение класса для закодирования
            RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(buffer);//кодируем в байты

            //на входе передаем массив байт, а навыходе строковое представление
            string passwordSalt = Convert.ToBase64String(buffer);//это мы сгенерировали соль 

            string passwordHash = GetPasswordHash(password, passwordSalt);

            return (password, passwordSalt);//возвращаем пароль и хэш

            //создание хэша пароля для хранения его в базе данных
        }

        public static bool VerifyPassword(string password, string passwordSalt, string passwordHash)
        {
            return GetPasswordHash(password, passwordSalt) == passwordHash;
        }

        public static string GetPasswordHash(string password, string passwordSalt)
        {
            password = $"{password}%{passwordSalt}%{SecretKey}";//создаем хэш
            byte[] buffer = Encoding.UTF8.GetBytes(password);//создали массив байтов

            SHA512 sha512 = new SHA512Managed();//используем массив для шифрования
            byte[] passwordHash = sha512.ComputeHash(buffer);
            return Convert.ToBase64String(passwordHash);
        }
    }
}
