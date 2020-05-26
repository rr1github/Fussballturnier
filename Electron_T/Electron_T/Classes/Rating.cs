using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electron_T.Classes
{
    public class Rating
    {
        public int rate { get; set; }
        public string id { get; set; }


        public int berechneTore(int bewertung)
        {
            int tore = 0;
            Random random = new Random();            
            switch (bewertung)
            {
                case 1:
                    tore = random.Next(0, 2);
                    break;
                case 2:
                    tore = random.Next(0, 3);
                    break;
                case 3:
                    tore = random.Next(1, 4);
                    break;
                case 4:
                    tore = random.Next(2, 5);
                    break;
                case 5:
                    tore = random.Next(2, 8);
                    break;
            }

            return tore;
        }
    }
}
