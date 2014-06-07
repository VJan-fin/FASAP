using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class ManagerC : Vraboten
    {

        public ManagerC(int vrabID, int resID, string ime, string prezime, string username, string password)
            : base(vrabID, resID, ime, prezime, username, password)
        {
        }

        public ManagerC()
        {
        }
        public override List<Narachki.Naracka> ListaNaracki(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            throw new NotImplementedException();
        }
        public override bool PrevzemiNaracka(Oracle.DataAccess.Client.OracleConnection conn, int ResID)
        {
            throw new NotImplementedException();
        }
    }
}
