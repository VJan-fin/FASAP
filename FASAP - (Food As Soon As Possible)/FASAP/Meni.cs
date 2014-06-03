using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class Meni : MenuComponent
    {
        public string Ime { get; set; }
        public List<MenuComponent> Content { get; set; }

        public Meni(string ime)
        {
            Ime = ime;
            Content = new List<MenuComponent>();
        }

        public override string GetDescription()
        {
            return "";
        }

        public override int ComputeCost()
        {
            throw new NotImplementedException();
        }

        public override string Print(string indent)
        {
            StringBuilder podmeni = new StringBuilder();
            podmeni.Append(String.Format("\n{0}{1}", indent, GetName()));
            foreach (var obj in Content)
                podmeni.Append(obj.Print(String.Format("\t{0}", indent)));
            return podmeni.ToString();
        }

        public override void AddComp(MenuComponent mc)
        {
            Content.Add(mc);
        }

        public override void RemoveComp(MenuComponent mc)
        {
            Content.Remove(mc);
        }

        public override string GetName()
        {
            return Ime;
        }

        public override List<MenuComponent> GetContent()
        {
            return Content;
        }

        public override MenuComponent GetReference(MenuComponent mc)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Meni))
                return false;
            Meni meni = obj as Meni;
            if (Ime != meni.Ime)
                return false;
            foreach (var obt in Content)
                foreach (var oob in meni.Content)
                    if (!obt.Equals(oob))
                        return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
