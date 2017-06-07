using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class FunctionMyConsole
    {
        private static List<User> allUsers = new List<User>();
        public static void Password()
        {
            SecureString pwd = new SecureString();
            while (true)
            {
                ConsoleKeyInfo k = Console.ReadKey(true);
                if (k.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (k.Key == ConsoleKey.Backspace)
                {
                    pwd.RemoveAt(pwd.Length - 1);
                    Console.Write("\b \b");
                }
                else
                {
                    pwd.AppendChar(k.KeyChar);
                    Console.Write("*");
                }
            }
        }
    }
}
