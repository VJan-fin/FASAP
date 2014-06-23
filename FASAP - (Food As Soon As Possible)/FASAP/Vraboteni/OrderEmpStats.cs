using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    class OrderEmpStats
    {
        /// <summary>
        /// Podreduvanje na listata vraboteni spored imeto.
        /// Predodredena vrednost e vo rastecki redosled
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="asc"></param>
        public static void OrderByName(List<VrabPrometProcent> emp, bool asc = true)
        {
            StatsCompareByName comp = new StatsCompareByName();
            emp.Sort(comp);
            if (!asc)
                emp.Reverse();
        }

        /// <summary>
        /// Podreduvanje na listata vraboteni spored prezimeto.
        /// Predodredena vrednost e vo rastecki redosled
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="asc"></param>
        /// 
        public static void OrderBySurname(List<VrabPrometProcent> emp, bool asc = true)
        {
            StatsCompareBySurname comp = new StatsCompareBySurname();
            emp.Sort(comp);
            if (!asc)
                emp.Reverse();
        }
        /// <summary>
        /// Podreduvanje na listata vraboteni spored ostvareniot promet.
        /// Predodredena vrednost e vo rastecki redosled
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="asc"></param>
        /// 
        public static void OrderByPromet(List<VrabPrometProcent> emp, bool asc = true)
        {
            StatsCompareByPromet comp = new StatsCompareByPromet();
            emp.Sort(comp);
            if (!asc)
                emp.Reverse();
        }
        /// <summary>
        /// Podreduvanje na listata vraboteni spored procentot.
        /// Predodredena vrednost e vo rastecki redosled
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="asc"></param>
        /// 
        public static void OrderByPercent(List<VrabPrometProcent> emp, bool asc = true)
        {
            StatsCompareByPercent comp = new StatsCompareByPercent();
            emp.Sort(comp);
            if (!asc)
                emp.Reverse();
        }
    }
}
