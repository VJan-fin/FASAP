using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class Dodatok : Stavka
    {
        public MenuComponent Osnovna { get; set; }

        public Dodatok(int id, string ime, decimal cena, string opis = "")
            : base(id, ime, cena, opis)
        { }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override int ComputeCost()
        {
            return base.ComputeCost() + Osnovna.ComputeCost();
        }

        public override string GetDescription()
        {
            if (Osnovna != null)
                return String.Format("{0}, {1}", Osnovna.GetDescription(), base.GetDescription());
            else return GetDescription();
        }

        public override string GetName()
        {
            if (Osnovna != null)
                return String.Format("{0}, {1}", Osnovna.GetName(), base.GetName());
            else return GetName();
        }

        public override MenuComponent GetReference(MenuComponent mc)
        {
            if (mc == null)
                throw new NotImplementedException();
            Dodatok dodatok = new Dodatok(ID, Ime, Cena, Opis);
            dodatok.Osnovna = mc;
            return dodatok;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return true;
            if(!(obj is Dodatok))
                return false;
            Dodatok dodatok = obj as Dodatok;
            if(ID != dodatok.ID)
                return false;
            if (!Osnovna.Equals(dodatok.Osnovna))
                return false;
            return true;
        }
    }
}
