﻿using System;
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
                if (line == 1)
                {
                }
                else if (line == 2)
                {
                    Console.Write("Username: ");
                    string username = Console.ReadLine().Replace("Username: ", "");
                    Console.Write("Password: ");
                    string password = Console.ReadLine().Replace("Password: ", "");
                    UserUsername = username;
                    UserPassword = password;
                    string role = UserRole = DB.signIn(conn, username, password);

                    Init(conn, role, username, password);
                }
                else Console.WriteLine("Error: qqqq Write correct command \n");
                Console.ReadLine();
            }
        }
    }
}
//Patient patient = new Patient("patient", "patient");hesa kgam du araok
//Doctor doctor = new Doctor("doctor", "doctor");
//doctor.AddPatient(patient);
//doctor.GetPatientHistory();
//pateint historyin veradzardzuma ira patientnerin @ndhamen@
//??chi kara @tex pacientin accept ani?
//ed funkcian patientnerin veradzardznelu hamara xi @tex accept ani??? ba vorna accept anelu?
