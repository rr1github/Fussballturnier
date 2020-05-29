using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Electron_T.Classes
{
    public class DatenbankZugriff
    {
        SQLiteConnection connection;
        List<Mannschaft> mannschaftList = new List<Mannschaft>();        

        public DatenbankZugriff()
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"datenbank\Fussballturnier.db";
            
            connection = new SQLiteConnection("Data Source = " + dbPath + ";Version=3;");
            connection.Open();
        }

        
        public List<Mannschaft> SelectAll()
        {
            mannschaftList.Clear();
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            
            string query = "SELECT * FROM mannschaft ORDER BY punkte desc, differenz desc";            

            command.CommandText = query;

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Mannschaft mannschaft = new Mannschaft();

                mannschaft.id = Convert.ToInt32(reader["id"]);
                mannschaft.name = reader["name"].ToString();
                mannschaft.anzahlSpiele = Convert.ToInt32(reader["anzahlSpiele"]);
                mannschaft.punkte = Convert.ToInt32(reader["punkte"]);
                mannschaft.tore = Convert.ToInt32(reader["tore"]);
                mannschaft.gegentore = Convert.ToInt32(reader["gegentore"]);
                mannschaft.differenz = Convert.ToInt32(reader["differenz"]);
                mannschaft.bewertung = Convert.ToInt32(reader["bewertung"]);

                mannschaftList.Add(mannschaft);
            }
            reader.Close();
            return mannschaftList;
        }

        public void Insert(string insertName)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;

            command.Parameters.AddWithValue("@name", insertName);
            command.CommandText = "INSERT INTO mannschaft (name, anzahlSpiele, punkte, tore, gegentore, differenz, bewertung) VALUES (@name ,0,0,0,0,0,3)";
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public void UpdateRating(string id, string rating)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@bewertung", rating);
            command.CommandText = "UPDATE mannschaft SET bewertung = @bewertung WHERE id = @id";
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }


        public void UpdateSimulation(List<Mannschaft> listTeams)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;

            foreach(Mannschaft team in listTeams)
            {
                command.CommandText = "UPDATE mannschaft SET punkte = " + team.punkte + ", tore = " + team.tore + ", gegentore = " + team.gegentore + ", differenz = " + team.differenz + ", anzahlSpiele = " + team.anzahlSpiele + " WHERE id = " + team.id;
                command.ExecuteNonQuery();
            }
           
        }

        public void ResetSimulation(List<Mannschaft> listTeams)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;

            foreach (Mannschaft team in listTeams)
            {
                command.CommandText = "UPDATE mannschaft SET punkte = " + 0 + ", tore = " + 0 + ", gegentore = " + 0 + ", differenz = " + 0 + ", anzahlSpiele = " + 0 + " WHERE id = " + team.id;
                command.ExecuteNonQuery();
            }

        }

        public void Delete(string id)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM mannschaft WHERE id =" + id;
            command.ExecuteNonQuery();
        }
    }
}
