using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using SmetkaZaNaracka.Narachki;
using System.Data;

namespace SmetkaZaNaracka
{
    public abstract class Vraboten
    {
        public int VrabotenID { get; set; }
        public int RestoranID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdRestoran { get; set; }
        public string ImeRestoran { get; set; }

        public Vraboten()
        {
        }
        public Vraboten(int vrabID, int resID, string ime, string prezime, string username, string password)
        {
            this.VrabotenID = vrabID;
            this.RestoranID = resID;
            this.Ime = ime;
            this.Prezime = prezime;
            Username = username;
            Password = password;
        }

        public void PostaviRestoran(OracleConnection conn)
        {
            string sqlMeni = @"Select restoran_id, IME_RESTORAN
                            from restoran
                            where Restoran_id in
                                (select restoran_id
                                from korisnik
                                where KORISNICHKO_IME = :UserName AND LOZINKA = :Password)";
            OracleCommand cmd = new OracleCommand(sqlMeni, conn);
            OracleParameter prm = new OracleParameter("UserName", OracleDbType.Varchar2);
            prm.Value = Username;
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("Password", OracleDbType.Varchar2);
            prm.Value = Password;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            RestoranID = dr.GetInt16(0);
            ImeRestoran = dr.GetString(1);
        }

        public abstract bool PrevzemiNaracka(OracleConnection conn);

        public abstract List<Naracka> ListaNaracki(OracleConnection conn);

        public override string ToString()
        {
            return String.Format("{0} {1}", Ime, Prezime);
        }

        public abstract string GetFunkcija();

        public abstract void OslobodiNaracki(OracleConnection conn);

        public abstract void IncrementOrderNumber(OracleConnection conn, Naracka nar);
    }
}
