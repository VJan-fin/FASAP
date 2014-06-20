using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using SmetkaZaNaracka.Narachki;
using System.Windows.Documents;
using SmetkaZaNaracka.Naracki;

namespace SmetkaZaNaracka
{
    public class Kelner : Vraboten
    {
        public Kelner(int vrabID, int resID, string ime, string prezime, string username, string password)
            : base(vrabID, resID, ime, prezime, username, password)
        {
        }

        public Kelner()
        {
        }

        public override bool PrevzemiNaracka(Oracle.DataAccess.Client.OracleConnection conn)
        {
            string updateOnsite = @"UPDATE ONSITE_NARACHKA
                                    set vraboten_id = :VrabID
                                    where restoran_id = :ResID
                                    AND VRABOTEN_ID is null
                                    AND narachka_id = (
                                    select min(narachka_id)
                                    from ONSITE_narachka
                                    where restoran_id = :ResID
                                    AND VRABOTEN_ID is null
                                    )";

            OracleCommand cmd = new OracleCommand(updateOnsite, conn);

            OracleParameter prm = new OracleParameter("VrabID", OracleDbType.Int16);
            prm.Value = VrabotenID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ResID", OracleDbType.Int16);
            prm.Value = RestoranID;
            cmd.Parameters.Add(prm);

            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                br = -1;
            }
            if (br == 0)
                throw new NotImplementedException("Нема недоделени \"onsite\" нарачки.");
            if (br == -1)
                throw new NotImplementedException("Нема пристап до базата на податоци");
            return true;
        }
    
        public override List<Narachki.Naracka>  ListaNaracki(Oracle.DataAccess.Client.OracleConnection conn)
        {
            string sqlMeni = @"SELECT n.Narachka_ID, n.VKUPNA_CENA, n.VREME, site.BROJ_MASA,st.stavka_id, st.ime_stavka, st.opis_stavka, st.cena_stavka, st.dodatok_stavka, vk.kolichina_stavka
                                FROM Korisnik k, Vraboten v, Onsite_narachka site, Narachka n, Vkluchuva vk, Stavka st
                                WHERE k.Vraboten_ID = v.Vraboten_ID AND site.Vraboten_ID = v.Vraboten_ID
		                                AND n.Narachka_ID = site.Narachka_ID AND n.Restoran_ID = site.Restoran_ID
		                                AND n.Restoran_ID = k.Restoran_ID 
                                        AND vk.RESTORAN_ID = n.RESTORAN_ID
                                        AND vk.NARACHKA_ID = n.NARACHKA_ID
                                        AND vk.RESTORAN_ID = st.RESTORAN_ID
                                        AND vk.STAVKA_ID = st.STAVKA_ID
                                        AND vk.IME_MENI = st.IME_MENI
		                                AND k.Korisnichko_ime = :UserName AND k.lozinka = :Password
		                                AND n.Realizirana = 0
                                        ORDER BY n.NARACHKA_ID, vk.VKLUCHUVA_ID";
            OracleCommand cmd = new OracleCommand(sqlMeni, conn);
            OracleParameter prm = new OracleParameter("UserName", OracleDbType.Varchar2);
            prm.Value = Username;
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("Password", OracleDbType.Varchar2);
            prm.Value = Password;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Naracka> lista = new List<Naracka>();
            Naracka naracka;
            while (dr.Read())
            {
                int narid = dr.GetInt16(0);
                if (lista.Count == 0)
                {
                    naracka = new Onsite(narid, dr.GetInt16(1), dr.GetDateTime(2), dr.GetInt16(3));
                    Object obj = dr.GetValue(6);
                    String pomosen;
                    if (obj == null)
                        pomosen = null;
                    else pomosen = obj as String;
                    Stavka stavka = new Stavka(dr.GetInt16(4), dr.GetString(5), (decimal)dr.GetValue(7), pomosen);
                    OrderComponent oc = new OrderComponent(stavka, dr.GetInt16(9));
                    naracka.Add(oc);
                    lista.Add(naracka);
                }
                else if (narid == lista.Last().NarackaID)
                {
                    Object obj = dr.GetValue(6);
                    String pomosen;
                    if (obj == null)
                        pomosen = null;
                    else pomosen = obj as String;
                    String IsDecorator = (string)dr.GetValue(8);
                    if (IsDecorator == "1")
                    {
                        Dodatok dodatok = new Dodatok(dr.GetInt16(4), dr.GetString(5), (decimal)dr.GetValue(7), pomosen);
                        dodatok.Osnovna = lista.Last().Stavki.Last().Item;
                        lista.Last().Stavki.Last().Item = dodatok;
                    }
                    else
                    {
           
                        Stavka stavka = new Stavka(dr.GetInt16(4), dr.GetString(5), (decimal)dr.GetValue(7), pomosen);
                        OrderComponent oc = new OrderComponent(stavka, dr.GetInt16(9));
                        lista.Last().Add(oc);
                    }
                }
                else
                {
                    naracka = new Onsite(narid, dr.GetInt16(1), dr.GetDateTime(2), dr.GetInt16(3));
                    Object obj = dr.GetValue(6);
                    String pomosen;
                    if (obj == null)
                        pomosen = null;
                    else pomosen = obj as String;
                    Stavka stavka = new Stavka(dr.GetInt16(4), dr.GetString(5), (decimal)dr.GetValue(7), pomosen);
                    OrderComponent oc = new OrderComponent(stavka, dr.GetInt16(9));
                    naracka.Add(oc);
                    lista.Add(naracka);
                }
            }
            return lista;
        }

        public override string GetFunkcija()
        {
            return "Келнер";
        }

        public override void OslobodiNaracki(OracleConnection conn)
        {
            string updateOnsite = @"UPDATE ONSITE_NARACHKA
                                    set vraboten_id = null
                                    where VRABOTEN_ID = :VrabID
                                    AND restoran_id = :ResID
                                    AND narachka_id in
                                        (select narachka_id
                                        from narachka
                                        where realizirana = 0
                                        AND restoran_id = :ResID )";

            OracleCommand cmd = new OracleCommand(updateOnsite, conn);

            OracleParameter prm = new OracleParameter("VrabID", OracleDbType.Int16);
            prm.Value = VrabotenID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ResID", OracleDbType.Int16);
            prm.Value = RestoranID;
            cmd.Parameters.Add(prm);

            int br = 0;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new NotImplementedException("Нема пристап до базата на податоци" + br.ToString());
            }
        }
    }   
}
