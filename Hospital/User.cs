using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hospital
{
    abstract class User
    {
        protected String username;
        protected String password;
        private Role role;

        public User(String username, String password) {
            this.username = username;
            this.password = password;
            role = GetUserRole();
        }
        protected abstract Role GetUserRole();
        public abstract void ActivCommands();

        public String getUsername() {
            return this.username;
        }
        public String getPassword() {
            return this.password;
        }
        public Role GetRole()
        {
            return this.role;
        }
    }
}
