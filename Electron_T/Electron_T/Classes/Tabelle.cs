using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace Electron_T.Classes
{
    public class Tabelle
    {
        public List<Mannschaft> teams = new List<Mannschaft>();

        public void addMannschaft(Mannschaft mannschaftNeu)
        {           
            teams.Add(mannschaftNeu);
        }

        public void getManschaften(List<Mannschaft> teams)
        {
            this.teams = teams;
        }

        public void sortiere()
        {           
           teams = teams.OrderByDescending(a => a.getDifferenz()).ToList();
           teams = teams.OrderByDescending(a => a.getPunkte()).ToList();
        }

        public string getTabelle()
        {            
            string header = "<table id=\"example\" class=\"display table table - striped\" style=\"width: 100% \"> <thead> <tr>" +
                "<th> Pos </th>" +
                "<th> Name </th>" +
                "<th> Spiele </th>" +
                "<th> Punkte </th>" +
                "<th> Tore </th>" +
                "<th> Gegentore</th>" +
                "<th> Differenz </th>" +
                "<th> Spielstärke </th>" +
                "<th> </th>" +
                "</tr> </thead> <tbody>";

            string ausgabe = "";
            for (int i = 0; i <= teams.Count -1; i++)
            {
              ausgabe = ausgabe + "<tr>" + "<td>" + (i + 1).ToString() + "</td>" + "<td>" + getImage(teams[i].getName()) + "<div class=\"d-flex align-items-center ml-3 font-weight-bold\">" + teams[i].getName() + " </div> </div> </td>" + "<td>" + teams[i].getAnzahlSpiele() + "</td>" + "<td>" + teams[i].getPunkte() + "</td>" + "<td>" + teams[i].getTore() + "</td>" + "<td>" + teams[i].getGegentore() + "</td>" + "<td>" + teams[i].getDifferenz() + "</td>" + "<td> <div class=\"ui rating\" data-rating=\"" + teams[i].bewertung + "\" data-value=\"" + teams[i].id + "\" data-max-rating=\"5\"></div></td>" +" <td> <a data-value =\"" + teams[i].id + "\"class=\"btn-sm btnDelete waves-effect red darken-3 white-text z-depth-0\"><i class=\"far fa-trash-alt\"></i></a></td>" + "</tr>";
            }
           
            string ausgabeString = header + ausgabe + "</tbody> </table>";
            return ausgabeString;
        }

        public List<Mannschaft> getTeams()
        {
            return teams;
        }

        public string getImage(string clubName)
        {
            WebClient web = new WebClient();
            var htmlCode = web.DownloadString("https://www.bing.com/search?q="+ clubName + "website");
            string anhaltePunkt = "<a href=\"https://";
            int posStart = htmlCode.IndexOf(anhaltePunkt) + anhaltePunkt.Length;
            int posEnde = 0;
            char[] kette = htmlCode.ToArray();

            for (int i = posStart; i <= kette.Length - 1; i++)
            {
                if (kette[i] == '"')
                {
                    posEnde = i -1;
                    break;
                }
            }
            string name = htmlCode.Substring(posStart, posEnde - posStart);
            if(name.Contains("/"))
            {
                name = name.Substring(0, name.LastIndexOf("/"));
            }
            
            string imageOutput = "http://logo.clearbit.com/" + name;
            imageOutput = "<div class=\"d-flex\"> <div> <img src=\"" + imageOutput + "?size=25" + "\" > </div>";
            return imageOutput;
        }
    }
}
