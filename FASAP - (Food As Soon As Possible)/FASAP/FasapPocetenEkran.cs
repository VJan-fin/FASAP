using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka
{
    public partial class FasapPocetenEkran : Form
    {
        private List<Restoran> Restorani { get; set; }
        private OracleConnection Conn { get; set; }

        public FasapPocetenEkran()
        {
            InitializeComponent();
            Restorani = new List<Restoran>();
            //FasapPocetenEkran_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchFilter sf = new SearchFilter(Restorani, Conn);
            sf.ShowDialog();
        }

        private void FasapPocetenEkran_Load(object sender, EventArgs e)
        {
            
              string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
             + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
             + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();
            
            string sql = "SELECT IME_RESTORAN, ONLINE_SMETKI, KLIENTI FROM ( SELECT R.IME_RESTORAN, G.SMETKI ONLINE_SMETKI, G.KLIENTI FROM RESTORAN R JOIN ( SELECT RES.RESTORAN_ID, COUNT(ONL.RESTORAN_ID) AS SMETKI, COUNT(DISTINCT ONL.IME_KLIENT || ONL.PREZIME_KLIENT || ADRESA_ZA_DOSTAVA) AS KLIENTI FROM RESTORAN RES LEFT OUTER JOIN NARACHKA NAR ON RES.RESTORAN_ID = NAR.RESTORAN_ID AND MONTHS_BETWEEN(SYSDATE, NAR.VREME) <= 1 LEFT OUTER JOIN ONLINE_NARACHKA ONL ON NAR.RESTORAN_ID = ONL.RESTORAN_ID AND NAR.NARACHKA_ID = ONL.NARACHKA_ID GROUP BY RES.RESTORAN_ID ) G ON R.RESTORAN_ID = G.RESTORAN_ID ORDER BY G.SMETKI DESC, G.KLIENTI DESC, R.IME_RESTORAN ASC ) WHERE ROWNUM <= 5"; // C#
            OracleCommand cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader(); // C#
            int i = 0;
            while (dr.Read())
            {
                lbNajbarani.Items.Add((i + 1) + ". " + dr.GetString(0));
                i++;
            }

            sql = "Select distinct KATEGORIJA from RESTORAN ORDER BY KATEGORIJA"; // C#
            cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();
            while (dr.Read())
                lbKategorija.Items.Add(dr.GetString(0));

            sql = "Select distinct GRAD from RESTORAN ORDER BY GRAD"; // C#
            cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if(!dr.IsDBNull(0))
                lbGrad.Items.Add(dr.GetString(0));
            }

            sql = "Select * from RESTORAN"; // C#
            cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();
            Restoran res;
            while (dr.Read())
            {
                res = new Restoran();
                res.RestoranID = (int)dr.GetValue(0);
                res.Ime = dr.GetString(2);
                if (!dr.IsDBNull(3))
                {
                    res.Ulica = dr.GetString(3);
                }
                else res.Ulica = "";
                if (!dr.IsDBNull(4))
                {
                    res.Grad = dr.GetString(4);
                }
                else res.Grad = "";
              
                res.Rejting = (float)dr.GetValue(5);
                if (!dr.IsDBNull(6))
                {
                    res.RabotnoVreme = dr.GetString(6);
                }
                else res.RabotnoVreme = "";
                if (!dr.IsDBNull(7))
                {
                    res.Kapacitet = dr.GetInt16(7);
                }
                else res.Kapacitet =null;
                if (!dr.IsDBNull(8))
                {
                    res.BrojMasi = dr.GetInt16(8);
                }
                else res.BrojMasi = null;
                if (!dr.IsDBNull(9))
                {
                    res.CenaZaDostava = dr.GetInt16(9);
                }
                else res.CenaZaDostava =null;
                if (!dr.IsDBNull(10))
                {
                    res.PragZaDostava = dr.GetInt16(10);
                }
                else res.PragZaDostava = null;
                if (!dr.IsDBNull(11))
                {
                    res.DatumNaOtvoranje = dr.GetDateTime(11);
                }
                else res.DatumNaOtvoranje = null;
              
                res.Kategorija = dr.GetString(12);
                Restorani.Add(res);
                lbRestorani.Items.Add(res);
            }

            sql = "Select * from IMENIK"; // C#
            cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();

            while(dr.Read())
                foreach (var obj in Restorani)
                    if(obj.RestoranID == (int)dr.GetValue(0))
                        obj.Kontakt.Add(new Telefon(dr.GetString(1)));
        }

        private void lbRestorani_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var obj = (Restoran)lbRestorani.SelectedItem;
            IzvrsuvanjeNaracka fasapNaracka = new IzvrsuvanjeNaracka(obj, Conn);
            fasapNaracka.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InfoForma f = new InfoForma(Conn);
            f.Show();
        }
    }
}

