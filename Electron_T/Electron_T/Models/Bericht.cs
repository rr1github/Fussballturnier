using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electron_T.Models
{
    public class Bericht
    {
        public int ID { get; set; }
        public int kalenderwoche { get; set; }
        public string jahr { get; set; }
        public string text { get; set; }

    }
}
