using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electron_T.Classes
{
    public class Mannschaft
    {
        public int id { get; set; }
        public string name { get; set; }
        public int punkte { get; set; }
        public int tore { get; set; }
        public int gegentore { get; set; }
        public int anzahlSpiele { get; set; }
        public int differenz { get; set; }
        public int bewertung { get; set; }


        public void addPunkte(int punkte)
        {
            this.punkte = this.punkte + punkte;
        }

        public void addTore(int tore)
        {
            this.tore = this.tore + tore;
        }

        public void addGegentore(int gegentore)
        {
            this.gegentore = this.gegentore + gegentore;
        }

        public int getTore()
        {
            return tore;
        }

        public int getGegentore()
        {
            return gegentore;
        }

        public int getPunkte()
        {
            return punkte;
        }

        public int getID()
        {
            return id;
        }

        public int berechneDiff(int tore, int gegentore)
        {
            int diff = tore - gegentore;
            differenz = differenz + diff;

            return differenz;
        }

        public int getDifferenz()
        {
            return differenz;
        }

        public void addAnzahlSpiele()
        {
            anzahlSpiele = anzahlSpiele + 1;
        }

        public int getAnzahlSpiele()
        {
            return anzahlSpiele;
        }

        public String getName()
        {
            return name;
        }

    }

}
