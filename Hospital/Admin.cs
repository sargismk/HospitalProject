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

        public void AddDoctor(Doctor doctor)
        {
            //connect to db to add doctor

        }
        protected override Role GetUserRole()
        {
            return Role.ADMIN;
        }
    }
}
