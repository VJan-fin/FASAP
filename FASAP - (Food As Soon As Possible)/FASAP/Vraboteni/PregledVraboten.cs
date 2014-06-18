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
        private bool StatusVrab { get; set; }

        /// <summary>
        /// Pomosna lista koja gi sodrzi pomosnite znaci koi
        /// vo nekoi od rezimite na raboti ne se vidlivi
        /// </summary>
        private List<LabelFASAP> Nevidlivi { get; set; }
        /// <summary>
        /// Pomosna promenliva koja oznacuva koj e tekovniot
        /// rezim na rabota
        /// (true - Moze da se menuvaat podatocite; false)
        /// </summary>
        private bool ModifyMode { get; set; }
        /// <summary>
        /// Pomosna lista koja gi sodrzi site kontroli koi mozat
        /// da bidat izmeneti vo soodvetniot rezim na rabota
        /// </summary>
        private List<TextBox> Writeable { get; set; }

        public PregledVraboten(int id, Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            this.VrabotenID = id;
            this.Restoran = restoran;
            this.Conn = conn;

            this.Init();
        }
        /*
        // samo za primer
        public PregledVraboten()
        {
            InitializeComponent();
            this.Restoran = new Restoran() { RestoranID = 1 };
            this.VrabotenID = 7;
            string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
             + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
             + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            this.Init();
        }
        */
        private void Init()
        {
            this.Opacity = 0;

            this.Nevidlivi = new List<LabelFASAP>();
            this.Nevidlivi.Add(lblZad1);
            this.Nevidlivi.Add(lblZad2);
            this.Nevidlivi.Add(lblZad3);
            this.Nevidlivi.Add(lblZad4);
            this.Nevidlivi.Add(lblZad5);
            this.Nevidlivi.Add(lblZab);

            this.Writeable = new List<TextBox>();
            this.Writeable.Add(tbIme);
            this.Writeable.Add(tbPrezime);
            this.Writeable.Add(tbEmbg);
            this.Writeable.Add(tbAdresa);
            this.Writeable.Add(tbDenRagjanje);
            this.Writeable.Add(tbMesecRagjanje);
            this.Writeable.Add(tbGodinaRagjanje);
            this.Writeable.Add(tbDenVrab);
            this.Writeable.Add(tbMesecVrab);
            this.Writeable.Add(tbGodVrab);
            this.Writeable.Add(tbPlata);
            this.Writeable.Add(tbOdmor);

            this.ModifyMode = false;
            this.PostaviVidlivost();
            this.PostaviMenlivost();
            this.PopolniPozicii();
            this.PopolniInfo();
        }

        /// <summary>
        /// Azuriranje na vidlivosta na odredeni elementi
        /// vo zavisnost od rezimot na rabota
        /// Rezim 1 - Samo pregled na podatocite (ModifyMode = false)
        /// Rezim 2 - Azuriranje na podatocite (ModifyMode = true)
        /// </summary>
        private void PostaviVidlivost()
        {
            foreach (var item in this.Nevidlivi)
                item.Visible = this.ModifyMode;
            pictureBox4.Visible = this.ModifyMode;
            pictureBox5.Visible = this.ModifyMode;
            pictureBox6.Visible = this.ModifyMode;
            pictureBox7.Visible = this.ModifyMode;
        }

        /// <summary>
        /// Metod koj soodvetno go postavuva svojstvoto
        /// ReadOnly na TextBox kontrolite, vo zavisnost
        /// od rezimot na rabota
        /// </summary>
        private void PostaviMenlivost()
        {
            foreach (var item in this.Writeable)
                item.ReadOnly = !this.ModifyMode;
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

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    this.Pozicii.Add(dr.GetString(0));
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            for (int i = 0; i < this.Pozicii.Count; i++)
                if (this.Pozicii[i] == this.lblPozicija.Text)
                {
                    this.PozInd = i;
                    break;
                }

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
        /// Vcituvanje na site podatoci za dadeniot vraboten
        /// i popolnuvanje na soodvetnite polinja
        /// </summary>
        private void PopolniInfo()
        {
            this.lblVrabotenID.Text = this.VrabotenID.ToString();

            // Vcituvanje na licnite podatoci na vraboteniot
            string sqlVrab = @"SELECT * FROM VRABOTEN WHERE VRABOTEN_ID = :VRAB_ID";
            OracleCommand cmd = new OracleCommand(sqlVrab, this.Conn);
            cmd.CommandType = CommandType.Text;
            OracleParameter prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
            prm.Value = this.VrabotenID;
            cmd.Parameters.Add(prm);

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tbIme.Text = dr.GetString(1);
                    tbPrezime.Text = dr.GetString(2);
                    tbEmbg.Text = dr.GetString(3);
                    try
                    {
                        DateTime rag = dr.GetDateTime(4);
                        tbDenRagjanje.Text = rag.Day.ToString();
                        tbMesecRagjanje.Text = rag.Month.ToString();
                        tbGodinaRagjanje.Text = rag.Year.ToString();
                    }
                    catch (InvalidCastException e)
                    {
                        tbDenRagjanje.Text = "";
                        tbMesecRagjanje.Text = "";
                        tbGodinaRagjanje.Text = "";
                    }

                    try
                    {
                        tbAdresa.Text = dr.GetString(5);
                    }
                    catch (InvalidCastException e)
                    {
                        tbAdresa.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            // Popolnuvanje na poziciite povrzani so rabotniot odnos
            // na vraboteniot vo dadeniot restoran
            string sqlFunk = @"SELECT * FROM IZVRSHUVA WHERE VRABOTEN_ID = :VRAB_ID AND RESTORAN_ID = :REST_ID";
            cmd = new OracleCommand(sqlFunk, this.Conn);
            cmd.CommandType = CommandType.Text;
            prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
            prm.Value = this.VrabotenID;
            cmd.Parameters.Add(prm);
            prm = new OracleParameter("REST_ID", OracleDbType.Int64);
            prm.Value = this.Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblPozicija.Text = dr.GetString(2);
                    for (int i = 0; i < this.Pozicii.Count; i++)
                        if (this.Pozicii[i] == lblPozicija.Text)
                        {
                            this.PozInd = i;
                            break;
                        }
                    DateTime vrab = dr.GetDateTime(3);
                    tbDenVrab.Text = vrab.Day.ToString();
                    tbMesecVrab.Text = vrab.Month.ToString();
                    tbGodVrab.Text = vrab.Year.ToString();
                    string st = dr.GetString(4);
                    if (st == "1")
                        this.StatusVrab = true;
                    else
                        this.StatusVrab = false;
                    this.UpdateStatusVrab();
                    tbPlata.Text = dr.GetInt64(5).ToString();
                    tbOdmor.Text = dr.GetInt64(6).ToString();
                    tbVkNaracki.Text = dr.GetInt64(7).ToString();
                    tbStaz.Text = dr.GetInt64(8).ToString();
                }
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

        /// <summary>
        /// Gi azurira podatocite za vraboteniot so menuvanje
        /// na vrednostite na soodvetnite atributi vo bazata
        /// </summary>
        private bool AzurirajPodatoci()
        {
            // Azuriranje na licnite podatoci za vraboteniot
            string sqlVrab = @"UPDATE VRABOTEN SET IME_VRABOTEN = :IME, PREZIME_VRABOTEN = :PREZIME, EMBG = :EMBG, DATUM_NA_RAGJANJE = TO_DATE(:DAT, 'dd.MM.yyyy'), ADRESA_NA_ZHIVEENJE = :ADR WHERE VRABOTEN_ID = :VRAB_ID";
            //string sqlVrab = @"UPDATE VRABOTEN SET IME_VRABOTEN = :IME WHERE VRABOTEN_ID = 7";
            OracleCommand cmd = new OracleCommand(sqlVrab, this.Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleParameter prm = new OracleParameter("IME", OracleDbType.Varchar2);
                prm.Value = this.tbIme.Text.Trim();
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("PREZIME", OracleDbType.Varchar2);
                prm.Value = this.tbPrezime.Text.Trim();
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("EMBG", OracleDbType.Char);
                prm.Value = this.tbEmbg.Text.Trim();
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("DAT", OracleDbType.Varchar2);
                if (tbDenRagjanje.Text.Trim() != "")
                    prm.Value = tbDenRagjanje.Text.Trim() + "." + tbMesecRagjanje.Text.Trim() + "." + tbGodinaRagjanje.Text.Trim();
                else
                    prm.Value = null;
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("ADR", OracleDbType.Varchar2);
                if (this.tbAdresa.Text.Trim() != "")
                    prm.Value = this.tbAdresa.Text.Trim();
                else
                    prm.Value = null;
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
                prm.Value = this.VrabotenID;
                cmd.Parameters.Add(prm);
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            //cmd.ExecuteNonQuery();
            
            cmd.CommandType = CommandType.Text;
            int br1;
            try
            {
                br1 = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                br1 = -1;
            }
            
            // Azuriranje na podatocite povrzani so rabotniot odnos
            // na vraboteniot vo dadeniot restoran
            string sqlIzvrsuva = @"UPDATE IZVRSHUVA SET POZICIJA = :POZ, DATUM_NA_VRABOTUVANJE = TO_DATE(:DAT, 'dd.MM.yyyy'), STATUS = :STAT, PLATA = :PLATA, GODISHEN_ODMOR = :ODMOR WHERE VRABOTEN_ID = :VRAB_ID AND RESTORAN_ID = :REST_ID";
            cmd = new OracleCommand(sqlIzvrsuva, this.Conn);

            try
            {
                OracleParameter prm = new OracleParameter("POZ", OracleDbType.Varchar2);
                prm.Value = this.lblPozicija.Text;
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("DAT", OracleDbType.Varchar2);
                if (tbDenVrab.Text.Trim() != "")
                    prm.Value = tbDenVrab.Text.Trim() + "." + tbMesecVrab.Text.Trim() + "." + tbGodVrab.Text.Trim();
                else
                    prm.Value = null;
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("STAT", OracleDbType.Char);
                if (this.StatusVrab)
                    prm.Value = '1';
                else
                    prm.Value = '0';
                cmd.Parameters.Add(prm);
                int plata = int.Parse(this.tbPlata.Text.Trim());
                prm = new OracleParameter("PLATA", OracleDbType.Int64);
                prm.Value = plata;
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("ODMOR", OracleDbType.Int64);
                if (tbOdmor.Text.Trim() != "")
                {
                    int odmor = int.Parse(this.tbOdmor.Text.Trim());
                    prm.Value = odmor;
                }
                else
                    prm.Value = 0;
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
                prm.Value = this.VrabotenID;
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            //cmd.ExecuteNonQuery();
            
            int br2;
            try
            {
                br2 = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                br2 = -1;
            }
            
            if (br1 == -1 || br2 == -1)
            {
                this.PopolniInfo();
                return false;
            }

            this.PopolniInfo();
            return true;
        }

        /// <summary>
        /// Azuriranje na labelata za statusot na vraboteniot
        /// </summary>
        private void UpdateStatusVrab()
        {
            if (this.StatusVrab)
                lblStatus.Text = "Активен  ";
            else
                lblStatus.Text = "Неактивен  ";
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

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowLeft;
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowLeft;
            this.Cursor = Cursors.Default;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowRight___Copy;
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowRight;
            this.Cursor = Cursors.Default;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (this.Pozicii.Count > 1)
            {
                this.PozInd = (this.PozInd + 1) % this.Pozicii.Count;
                this.UpdatePozicii();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.Pozicii.Count > 1)
            {
                if (this.PozInd == 0)
                    this.PozInd = this.Pozicii.Count;
                this.PozInd--;
                this.UpdatePozicii();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.StatusVrab = !this.StatusVrab;
            UpdateStatusVrab();
        }

        /// <summary>
        /// Proverka dali datumot e celosen (ili site polinja se prazni
        /// ili site se popolneti, ne se dozvoluvaat delumni podatoci)
        /// </summary>
        /// <returns></returns>
        private bool CheckDates()
        {
            bool d1 = false;
            if (tbDenRagjanje.Text.Length != 0 && tbMesecRagjanje.Text.Length != 0 && tbGodinaRagjanje.Text.Length != 0)
                d1 = true;
            if (tbDenRagjanje.Text.Length == 0 && tbMesecRagjanje.Text.Length == 0 && tbGodinaRagjanje.Text.Length == 0)
                d1 = true;

            if (!d1)
            {
                tbDenRagjanje.SelectAll();
                tbMesecRagjanje.SelectAll();
                tbGodinaRagjanje.SelectAll();
            }

            bool d2 = false;
            if (tbDenVrab.Text.Length != 0 && tbMesecVrab.Text.Length != 0 && tbGodVrab.Text.Length != 0)
                d2 = true;
            if (tbDenVrab.Text.Length == 0 && tbMesecVrab.Text.Length == 0 && tbGodVrab.Text.Length == 0)
                d2 = true;

            if (!d2)
            {
                tbDenVrab.SelectAll();
                tbMesecVrab.SelectAll();
                tbGodVrab.SelectAll();
            }

            return (d1 && d2);
        }

        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            if (!this.ModifyMode)
            {
                buttonIzmeni.Text = "Зачувај";
                this.ModifyMode = true;
            }
            else
            {
                if (this.CheckDates())
                {
                    MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да ги зачувате промените?");
                    if (mbf.ShowDialog() == DialogResult.Yes)
                    {
                        if (this.AzurirajPodatoci())
                        {
                            MessageBoxForm mbf1 = new MessageBoxForm("Податоците беа успешно променети!", false);
                            mbf1.ShowDialog();
                            buttonIzmeni.Text = "Измени";
                            this.ModifyMode = false;
                        }
                        else
                        {
                            MessageBoxForm mbf1 = new MessageBoxForm("Обидот за промена на некои од податоците не беше успешен.\nОбидете се повторно.", false);
                            mbf1.ShowDialog();
                        }
                    }

                }
                else
                {
                    MessageBoxForm mbf = new MessageBoxForm("Невалиден датум!", false);
                    mbf.ShowDialog();
                }
            }
                
            this.PostaviVidlivost();
            this.PostaviMenlivost();
            this.PopolniInfo();
        }

        /// <summary>
        /// Otstranuvanje na cursor-ot od kontrolite koga ne e
        /// vozmozno da se menuva sodrzinata
        /// </summary>
        /// <param name="sender"></param>
        private void LoseCursor(object sender)
        {
            (sender as TextBox).Enabled = false;
            (sender as TextBox).Enabled = true;
        }

        private void tbStaz_Click(object sender, EventArgs e)
        {
            MessageBoxForm mbf = new MessageBoxForm("Не е возможна промена на тоа поле!", false);
            mbf.ShowDialog();
            this.LoseCursor(sender);
        }

        private void tbIme_Click(object sender, EventArgs e)
        {
            if (!this.ModifyMode)
            {
                MessageBoxForm mbf = new MessageBoxForm("Не е возможна промена на тоа поле.\nПритиснете измени и обидете се повторно!", false);
                mbf.ShowDialog();
                this.LoseCursor(sender);
            }
        }

        private void tbIme_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
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
        }

        private void tbEmbg_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
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
        }

        private void tbDen_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
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
        }

        private void tbGodina_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
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
        }

        private void tbPlata_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
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
        }

        private void tbOdmor_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
            {
                TextBox tb = (sender as TextBox);
                int res;
                if (int.TryParse(tb.Text, out res) && res > 26)
                {
                    MessageBoxForm mbf = new MessageBoxForm("Годишниот одмор не може да биде подолг од 26 дена!", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                }
                tb.SelectAll();
            }
        }

        private void buttonOtkazi_Click(object sender, EventArgs e)
        {
            if (this.ModifyMode)
            {
                MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да ги отфрлите промените?");
                if (mbf.ShowDialog() == DialogResult.Yes)
                {
                    //this.AutoValidate = AutoValidate.Disable;
                    this.ModifyMode = false;
                    this.buttonIzmeni.Text = "Измени";

                    this.PostaviVidlivost();
                    this.PostaviMenlivost();
                    this.PopolniInfo();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }
    }
}
