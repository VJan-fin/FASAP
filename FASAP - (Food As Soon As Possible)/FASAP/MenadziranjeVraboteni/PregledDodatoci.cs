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
    public partial class PregledDodatoci : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        private int VrabotenID { get; set; }

        private int tekovnaGodina { get; set; }
        /// <summary>
        /// Pomosna lista koja gi sodrzi poziciite vo
        /// koi treba da se vnesat podatocite za prometot
        /// </summary>
        private List<LabelFASAP> DodatokMeseci { get; set; }

        public PregledDodatoci(OracleConnection conn, Restoran rest, int id)
        {
            InitializeComponent();

            this.Conn = conn;
            this.Restoran = rest;
            this.VrabotenID = id;
            Init();
        }
        /*
        // samo za primer
        public PregledDodatoci()
        {
            InitializeComponent();

            this.Restoran = new Restoran() { RestoranID = 1, Ime = "Ресторан Лира" };
            this.VrabotenID = 5;

            string oradb = "Data Source=(DESCRIPTION="
          + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
          + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
          + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            Init();
        }
        */
        public void Init()
        {
            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.tekovnaGodina = DateTime.Now.Year;
            this.lblGodina.Text = this.tekovnaGodina.ToString();
            this.lblVrabotenID.Text = this.VrabotenID.ToString() + " ";

            this.DodatokMeseci = new List<LabelFASAP>();
            this.DodatokMeseci.Add(this.lblDodatok1);
            this.DodatokMeseci.Add(this.lblDodatok2);
            this.DodatokMeseci.Add(this.lblDodatok3);
            this.DodatokMeseci.Add(this.lblDodatok4);
            this.DodatokMeseci.Add(this.lblDodatok5);
            this.DodatokMeseci.Add(this.lblDodatok6);
            this.DodatokMeseci.Add(this.lblDodatok7);
            this.DodatokMeseci.Add(this.lblDodatok8);
            this.DodatokMeseci.Add(this.lblDodatok9);
            this.DodatokMeseci.Add(this.lblDodatok10);
            this.DodatokMeseci.Add(this.lblDodatok11);
            this.DodatokMeseci.Add(this.lblDodatok12);

            this.ObnoviEkran();
        }

        /// <summary>
        /// Inicijalizacija na labelite vo tabelata
        /// </summary>
        private void ClearLabels()
        {
            foreach (var item in this.DodatokMeseci)
                item.Text = " - ";
            this.lblVkDodatok.Text = " - ";
        }

        /// <summary>
        /// Inicijalizacija na labelite i povtorno vcituvanje
        /// na podatocite od bazata
        /// </summary>
        private void ObnoviEkran()
        {
            this.lblGodina.Text = this.tekovnaGodina.ToString();
            this.ClearLabels();
            this.VcitajPodatoci();
        }

        /// <summary>
        /// Popolnuvanje na tabelata so soodvetnite
        /// podatoci koi vleguvaat vo izvestajot
        /// </summary>
        private void VcitajPodatoci()
        {
            String sqlTab = @"SELECT MESEC_DODATOK, IZNOS_DODATOK
                            FROM DODATOK
                            WHERE RESTORAN_ID = :REST_ID AND VRABOTEN_ID = :VRAB_ID AND GODINA_DODATOK = :GOD
                            ORDER BY MESEC_DODATOK";

            OracleCommand cmd = new OracleCommand(sqlTab, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
                prm.Value = this.VrabotenID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("GOD", OracleDbType.Int64);
                prm.Value = this.tekovnaGodina;
                cmd.Parameters.Add(prm);

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                int ind = 0;
                int godDodatok = 0;
                while (dr.Read())
                {
                    int tmp = dr.GetInt32(1);
                    //int pom = int.Parse(dr.GetString(0).ToString()) - 1;
                    //MessageBox.Show(pom.ToString());
                    ind = int.Parse(dr.GetString(0).ToString()) - 1;
                    this.DodatokMeseci[ind].Text = tmp.ToString() + " ден. ";
                    godDodatok += tmp;
                    //ind++;
                }

                if (ind != 0)
                    this.lblVkDodatok.Text = godDodatok.ToString() + " ден. ";
                else
                    this.lblVkDodatok.Text = " - ";
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

        private void btnTekoven_Click(object sender, EventArgs e)
        {
            this.tekovnaGodina = DateTime.Now.Year;
            this.ObnoviEkran();
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Hand;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Default;
            pb.Image = Resources.DarkArrowLeft;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Hand;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Default;
            pb.Image = Resources.DarkArrowRight;
        }

        private void btnTekoven_MouseEnter(object sender, EventArgs e)
        {
            this.btnTekoven.Image = Resources.LightButton___Copy;
            this.btnTekoven.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnTekoven_MouseLeave(object sender, EventArgs e)
        {
            this.btnTekoven.Image = Resources.DarkButton___Copy;
            this.btnTekoven.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void pbLeftG_Click(object sender, EventArgs e)
        {
            if (this.tekovnaGodina > 1950)
            {
                this.tekovnaGodina--;
                this.ObnoviEkran();
            }
            else
            {
                MessageBoxForm mbf = new MessageBoxForm("Извештаи постари од 1950 не може да бидат прикажани", false);
                mbf.ShowDialog();
            }
        }

        private void pbRightG_Click(object sender, EventArgs e)
        {
            if (this.tekovnaGodina < DateTime.Now.Year)
            {
                this.tekovnaGodina++;
                this.ObnoviEkran();
            }
            else
            {
                MessageBoxForm mbf = new MessageBoxForm("Прикажана е тековната година", false);
                mbf.ShowDialog();
            }
        }
    }
}
