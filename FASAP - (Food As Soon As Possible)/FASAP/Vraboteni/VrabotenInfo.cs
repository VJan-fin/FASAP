using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class VrabotenInfo
    {
        public int VrabotenID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pozicija { get; set; }
        public int Plata { get; set; }
        public int Naracki { get; set; }
        public int Status { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Ime, this.Prezime);
        }
    }
}
