using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Admin : User
    {
        public Admin(String username, String password) : base( username,  password)
        {
           
        }

        public override void ActivCommands()
        {
            int i = 1;
            foreach (string c in Enum.GetNames(typeof(AdminCommand)))
            {
                Console.WriteLine(i + " " + c);
                i++;
            }
        }

        public void AddDoctor(MySqlConnection conn,string username,string password,string profession)
        {
            DB.signUp(conn, username, password, "doctor",profession, (result) =>
            {
            switch (result)
            {
                case 1:
                    Console.WriteLine("Doctor are successfully created");
                    break;
                case 2:
                    Console.WriteLine("Username: " + username + " already exists");
                    break;
                case 3:
                    Console.WriteLine("Error: something went wrong");
                    break;
            }
        });
        }
        protected override Role GetUserRole()
        {
            return Role.ADMIN;
        }
    }
}
