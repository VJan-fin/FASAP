using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Narachki;

namespace SmetkaZaNaracka.Naracki
{
    public class Onsite : Naracka
    {
        public int BrojMasa { get; set; }

        public Onsite(int narackaID, int vkupnaCena, DateTime vreme, int brojMasa)
            : base(narackaID, vkupnaCena, vreme)
        {
            BrojMasa = brojMasa;
        }
    }
}
