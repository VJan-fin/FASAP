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
    public partial class PregledInformacii : BackgroundForm
    {
        private bool ModifyMode { get; set; }
        private List<LabelFASAP> Nevidlivi { get; set; }
        private List<TextBox> Writeable { get; set; }
        private List<string> Telefoni { get; set; }
        private int RestoranID { get; set; }
        private int telInd { get; set; }
        private OracleConnection Conn { get; set; }

        public PregledInformacii(OracleConnection conn,int RestoranID)
        {
            InitializeComponent();
            Conn = conn;
            this.RestoranID=RestoranID;
            DoubleBuffered = true;
            Opacity = 0;
            init();
        }
        public PregledInformacii() //probno
        {
            InitializeComponent();
            string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
             + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
             + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();
            RestoranID = 3;
            init();
        }

        private void PregledInformacii_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("restoran>"+RestoranID);
            foreach (TextBox tb in Writeable)
            {
                tb.DeselectAll();
            }
        }
        private void init() {
            Nevidlivi = new List<LabelFASAP>();
            Nevidlivi.Add(lblZad1);
            Nevidlivi.Add(lblZad2);
            Nevidlivi.Add(lblZab);
            //Nevidlivi.Add(lblTelefon);

            Writeable = new List<TextBox>();
            Writeable.Add(tbIme);
            Writeable.Add(tbKategoorija);
            Writeable.Add(tbUlica);
            Writeable.Add(tbGrad);
            Writeable.Add(tbRab);
            Writeable.Add(tbKapacitet);
            Writeable.Add(tbBrojMasi);
            Writeable.Add(tbCenaDostava);
            Writeable.Add(tbPragDostava);
            Writeable.Add(tbDen);
            Writeable.Add(tbMesec);
            Writeable.Add(tbGodina);
            Writeable.Add(tbTelefon);

           
            this.ModifyMode = false;
            this.PostaviVidlivost();
            this.PostaviMenlivost();
            this.PopolniTelefoni();
            this.PopolniInfo();

            foreach (TextBox tb in Writeable)
            {
                tb.DeselectAll();
            }
        }
        private int dodadiTelefon()
        {
          
            string insertRest = @"INSERT INTO IMENIK (RESTORAN_ID, TELEFON) VALUES (:REST_ID, :TEL)";
            OracleCommand cmd = new OracleCommand(insertRest, Conn);
            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("TEL", OracleDbType.Varchar2);
                prm.Value = this.tbTelefon.Text.Trim();


                cmd.Parameters.Add(prm);
            }
            catch (Exception ex)
            {
                return -1;
            }
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
            return br1;
        }
        private int izbrisiTelefon()
        {
            string sql = @"DELETE FROM IMENIK WHERE RESTORAN_ID= :REST_ID AND TELEFON= :TEL";
            OracleCommand cmd = new OracleCommand(sql, Conn);
            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("TEL", OracleDbType.Varchar2);
                prm.Value = this.lblListaTel.Text.Trim();
                cmd.Parameters.Add(prm);
            }
            catch (Exception ex)
            {
                return -1;
            }
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
            return br1;
        
        }
        private void PopolniTelefoni()
        {
            this.telInd = 0;
            this.Telefoni = new List<string>();

            string sqlPozicii = @"SELECT TELEFON FROM IMENIK WHERE RESTORAN_ID= :REST_ID ";
            OracleCommand cmd = new OracleCommand(sqlPozicii, this.Conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        this.Telefoni.Add(dr.GetString(0));
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

            if (Telefoni.Count() != 0)
            {
                lblListaTel.Text = Telefoni[0];
            }
            else lblListaTel.Text = "";
           
        }
        private void PostaviVidlivost()
        {
            foreach (var item in this.Nevidlivi)
                item.Visible = this.ModifyMode;
            tbTelefon.Visible = this.ModifyMode;
            btnIzbrisiTel.Visible = this.ModifyMode;
            btnDodadiTel.Visible = this.ModifyMode;
        }
        private void PostaviMenlivost()
        {
            foreach (var item in this.Writeable)
                item.ReadOnly = !this.ModifyMode;
        }
        private void PopolniInfo()
        {

            string sqlVrab = @"SELECT * FROM RESTORAN WHERE RESTORAN_ID = :REST_ID";
            OracleCommand cmd = new OracleCommand(sqlVrab, this.Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tbIme.Text = dr.GetString(2);
                    if (!dr.IsDBNull(3))
                    {
                        tbUlica.Text = dr.GetString(3);
                    }
                    else tbUlica.Text = "";
                    if (!dr.IsDBNull(4))
                    {
                        tbGrad.Text = dr.GetString(4);
                    }
                    else tbGrad.Text = "";

                    if (!dr.IsDBNull(6))
                    {
                        tbRab.Text = dr.GetString(6);
                    }
                    else tbRab.Text = "";
                    if (!dr.IsDBNull(7))
                    {
                        tbKapacitet.Text = string.Format("{0}", dr.GetInt16(7));
                    }
                    else tbKapacitet.Text = "";
                    if (!dr.IsDBNull(8))
                    {
                        tbBrojMasi.Text = string.Format("{0}", dr.GetInt16(8));
                    }
                    else tbBrojMasi.Text = "";
                    if (!dr.IsDBNull(9))
                    {
                        tbCenaDostava.Text = string.Format("{0}", dr.GetInt16(9));
                    }
                    else tbCenaDostava.Text = "";
                    if (!dr.IsDBNull(10))
                    {
                        tbPragDostava.Text = string.Format("{0}", dr.GetInt16(10));
                    }
                    else tbPragDostava.Text = null;


                    if (!dr.IsDBNull(11))
                    {
                        DateTime rag = dr.GetDateTime(11);
                        tbDen.Text = rag.Day.ToString();
                        tbMesec.Text = rag.Month.ToString();
                        tbGodina.Text = rag.Year.ToString();
                    }
                    else
                    {
                        tbDen.Text = "";
                        tbMesec.Text = "";
                        tbGodina.Text = "";
                    }

                    tbKategoorija.Text = dr.GetString(12);
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
        private bool azurirajInfo()
        {
            // Azuriranje na podatocite za restoranot
            string sqlRest = @"UPDATE RESTORAN SET IME_RESTORAN = :IME, ULICA = :UL, GRAD = :GR, RABOTNO_VREME = :RB, KAPACITET = :KAP, BROJ_MASI = :BR_M, CENA_ZA_DOSTAVA = :CENA, PRAG_DOSTAVA = :PRAG, DATUM_NA_OTVORANJE = :DAT, KATEGORIJA = :KAT WHERE RESTORAN_ID = :REST_ID";

            OracleCommand cmd = new OracleCommand(sqlRest, this.Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleParameter prm = new OracleParameter("IME", OracleDbType.Varchar2);
                prm.Value = this.tbIme.Text.Trim();
                cmd.Parameters.Add(prm);
                
                prm = new OracleParameter("UL", OracleDbType.Varchar2);
                if (tbUlica.Text.Trim() != "") prm.Value = this.tbUlica.Text.Trim();
                else prm.Value = null;
                cmd.Parameters.Add(prm);
               
                prm = new OracleParameter("GR", OracleDbType.Varchar2);
                if (tbGrad.Text.Trim() != "")
                    prm.Value = this.tbGrad.Text.Trim();
                else prm.Value = null;
                cmd.Parameters.Add(prm);
              
                prm = new OracleParameter("RB", OracleDbType.Varchar2);
                if (tbRab.Text.Trim() != "") prm.Value = this.tbRab.Text;
                else prm.Value = null;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("KAP", OracleDbType.Int32);
                if (tbKapacitet.Text.Trim() != "") prm.Value = int.Parse(this.tbKapacitet.Text.Trim());
                else prm.Value = null;
                cmd.Parameters.Add(prm);
                //MessageBox.Show(""+prm.Value);

                prm = new OracleParameter("BR_M", OracleDbType.Int32);
                if (tbBrojMasi.Text.Trim() != "")
                    prm.Value = int.Parse(this.tbBrojMasi.Text.Trim());
                else prm.Value = null;
                cmd.Parameters.Add(prm);
      
                prm = new OracleParameter("CENA", OracleDbType.Int32);
                if (tbCenaDostava.Text.Trim() != "")
                    prm.Value = int.Parse(this.tbCenaDostava.Text.Trim());
                else
                    prm.Value = null;
                cmd.Parameters.Add(prm);
            
                prm = new OracleParameter("PRAG", OracleDbType.Int32);
                if (tbPragDostava.Text.Trim() != "")
                    prm.Value = int.Parse(this.tbPragDostava.Text.Trim());
                else
                    prm.Value = null;
                cmd.Parameters.Add(prm);
             
                prm = new OracleParameter("DAT", OracleDbType.Date);
                if (tbDen.Text.Trim() != "" && tbMesec.Text.Trim()!="" && tbGodina.Text.Trim()!="")
                //prm.Value = tbDen.Text.Trim() + "." + tbMesec.Text.Trim() + "." + tbGodina.Text.Trim();
                {
                    
                    DateTime dt = new DateTime(int.Parse(this.tbGodina.Text.Trim()),int.Parse(this.tbMesec.Text.Trim()),int.Parse(this.tbDen.Text.Trim()));
                    prm.Value = dt;
                }
                else
                    prm.Value = null;
                cmd.Parameters.Add(prm);
              
                prm = new OracleParameter("KAT", OracleDbType.Varchar2);
                prm.Value = this.tbKategoorija.Text.Trim();
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID", OracleDbType.Int32);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);
             
                //cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            int br1;
            try
            {
                br1 = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                 br1=-1;
            }

            if (br1 == -1) return false;
            return true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (Telefoni.Count() > 1)
            {
                this.telInd++;
                telInd = telInd % Telefoni.Count();
                lblListaTel.Text = Telefoni[telInd];
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (Telefoni.Count() > 1)
            {
                this.telInd--;
                telInd = telInd + Telefoni.Count();
                telInd = telInd % Telefoni.Count();
                lblListaTel.Text = Telefoni[telInd];
            }
        }



        private void btnIzbrisiTel_MouseEnter(object sender, EventArgs e)
        {
            this.btnIzbrisiTel.Image = Resources.LightButton___Copy;
            this.btnIzbrisiTel.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnIzbrisiTel_MouseLeave(object sender, EventArgs e)
        {
            this.btnIzbrisiTel.Image = Resources.DarkButton___Copy;
            this.btnIzbrisiTel.ForeColor = Color.Khaki;
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

        private void buttonFASAP2_MouseEnter(object sender, EventArgs e)
        {
            this.buttonFASAP2.Image = Resources.LightButton___Copy;
            this.buttonFASAP2.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void buttonFASAP2_MouseLeave(object sender, EventArgs e)
        {
            this.buttonFASAP2.Image = Resources.DarkButton___Copy;
            this.buttonFASAP2.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }
      

        private void pictureBox3_MouseEnter_1(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowLeft;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowRight;
        }
        private bool CheckDates()
        {
            bool d1 = false;
            if (tbDen.Text.Length != 0 && tbMesec.Text.Length != 0 && tbGodina.Text.Length != 0)
                d1 = true;
            if (tbDen.Text.Length == 0 && tbMesec.Text.Length == 0 && tbGodina.Text.Length == 0)
                d1 = true;

            if (!d1)
            {
                tbDen.SelectAll();
                tbMesec.SelectAll();
                tbGodina.SelectAll();
            }

            bool d2 = false;
            if (tbDen.Text.Length != 0 && tbMesec.Text.Length != 0 && tbGodina.Text.Length != 0)
                d2 = true;
            if (tbDen.Text.Length == 0 && tbMesec.Text.Length == 0 && tbGodina.Text.Length == 0)
                d2 = true;

            if (!d2)
            {
                tbDen.SelectAll();
                tbMesec.SelectAll();
                tbGodina.SelectAll();
            }

            return (d1 && d2);
        }

       
        private void btnIzbrisiTel_Click(object sender, EventArgs e)
        {
            if (lblListaTel.Text.Trim() != "")
            {
                MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да го избришете телефонскиот број?");
                if (mbf.ShowDialog() == DialogResult.Yes)
                {
                   int b= izbrisiTelefon();
                   if (b ==-1)
                   {
                       MessageBoxForm m = new MessageBoxForm("Телефонот не можеше да се избрише. Обидете се повторно!",false);
                       m.ShowDialog();
                   }
                   else
                   {
                       MessageBoxForm m = new MessageBoxForm("Телефонот е успешно избришан!",false);
                       m.ShowDialog();
                       PopolniTelefoni();
                   }
                }
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

        private void tbKategoorija_Validating(object sender, CancelEventArgs e)
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

        private void tbKapacitet_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
            {
                TextBox tb = (sender as TextBox);
                int res;
                if (tb.Text.Trim() != "" && !int.TryParse(tb.Text, out res))
                {
                    MessageBoxForm mbf = new MessageBoxForm("Пoлето мора да содржи само цифри!", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                }

                tb.SelectAll();
            }
        }

        private void tbBrojMasi_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
            {
                TextBox tb = (sender as TextBox);
                int res;
                if (tb.Text.Trim() != "" && !int.TryParse(tb.Text, out res))
                {
                    MessageBoxForm mbf = new MessageBoxForm("Пoлето мора да содржи само цифри!", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                }

                tb.SelectAll();
            }
        }

        private void tbCenaDostava_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
            {
                TextBox tb = (sender as TextBox);
                int res;
                if (tb.Text.Trim() != "" && !int.TryParse(tb.Text, out res))
                {
                    MessageBoxForm mbf = new MessageBoxForm("Пoлето мора да содржи само цифри!", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                }

                tb.SelectAll();
            }
        }

        private void tbPragDostava_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
            {
                TextBox tb = (sender as TextBox);
                int res;
                if (tb.Text.Trim() != "" && !int.TryParse(tb.Text, out res))
                {
                    MessageBoxForm mbf = new MessageBoxForm("Пoлето мора да содржи само цифри!", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                }

                tb.SelectAll();
            }
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            if (!this.ModifyMode)
            {
                buttonFASAP1.Text = "Зачувај";
                this.ModifyMode = true;
            }
            else
            {
                if (this.CheckDates())
                {
                    MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да ги зачувате промените?");
                    if (mbf.ShowDialog() == DialogResult.Yes)
                    {
                        if (this.azurirajInfo())
                        {
                            MessageBoxForm mbf1 = new MessageBoxForm("Податоците беа успешно променети!", false);
                            mbf1.ShowDialog();
                            buttonFASAP1.Text = "Измени";
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

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            if (this.ModifyMode)
            {
                MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да ги отфрлите промените?");
                if (mbf.ShowDialog() == DialogResult.Yes)
                {
                    //this.AutoValidate = AutoValidate.Disable;
                    this.ModifyMode = false;
                    this.buttonFASAP2.Text = "Измени";

                    this.PostaviVidlivost();
                    this.PostaviMenlivost();
                    this.PopolniInfo();
                }
            }
            else
                this.Close();
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

        private void tbMesec_Validating(object sender, CancelEventArgs e)
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

        private void btnDodadiTel_MouseEnter(object sender, EventArgs e)
        {
            this.btnDodadiTel.Image = Resources.LightButton___Copy;
            this.btnDodadiTel.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnDodadiTel_MouseLeave(object sender, EventArgs e)
        {
            this.btnDodadiTel.Image = Resources.DarkButton___Copy;
            this.btnDodadiTel.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnDodadiTel_Click(object sender, EventArgs e)
        {
            if (tbTelefon.Text == "")
            {
                MessageBoxForm m = new MessageBoxForm("Немате внесено телефон!", false);
                m.ShowDialog();
                tbTelefon.SelectAll();
            }
            else
            {
                int br=dodadiTelefon();
                if (br == -1)
                {
                    MessageBoxForm m = new MessageBoxForm("Неможеше да се додаде телефонот. Обидете се повторно!", false);
                    m.ShowDialog();
                }
                else
                {
                    MessageBoxForm m = new MessageBoxForm("Телефонот е успешно додаден!", false);
                    m.ShowDialog();
                    PopolniTelefoni();
                    tbTelefon.Clear();
                }
            }
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

        private void LoseCursor(object sender)
        {
            (sender as TextBox).Enabled = false;
            (sender as TextBox).Enabled = true;
        }

        

       
        
       

    }
}
