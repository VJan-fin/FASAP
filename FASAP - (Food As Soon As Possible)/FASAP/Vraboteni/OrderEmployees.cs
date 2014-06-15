using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class OrderEmployees
    {
        /// <summary>
        /// Podreduvanje na listata vraboteni spored brojot.
        /// Predodredena vrednost e vo rastecki redosled
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="asc"></param>
        public static void OrderByID(List<VrabotenInfo> emp, bool asc = true)
        {
            CompareByID comp = new CompareByID();
            emp.Sort(comp);
            if (!asc)
                emp.Reverse();
        }

        /// <summary>
        /// Podreduvanje na listata vraboteni spored imeto.
        /// Predodredena vrednost e vo rastecki redosled
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="asc"></param>
        public static void OrderByName(List<VrabotenInfo> emp, bool asc = true)
        {
            CompareByName comp = new CompareByName();
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
        public static void OrderBySurname(List<VrabotenInfo> emp, bool asc = true)
        {
            CompareBySurname comp = new CompareBySurname();
            emp.Sort(comp);
            if (!asc)
                emp.Reverse();
        }

        /// <summary>
        /// Podreduvanje na listata vraboteni spored platata.
        /// Predodredena vrednost e vo rastecki redosled
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="asc"></param>
        public static void OrderBySalary(List<VrabotenInfo> emp, bool asc = true)
        {
            CompareBySalary comp = new CompareBySalary();
            emp.Sort(comp);
            if (!asc)
                emp.Reverse();
        }
    }
}
