﻿using System;
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
        {
            Osnovna = null;
        }

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

        public override bool Sodrzi(Object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Dodatok))
                return false;
            Dodatok dodatok = obj as Dodatok;
            if (ID != dodatok.ID || !Parent.Equals(dodatok.Parent))
                if (Osnovna != null)
                    return Osnovna.Sodrzi(obj);
            return true;
        }

        public override string GetName()
        {
            if (Osnovna != null)
                return String.Format("{0}, {1}", Osnovna.GetName(), base.GetName());
            else return base.GetName();
        }

        public override MenuComponent GetReference(MenuComponent mc)
        {
            if (mc == null)
                throw new NotImplementedException(String.Format("Не е можно да го нарачате само додатокот \"{0}\"", GetName()));
            MenuComponent pom = this;
            while (pom.Parent != null)
            {
                if (pom.Parent.Equals(mc.Parent))
                    break;
                pom = pom.Parent;
            }
            if (pom.Parent == null)
                throw new NotImplementedException(String.Format("Додатокот \"{0}\" не е соодветен на производот \"{1}\"", this.GetName(), mc.GetName())); 
            if (mc.Sodrzi(this))
                throw new NotImplementedException(String.Format("Производот \"{0}\" веќе го содржи додатокот \"{1}\"", mc.GetName(), GetName()));
            Dodatok dodatok = new Dodatok(ID, Ime, Cena, Opis);
            dodatok.Parent = Parent;
            dodatok.Osnovna = mc;
            return dodatok;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if(!(obj is Dodatok))
                return false;
            Dodatok dodatok = obj as Dodatok;
            if(ID != dodatok.ID || !Parent.Equals(dodatok.Parent))
                return false;
            if (!Osnovna.Equals(dodatok.Osnovna))
                return false;
            return true;
        }
    }
}