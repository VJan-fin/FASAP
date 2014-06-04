using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmetkaZaNaracka.Properties;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka
{
    public partial class PregledVraboten : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        private int VrabotenID { get; set; }
        private List<string> Pozicii { get; set; }
        private int PozInd { get; set; }

        private List<LabelFASAP> Nevidlivi { get; set; }
        private bool ModifyMode { get; set; }

        public PregledVraboten(Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            this.Restoran = restoran;
            this.Conn = conn;

            this.Init();

        }

        // samo za primer
        public PregledVraboten()
        {
            InitializeComponent();
            this.Restoran = new Restoran() { RestoranID = 1 };
            string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
             + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
             + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            this.Init();
        }

        private void Init()
        {
            this.Nevidlivi = new List<LabelFASAP>();
            this.Nevidlivi.Add(lblZad1);
            this.Nevidlivi.Add(lblZad2);
            this.Nevidlivi.Add(lblZad3);
            this.Nevidlivi.Add(lblZad4);
            this.Nevidlivi.Add(lblZad5);
            this.Nevidlivi.Add(lblZab);

            this.ModifyMode = false;
            this.PostaviVidlivost();
        }

        private void PostaviVidlivost()
        {
            foreach (var item in this.Nevidlivi)
                item.Visible = this.ModifyMode;
        }

        private void PopolniPozicii()
        {
            this.PozInd = 0;
            this.Pozicii = new List<string>();

            string sqlPozicii = @"SELECT * FROM FUNKCIJA ORDER BY POZICIJA";
            OracleCommand cmd = new OracleCommand(sqlPozicii, this.Conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                this.Pozicii.Add(dr.GetString(0));

            for (int i = 0; i < this.Pozicii.Count; i++)
                if (this.Pozicii[i] == this.lblPozicija.Text)
                {
                    this.PozInd = i;
                    break;
                }
            //UpdatePozicii();
        }

        private void buttonIzmeni_MouseEnter(object sender, EventArgs e)
        {
            this.buttonIzmeni.Image = Resources.LightButton___Copy;
            this.buttonIzmeni.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void buttonIzmeni_MouseLeave(object sender, EventArgs e)
        {
            this.buttonIzmeni.Image = Resources.DarkButton___Copy;
            this.buttonIzmeni.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void buttonOtkazi_MouseEnter(object sender, EventArgs e)
        {
            this.buttonOtkazi.Image = Resources.LightButton___Copy;
            this.buttonOtkazi.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void buttonOtkazi_MouseLeave(object sender, EventArgs e)
        {
            this.buttonOtkazi.Image = Resources.DarkButton___Copy;
            this.buttonOtkazi.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }
    }
}
