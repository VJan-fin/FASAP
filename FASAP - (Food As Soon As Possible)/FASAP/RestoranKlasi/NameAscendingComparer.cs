using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka.RestoranKlasi
{
    class NameAscendingComparer : IComparer<Restoran>
    {
        public int Compare(Restoran x, Restoran y)
        {
            int pom = x.Ime.CompareTo(y.Ime);
            if (pom == 0)
                return y.Rejting.CompareTo(x.Rejting);
            return pom;
        }
    }
}
