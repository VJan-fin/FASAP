using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Narachki;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka.Naracki
{
    public class Online : Naracka
    {
        public string AdresaZaDostava { get; set; }
        public string Kontakt { get; set; }
        public string ImeKlient { get; set; }
        public string PrezimeKlient { get; set; }
        public int CenaZaDostava { get; set; }

        public Online(int narackaID, int vkupnaCena, DateTime vreme, string adresaZaDostava, string kontakt, string imeKlient, string prezimeKlient)
            : base(narackaID, vkupnaCena, vreme)
        {
            AdresaZaDostava = adresaZaDostava;
            Kontakt = kontakt;
            ImeKlient = imeKlient;
            PrezimeKlient = prezimeKlient;
        }

        public override void SqlInsert(OracleConnection conn, int resID)
        {

        }
    }
}
