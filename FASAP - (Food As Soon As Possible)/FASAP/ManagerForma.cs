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

        public ManagerForma(OracleConnection conn ,ManagerC manager)
        {
            InitializeComponent();
            init();
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
            init();
        }
        private void init()
        {
            string sqlVrab = @"SELECT IME_RESTORAN FROM RESTORAN WHERE RESTORAN_ID = :REST_ID";
            OracleCommand cmd = new OracleCommand(sqlVrab, this.Conn);
            cmd.CommandType = CommandType.Text;
            OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
            prm.Value = this.Manager.RestoranID;
            cmd.Parameters.Add(prm);

            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblrest.Text = dr.GetString(0);
            }

            lblManager.Text = Manager.Ime + " " + Manager.Prezime;
        }
        private void btnInfo_MouseEnter(object sender, EventArgs e)
        {
            this.btnInfo.Image = Resources.LightButton___Copy;
            this.btnInfo.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnInfo_MouseLeave(object sender, EventArgs e)
        {
            this.btnInfo.Image = Resources.DarkButton___Copy;
            this.btnInfo.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnVraboteni_MouseEnter(object sender, EventArgs e)
        {
            this.btnVraboteni.Image = Resources.LightButton___Copy;
            this.btnVraboteni.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnVraboteni_MouseLeave(object sender, EventArgs e)
        {
            this.btnVraboteni.Image = Resources.DarkButton___Copy;
            this.btnVraboteni.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnPonuda_MouseEnter(object sender, EventArgs e)
        {
            this.btnPonuda.Image = Resources.LightButton___Copy;
            this.btnPonuda.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnPonuda_MouseLeave(object sender, EventArgs e)
        {
            this.btnPonuda.Image = Resources.DarkButton___Copy;
            this.btnPonuda.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnFinansii_MouseEnter(object sender, EventArgs e)
        {
            this.btnFinansii.Image = Resources.LightButton___Copy;
            this.btnFinansii.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnFinansii_MouseLeave(object sender, EventArgs e)
        {
            this.btnFinansii.Image = Resources.DarkButton___Copy;
            this.btnFinansii.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnIzvestai_MouseEnter(object sender, EventArgs e)
        {
            this.btnIzvestai.Image = Resources.LightButton___Copy;
            this.btnIzvestai.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnIzvestai_MouseLeave(object sender, EventArgs e)
        {
            this.btnIzvestai.Image = Resources.DarkButton___Copy;
            this.btnIzvestai.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void buttonFASAP1_MouseEnter(object sender, EventArgs e)
        {
            this.buttonFASAP1.Image = Resources.LightButton___Copy;
            this.buttonFASAP1.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void buttonFASAP1_MouseLeave(object sender, EventArgs e)
        {
            this.buttonFASAP1.Image = Resources.DarkButton___Copy;
            this.buttonFASAP1.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            PregledInformacii p = new PregledInformacii(Conn,Manager.RestoranID);
            p.Show();
        }

        private void btnVraboteni_Click(object sender, EventArgs e)
        {
            // samo za primer
            CurrRestoran = new Restoran() { RestoranID = 1, Ime = "Гостилница Лира" };
            ListaVraboteni v = new ListaVraboteni(this.CurrRestoran, this.Conn);
            //Vraboteni v = new Vraboteni(Conn, Manager.RestoranID);
            v.Show();
        }

        
    }
}
