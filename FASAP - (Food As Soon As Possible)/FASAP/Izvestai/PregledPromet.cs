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
        /// Pomosna lista koja gi sodrzi prvite 4 meseci vo
        /// godinata: januari, fevruari, mart i april
        /// </summary>
        private List<LabelFASAP> PrometMeseci1234 { get; set; }
        /// <summary>
        /// Pomosna lista koja gi sodrzi vtorite 4 meseci vo
        /// godinata: maj, juni, juli i avgust
        /// </summary>
        private List<LabelFASAP> PrometMeseci5678 { get; set; }
        /// <summary>
        /// Pomosna lista koja gi sodrzi tretite 4 meseci vo
        /// godinata: septemvri, oktomvri, noemvri i dekemvri
        /// </summary>
        private List<LabelFASAP> PrometMeseci9101112 { get; set; }

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

            this.Restoran = new Restoran() { RestoranID = 1, Ime = "Гостилница Лира" };

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

            

            //this.ObnoviEkran();
        }
    }
}
