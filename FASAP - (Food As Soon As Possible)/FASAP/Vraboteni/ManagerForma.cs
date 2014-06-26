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
using System.IO;
using System.Net;
using System.Threading;
using SmetkaZaNaracka.Izvestai;

namespace SmetkaZaNaracka
{
    public partial class ManagerForma : BackgroundForm
    {
        private Restoran CurrRestoran { get; set; }
        private ManagerC Manager { get; set; }
        private OracleConnection Conn { get; set; }
        private Semaphore LoadingSemaphore { get; set; }

        public ManagerForma(OracleConnection conn, ManagerC manager)
        {
            InitializeComponent();
            LoadingSemaphore = new Semaphore(0, 100);
            Manager = manager;
            Conn = conn;
            pictureBoxLogo.Image = Resources.FASAP_LOGO;
            Init();
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
            Init();
        }

        private void LoadLogo()
        {
            // Доколку нема линк постави ја предодредената слика
            if (CurrRestoran.LogoUrl == null)
                return;
            Image img;
            try
            {
                // Пробај да ја вчиташ сликата
                img = ImageFromURL(CurrRestoran.LogoUrl);

                // Забелешка: Вчитување со посебен метод за да се земе Image објект. Можно е и директно во PictureBox
                // но така не е можно да се обезбеди паралелизам, а да не се примети кочење, бидејќи самите контроли
                // синхронизираат и не дозволуваат во исто време две нишки да прават измени.
            }
            catch (Exception)
            {
                // Доколку не може да се вчита постави предодредена слика
                img = Resources.FASAP_LOGO;
            }

            // Чекај додека не заврши почетното исцртување на формата (важно за да не се приметува сецкање при појавувањето на формата)
            LoadingSemaphore.WaitOne();

            // Постави ја сликата на pictureBoxLogo
            SetPbDefaultLogo(pictureBoxLogo, img);
        }

        delegate void SetDefaultLogoMethod(PictureBox fs, Image obj);

        private void SetPbDefaultLogo(PictureBox fs, Image obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                SetDefaultLogoMethod d = new SetDefaultLogoMethod(SetPbDefaultLogo);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Image = obj;
            }
        }
        /// <summary>
        /// Pocetna inicijalizacija na formata i popolnuvanje
        /// na pozicijata za ime na menadzerot
        /// </summary>
        private void Init()
        {
            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.lblManager.Text = Manager.Ime + " " + Manager.Prezime;
            this.popolniRestoran();

            // Вчитување на лого за ресторанот
            Thread oThread = new Thread(new ThreadStart(LoadLogo));
            oThread.Start();
        }

        /// <summary>
        /// Vcituvanje na site podatoci za restoranot vo koj
        /// raboti menadzerot koj e tekovno najaven
        /// </summary>
        private void popolniRestoran()
        {
            string sqlRestoran = @"SELECT * FROM RESTORAN WHERE RESTORAN_ID = :REST_ID";
            OracleCommand cmd = new OracleCommand(sqlRestoran, this.Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.Manager.RestoranID;
                cmd.Parameters.Add(prm);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                CurrRestoran = new Restoran();
                if (dr.Read())
                {
                    CurrRestoran.RestoranID = (int)dr.GetValue(0);
                    CurrRestoran.Ime = dr.GetString(2);
                    if (!dr.IsDBNull(3))
                    {
                        CurrRestoran.Ulica = dr.GetString(3);
                    }
                    else CurrRestoran.Ulica = "";
                    if (!dr.IsDBNull(4))
                    {
                        CurrRestoran.Grad = dr.GetString(4);
                    }
                    else CurrRestoran.Grad = "";

                    CurrRestoran.Rejting = (float)dr.GetValue(5);
                    if (!dr.IsDBNull(6))
                    {
                        CurrRestoran.RabotnoVreme = dr.GetString(6);
                    }
                    else CurrRestoran.RabotnoVreme = "";
                    if (!dr.IsDBNull(7))
                    {
                        CurrRestoran.Kapacitet = dr.GetInt16(7);
                    }
                    else CurrRestoran.Kapacitet = null;
                    if (!dr.IsDBNull(8))
                    {
                        CurrRestoran.BrojMasi = dr.GetInt16(8);
                    }
                    else CurrRestoran.BrojMasi = null;
                    if (!dr.IsDBNull(9))
                    {
                        CurrRestoran.CenaZaDostava = dr.GetInt16(9);
                    }
                    else CurrRestoran.CenaZaDostava = null;
                    if (!dr.IsDBNull(10))
                    {
                        CurrRestoran.PragZaDostava = dr.GetInt16(10);
                    }
                    else CurrRestoran.PragZaDostava = null;
                    if (!dr.IsDBNull(11))
                    {
                        CurrRestoran.DatumNaOtvoranje = dr.GetDateTime(11);
                    }
                    else CurrRestoran.DatumNaOtvoranje = null;

                    Object LogoUrl = dr.GetValue(13);
                    if (LogoUrl == null)
                        CurrRestoran.LogoUrl = null;
                    else CurrRestoran.LogoUrl = LogoUrl as String;

                    CurrRestoran.Kategorija = dr.GetString(12);
                }

                this.lblrest.Text = this.CurrRestoran.Ime;
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

        private void btnInfo_Click(object sender, EventArgs e)
        {
            PregledInformacii p = new PregledInformacii(Conn,Manager.RestoranID);
            p.Show();
        }

        private void btnVraboteni_Click(object sender, EventArgs e)
        {
            ListaVraboteni v = new ListaVraboteni(this.CurrRestoran, this.Conn);
            v.Show();
        }

        private void btnPonuda_Click(object sender, EventArgs e)
        {
            PregledMeni v = new PregledMeni(this.CurrRestoran, Manager, this.Conn);
            v.Show();
        }

        private void btnPregledPromet_MouseEnter(object sender, EventArgs e)
        {
            ButtonFASAP btn = (sender as ButtonFASAP);
            btn.Image = Resources.LightButton___Copy;
            btn.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnPregledPromet_MouseLeave(object sender, EventArgs e)
        {
            ButtonFASAP btn = (sender as ButtonFASAP);
            btn.Image = Resources.DarkButton___Copy;
            btn.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnPregledPromet_Click(object sender, EventArgs e)
        {
            PregledPromet pp = new PregledPromet(this.Conn, this.CurrRestoran);
            pp.Show();
        }

        private void btnPridonesPromet_Click(object sender, EventArgs e)
        {
            Izv1PridonesVoPromet pp = new Izv1PridonesVoPromet(this.Conn, this.CurrRestoran.RestoranID);
            pp.Show();
        }

        private void btnKvartalnaSostojba_Click(object sender, EventArgs e)
        {
            PregledKvartalnaSostojba ks = new PregledKvartalnaSostojba(this.Conn, this.CurrRestoran);
            ks.Show();
        }

        private Image ImageFromURL(string Url)
        {
            byte[] imageData = DownloadData(Url);
            Image img = null;

            MemoryStream stream = new MemoryStream(imageData);
            img = Image.FromStream(stream);


            stream.Close();

            return img;
        }

        private byte[] DownloadData(string Url)
        {
            string empty = string.Empty;
            return DownloadData(Url, out empty);
        }

        private byte[] DownloadData(string Url, out string responseUrl)
        {
            byte[] downloadedData = new byte[0];
            try
            {
                //Get a data stream from the url  
                WebRequest req = WebRequest.Create(Url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                responseUrl = response.ResponseUri.ToString();

                //Download in chuncks  
                byte[] buffer = new byte[1024];

                //Get Total Size  
                int dataLength = (int)response.ContentLength;

                //Download to memory  
                //Note: adjust the streams here to download directly to the hard drive  
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    //Try to read the data  
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        //Write the downloaded data  
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }

                //Convert the downloaded stream to a byte array  
                downloadedData = memStream.ToArray();

                //Clean up  
                stream.Close();
                memStream.Close();
            }
            catch (Exception)
            {
                responseUrl = string.Empty;
                return new byte[0];
            }

            return downloadedData;
        }
        public override void LoadingMethod()
        {
            LoadingSemaphore.Release(1);
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            
            if (!ValidateChildren() || tbURL.Text.Trim().Length == 0)
                return;
            MessageBoxForm mbf = new MessageBoxForm("Дали навистина сакате да го промените логото на ресторанот?");
            if (mbf.ShowDialog() == DialogResult.Yes)
            {
                CurrRestoran.LogoUrl = tbURL.Text;
                Thread oThread = new Thread(new ThreadStart(PostaviNovoLogo));
                oThread.Start();
                LoadingMethod();
            }
        }

        public void PostaviNovoLogo()
        {
             string sqlRestoran = @"UPDATE RESTORAN
                                    SET SLIKA = :Slika
                                    WHERE RESTORAN_ID = :REST_ID";
            OracleCommand cmd = new OracleCommand(sqlRestoran, this.Conn);

            OracleParameter prm = new OracleParameter("Slika", OracleDbType.Varchar2);

            prm.Value = CurrRestoran.LogoUrl;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
                
            prm = new OracleParameter("REST_ID", OracleDbType.Int32);

            prm.Value = CurrRestoran.RestoranID;               
            cmd.Parameters.Add(prm);                
            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.ExecuteNonQuery();
                LoadLogo();
            }
            catch (Exception)
            {
            }
        }

        private void tbURL_Validating(object sender, CancelEventArgs e)
        {
            if (tbURL.Text.Trim().Length == 0)
                return;
            Uri uriResult;
            bool result = Uri.TryCreate(tbURL.Text, UriKind.Absolute, out uriResult);
            if (!result)
            {
                errorProvider1.SetError(tbURL, "Ова поле мора да биде пополнето со URL - линк до сликата која што сакате да ја поставите");
                e.Cancel = true;
            }
            else errorProvider1.SetError(tbURL, "");
        }

        private void btnPregledRegioni_Click(object sender, EventArgs e)
        {
            PregledPoRegioni ppr = new PregledPoRegioni(Conn, CurrRestoran);
            ppr.Show();
        }

        private void btnPregledProdazba_Click(object sender, EventArgs e)
        {
            PregledNaProdazba pnp = new PregledNaProdazba(Conn, CurrRestoran);
            pnp.Show();
        }

    }
}
