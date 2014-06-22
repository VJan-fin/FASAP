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

        public override List<Narachki.Naracka> ListaNaracki(Oracle.DataAccess.Client.OracleConnection conn)
        {
            Dictionary<VklucuvaKey, Stavka> Stavki = new Dictionary<VklucuvaKey, Stavka>();
            string sqlMeni = @"SELECT n.Narachka_ID, n.VKUPNA_CENA, n.VREME, site.BROJ_MASA,st.stavka_id, st.ime_stavka, st.opis_stavka, st.cena_stavka, st.dodatok_stavka, vk.kolichina_stavka, vk.vkluchuva_id, vk.dodatok_id
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
            Dictionary<int, Naracka> Naracki = new Dictionary<int, Naracka>();
            while (dr.Read())
            {
                int narid = dr.GetInt32(0);
                if (!Naracki.ContainsKey(narid))
                {
                    Naracka nar = new Onsite(narid, dr.GetInt32(1), dr.GetDateTime(2), dr.GetInt32(3));
                    Naracki.Add(narid, nar);
                    lista.Add(nar);
                }
                int vklucuvaid = dr.GetInt32(10);
                int? dodatokid;
                object obj1 = dr.GetValue(11);
                if (obj1 == null)
                    dodatokid = null;
                else dodatokid = obj1 as int?;
                Object obj = dr.GetValue(6);
                String pomosen;
                if (obj == null)
                    pomosen = null;
                else pomosen = obj as String;
                String IsDecorator = (string)dr.GetValue(8);
                if (IsDecorator == "1")
                {
                    Dodatok dodatok = new Dodatok(dr.GetInt16(4), dr.GetString(5), (decimal)dr.GetValue(7), pomosen);
                    dodatok.DodatokKey = new VklucuvaKey(narid, dodatokid);
                    dodatok.VklucuvaKey = new VklucuvaKey(narid, vklucuvaid);
                    dodatok.Kolicina = dr.GetInt32(9);
                    Stavki.Add(new VklucuvaKey(narid, vklucuvaid), dodatok);
                }
                else
                {
                    Stavka stavka = new Stavka(dr.GetInt16(4), dr.GetString(5), (decimal)dr.GetValue(7), pomosen);
                    stavka.DodatokKey = new VklucuvaKey(narid, dodatokid);
                    stavka.VklucuvaKey = new VklucuvaKey(narid, vklucuvaid);
                    stavka.Kolicina = dr.GetInt32(9);
                    Stavki.Add(new VklucuvaKey(narid, vklucuvaid), stavka);
                }
            }

            Dictionary<VklucuvaKey, Stavka> StavkiKoiSeRef = new Dictionary<VklucuvaKey, Stavka>();
            foreach (var obj in Stavki)
            {
                Stavka st;
                if (Stavki.TryGetValue(obj.Value.getDodatokKey(), out st))
                {
                    (obj.Value as Dodatok).Osnovna = st;
                    StavkiKoiSeRef.Add(obj.Value.getDodatokKey(), st);
                }
            }

            foreach (var obj in Stavki)
            {
                if (!StavkiKoiSeRef.ContainsKey(obj.Value.getVklucuvaKey()))
                {
                    Naracka nar;
                    if (Naracki.TryGetValue(obj.Value.VklucuvaKey.NarackaID, out nar))
                        nar.Add(new OrderComponent(obj.Value, obj.Value.Kolicina));
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

        public override void IncrementOrderNumber(OracleConnection conn, Naracka nar)
        {
            string updateOnsite = @"update IZVRSHUVA
                                    set IZVRSHENI_NARACHKI = IZVRSHENI_NARACHKI + 1
                                    where VRABOTEN_ID = :VrabotenID
                                    AND RESTORAN_ID = :ResID
                                    AND POZICIJA = 'Келнер'";

            OracleCommand cmd = new OracleCommand(updateOnsite, conn);

            OracleParameter prm = new OracleParameter("VrabotenID", OracleDbType.Int64);
            prm.Value = VrabotenID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = RestoranID;
            cmd.Parameters.Add(prm);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
