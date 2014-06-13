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
    // stavi razlicna boja za vrabotenite koi se neaktivni
    // sortiranje vo rastecki/opagacki redosled po: br. vraboten, ime, prezime, plata
    // filtriranje na vrabotenite po: pozicija, status (posebno po dvata kriteriuma)
    // default e da gi lista samo aktivnite vraboteni sortirani po vraboten broj
    // dodadi kopcinja za otvoranje na formite za dodavanje nov vraboten i 
    public partial class ListaVraboteni : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        private List<VrabotenInfo> AllEmployees { get; set; }
        private VrabotenInfo CurrentEmp { get; set; }

        private List<LabelFASAP> ListaVrab { get; set; }
        private int indVrab { get; set; }

        // samo za primer
        public ListaVraboteni()
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

            this.Init();
        }

        public ListaVraboteni(Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            this.Restoran = restoran;
            this.Conn = conn;
            this.Init();
        }

        public void Init()
        {
            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.lblImeRestoran.Text = this.Restoran.Ime + " ";
            this.AllEmployees = new List<VrabotenInfo>();

            this.ListaVrab = new List<LabelFASAP>();
            this.ListaVrab.Add(lbl1);
            this.ListaVrab.Add(lbl2);
            this.ListaVrab.Add(lbl3);
            this.ListaVrab.Add(lbl4);
            this.ListaVrab.Add(lbl5);
            this.ListaVrab.Add(lbl6);
            this.ListaVrab.Add(lbl7);
            this.ListaVrab.Add(lbl8);
            this.ListaVrab.Add(lbl9);
            this.ListaVrab.Add(lbl10);
            this.indVrab = 0;

            this.VcitajVraboteni();
        }

        public void VcitajVraboteni()
        {
            string sqlVraboteni = @"SELECT VRABOTEN_ID, IME_VRABOTEN, PREZIME_VRABOTEN, POZICIJA, PLATA, STATUS, IZVRSHENI_NARACHKI FROM VRABOTEN NATURAL JOIN IZVRSHUVA WHERE RESTORAN_ID = :RES_ID ORDER BY VRABOTEN_ID";
            OracleCommand cmd = new OracleCommand(sqlVraboteni, Conn);
            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int64);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                VrabotenInfo vr = new VrabotenInfo();
                vr.VrabotenID = dr.GetInt32(0);
                vr.Ime = dr.GetString(1);
                vr.Prezime = dr.GetString(2);
                vr.Pozicija = dr.GetString(3);
                vr.Plata = dr.GetInt32(4);
                int st;
                if (int.TryParse(dr.GetString(5), out st))
                    vr.Status = st;
                vr.Naracki = dr.GetInt32(6);
                this.AllEmployees.Add(vr);
            }

            UpdateVrab();
        }

        public void UpdateVrab()
        {
            int ind = this.indVrab;
            for (int i = 0; i < this.ListaVrab.Count; i++)
            {
                if (ind < this.AllEmployees.Count)
                {
                    this.ListaVrab[i].UpdateObject(this.AllEmployees[ind]);
                    ind++;
                }
                else
                    this.ListaVrab[i].UpdateObject(null);
            }
        }

        public void PopolniVraboten()
        {
            this.lblBrVraboten.Text = this.CurrentEmp.VrabotenID.ToString();
            this.lblImePrezime.Text = this.CurrentEmp.Ime + " " + this.CurrentEmp.Prezime + " ";
            this.lblPozicija.Text = this.CurrentEmp.Pozicija + " ";
            this.lblPlata.Text = this.CurrentEmp.Plata.ToString();
            if (this.CurrentEmp.Status == 0)
                this.lblStatus.Text = "Неактивен ";
            else
                this.lblStatus.Text = "Активен ";
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                this.CurrentEmp = lb.LblObject as VrabotenInfo;
                this.PopolniVraboten();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (this.AllEmployees.Count > this.ListaVrab.Count)
            {
                if (this.indVrab != 0)
                    this.indVrab--;
                this.PopolniVraboten();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (this.AllEmployees.Count > this.ListaVrab.Count)
            {
                if (this.indVrab < this.AllEmployees.Count - this.ListaVrab.Count)
                    this.indVrab++;
                this.PopolniVraboten();
            }
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowUp;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowUp;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowDown;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowDown;
        }

        private void lblMeni1_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                lb.Font = new Font("Trebuchet MS", 20, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lblMeni1_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                lb.Font = new Font("Trebuchet MS", 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void buttonOtkazi_MouseEnter(object sender, EventArgs e)
        {
            ButtonFASAP btn = sender as ButtonFASAP;
            btn.Image = Resources.LightButton___Copy;
            btn.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void buttonOtkazi_MouseLeave(object sender, EventArgs e)
        {
            ButtonFASAP btn = sender as ButtonFASAP;
            btn.Image = Resources.DarkButton___Copy;
            btn.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void buttonOtkazi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDodadiVrab_Click(object sender, EventArgs e)
        {
            DodavanjeVraboten addEmpForm = new DodavanjeVraboten(this.Restoran, this.Conn);
            addEmpForm.ShowDialog();
        }

        private void buttonPregledVrab_Click(object sender, EventArgs e)
        {
            if (this.CurrentEmp != null)
            {
                PregledVraboten viewEmpForm = new PregledVraboten(this.CurrentEmp.VrabotenID, this.Restoran, this.Conn);
                viewEmpForm.ShowDialog();
            }
            else
            {
                MessageBoxForm mbf = new MessageBoxForm("Немате одбрано вработен!\nОдберете вработен и обидете се повторно", false);
                mbf.ShowDialog();
            }
        }
    }
}
