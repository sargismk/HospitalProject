using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hospital
{
    class DB
    {

        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database+ ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
        public static void signUp(MySqlConnection conn, string username, string password, string role, string profession, Action<int> callback)
        {
            MySqlCommand user = new MySqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", conn);
            user.Parameters.AddWithValue("@username", username);
            user.Parameters.AddWithValue("@password", password);
            MySqlDataReader Reader = user.ExecuteReader();
            if (!Reader.HasRows)
            {
                try
                {
                    Reader.Close();
                    MySqlCommand exists_user = new MySqlCommand("INSERT INTO users (username, password, role, profession) VALUES (@username, @password, @role, @profession)", conn);
                    exists_user.Parameters.AddWithValue("@username", username);
                    exists_user.Parameters.AddWithValue("@password", password);
                    exists_user.Parameters.AddWithValue("@role", role);
                    exists_user.Parameters.AddWithValue("@profession", profession);
                    MySqlDataReader ReaderExistsUser = exists_user.ExecuteReader();
                    ReaderExistsUser.Close();
                    callback(1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    callback(3);
                }
            }
            else
            {
                Reader.Close();
                callback(2);
            }
        }
        public static string signIn(MySqlConnection conn, string username, string password)
        {
            MySqlCommand user = new MySqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", conn);
            user.Parameters.AddWithValue("@username", username);//@usernamin veragruma usernamei dimac grac@
            user.Parameters.AddWithValue("@password", password);
            MySqlDataReader Reader = user.ExecuteReader();//bazayi ed toxi amboxj texekutyun@ sarguma obyekt
            if (!Reader.HasRows)
            {
                Console.WriteLine("Error: Username and Password dont match \n");
                return "error";
            }
            else
            {
                Reader.Read();
                string role = (string)Reader["role"];
                Reader.Close();

                return role;
            }
        }
        public static MySqlDataReader getUsers(MySqlConnection conn, string role)
        {
            MySqlCommand users = new MySqlCommand("SELECT * FROM users WHERE role = @role", conn);
            users.Parameters.AddWithValue("@role", role);
            MySqlDataReader Reader = users.ExecuteReader();
            if (!Reader.HasRows)
            {
                Console.WriteLine("Error: There are not " + role + " \n");
            }

            return Reader;
        }
        public static void RequestForConsultation(MySqlConnection conn, string doctor_username, string patient_username, DateTime date)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("INSERT INTO requests (doctor_username, patient_username, date) VALUES (@doctor_username, @patient_username, @date)", conn);
                comm.Parameters.AddWithValue("@doctor_username", doctor_username);
                comm.Parameters.AddWithValue("@patient_username", patient_username);
                comm.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd H"));

                comm.ExecuteNonQuery();

                Console.WriteLine("Request sended");

                conn.Close();

                Program.Start(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public static MySqlDataReader getRequests(MySqlConnection conn, string db_user_username, string user_username, Boolean accepted)
        {
            MySqlCommand requests = new MySqlCommand("SELECT * FROM requests WHERE " + db_user_username + " = @user_username AND accepted = @accepted AND date >= CURDATE()", conn);
            requests.Parameters.AddWithValue("@user_username", user_username);
            requests.Parameters.AddWithValue("@accepted", accepted);
            MySqlDataReader Reader = requests.ExecuteReader();
            if (!Reader.HasRows)
            {
                Console.WriteLine("There are no requests in this time \n");
            }
            return Reader;
        }
        public static void AcceptRequest(MySqlConnection conn, int id)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("UPDATE requests SET accepted = @accepted WHERE id = @id", conn);

                comm.Parameters.AddWithValue("@accepted", true);
                comm.Parameters.AddWithValue("@id", id);

                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}