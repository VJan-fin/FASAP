using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public abstract class MenuComponent
    {
        public MenuComponent Parent { get; set; }

        public abstract List<MenuComponent> GetContent();

        public abstract string GetName();

        public abstract string GetDescription();

        public abstract int ComputeCost();

        public abstract string Print(string indent);

        public abstract void AddComp(MenuComponent mc);

        public abstract void RemoveComp(MenuComponent mc);

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
    }
}
