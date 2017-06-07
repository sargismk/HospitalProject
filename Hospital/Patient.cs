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
        public void TypeDate(MySqlConnection conn, string doctor_username, string patient_username)
        {
            Console.WriteLine("Enter a date like: (yyyy.MM.dd H:mm)");
            DateTime date;
            if (DateTime.TryParse(Console.ReadLine(), out date))
            {
                DB.RequestForConsultation(conn, doctor_username, patient_username, date);
                Console.WriteLine("{0:yyyy-MM-dd H:mm}", date);
            }
            else
            {
                Console.WriteLine("You have entered an incorrect value.");
                TypeDate(conn, doctor_username, patient_username);
            }

            Console.ReadLine();
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

            TypeDate(conn, doctors[line - 1], patient.getUsername());
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

            while (Reader.Read())
            {
                requests.Add((int)Reader["id"]);
                Console.Write(Reader["doctor_username"] + " " + Convert.ToDateTime(Reader["date"]).ToString("yyyy-MM-dd H:mm") + "\n");
            }
            Reader.Close();

            Console.WriteLine();
            conn.Close();
        }
        public void PatientHistory(MySqlConnection conn, Patient patient)
        {
            MySqlDataReader Reader = DB.getRequests(conn, "patient_username", patient.getUsername(), true);
            int i = 1;
            while (Reader.Read())
            {
                Console.Write(i + " " + Reader["doctor_username"] + " " + "\n");
                i++;
            }
        }

    }
}
