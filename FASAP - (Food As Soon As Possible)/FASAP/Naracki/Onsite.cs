using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Narachki;

namespace SmetkaZaNaracka.Naracki
{
    public class Onsite : Naracka
    {
        public Onsite(int narackaID, int vkupnaCena, DateTime vreme) : base(narackaID, vkupnaCena, vreme) {}
    }
}
