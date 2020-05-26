using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Electron_T.Classes
{
    public class Spielpaarung
    {
        private int toreA;
        private int toreB;

        Mannschaft mA;
        Mannschaft mB;

        SQLiteConnection connection;

        public Spielpaarung()
        {
            connection = new SQLiteConnection("Data Source = " + @"C:\Users\Ramon\Desktop\Fussballturnier.db" + ";Version=3;");
            connection.Open();
        }


        public void create(Mannschaft manschaft1, Mannschaft mannschaft2)
        {
            mA = manschaft1;
            mB = mannschaft2;

            mA.addAnzahlSpiele();
            mB.addAnzahlSpiele();
        }

        public void setTore(int toreA, int toreB)
        {
            this.toreA = toreA;
            mA.addTore(toreA);
            mA.addGegentore(toreB);

            this.toreB = toreB;
            mB.addTore(toreB);
            mB.addGegentore(toreA);

            mA.berechneDiff(toreA, toreB);
            mB.berechneDiff(toreB, toreA);
        }


        public void setPunkte()
        {
            if (toreA == toreB)
            {
                mA.addPunkte(1);
                mB.addPunkte(1);
            }
            else
            {
                if (toreA > toreB)
                {
                    mA.addPunkte(3);
                }
                else
                {
                    mB.addPunkte(3);
                }
            }

        }
    }

}
