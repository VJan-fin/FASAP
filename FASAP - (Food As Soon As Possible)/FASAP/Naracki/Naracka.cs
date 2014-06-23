using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

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

        public void AddA(OrderComponent oc)
        {
            if (oc == null)
                return;
            Stavki.Add(oc);
        }

        public void Add(OrderComponent oc)
        {
            int i = 0;
            VkupnaCena += oc.ComputePrice();
            for (i = 0; i < Stavki.Count; i++)
                if (oc.Equals(Stavki[i]))
                {
                    Stavki[i].Quantity += oc.Quantity;
                    break;
                }
            if (i == Stavki.Count)
                Stavki.Add(oc);
        }

        public void Remove(OrderComponent oc)
        {
            if (Stavki.Remove(oc))
                VkupnaCena -= oc.ComputePrice();
        }

        public override string ToString()
        {
            return String.Format("Нарачка: {0}", NarackaID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Naracka))
                return false;
            Naracka nar = obj as Naracka;
            if (nar.NarackaID != NarackaID)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual void SqlInsert(OracleConnection conn, int resID)
        {
        }
    }
}
