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
    public partial class PregledPromet : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }

        private int tekovnaGodina { get; set; }
        /// <summary>
        /// Pomosna lista koja gi sodrzi poziciite vo
        /// koi treba da se vnesat podatocite za prometot
        /// </summary>
        private List<LabelFASAP> PrometMeseci { get; set; }

        public PregledPromet(OracleConnection conn, Restoran rest)
        {
            this.Conn = conn;
            this.Restoran = rest;
            Init();
        }

        // samo za primer
        public PregledPromet()
        {
            InitializeComponent();

            this.Restoran = new Restoran() { RestoranID = 1, Ime = "Ресторан Лира" };

            string oradb = "Data Source=(DESCRIPTION="
          + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
          + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
          + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            Init();
        }

        public void Init()
        {
            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.tekovnaGodina = DateTime.Now.Year;
            this.lblGodina.Text = this.tekovnaGodina.ToString();
            this.lblImeRestoran.Text = this.Restoran.Ime + " ";

            this.PrometMeseci = new List<LabelFASAP>();
            this.PrometMeseci.Add(this.lblPromet1);
            this.PrometMeseci.Add(this.lblPromet2);
            this.PrometMeseci.Add(this.lblPromet3);
            this.PrometMeseci.Add(this.lblPromet4);
            this.PrometMeseci.Add(this.lblPromet5);
            this.PrometMeseci.Add(this.lblPromet6);
            this.PrometMeseci.Add(this.lblPromet7);
            this.PrometMeseci.Add(this.lblPromet8);
            this.PrometMeseci.Add(this.lblPromet9);
            this.PrometMeseci.Add(this.lblPromet10);
            this.PrometMeseci.Add(this.lblPromet11);
            this.PrometMeseci.Add(this.lblPromet12);

            this.ObnoviEkran();
        }

        /// <summary>
        /// Inicijalizacija na labelite vo tabelata
        /// </summary>
        private void ClearLabels()
        {
            foreach (var item in this.PrometMeseci)
                item.Text = " - ";
        }

        /// <summary>
        /// Inicijalizacija na labelite i povtorno vcituvanje
        /// na podatocite od bazata
        /// </summary>
        private void ObnoviEkran()
        {
            this.lblGodina.Text = this.tekovnaGodina.ToString();
            this.ClearLabels();
            //this.VcitajPodatoci();
        }

        /// <summary>
        /// Popolnuvanje na tabelata so soodvetnite
        /// podatoci koi vleguvaat vo izvestajot
        /// </summary>
        private void VcitajPodatoci()
        {
            String sqlTab = "";
            OracleCommand cmd = new OracleCommand(sqlTab, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID1", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID2", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID3", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID4", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("GOD", OracleDbType.Int64);
                prm.Value = this.tekovnaGodina;
                cmd.Parameters.Add(prm);

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                int ind = 0;
                while (dr.Read())
                {
                    /*this.VkPromet[ind].Text = dr.GetInt32(2).ToString() + " äåí. ";
                    this.VkPlata[ind].Text = dr.GetInt32(3).ToString() + " äåí. ";
                    this.VkDodatoci[ind].Text = dr.GetInt32(4).ToString() + " äåí. ";
                    this.VkTrosoci[ind].Text = dr.GetInt32(5).ToString() + " äåí. ";
                    this.Sostojba[ind].Text = dr.GetInt32(6).ToString() + " äåí. ";
                    ind++;*/
                }
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("blabla", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }
        }
    }
}
