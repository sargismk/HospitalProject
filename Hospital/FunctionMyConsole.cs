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
        public static void signUp(MySqlConnection con)
        {
            Console.WriteLine("SignUp how:" + "\n" + "1.Doctor" + "\n" + "2.Patient");
            int line = Convert.ToInt16(Console.ReadLine());
            if (line == 1) { }
            else if (line == 2) { }
            else { Console.WriteLine("Error:  Write correct command \n"); }
        }
    }
}
