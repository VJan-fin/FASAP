using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace SmetkaZaNaracka
{
    public class Restoran
    {
        public int RestoranID { get; set; }
        public string Ime { get; set; }
        public string PicturePath { get; set; }
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string RabotnoVreme { get; set; }
        public int? Kapacitet { get; set; }
        public int? BrojMasi { get; set; }
        public int? SlobodniMasi { get; set; }
        public int? CenaZaDostava { get; set; }
        public int? PragZaDostava { get; set; }
        public DateTime? DatumNaOtvoranje { get; set; }
        public string Kategorija { get; set; }
        public double Rejting { get; set; }
        public List<Telefon> Kontakt { get; set; }
        public MenuComponent GlavnoMeni { get; set; }
        public String LogoUrl { get; set; }

        public bool Sodrzi(String zbor)
        {
            String z = zbor.ToLower();
            if (Ime.ToLower().Contains(z))
                return true;
            if (Grad.ToLower().Contains(z))
                return true;
            if (Kategorija.ToLower().Contains(z))
                return true;
            if (Ulica.ToLower().Contains(z))
                return true;
            return false;
        }

        public MenuComponent GetSameComponent(MenuComponent mc)
        {
            if (GlavnoMeni == null)
                return null;
            return GlavnoMeni.GetSameComponent(mc);
        }

        public bool SodrziComponent(MenuComponent mc)
        {
            if (GlavnoMeni == null)
                return false;
            return GlavnoMeni.Sodrzi(mc);
        }

        public Restoran()
        {
            Kontakt = new List<Telefon>();
            PicturePath = null;
        }

        public override string ToString()
        {
            return Ime;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if(!(obj is Restoran))
                return false;
            Restoran res = obj as Restoran;
            if(RestoranID != res.RestoranID)
                return false;
            return true;
        }

        public string GetSlobodniMasi(OracleConnection conn)
        {
            string sql = @"SELECT RES.BROJ_MASI - COUNT(ONSI.NARACHKA_ID) AS SLOBODNI_MASI
                            FROM RESTORAN RES 
                            LEFT OUTER JOIN NARACHKA NAR ON RES.RESTORAN_ID = NAR.RESTORAN_ID 
                            LEFT OUTER JOIN ONSITE_NARACHKA ONSI ON NAR.NARACHKA_ID = ONSI.NARACHKA_ID AND NAR.RESTORAN_ID = ONSI.RESTORAN_ID
                            WHERE RES.RESTORAN_ID = :ResID AND NAR.REALIZIRANA = '0'
                            GROUP BY (RES.RESTORAN_ID, RES.BROJ_MASI)";
            OracleCommand cmd = new OracleCommand(sql, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = RestoranID;
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            int pom = 0;
            bool vlegov = false;
            while (dr.Read())
            {
                vlegov = true;
                pom = dr.GetInt32(0);
            }
            if (!vlegov && BrojMasi != null)
                pom = (int)BrojMasi;
            SlobodniMasi = pom;
            return pom.ToString();
        }
    }
}
