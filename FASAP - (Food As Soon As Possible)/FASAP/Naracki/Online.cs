using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Narachki;

namespace SmetkaZaNaracka.Naracki
{
    public class Online : Naracka
    {
        public Online(int narackaID, int vkupnaCena, DateTime vreme) : base(narackaID, vkupnaCena, vreme) { }
    }
}
