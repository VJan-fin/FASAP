using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class Vraboten
    {
       public int VrabotenID { get; set; }
       public int RestoranID { get; set; } 
       public string Ime { get; set; }
       public string Prezime { get; set; }
       public string Pozicija { get; set; }

       public Vraboten()
       {
       }
       public Vraboten(int vrabID, int resID, string ime, string prezime, string pozicija)
       {
           this.VrabotenID = vrabID;
           this.RestoranID = resID;
           this.Ime = ime;
           this.Prezime = prezime;
           this.Pozicija = pozicija;
       }
        
    }
}
