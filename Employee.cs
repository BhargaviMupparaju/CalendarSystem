using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CalendarSystem
{
    class Employee
    {
        int userid;
        String password;
        String role;

        public Employee(int i, String p)
        {
            userid = i;
            password = p;
        }

        public Employee()
        {
        }

        public String getRole()
        {
            return role;

        }

        public bool verifyData(int uid, String pass)
        {
            bool result = false;
            string connStr = "server=157.89.28.29;user=student;database=csc340_db;port = 3306; password = Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT role FROM mupparajuemployee WHERE employeeid=@userid and password=@password";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@userid", uid);
                cmd.Parameters.AddWithValue("@password", pass);
                MySqlDataReader myReader = cmd.ExecuteReader();

                if (myReader.Read())
                {
                    role = myReader[0].ToString();
                    Console.WriteLine("numberOfConflictEvents " + role);
                    result = true;
                }
                else
                    result = false;
                myReader.Close();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return result;
        }
        public bool login(int userid, String password)
        {
            bool result = verifyData(userid, password);
            return result;
        }
    }
}
