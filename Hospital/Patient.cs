using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hospital
{
    class Patient : User
    {
        public Patient(String username, String password) : base(username, password)
        {
           
        }
        public void RequestForConsultation(MySqlConnection conn, Patient patient) {
            MySqlDataReader Reader = DB.getUsers(conn, "doctor");

            List<string> doctors = new List<string>();

            int i = 1;
            while (Reader.Read())
            {
                doctors.Add((string)Reader["username"]);
                Console.Write(i + " " + Reader["username"] + "\n");
                i++;
            }
            Reader.Close();

            int line = Convert.ToInt16(Console.ReadLine());

            DB.RequestForConsultation(conn, doctors[line - 1], patient.getUsername());

            Console.ReadLine();
        }

        protected override Role GetUserRole()
        {
            return Role.PATIENT;
        }

        public override void ActivCommands()
        {
            int i=1;
            foreach (string c in Enum.GetNames(typeof(PatientCommand)))
            {
                Console.WriteLine(i + " " + c);
                i++;
            }
        }
        public void MyRequest(MySqlConnection conn, Patient patient)
        {
            MySqlDataReader Reader = DB.getRequests(conn, "patient_username", patient.getUsername(), true);
            List<int> requests = new List<int>();

            int i = 1;
            while (Reader.Read())
            {
                requests.Add((int)Reader["id"]);
                Console.Write(i + " " + Reader["doctor_username"] + "\n");
                i++;
            }
            Reader.Close();

            Console.ReadLine();
        }

    }
}
