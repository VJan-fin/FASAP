using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    class Dostavuvac : Vraboten
    {
        public Dostavuvac(int vrabID, int resID, string ime, string prezime, string username, string password)
            : base(vrabID, resID, ime, prezime, username, password)
        {
        }

        public Dostavuvac() { }

        public override bool PrevzemiNaracka(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            throw new NotImplementedException();
        }

        public override List<Narachki.Naracka> ListaNaracki(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            throw new NotImplementedException();
        }
    }
}
