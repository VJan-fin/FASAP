using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public  class VrabPrometProcent
    {
        public int VrabID { get; set; }
        public String Ime { get; set; }
        public String Prezime { get; set; }
        public int Promet { get; set; }
        public decimal Procent { get; set; }
        public String Funkcija { get; set; }

        public VrabPrometProcent(){}

        public VrabPrometProcent(int vrabID, String ime, String prezima, int promet, decimal procent)
        {
            this.VrabID = vrabID;
            this.Ime = ime;
            this.Prezime = prezima;
            this.Promet = promet;
            this.Procent = procent;
            
        }

        public override string ToString()
        {
            String s=VrabID+ " "+ Ime+ " " + Prezime+" "+Promet+" "+Procent+ "\n";
            return s;
        }
        public String getImePrezime()
        {
            return String.Format("{0} {1}",Ime,Prezime);
        }
        public String getProcent()
        {
            return String.Format("{0}%",Procent);
        }
        public String getPromet()
        {
            return String.Format("{0} ден.",Promet);
        }
    }
}
