using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountHelper
{
    public class Program
    {
        public static void Main() 
        {
            /*
            Console.WriteLine("Enter user password: ");
            string userPassword = Console.ReadLine();

            var pasHash = PasswordUtils.CreatePasswordHash(userPassword);
            var pasSalt = PasswordUtils.GetPasswordHash(userPassword, pasHash.passwordSalt);
            Console.WriteLine($"PasswardSalt: {pasHash.passwordHash}\nPasswordHash: {pasSalt}");

            Console.ReadKey();
            */
            Console.WriteLine(PasswordUtils.CreatePasswordHash("12345"));


        }
    }
}
