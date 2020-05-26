using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Electron_T.Models
{
    public class Berichtsheft
    {
        public string ConnectionString { get; set; }

        public Berichtsheft(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("server=localhost;port=3306;database=berichtsheft;user=root;password=;");
        }

        public string getInfos()
        {
            string bla = "";
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from bericht where id = 162", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bla = reader.GetValue(0).ToString();
                    }
                }
            }

            return bla; 
        }


    }
}
