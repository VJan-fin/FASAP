using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class CompareByID : IComparer<VrabotenInfo>
    {
        public int Compare(VrabotenInfo x, VrabotenInfo y)
        {
            return x.VrabotenID.CompareTo(y.VrabotenID);
        }
    }

    public class CompareByName : IComparer<VrabotenInfo>
    {
        public int Compare(VrabotenInfo x, VrabotenInfo y)
        {
            return x.Ime.CompareTo(y.Ime);
        }
    }

    public class CompareBySurname : IComparer<VrabotenInfo>
    {
        public int Compare(VrabotenInfo x, VrabotenInfo y)
        {
            return x.Prezime.CompareTo(y.Prezime);
        }
    }

    public class CompareBySalary : IComparer<VrabotenInfo>
    {
        public int Compare(VrabotenInfo x, VrabotenInfo y)
        {
            return x.Plata.CompareTo(y.Plata);
        }
    }
}
