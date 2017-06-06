using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Doctor : User
    {
        IList<Patient> requestPatients;

        public Doctor(String username, String password) : base( username,  password)
        {
            requestPatients = new List<Patient>();
        }
        public void ServePatient(Patient patient)
        {
            requestPatients.Remove(patient);
        }
        public void AddPatient(Patient patient) {
            if (requestPatients != null) {
                requestPatients.Add(patient);
            }
        }
        public void MyRequest(MySqlConnection conn, Doctor doctor) {
            MySqlDataReader Reader = DB.getRequests(conn, "doctor_username", doctor.getUsername(), false);

            List<int> requests = new List<int>();

            int i = 1;
            while (Reader.Read())
            {
                requests.Add((int)Reader["id"]);
                Console.Write(i + " " + Reader["patient_username"] + " " + Reader["accepted"] + "\n");
                i++;
            }
            Reader.Close();

            int line = Convert.ToInt16(Console.ReadLine());

            DB.AcceptRequest(conn, requests[line - 1]);

            Console.ReadLine();
        }
        protected override Role GetUserRole()
        {
            return Role.DOCTOR;
        }

        public override void ActivCommands()
        {
            int i = 1;
            foreach (string c in Enum.GetNames(typeof(DoctorCommand)))
            {
                Console.WriteLine(i + " " + c);
                i++;
            }
        }
    }
}
