using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class Telefon
    {
        public string Tel { get; set; }

        public Telefon(string s)
        {
            Tel = s;
        }

        public override string ToString()
        {
            return Tel;
        }
    }
}
