using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using SmetkaZaNaracka.Narachki;
using System.Data;
using SmetkaZaNaracka.Naracki;

namespace SmetkaZaNaracka
{
    class Dostavuvac : Vraboten
    {
        public Dostavuvac(int vrabID, int resID, string ime, string prezime, string username, string password)
            : base(vrabID, resID, ime, prezime, username, password)
        {
        }

        public Dostavuvac() { }

        public override bool PrevzemiNaracka(Oracle.DataAccess.Client.OracleConnection conn)
        {
            string updateOnsite = @"UPDATE ONLINE_NARACHKA
                                    set vraboten_id = :VrabID
                                    where restoran_id = :ResID
                                    AND VRABOTEN_ID is null
                                    AND narachka_id = (
                                    select min(narachka_id)
                                    from ONLINE_narachka
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
                throw new NotImplementedException("Нема недоделени \"online\" нарачки.");
            if (br == -1)
                throw new NotImplementedException("Нема пристап до базата на податоци");
            return true;
        }

        public override List<Narachki.Naracka> ListaNaracki(Oracle.DataAccess.Client.OracleConnection conn)
        {
            List<Naracka> lista = new List<Naracka>();
            string sqlMeni = @"SELECT n.Narachka_ID, n.VKUPNA_CENA, n.VREME, line.ADRESA_ZA_DOSTAVA, line.KONTAKT, line.IME_KLIENT, line.PREZIME_KLIENT, st.stavka_id, st.ime_stavka, st.opis_stavka, st.cena_stavka, st.dodatok_stavka, vk.kolichina_stavka
                                FROM Korisnik k, Vraboten v, Online_narachka line, Narachka n, Vkluchuva vk, Stavka st
                                WHERE k.Vraboten_ID = v.Vraboten_ID AND line.Vraboten_ID = v.Vraboten_ID
		                                AND n.Narachka_ID = line.Narachka_ID AND n.Restoran_ID = line.Restoran_ID
		                                AND n.Restoran_ID = k.Restoran_ID
                                        AND vk.RESTORAN_ID = n.RESTORAN_ID
                                        AND vk.NARACHKA_ID = n.NARACHKA_ID
                                        AND st.RESTORAN_ID = vk.RESTORAN_ID
                                        AND st.STAVKA_ID = vk.STAVKA_ID
                                        AND st.ime_meni = vk.ime_meni
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
            Naracka naracka;
            //lista.Add(new Online(1, 1, DateTime.Now, "12", "12", "12", "12"));
            //return lista;
            while (dr.Read())
            {
                Object obj = dr.GetValue(9);
                String pomosen;
                if (obj == null)
                    pomosen = null;
                else pomosen = obj as String;
                int narid = dr.GetInt16(0);
                if (lista.Count == 0)
                {
                    naracka = new Online(narid, dr.GetInt16(1), dr.GetDateTime(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6));
                    Stavka stavka = new Stavka(dr.GetInt16(7), dr.GetString(8), (decimal)dr.GetValue(10), pomosen);
                    OrderComponent oc = new OrderComponent(stavka, dr.GetInt16(12)); 
                    naracka.Add(oc);
                    lista.Add(naracka);
                }
                else if (narid == lista.Last().NarackaID)
                {
                    String IsDecorator = (string)dr.GetValue(11);
                    if (IsDecorator == "1")
                    {
                        Dodatok dodatok = new Dodatok(dr.GetInt16(7), dr.GetString(8), (decimal)dr.GetValue(10), pomosen);
                        dodatok.Osnovna = lista.Last().Stavki.Last().Item;
                        lista.Last().Stavki.Last().Item = dodatok;
                    }
                    else
                    {
                        Stavka stavka = new Stavka(dr.GetInt16(7), dr.GetString(8), (decimal)dr.GetValue(10), pomosen);
                        OrderComponent oc = new OrderComponent(stavka, dr.GetInt16(12));
                        lista.Last().Add(oc);
                    }
                }
                else
                {
                    naracka = new Online(narid, dr.GetInt16(1), dr.GetDateTime(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6));
                    Stavka stavka = new Stavka(dr.GetInt16(7), dr.GetString(8), (decimal)dr.GetValue(10), pomosen);
                    OrderComponent oc = new OrderComponent(stavka, dr.GetInt16(12));
                    naracka.Add(oc);
                    lista.Add(naracka);
                }
                
            }
            return lista;
        }
        public override string GetFunkcija()
        {
            return "Доставувач";
        }

        public override void OslobodiNaracki(OracleConnection conn)
        {
            string updateOnsite = @"UPDATE ONLINE_NARACHKA
                                    set vraboten_id = null
                                    where VRABOTEN_ID = :VrabID
                                    AND restoran_id = :ResID
                                    AND narachka_id in
                                        (select narachka_id
                                        from narachka
                                        where realizirana = 0
                                        AND restoran_id = :ResID)";

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
