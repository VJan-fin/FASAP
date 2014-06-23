using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Narachki;

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
        public override List<Narachki.Naracka> ListaNaracki(Oracle.DataAccess.Client.OracleConnection conn)
        {
            throw new NotImplementedException();
        }
        public override bool PrevzemiNaracka(Oracle.DataAccess.Client.OracleConnection conn)
        {
            throw new NotImplementedException();
        }

        public override string GetFunkcija()
        {
            throw new NotImplementedException();
        }

        public override void OslobodiNaracki(Oracle.DataAccess.Client.OracleConnection conn)
        {
            throw new NotImplementedException();
        }

        public override void IncrementOrderNumber(Oracle.DataAccess.Client.OracleConnection conn, Naracka nar)
        {
            throw new NotImplementedException();
        }
    }
}
