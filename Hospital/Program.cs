using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Hospital
{
    class Program : FunctionMyConsole
    {
        public static string UserRole;
        public static string UserUsername;
        public static string UserPassword;

        static void Main(string[] args)
        {
            Start(false);
        }

        public static void Init(MySqlConnection conn, string role, string username, string password)
        {
            int line;
            switch (role)
            {
                case "doctor":
                    Doctor doctor = new Doctor(username, password);
                    doctor.ActivCommands();
                    line = Convert.ToInt16(Console.ReadLine());
                    switch (line)
                    {
                        case 2:
                            doctor.MyRequest(conn, doctor);
                            break;
                    }
                    break;
                case "patient":
                    Patient patient = new Patient(username, password);
                    patient.ActivCommands();
                    line = Convert.ToInt16(Console.ReadLine());
                    switch (line)
                    {
                        case 1:
                            patient.RequestForConsultation(conn, patient);
                            break;
                        case 2:
                            patient.MyRequest(conn, patient);
                            break;
                    }
                    break;
                case "admin":
                    Admin admin = new Admin(username, password);
                    admin.ActivCommands();
                    break;
            }

            Console.ReadLine();
        }

        public static void DoSignIn(MySqlConnection conn, string username, string password)
        {
            UserUsername = username;
            UserPassword = password;
            string role = UserRole = DB.signIn(conn, username, password);

            Init(conn, role, username, password);
        }

        public static void Start(Boolean signedIn)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            if (signedIn)
            {
                Init(conn, UserRole, UserUsername, UserPassword);
            }
            else
            {
                Console.WriteLine("1.SignUp" + "\n" + "2.SignIn");
                int line = Convert.ToInt16(Console.ReadLine());
                Console.Write("Username: ");
                string username = Console.ReadLine().Replace("Username: ", "");
                Console.Write("Password: ");
                string password = Console.ReadLine().Replace("Password: ", "");
                if (line == 1)
                {
                    DB.signUp(conn, username, password, "patient", (result) =>
                    {
                        switch(result)
                        {
                            case 1:
                                Console.WriteLine("You are successfuly signed up");
                                DoSignIn(conn, username, password);
                                break;
                            case 2:
                                Console.WriteLine("Username: " + username + " already exists");
                                Start(false);
                                break;
                            case 3:
                                Console.WriteLine("Error: something went wrong");
                                break;
                        }
                    });
                }
                else if (line == 2)
                {
                    DoSignIn(conn, username, password);
                }
                else Console.WriteLine("Error: qqqq Write correct command \n");
                Console.ReadLine();
            }
        }
    }
}
//Patient patient = new Patient("patient", "patient");
//Doctor doctor = new Doctor("doctor", "doctor");
//doctor.AddPatient(patient);
//doctor.GetPatientHistory();


