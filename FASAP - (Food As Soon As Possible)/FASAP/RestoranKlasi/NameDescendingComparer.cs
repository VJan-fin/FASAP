using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka.RestoranKlasi
{
    class NameDescendingComparer : IComparer<Restoran>
    {
        public int Compare(Restoran x, Restoran y)
        {
            int pom = y.Ime.CompareTo(x.Ime);
            if (pom == 0)
                return y.Rejting.CompareTo(x.Rejting);
            return pom;
        }
    }
}
