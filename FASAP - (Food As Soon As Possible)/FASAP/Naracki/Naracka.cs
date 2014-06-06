using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka.Narachki
{
    public class Naracka
    {
        public int NarackaID { get; set; }
        public int VkupnaCena { get; set; }
        public DateTime Vreme { get; set; }

        public Naracka(int narackaID, int vkupnaCena, DateTime vreme)
        {
            NarackaID = narackaID;
            VkupnaCena = vkupnaCena;
            Vreme = vreme;
        }


    }
}
