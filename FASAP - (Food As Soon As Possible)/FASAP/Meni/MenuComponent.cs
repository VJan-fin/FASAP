using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using SmetkaZaNaracka.Naracki;

namespace SmetkaZaNaracka
{
    public abstract class MenuComponent
    {
        public MenuComponent Parent { get; set; }
        public String ImeGlavno { get; set; }
        public VklucuvaKey DodatokKey { get; set; }
        public VklucuvaKey VklucuvaKey { get; set; }
        public int Kolicina { get; set; }

        public VklucuvaKey getVklucuvaKey()
        {
            return VklucuvaKey;
        }

        public VklucuvaKey getDodatokKey()
        {
            return DodatokKey;
        }

        public abstract List<MenuComponent> GetContent();

        public abstract string GetName();

        public abstract string GetDescription();

        public abstract int ComputeCost();

        public abstract string Print(string indent);

        public abstract void AddComp(MenuComponent mc);

        public abstract void RemoveComp(MenuComponent mc);

        public abstract void SetName(String name);

        public abstract void SetDescription(String description);

        public abstract void SetCost(int cost);

        /// <summary>
        /// Важен метод за да се креира нов објект идентичен на постоечкиот кој ќе уечествува во нарачката.
        /// За разлика од него конкретниот од кој се зема референцата постои во менито.
        /// </summary>
        /// <param name="mc"></param>
        /// <returns></returns>
        public abstract MenuComponent GetReference(MenuComponent mc);

        public override string ToString()
        {
            return GetName();
        }

        public virtual bool Sodrzi(Object obj)
        {
            return this.Equals(obj);
        }

        public abstract void SqlInsert(OracleConnection conn, int resID);

        public abstract void SqlDelete(OracleConnection conn, int resID);

        public abstract void SqlActivate(OracleConnection conn, int resID);

        public abstract void SQLUpdate(OracleConnection conn, int resID);

        public abstract int SqlVklucuva(OracleConnection conn, OracleTransaction myTrans, int resID, int narID, int vkID, int q);
    }
}
