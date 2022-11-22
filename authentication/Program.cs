using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace authentication
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter user name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter user password: ");
            string userPassword = Console.ReadLine();

            UserService userService = new UserService();
            Console.WriteLine(userService.Authenticate(userName, userPassword));

            Console.ReadKey();
        }
    }
}