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
    public partial class DodavanjeVraboten : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        private int VrabotenID { get; set; }
        private List<string> Pozicii { get; set; }
        private int PozInd { get; set; }

        public DodavanjeVraboten(Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            this.Restoran = restoran;
            this.Conn = conn;
            this.Opacity = 0;
            this.NajdiID();
            this.PopolniPozicii();
        }

        // samo za primer
        public DodavanjeVraboten()
        {
            InitializeComponent();
            this.Restoran = new Restoran() {RestoranID = 1};
            string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
             + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
             + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            this.Opacity = 0;
            this.NajdiID();
            this.PopolniPozicii();
        }

        /// <summary>
        /// Prezemanje na site mozni funkcii koi postojat vo bazata
        /// </summary>
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

            UpdatePozicii();
        }

        /// <summary>
        /// Azuriranje na prikazot na labelata so soodvetna sodrzina
        /// od listata pozicii
        /// </summary>
        private void UpdatePozicii()
        {
            if (this.Pozicii.Count != 0)
                this.lblPozicija.Text = this.Pozicii[this.PozInd].ToString();
            else
                this.lblPozicija.Text = " ";
        }

        /// <summary>
        /// Naogjanje na narednoto neiskoristeno ID
        /// za sledniot vraboten
        /// </summary>
        private void NajdiID()
        {
            string sqlID = @"SELECT MAX(VRABOTEN_ID) FROM VRABOTEN";
            OracleCommand cmd = new OracleCommand(sqlID, this.Conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                this.VrabotenID = dr.GetInt32(0) + 1;

            this.lblVrabotenID.Text = this.VrabotenID.ToString();
        }

        private void tbIme_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb.Text.Trim() == "")
            {
                MessageBoxForm mbf = new MessageBoxForm("Полето е задолжително!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            tb.SelectAll();
        }

        private void tbEmbg_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            long res;
            if (tb.Text.Trim() == "")
            {
                MessageBoxForm mbf = new MessageBoxForm("Полето е задолжително!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            else if (tb.Text.Length != 13)
            {
                MessageBoxForm mbf = new MessageBoxForm("ЕМБГ мора да има точно 13 цифри!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            else if (!long.TryParse(tb.Text, out res))
            {
                MessageBoxForm mbf = new MessageBoxForm("ЕМБГ мора да содржи само цифри!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            tb.SelectAll();
        }

        private void tbDen_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            int res;
            if (tb.Text.Trim() == "")
            {
                e.Cancel = false;
                return;
            }

            if (!int.TryParse(tb.Text, out res) || tb.Text.Length > 2)
            {
                MessageBoxForm mbf = new MessageBoxForm("Датумот е невалиден!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            tb.SelectAll();
        }

        private void tbGodina_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            int res;
            if (tb.Text.Trim() == "")
            {
                e.Cancel = false;
                return;
            }

            if (!int.TryParse(tb.Text, out res) || tb.Text.Length != 4)
            {
                MessageBoxForm mbf = new MessageBoxForm("Датумот е невалиден!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            tb.SelectAll();
        }

        private void tbPlata_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            int res;
            if (tb.Text.Trim() == "")
            {
                MessageBoxForm mbf = new MessageBoxForm("Полето е задолжително!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            else if (!int.TryParse(tb.Text, out res))
            {
                MessageBoxForm mbf = new MessageBoxForm("Платата мора да содржи само цифри!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            else if (int.TryParse(tb.Text, out res) && res <= 8500)
            {
                MessageBoxForm mbf = new MessageBoxForm("Износот на платата мора да биде поголема од 8500 ден.", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            tb.SelectAll();
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            MessageBoxForm mbf = new MessageBoxForm("Промените ќе бидат отфрлени.\nДали сте сигурни дека сакате да ја напуштите формата?");
            if (mbf.ShowDialog() == DialogResult.Yes)
            {
                this.AutoValidate = AutoValidate.Disable;
                this.Close();
            }
        }

        /// <summary>
        /// Proverka dali datumot e celosen (ili site polinja se prazni
        /// ili site se popolneti, ne se dozvoluvaat delumni podatoci)
        /// </summary>
        /// <returns></returns>
        private bool CheckDate()
        {
            if (tbDen.Text.Length != 0 && tbMesec.Text.Length != 0 && tbGodina.Text.Length != 0)
                return true;
            if (tbDen.Text.Length == 0 && tbMesec.Text.Length == 0 && tbGodina.Text.Length == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Dodavanje vo relacijata Vraboten
        /// </summary>
        /// <returns></returns>
        private int DodadiVoDBVraboten()
        {
            string insertVrab = @"INSERT INTO VRABOTEN (VRABOTEN_ID, IME_VRABOTEN, PREZIME_VRABOTEN, EMBG, DATUM_NA_RAGJANJE, ADRESA_NA_ZHIVEENJE) VALUES (:VRAB_ID, :IME, :PREZIME, :EMBG, :DAT, :ADR)";
            OracleCommand cmd = new OracleCommand(insertVrab, Conn);
            OracleParameter prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
            prm.Value = this.VrabotenID;
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("IME", OracleDbType.Varchar2);
            prm.Value = this.tbIme.Text.Trim();
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("PREZIME", OracleDbType.Varchar2);
            prm.Value = this.tbPrezime.Text.Trim();
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("EMBG", OracleDbType.Char);
            prm.Value = this.tbEmbg.Text.Trim();
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("DAT", OracleDbType.Date);
            if (tbDen.Text.Trim() != "")
            {
                int d = int.Parse(tbDen.Text.Trim());
                int m = int.Parse(tbMesec.Text.Trim());
                int y = int.Parse(tbGodina.Text.Trim());
                DateTime dt;
                try
                {
                    dt = new DateTime(y, m, d);
                    prm.Value = dt;
                }
                catch (Exception e)
                {
                    MessageBoxForm mbf = new MessageBoxForm("Невалиден датум!", false);
                    mbf.Show();
                }
            }
            else
                prm.Value = null;
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("ADR", OracleDbType.Varchar2);
            string adresa = "";
            adresa += this.tbAdresa.Text.Trim();
            if (this.tbGrad.Text.Trim() != "")
                adresa += ", " + this.tbGrad.Text.Trim();
            if (adresa != "")
                prm.Value = adresa;
            else
                prm.Value = null;
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                br = -1;
            }
            
            return br;
        }

        /// <summary>
        /// Dodavanje vo relacijata Izvrshuva
        /// </summary>
        /// <returns></returns>
        private int DodadiVoDBIzvrshuva()
        {
            string insertVrab = @"INSERT INTO IZVRSHUVA (VRABOTEN_ID, RESTORAN_ID, POZICIJA, DATUM_NA_VRABOTUVANJE, PLATA) VALUES (:VRAB_ID, :REST_ID, :POZ, TO_DATE(:DAT, 'dd.MM.yyyy'), :PLATA)";
            OracleCommand cmd = new OracleCommand(insertVrab, Conn);
            OracleParameter prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
            prm.Value = this.VrabotenID;
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("REST_ID", OracleDbType.Int64);
            prm.Value = this.Restoran.RestoranID;
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("POZ", OracleDbType.Varchar2);
            prm.Value = this.Pozicii[this.PozInd];
            cmd.Parameters.Add(prm);
            string datum = DateTime.Now.Day.ToString().Trim() + "." + DateTime.Now.Month.ToString().Trim() + "." + DateTime.Now.Year.ToString().Trim();
            prm = new OracleParameter("DAT", OracleDbType.Varchar2);
            prm.Value = datum;
            cmd.Parameters.Add(prm);
            int plata = int.Parse(this.tbPlata.Text.Trim());
            prm = new OracleParameter("PLATA", OracleDbType.Int64);
            prm.Value = plata;
            cmd.Parameters.Add(prm);

            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                br = -1;
            }

            return br;
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            if (CheckDate())
            {
                MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да го додадете вработениот во листата вработени?");
                if (mbf.ShowDialog() == DialogResult.Yes)
                {
                    int br1 = this.DodadiVoDBVraboten();
                    int br2 = this.DodadiVoDBIzvrshuva();
                    if (br1 == 1 && br2 == 1)
                    {
                        MessageBoxForm mbf1 = new MessageBoxForm("Вработениот беше успешно додаден!", false);
                        mbf1.ShowDialog();
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    else
                    {
                        MessageBoxForm mbf1 = new MessageBoxForm("Грешка!\nВработениот не може да се додаде!", false);
                        mbf1.ShowDialog();
                    }
                }
                    
            }
            else
            {
                MessageBoxForm mbf = new MessageBoxForm("Датумот е невалиден!", false);
                mbf.ShowDialog();
                tbDen.SelectAll();
                tbMesec.SelectAll();
                tbGodina.SelectAll();
            }
        }

        private void buttonFASAPPotvrdi_MouseEnter(object sender, EventArgs e)
        {
            this.buttonFASAP1.Image = Resources.LightButton___Copy;
            this.buttonFASAP1.ForeColor = Color.Sienna;
        }

        private void buttonFASAPPotvrdi_MouseLeave(object sender, EventArgs e)
        {
            this.buttonFASAP1.Image = Resources.DarkButton___Copy;
            this.buttonFASAP1.ForeColor = Color.Khaki;
        }

        private void buttonFASAP2_MouseEnter(object sender, EventArgs e)
        {
            this.buttonFASAP2.Image = Resources.LightButton___Copy;
            this.buttonFASAP2.ForeColor = Color.Sienna;
        }

        private void buttonFASAP2_MouseLeave(object sender, EventArgs e)
        {
            this.buttonFASAP2.Image = Resources.DarkButton___Copy;
            this.buttonFASAP2.ForeColor = Color.Khaki;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowLeft;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowRight;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (this.Pozicii.Count > 1)
            {
                this.PozInd = (this.PozInd + 1) % this.Pozicii.Count;
                UpdatePozicii();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.Pozicii.Count > 1)
            {
                if (this.PozInd == 0)
                    this.PozInd = this.Pozicii.Count;
                this.PozInd--;
                UpdatePozicii();
            }
        }
    }
}
