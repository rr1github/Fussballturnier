using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Electron_T.Models;
using Electron_T.Classes;
using ElectronNET.API;
using System.Threading;
using ElectronNET.API.Entities;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Electron_T.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {

            DatenbankZugriff db = new DatenbankZugriff();
            //HI

            Electron.IpcMain.On("async-msg1", (args) =>
            {
                List<Mannschaft> mannschaften = db.SelectAll();
                db.ResetSimulation(mannschaften);
                //mannschaften.Clear();
                mannschaften = db.SelectAll();
                /*
                for(int i = 0; i<= mannschaften.Count-1; i++)
                {
                    for(int z= 0; z<= mannschaften.Count -1; z++)
                    {
                        if(mannschaften[i].id != mannschaften[z].id)
                        {
                            Spielpaarung spielpaarung = new Spielpaarung();
                            spielpaarung.create(mannschaften[i], mannschaften[z]);
                            Random random = new Random();
                            spielpaarung.setTore(random.Next(0, 10), random.Next(0, 10));                            
                            spielpaarung.setPunkte();
                        }
                        

                    }
                }

    */

                for (int i = 0; i <= mannschaften.Count - 1; i++)
                {
                    
                    for (int z = i + 1; z <= mannschaften.Count - 1; z++)
                    {
                        if (mannschaften[i].id != mannschaften[z].id)
                        {
                            Spielpaarung spielpaarung = new Spielpaarung();
                            
                            spielpaarung.create(mannschaften[i], mannschaften[z]);

                            Rating rating = new Rating();                           
                            spielpaarung.setTore(rating.berechneTore(mannschaften[i].bewertung), rating.berechneTore(mannschaften[z].bewertung));                            
                            spielpaarung.setPunkte();

                            string nameA = mannschaften[i].name;
                            int toreA = mannschaften[i].tore;
                            int gtoreA = mannschaften[i].gegentore;
                            int difA = mannschaften[i].differenz;

                            string nameB = mannschaften[z].name;
                            int toreB = mannschaften[z].tore;
                            int gtoreB = mannschaften[z].gegentore;
                            int difB = mannschaften[z].differenz;
                        }

                    }
                }

                db.UpdateSimulation(mannschaften);
                

                Tabelle tab = new Tabelle();
                tab.getManschaften(mannschaften);
                tab.sortiere();
                string tabelleString = tab.getTabelle();

                var mainWindow = Electron.WindowManager.BrowserWindows.First();                
                Electron.IpcMain.Send(mainWindow, "asynchronous-reply", tabelleString);

                //var browserWindowOptions = new BrowserWindowOptions { Width = 800, Height = 600 }; await Electron.WindowManager.CreateWindowAsync(browserWindowOptions);
            });

            Electron.IpcMain.On("getLigen", (args) =>
            {
                List<Liga> listLiga = db.getLigen();

                string output = "<div class=\"input-field\"> <select> <option value = \"\" disabled selected>Liga</option>";

                foreach (Liga liga in listLiga)
                {
                    output = output + "<option value = '" + liga.name + "' >" + liga.name + "</option>";

                }
                output = output + "</div>";
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                Electron.IpcMain.Send(mainWindow, "setLigen", output);

            });


            Electron.IpcMain.On("insertMessage", (args) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();                
                string insertName = args.ToString();
                db.Insert(insertName);                
                List<Mannschaft> mannschaften = db.SelectAll();
                Tabelle tab = new Tabelle();
                tab.getManschaften(mannschaften);
                string tabelleString = tab.getTabelle();    
                
                Electron.IpcMain.Send(mainWindow, "asynchronous-reply", tabelleString);
            });


            Electron.IpcMain.On("msg-rating", (args) =>
            {
                string a = args.ToString();
                //Rating rating = JsonConvert.DeserializeObject<Rating>(args.ToString());
                string[] rateInfos = a.Split(',');
                
                string rate = rateInfos[0].Substring(rateInfos[0].IndexOf(':') +1 , rateInfos[0].Length - rateInfos[0].IndexOf(':') -1);
                string id = rateInfos[1].Substring(rateInfos[1].IndexOf(':') +1 , rateInfos[1].Length - rateInfos[1].IndexOf(':') -4);

                db.UpdateRating(id, rate);

            });

            Electron.IpcMain.On("msg-delete", (args) =>
            {
                string id = args.ToString();
                db.Delete(id);
                List<Mannschaft> mannschaften = db.SelectAll();
                Tabelle tab = new Tabelle();
                tab.getManschaften(mannschaften);
                string tabelleString = tab.getTabelle();
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                Electron.IpcMain.Send(mainWindow, "asynchronous-reply", tabelleString);
            });

            

            return View();


            
            
        }
    }
}