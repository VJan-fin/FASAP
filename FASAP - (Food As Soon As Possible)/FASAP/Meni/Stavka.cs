using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class Stavka : MenuComponent
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Opis { get; set; }
        public int Cena { get; set; }

        public Stavka(int id, string ime, decimal cena, string opis = "")
        {
            ID = id;
            Ime = ime;
            Opis = opis;
            Cena = (int)cena;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string GetDescription()
        {
            return Opis;
        }

        public override int ComputeCost()
        {
            return Cena;
        }

        public override string Print(string indent)
        {
            return String.Format("{0}{1}", indent);
        }

        public override void AddComp(MenuComponent mc)
        {
            throw new NotImplementedException();
        }

        public override void RemoveComp(MenuComponent mc)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return Ime;
        }

        public override List<MenuComponent> GetContent()
        {
            throw new NotImplementedException();
        }

        public override MenuComponent GetReference(MenuComponent mc)
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Stavka))
                return false;
            Stavka st = obj as Stavka;
            if (ID != st.ID || !Parent.Equals(st.Parent))
                return false;
            return true;
        }

        public StavkaKey GetStavkaKey()
        {
            StavkaKey sid = new StavkaKey();
            sid.IdStavka = ID;
            sid.ImeGlavno = ImeGlavno;
            return sid;
        }
    }
}
