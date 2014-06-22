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
    public partial class PregledKvartalnaSostojba : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }

        private int tekovnaGodina { get; set; }
        /// <summary>
        /// Pomosni listi koi gi sodrzat poziciite na tabelata
        /// </summary>
        private List<LabelFASAP> VkPromet { get; set; }
        private List<LabelFASAP> VkPlata { get; set; }
        private List<LabelFASAP> VkDodatoci { get; set; }
        private List<LabelFASAP> VkTrosoci { get; set; }
        private List<LabelFASAP> Sostojba { get; set; }

        public PregledKvartalnaSostojba(OracleConnection conn, Restoran rest)
        {
            this.Conn = conn;
            this.Restoran = rest;
            Init();
        }

        // samo za primer
        public PregledKvartalnaSostojba()
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

            this.VkPromet = new List<LabelFASAP>();
            this.VkPromet.Add(this.lblPromet1);
            this.VkPromet.Add(this.lblPromet2);
            this.VkPromet.Add(this.lblPromet3);
            this.VkPromet.Add(this.lblPromet4);

            this.VkPlata = new List<LabelFASAP>();
            this.VkPlata.Add(this.lblPlata1);
            this.VkPlata.Add(this.lblPlata2);
            this.VkPlata.Add(this.lblPlata3);
            this.VkPlata.Add(this.lblPlata4);

            this.VkDodatoci = new List<LabelFASAP>();
            this.VkDodatoci.Add(this.lblDodatok1);
            this.VkDodatoci.Add(this.lblDodatok2);
            this.VkDodatoci.Add(this.lblDodatok3);
            this.VkDodatoci.Add(this.lblDodatok4);

            this.VkTrosoci = new List<LabelFASAP>();
            this.VkTrosoci.Add(this.lblTrosoci1);
            this.VkTrosoci.Add(this.lblTrosoci2);
            this.VkTrosoci.Add(this.lblTrosoci3);
            this.VkTrosoci.Add(this.lblTrosoci4);

            this.Sostojba = new List<LabelFASAP>();
            this.Sostojba.Add(this.lblSostojba1);
            this.Sostojba.Add(this.lblSostojba2);
            this.Sostojba.Add(this.lblSostojba3);
            this.Sostojba.Add(this.lblSostojba4);
        }

        private string GetReport()
        {
            string sqlReport;

            return "";
        }
    }
}
