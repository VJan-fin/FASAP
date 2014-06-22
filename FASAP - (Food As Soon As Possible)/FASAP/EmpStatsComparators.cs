using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{

    public class StatsCompareByName : IComparer<VrabPrometProcent>
    {
        public int Compare(VrabPrometProcent x, VrabPrometProcent y)
        {
            return x.Ime.CompareTo(y.Ime);
        }
    }
    public class StatsCompareBySurname : IComparer<VrabPrometProcent>
    {
        public int Compare(VrabPrometProcent x, VrabPrometProcent y)
        {
            return x.Prezime.CompareTo(y.Prezime);
        }
    }
    public class StatsCompareByPercent : IComparer<VrabPrometProcent>
    {
        public int Compare(VrabPrometProcent x, VrabPrometProcent y)
        {
            return x.Procent.CompareTo(y.Procent);
        }
    }
    public class StatsCompareByPromet : IComparer<VrabPrometProcent>
    {
        public int Compare(VrabPrometProcent x, VrabPrometProcent y)
        {
            return x.Promet.CompareTo(y.Promet);
        }
    }

}
