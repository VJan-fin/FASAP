using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka.RestoranKlasi
{
    class RejtingAscendingComparer : IComparer<Restoran>
    {
        public int Compare(Restoran x, Restoran y)
        {
            int pom = x.Rejting.CompareTo(y.Rejting);
            if (pom == 0)
                return x.Ime.CompareTo(y.Ime);
            return pom;
        }
    }
}
