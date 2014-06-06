using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using SmetkaZaNaracka.Narachki;

namespace SmetkaZaNaracka
{
    public abstract class Vraboten
    {
       public int VrabotenID { get; set; }
       public int RestoranID { get; set; } 
       public string Ime { get; set; }
       public string Prezime { get; set; }

       public Vraboten()
       {
       }
       public Vraboten(int vrabID, int resID, string ime, string prezime, string username, string password)
       {
           this.VrabotenID = vrabID;
           this.RestoranID = resID;
           this.Ime = ime;
           this.Prezime = prezime;
       }

       public abstract bool PrevzemiNaracka(OracleConnection conn, int ResID);

       public abstract List<Naracka> ListaNaracki(OracleConnection conn, int resID);
    }
}
