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
    public partial class UpravuvanjeSmetki : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        private int VrabotenID { get; set; }
        private string KorisnickoIme { get; set; }
        private string Lozinka { get; set; }

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

        // samo za proba
        /*
        public UpravuvanjeSmetki()
        {
            InitializeComponent();

            this.Restoran = new Restoran() { RestoranID = 1 };
            this.VrabotenID = 8;
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
        public UpravuvanjeSmetki(int id, Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            this.Restoran = restoran;
            this.VrabotenID = id;
            this.Conn = conn;
            this.KorisnickoIme = null;
            this.Lozinka = null;
            this.ModifyMode = true;

            Init();
        }

        public UpravuvanjeSmetki(int id, Restoran restoran, OracleConnection conn, string user, string pass)
        {
            InitializeComponent();
            this.Restoran = restoran;
            this.VrabotenID = id;
            this.Conn = conn;
            this.KorisnickoIme = user;
            this.Lozinka = pass;
            this.ModifyMode = false;

            Init();
        }

        public void Init()
        {
            this.Opacity = 0;

            this.Nevidlivi = new List<LabelFASAP>();
            this.Nevidlivi.Add(lblZad1);
            this.Nevidlivi.Add(lblZad2);
            this.Nevidlivi.Add(lblZab);
            this.Nevidlivi.Add(labelFASAP4);

            this.Writeable = new List<TextBox>();
            this.Writeable.Add(tbUserName);
            this.Writeable.Add(tbPassword);
            this.Writeable.Add(tbRetypePassword);

            this.PostaviVidlivost();
            this.PostaviMenlivost();
            this.PopolniInfo();
        }

        /// <summary>
        /// Vcituvanje na site podatoci za dadeniot vraboten
        /// i popolnuvanje na soodvetnite polinja
        /// </summary>
        private void PopolniInfo()
        {
            this.lblVrabotenID.Text = this.VrabotenID.ToString();

            if (this.KorisnickoIme != null && this.Lozinka != null)
            {
                this.tbUserName.Text = this.KorisnickoIme;
                this.tbPassword.Text = this.Lozinka;
                return;
            }

            // Vcituvanje na podatocite za korisnickata smetka
            string sqlSmetka = @"SELECT KORISNICHKO_IME, LOZINKA FROM KORISNIK WHERE RESTORAN_ID = :REST_ID AND VRABOTEN_ID = :VRAB_ID";
            OracleCommand cmd = new OracleCommand(sqlSmetka, this.Conn);
            cmd.CommandType = CommandType.Text;
            OracleParameter prm;

            try
            {
                prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);
                prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
                prm.Value = this.VrabotenID;
                cmd.Parameters.Add(prm);

                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tbUserName.Text = dr.GetString(0);
                    tbPassword.Text = dr.GetString(1);
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
        /// Azuriranje na vidlivosta na odredeni elementi
        /// vo zavisnost od rezimot na rabota
        /// Rezim 1 - Samo pregled na podatocite (ModifyMode = false)
        /// Rezim 2 - Azuriranje na podatocite (ModifyMode = true)
        /// </summary>
        private void PostaviVidlivost()
        {
            foreach (var item in this.Nevidlivi)
                item.Visible = this.ModifyMode;
            tbRetypePassword.Visible = this.ModifyMode;
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
        /// Otstranuvanje na cursor-ot od kontrolite koga ne e
        /// vozmozno da se menuva sodrzinata
        /// </summary>
        /// <param name="sender"></param>
        private void LoseCursor(object sender)
        {
            (sender as TextBox).Enabled = false;
            (sender as TextBox).Enabled = true;
        }

        public int AzurirajPodatoci()
        {
            if (this.KorisnickoIme != null && this.Lozinka != null)
            {
                // Azuriranje na korisnickoto ime i na lozinkata koi veke
                // postojat vo bazata
                string sqlUser = @"UPDATE KORISNIK SET KORISNICHKO_IME = :KOR, LOZINKA = :LOZ WHERE RESTORAN_ID = :REST_ID AND VRABOTEN_ID = :VRAB_ID";
                OracleCommand cmd = new OracleCommand(sqlUser, this.Conn);
                cmd.CommandType = CommandType.Text;

                try
                {
                    OracleParameter prm = new OracleParameter("KOR", OracleDbType.Varchar2);
                    prm.Value = this.tbUserName.Text.Trim();
                    cmd.Parameters.Add(prm);
                    prm = new OracleParameter("LOZ", OracleDbType.Varchar2);
                    prm.Value = this.tbPassword.Text.Trim();
                    cmd.Parameters.Add(prm);
                    prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                    prm.Value = this.Restoran.RestoranID;
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

                int br;
                try
                {
                    br = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    br = -1;
                }

                this.KorisnickoIme = this.tbUserName.Text.Trim();
                this.Lozinka = this.tbPassword.Text.Trim();
                this.PopolniInfo();

                return br;
            }
            else
            {
                //Dodavanje na vnesenite podatoci za korisnicko ime i lozinka
                string sqlUser = @"INSERT INTO KORISNIK (RESTORAN_ID, KORISNICHKO_IME, LOZINKA, VRABOTEN_ID) VALUES (:REST_ID, :KOR, :LOZ, :VRAB_ID)";
                OracleCommand cmd = new OracleCommand(sqlUser, this.Conn);
                cmd.CommandType = CommandType.Text;

                try
                {
                    OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                    prm.Value = this.Restoran.RestoranID;
                    cmd.Parameters.Add(prm);
                    prm = new OracleParameter("KOR", OracleDbType.Varchar2);
                    prm.Value = this.tbUserName.Text.Trim();
                    cmd.Parameters.Add(prm);
                    prm = new OracleParameter("LOZ", OracleDbType.Varchar2);
                    prm.Value = this.tbPassword.Text.Trim();
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
                
                int br;
                try
                {
                    br = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    br = -1;
                }

                this.PopolniInfo();

                return br;
            }
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
                if (this.tbPassword.Text.Trim() == this.tbRetypePassword.Text.Trim())
                {
                    MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да ги зачувате промените?");
                    if (mbf.ShowDialog() == DialogResult.Yes)
                    {
                        if (this.AzurirajPodatoci() != -1)
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
                    MessageBoxForm mbf = new MessageBoxForm("Потврдата на лозинката не одговара на првично внесената лозинка!", false);
                    mbf.ShowDialog();
                    this.tbPassword.Clear();
                    this.tbRetypePassword.Clear();
                }
            }

            this.tbRetypePassword.Clear();
            this.PostaviVidlivost();
            this.PostaviMenlivost();
            this.PopolniInfo();
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

            this.tbRetypePassword.Clear();
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

        private void tbUserName_Click(object sender, EventArgs e)
        {
            if (!this.ModifyMode)
            {
                MessageBoxForm mbf = new MessageBoxForm("Не е возможна промена на тоа поле.\nПритиснете измени и обидете се повторно!", false);
                mbf.ShowDialog();
                this.LoseCursor(sender);
            }
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
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
    }
}
