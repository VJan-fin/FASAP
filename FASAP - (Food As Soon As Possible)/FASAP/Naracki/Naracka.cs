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
        public List<OrderComponent> Stavki { get; set; }

        public Naracka(int narackaID, int vkupnaCena, DateTime vreme)
        {
            NarackaID = narackaID;
            VkupnaCena = vkupnaCena;
            Vreme = vreme;
            Stavki = new List<OrderComponent>();
        }

        public void Add(OrderComponent oc)
        {
            Stavki.Add(oc);
        }

        public override string ToString()
        {
            return String.Format("Нарачка: {0}", NarackaID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if(!(obj is Naracka))
                return false;
            Naracka nar = obj as Naracka;
            if (nar.NarackaID != NarackaID)
                return false;
            return true;
        }
    }
}
