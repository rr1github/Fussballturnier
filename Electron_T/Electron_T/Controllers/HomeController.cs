using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Electron_T.Models;
using Electron_T.Classes;
using ElectronNET.API;
//using MySql.Data.MySqlClient;
using System.Data.SQLite;

namespace Electron_T.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            DatenbankZugriff db = new DatenbankZugriff();
            List<Mannschaft> mannschaften = db.SelectAll();



            Berichtsheft context = HttpContext.RequestServices.GetService(typeof(Berichtsheft)) as Berichtsheft;
            string bla = context.getInfos();

            /*
             connection.Open();

            SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = "UPDATE Thema SET selected = 0";
            command.ExecuteNonQuery();
            command.Parameters.Clear();

            string themenName = textBoxHinzufügen.Text;
            string selected = "1";

            command.Parameters.AddWithValue("@Name", themenName);
            command.Parameters.AddWithValue("@selected", selected);
            command.CommandText = "Insert into Thema(Name_Thema, selected) VALUES(@Name, @selected)";

            command.ExecuteNonQuery();
            */

            Tabelle tab = new Tabelle();
            /*
            
            List<Mannschaft> mannschaften = new List<Mannschaft>();

            Mannschaft mannschaft1 = new Mannschaft();
            tab.addMannschaft(mannschaft1);
            mannschaften.Add(mannschaft1);

            Mannschaft mannschaft2 = new Mannschaft();
            tab.addMannschaft(mannschaft2);
            mannschaften.Add(mannschaft2);

            Mannschaft mannschaft3 = new Mannschaft();
            tab.addMannschaft(mannschaft3);
            mannschaften.Add(mannschaft3);

            Mannschaft mannschaft4 = new Mannschaft();
            tab.addMannschaft(mannschaft4);
            mannschaften.Add(mannschaft4);
            

            for (int i = 0; i <= mannschaften.Count() - 1; i++)
            {
                for (int z = 0; z <= mannschaften.Count() - 1; z++)
                {
                    Spielpaarung spiel1 = new Spielpaarung();
                    spiel1.create(mannschaften[i], mannschaften[z]);
                    spiel1.setTore(new Random().Next(5), new Random().Next(5));
                    spiel1.setPunkte();
                    //tab.sortiere();
                }
            }
            /*
                    Spielpaarung spiel1 = new Spielpaarung();
                    spiel1.create(mannschaft1, mannschaft2);
                    spiel1.setTore(0,2);
                    spiel1.setPunkte();
                    tab.sortiere();

            */
            tab.getManschaften(mannschaften);
            string tabelleString = tab.getTabelle();

            //return View(context.getInfos());



/*     string Connectionstring = "server={localhost};password={};database={berichtsheft};"; //SslMode=Preferred

     MySqlConnection mySqlConnection = new MySqlConnection(Connectionstring);
     mySqlConnection.Open();

     MySqlCommand command = mySqlConnection.CreateCommand();
     command.CommandText = "SELECT * FROM bericht";

     MySqlDataReader reader = command.ExecuteReader();

     while (reader.Read())
     {
         if (reader.Read())
         {
             string id = reader[0].ToString();
             string Titelname = reader[1].ToString();


         }
     }

     reader.Close();
     command.Dispose();
     mySqlConnection.Close();
     */





        Electron.IpcMain.On("async-msg", (args) =>
        {
            var mainWindow = Electron.WindowManager.BrowserWindows.First();
            Electron.IpcMain.Send(mainWindow, "asynchronous-reply", tabelleString);
        });

        return View();
    }

public IActionResult Privacy()
{
return View();
}

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
}
}
