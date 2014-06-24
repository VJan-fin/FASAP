using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using SmetkaZaNaracka.Narachki;
using SmetkaZaNaracka.Properties;
using SmetkaZaNaracka.Naracki;
using System.Globalization;
using System.Threading;

namespace SmetkaZaNaracka
{
    public partial class VrabotenForma : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Vraboten Vraboten { get; set; }
        private List<LabelFASAP> Naracki { get; set; }
        private List<LabelFASAP> Stavki { get; set; }
        public Naracka CurrNaracka { get; set; }
        public int indNaracki { get; set; }
        public int indStavki { get; set; }
        public List<Naracka> naracki { get; set; }
        public int ErrorMessageTime = 3;
        public Semaphore LoadingSemaphore { get; set; }

        public VrabotenForma(OracleConnection conn, Vraboten vraboten)
        {
            InitializeComponent();
            this.Conn = conn;
            this.Vraboten = vraboten;
            LoadingSemaphore = new Semaphore(0, 10);
            Opacity = 0;
        }

        public VrabotenForma()
        {
            InitializeComponent();
            string oradb = "Data Source=(DESCRIPTION="
          + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
          + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
          + "User Id=DBA_20132014L_GRP_020;Password=7734924;";
            try
            {
                Conn = new OracleConnection();
                Conn.ConnectionString = oradb;
                Conn.Open();
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!Проверете ја конекцијата", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }
            LoadingSemaphore = new Semaphore(0, 10);
            //Vraboten = new Dostavuvac(4, 2, "Бојан", "Бојоски", "bojanbojoski@gmail.com", "1707994678005");
            Vraboten = new Kelner(2, 2, "Радослав", "Стрезоски", "radoslavstrezoski@gmail.com", "510969678006");
            Opacity = 0;
        }

        private void VrabotenForma_Load(object sender, EventArgs e)
        {
            Naracki = new List<LabelFASAP>();
            Naracki.Add(lbl1);
            Naracki.Add(lbl2);
            Naracki.Add(lbl3);
            Naracki.Add(lbl4);
            Naracki.Add(lbl5);
            Naracki.Add(lbl6);
            Naracki.Add(lbl7);
            Naracki.Add(lbl8);
            Naracki.Add(lbl9);
            Naracki.Add(lbl10);
            Naracki.Add(lbl11);
            Naracki.Add(lbl12);

            Stavki = new List<LabelFASAP>();
            Stavki.Add(lblNaracka1);
            Stavki.Add(lblNaracka2);
            Stavki.Add(lblNaracka3);
            Stavki.Add(lblNaracka4);
            Stavki.Add(lblNaracka5);
            Stavki.Add(lblNaracka6);
            Stavki.Add(lblNaracka7);
            Stavki.Add(lblNaracka8);
            Stavki.Add(lblNaracka9);

            if (Vraboten.GetFunkcija() == "Доставувач")
            {
                lblMasaOpis.Text = "Адреса за достава: ";
                lblKontaktOpis.Visible = true;
                lblKlient.Visible = true;
                lblKontakt.Visible = true;
                lblKlientOpis.Visible = true;
                lblCenaZaDostavaOpis.Visible = true;
                lblCenaZaDostava.Visible = true;
            }


            lblImeVraboten.Text = String.Format(":{0} - {1}: ", Vraboten.ToString(), Vraboten.GetFunkcija());
            Vraboten.PostaviRestoran(Conn);

            lblImeRestoran.Text = String.Format(":{0}:  ", Vraboten.ImeRestoran);
            naracki = new List<Naracka>();

            buttonFASAP1_Click(null, null);
            timer2.Start();
        }

        public void PrevzemiNaracki()
        {
            naracki = Vraboten.ListaNaracki(Conn);
            PostaviNaracki();
        }

        public void PostaviNaracki()
        {
            for (int i = 0; i < Naracki.Count; i++)
                if (i < naracki.Count)
                {
                    Naracki[i].UpdateObject(naracki[i + indNaracki]);
                    if (naracki[i + indNaracki].Equals(CurrNaracka))
                    {
                        Naracki[i].Image = Resources.LabelBackgroundSelected;
                        Naracki[i].ForeColor = Color.SaddleBrown;
                    }
                    else
                    {
                        Naracki[i].Image = Resources.LabelBackground2;
                        Naracki[i].ForeColor = Color.Gold;
                    }
                }
                else
                {
                    Naracki[i].UpdateObject(null);
                    Naracki[i].Image = Resources.LabelBackground2;
                    Naracki[i].ForeColor = Color.Gold;
                }
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                CurrNaracka = lb.LblObject as Naracka;
                indStavki = 0;
                PostaviStavki();
                PostaviNaracki();
            }
        }

        public void PostaviStavki()
        {
            if (CurrNaracka != null)
            {
                for (int i = 0; i < Stavki.Count; i++)
                    if (i < CurrNaracka.Stavki.Count)
                    {
                        Stavki[i].UpdateObject(CurrNaracka.Stavki[i + indStavki]);
                    }
                    else Stavki[i].UpdateObject(null);

                lblVreme.Text = (DateTime.Now - CurrNaracka.Vreme).ToString(@"dd\.hh\:mm");
                lblVkupno.Text = CurrNaracka.VkupnaCena.ToString();
                if (CurrNaracka is Onsite)
                {
                    Onsite os = CurrNaracka as Onsite;
                    lblMasa.Text = os.BrojMasa.ToString();
                }
                else
                {
                    Online os = CurrNaracka as Online;
                    lblCenaZaDostava.UpdateObject(os.CenaZaDostava);
                    lblMasa.Text = os.AdresaZaDostava;
                    lblKlient.Text = String.Format("{0} {1} ", os.ImeKlient, os.PrezimeKlient);
                    lblKontakt.Text = os.Kontakt;
                }
            }
        }

        private void lbl1_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                lb.Font = new Font("Trebuchet MS", 19, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lbl1_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                lb.Font = new Font("Trebuchet MS", 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void pbListUp_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowUp;
        }

        private void pbNarackiUp_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowUp;
        }

        private void pbNarackiDown_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowDown;
        }

        private void pbNarackiDown_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowDown;
        }

        private void buttonFASAP1_MouseEnter(object sender, EventArgs e)
        {
            ButtonFASAP bt = sender as ButtonFASAP;
            Cursor = Cursors.Hand;
            bt.Image = Resources.LightButton___Copy;
            bt.ForeColor = Color.SaddleBrown;
        }

        private void buttonFASAP1_MouseLeave(object sender, EventArgs e)
        {
            ButtonFASAP bt = sender as ButtonFASAP;
            Cursor = Cursors.Default;
            bt.Image = Resources.DarkButton___Copy;
            bt.ForeColor = Color.Khaki;
        }

        private void pbNarackiUp_Click(object sender, EventArgs e)
        {
            if (indNaracki > 0)
            {
                indNaracki--;
                PostaviNaracki();
            }
        }

        private void pbNarackiDown_Click(object sender, EventArgs e)
        {
            int pom = naracki.Count - Naracki.Count;
            if (indNaracki < pom)
            {
                indNaracki++;
                PostaviNaracki();
            }
        }

        private void pbStavkiUp_Click(object sender, EventArgs e)
        {
            if (indStavki > 0)
            {
                indStavki--;
                PostaviStavki();
            }
        }

        private void pbStavkiDown_Click(object sender, EventArgs e)
        {
            if (CurrNaracka != null)
            {
                int pom = CurrNaracka.Stavki.Count - Stavki.Count;
                if (indStavki < pom)
                {
                    indStavki++;
                    PostaviStavki();
                }
            }
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            try
            {
                Vraboten.PrevzemiNaracka(Conn);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                timer1.Stop();
                ErrorMessageTime = 3;
                timer1.Start();
            }
            PrevzemiNaracki();
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            if (CurrNaracka == null)
            {
                lblErrorMessage.Text = "Ве молиме селектирајте нарачка пред да пробате да ја реализирате.";
                lblErrorMessage.Visible = true;
                timer1.Stop();
                ErrorMessageTime = 3;
                timer1.Start();
                return;
            }

            string updateOnsite = @"update NARACHKA
                                    set realizirana = 1
                                    where narachka_id = :NarID
                                    AND restoran_id = :ResID";

            OracleCommand cmd = new OracleCommand(updateOnsite, Conn);

            OracleParameter prm = new OracleParameter("NarID", OracleDbType.Int16);
            prm.Value = CurrNaracka.NarackaID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ResID", OracleDbType.Int16);
            prm.Value = Vraboten.RestoranID;
            cmd.Parameters.Add(prm);

            try
            {
                cmd.ExecuteNonQuery();
                foreach (var obj in Stavki)
                    obj.Text = ": : ";
                lblVreme.Text = " ";
                lblVkupno.Text = " ";
                lblMasa.Text = " ";
                lblKontakt.Text = " ";
                lblKlient.Text = " ";
                Thread oThread = new Thread(new ThreadStart(PostaviDodatok));
                oThread.Start();
                oThread = new Thread(new ThreadStart(IncrementOrderNumber));
                oThread.Start();
                oThread = new Thread(new ThreadStart(PostaviPromet));
                oThread.Start();
                LoadingSemaphore.WaitOne();
                LoadingSemaphore.WaitOne();
                LoadingSemaphore.WaitOne();
                CurrNaracka = null;
                PrevzemiNaracki();
                timer2.Start();
            }
            catch (Exception)
            {
                lblErrorMessage.Text = "Проверете ја вашата интернет конекција.";
                lblErrorMessage.Visible = true;
                timer1.Stop();
                ErrorMessageTime = 3;
                timer1.Start();
            }
        }

        public void PostaviPromet()
        {
            string updateOnsite = @"insert into PROMET (RESTORAN_ID, MESEC_PROMET, GODINA_PROMET, IZNOS_PROMET) VALUES (:ResID, :Month, :Year, 0)";
            OracleCommand cmd = new OracleCommand(updateOnsite, Conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int32);
            prm.Value = Vraboten.RestoranID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Month", OracleDbType.Char);
            prm.Value = String.Format("{0:00}", DateTime.Now.Month);
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Year", OracleDbType.Char);
            prm.Value = DateTime.Now.Year.ToString();
            cmd.Parameters.Add(prm);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

            updateOnsite = @"Update PROMET SET IZNOS_PROMET = IZNOS_PROMET + :Vkupno where RESTORAN_ID = :ResID AND MESEC_PROMET = :Month AND GODINA_PROMET = :YEAR";

            cmd = new OracleCommand(updateOnsite, Conn);

            prm = new OracleParameter("Vkupno", OracleDbType.Int32);
            prm.Value = CurrNaracka.VkupnaCena;
            cmd.Parameters.Add(prm);
            LoadingSemaphore.Release();
            prm = new OracleParameter("ResID", OracleDbType.Int32);
            prm.Value = Vraboten.RestoranID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Month", OracleDbType.Char);
            prm.Value = String.Format("{0:00}", DateTime.Now.Month);
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Year", OracleDbType.Char);
            prm.Value = DateTime.Now.Year.ToString();
            cmd.Parameters.Add(prm);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }

        public void PostaviDodatok()
        {
            CurrNaracka.PostaviDodatok(Conn, Vraboten.RestoranID, Vraboten.VrabotenID);
            LoadingSemaphore.Release();
        }

        public void IncrementOrderNumber()
        {
            Vraboten.IncrementOrderNumber(Conn, CurrNaracka);
            LoadingSemaphore.Release();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ErrorMessageTime--;
            if (ErrorMessageTime == 0)
            {
                lblErrorMessage.Visible = false;
                timer1.Stop();
            }
        }

        private void VrabotenForma_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Vraboten.OslobodiNaracki(Conn);
            }
            catch (Exception)
            {
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (naracki.Count == 0)
            {
                try
                {
                    Vraboten.PrevzemiNaracka(Conn);
                    PrevzemiNaracki();
                }
                catch (Exception)
                {

                }
            }
            else timer2.Stop();
        }
    }
}
