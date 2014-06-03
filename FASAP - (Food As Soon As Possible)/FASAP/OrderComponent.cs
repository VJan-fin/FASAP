using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class OrderComponent
    {
        public MenuComponent Item { get; set; }
        public int Quantity { get; set; }

        public OrderComponent(MenuComponent i, int q = 1)
        {
            Item = i;
            this.Quantity = q;
        }

        public void QuantityUp()
        {
            this.Quantity++;
        }

        public void QuantityDown()
        {
            this.Quantity--;
        }

        public int ComputePrice()
        {
            return Item.ComputeCost() * Quantity;
        }

        public string GetName()
        {
            return String.Format("{0} x{1}", Item.GetName(), Quantity);
        }

        public override string ToString()
        {
            return String.Format("{0}\t{1}", this.GetName(), this.ComputePrice());
        }
    }
}
