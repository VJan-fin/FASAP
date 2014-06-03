using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka.RestoranKlasi
{
    class RejtingDescendingComparer : IComparer<Restoran>
    {

        public int Compare(Restoran x, Restoran y)
        {
            int pom = y.Rejting.CompareTo(x.Rejting);
            if (pom == 0)
                return x.Ime.CompareTo(y.Ime);
            return pom;
        }
    }
}
