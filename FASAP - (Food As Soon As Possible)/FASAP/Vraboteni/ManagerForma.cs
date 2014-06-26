using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka
{
    public partial class ManagerForma : BackgroundForm
    {
        private Restoran CurrRestoran { get; set; }
        private ManagerC Manager { get; set; }
        private OracleConnection Conn { get; set; }

        public ManagerForma(OracleConnection conn, ManagerC manager)
        {
            InitializeComponent();
            Manager = manager;
            Conn = conn;

            Init();
        }
        
        public ManagerForma() //probno
        {
            InitializeComponent();
            string oradb = "Data Source=(DESCRIPTION="
           + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
           + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
           + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();
            Manager = new ManagerC(8, 1, "Гордана", "Иванова-Крстевска", "gordanaivanovakrstevska@gmail.com", "1804978455221");
            Init();
        }
        
        /// <summary>
        /// Pocetna inicijalizacija na formata i popolnuvanje
        /// na pozicijata za ime na menadzerot
        /// </summary>
        private void Init()
        {
            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.lblManager.Text = Manager.Ime + " " + Manager.Prezime;
            this.popolniRestoran();
        }

        /// <summary>
        /// Vcituvanje na site podatoci za restoranot vo koj
        /// raboti menadzerot koj e tekovno najaven
        /// </summary>
        private void popolniRestoran()
        {
            string sqlRestoran = @"SELECT * FROM RESTORAN WHERE RESTORAN_ID = :REST_ID";
            OracleCommand cmd = new OracleCommand(sqlRestoran, this.Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.Manager.RestoranID;
                cmd.Parameters.Add(prm);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                CurrRestoran = new Restoran();
                if (dr.Read())
                {
                    CurrRestoran.RestoranID = (int)dr.GetValue(0);
                    CurrRestoran.Ime = dr.GetString(2);
                    if (!dr.IsDBNull(3))
                    {
                        CurrRestoran.Ulica = dr.GetString(3);
                    }
                    else CurrRestoran.Ulica = "";
                    if (!dr.IsDBNull(4))
                    {
                        CurrRestoran.Grad = dr.GetString(4);
                    }
                    else CurrRestoran.Grad = "";

                    CurrRestoran.Rejting = (float)dr.GetValue(5);
                    if (!dr.IsDBNull(6))
                    {
                        CurrRestoran.RabotnoVreme = dr.GetString(6);
                    }
                    else CurrRestoran.RabotnoVreme = "";
                    if (!dr.IsDBNull(7))
                    {
                        CurrRestoran.Kapacitet = dr.GetInt16(7);
                    }
                    else CurrRestoran.Kapacitet = null;
                    if (!dr.IsDBNull(8))
                    {
                        CurrRestoran.BrojMasi = dr.GetInt16(8);
                    }
                    else CurrRestoran.BrojMasi = null;
                    if (!dr.IsDBNull(9))
                    {
                        CurrRestoran.CenaZaDostava = dr.GetInt16(9);
                    }
                    else CurrRestoran.CenaZaDostava = null;
                    if (!dr.IsDBNull(10))
                    {
                        CurrRestoran.PragZaDostava = dr.GetInt16(10);
                    }
                    else CurrRestoran.PragZaDostava = null;
                    if (!dr.IsDBNull(11))
                    {
                        CurrRestoran.DatumNaOtvoranje = dr.GetDateTime(11);
                    }
                    else CurrRestoran.DatumNaOtvoranje = null;

                    CurrRestoran.Kategorija = dr.GetString(12);
                }

                this.lblrest.Text = this.CurrRestoran.Ime;
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            PregledInformacii p = new PregledInformacii(Conn,Manager.RestoranID);
            p.Show();
        }

        private void btnVraboteni_Click(object sender, EventArgs e)
        {
            ListaVraboteni v = new ListaVraboteni(this.CurrRestoran, this.Conn);
            v.Show();
        }

        private void btnPonuda_Click(object sender, EventArgs e)
        {
            PregledMeni v = new PregledMeni(this.CurrRestoran, Manager, this.Conn);
            v.Show();
        }

        private void btnPregledPromet_MouseEnter(object sender, EventArgs e)
        {
            ButtonFASAP btn = (sender as ButtonFASAP);
            btn.Image = Resources.LightButton___Copy;
            btn.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnPregledPromet_MouseLeave(object sender, EventArgs e)
        {
            ButtonFASAP btn = (sender as ButtonFASAP);
            btn.Image = Resources.DarkButton___Copy;
            btn.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnPregledPromet_Click(object sender, EventArgs e)
        {
            PregledPromet pp = new PregledPromet(this.Conn, this.CurrRestoran);
            pp.Show();
        }

        private void btnPridonesPromet_Click(object sender, EventArgs e)
        {
            Izv1PridonesVoPromet pp = new Izv1PridonesVoPromet(this.Conn, this.CurrRestoran.RestoranID);
            pp.Show();
        }

        private void btnKvartalnaSostojba_Click(object sender, EventArgs e)
        {
            PregledKvartalnaSostojba ks = new PregledKvartalnaSostojba(this.Conn, this.CurrRestoran);
            ks.Show();
        }
    }
}
