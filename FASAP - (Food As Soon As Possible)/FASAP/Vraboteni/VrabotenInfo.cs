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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is VrabotenInfo))
                return false;
            if (this.VrabotenID == (obj as VrabotenInfo).VrabotenID)
                return true;
            else
                return false;
        }
    }
}
