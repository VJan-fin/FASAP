using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Text.RegularExpressions;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka
{
    public partial class DodavanjeRestoran : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private int RestoranID { get; set; }


        public DodavanjeRestoran(OracleConnection conn)
        {
            InitializeComponent();
            Conn = conn;
            this.DoubleBuffered = true;
            Opacity = 0;
            NajdiID();
        }
        private void NajdiID()
        {
            string sqlID = @"SELECT MAX(RESTORAN_ID) FROM RESTORAN";
            OracleCommand cmd = new OracleCommand(sqlID, this.Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    this.RestoranID = dr.GetInt32(0) + 1;
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
        private int dodadiVoImenik(){
            string insertImenik = @"Insert into IMENIK (RESTORAN_ID,TELEFON) values (:REST_ID,:TEL)";
            OracleCommand cmd = new OracleCommand(insertImenik, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("TEL", OracleDbType.Varchar2);
                if (tbTel.Text.Trim() != "")
                    prm.Value = this.tbTel.Text.Trim();
                else prm.Value = null;
                cmd.Parameters.Add(prm);
            }
            catch (Exception ex)
            {
               return  -1;
            }

            int br;

            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                br = -1;
            }

            return br;
        }
        private int dodadiGlavnoMeni()
        {
            string sql = @"INSERT INTO MENI (RESTORAN_ID,IME_MENI,VALIDNOST_MENI,IME_GLAVNO) VALUES (:REST_ID, :IME, :VAL, :GLAVNO)";
            OracleCommand cmd = new OracleCommand(sql, Conn);
            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("IME", OracleDbType.Varchar2);
                string ime ="Мени "+ tbIme.Text.Trim();
                prm.Value = ime;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("VAL", OracleDbType.Char);
                prm.Value = '1';
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("GLAVNO", OracleDbType.Varchar2);
                prm.Value = null;
                cmd.Parameters.Add(prm);

            }
            catch (Exception ex)
            {
                return -1;
            }

            int br;

            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                br = -1;
            }

            return br;
        }
        private int DodadiVoDBRestoran()
        {
            string insertRest = @"INSERT INTO RESTORAN (RESTORAN_ID, IME_RESTORAN, KATEGORIJA, ULICA, GRAD, RABOTNO_VREME, KAPACITET, BROJ_MASI, CENA_ZA_DOSTAVA,PRAG_DOSTAVA, DATUM_NA_OTVORANJE,SLIKA) VALUES (:REST_ID, :IME, :KAT, :UL, :GRAD, :RAB, :KAP, :MASI, :DOSTAVA,:PRAG, :DATUM,:S)";
            OracleCommand cmd = new OracleCommand(insertRest, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("IME", OracleDbType.Varchar2);
                prm.Value = this.tbIme.Text.Trim();
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("KAT", OracleDbType.Varchar2);
                prm.Value = this.tbKategorija.Text.Trim();
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("UL", OracleDbType.Varchar2);
                if (tbUlica.Text.Trim() != "") prm.Value = this.tbUlica.Text.Trim();
                else prm.Value = null;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("GRAD", OracleDbType.Varchar2);
                if (tbGrad.Text.Trim() != "")
                    prm.Value = this.tbGrad.Text.Trim();
                else prm.Value = null;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("RAB", OracleDbType.Varchar2);
                if (tbRabotnoVreme.Text.Trim() != "") prm.Value = this.tbRabotnoVreme.Text;
                else prm.Value = null;
                cmd.Parameters.Add(prm);


                prm = new OracleParameter("KAP", OracleDbType.Int64);
                if (tbkapacitet.Text.Trim() != "") prm.Value = int.Parse(this.tbkapacitet.Text.Trim());
                else prm.Value = null;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("MASI", OracleDbType.Int64);
                if (tbBrMasi.Text.Trim() != "")
                    prm.Value = int.Parse(this.tbBrMasi.Text.Trim());
                else prm.Value = null;
                cmd.Parameters.Add(prm);


                prm = new OracleParameter("DOSTAVA", OracleDbType.Int64);
                if (tbDostava.Text.Trim() != "")
                    prm.Value = int.Parse(this.tbDostava.Text.Trim());
                else
                    prm.Value = null;

                cmd.Parameters.Add(prm);
                //
                prm = new OracleParameter("PRAG", OracleDbType.Int64);
                if (tbPrag.Text.Trim() != "")
                    prm.Value = int.Parse(this.tbPrag.Text.Trim());
                else
                    prm.Value = null;

                cmd.Parameters.Add(prm);
                //
                prm = new OracleParameter("DATUM", OracleDbType.Date);
                if (tbDatum.Text.Trim() != "")
                {
                    char[] sepa = { '/' };
                    String[] dates = tbDatum.Text.Trim().Split(sepa);
                    DateTime dt = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]));
                    prm.Value = dt;
                }

                else
                    prm.Value = null;
                cmd.Parameters.Add(prm);

                //
                prm = new OracleParameter("S", OracleDbType.Varchar2);
                if (tbUrl.Text.Trim() != "")
                    prm.Value = this.tbUrl.Text.Trim();
                else prm.Value = null;
                cmd.Parameters.Add(prm);

            }
            catch (Exception ex)
            {
                return -1;
            }


            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                br = -1;
            }

            return br;

        }

        private int dodadiVoDBKorisnik()
        {

            string insertRest = @"INSERT INTO KORISNIK (KORISNICHKO_IME,RESTORAN_ID,LOZINKA,VRABOTEN_ID) VALUES (:KIME, :REST_ID, :LOZ, :VRAB_ID)";
            OracleCommand cmd = new OracleCommand(insertRest, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("KIME", OracleDbType.Varchar2);

                prm.Value = tbUser.Text.Trim();
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("LOZ", OracleDbType.Varchar2);
                prm.Value = tbPass.Text.Trim();
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);

                prm.Value = 0;
                cmd.Parameters.Add(prm);
            }
            catch (Exception ex)
            {
                return -1;
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

            return br;

        }

        private void tbUser_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (sender as TextBox);


            Regex regex = new Regex(@"[A-Z0-9._%+-]*@fasap.com$");

            if (tb.Text.Trim() == "")
            {
                MessageBoxForm mbf = new MessageBoxForm("Полето е задолжително!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            else if (!regex.IsMatch(tb.Text.Trim()))
            {
                MessageBoxForm mbf = new MessageBoxForm("Корисничкото име треба да е во формат: imeNaRestoran@fasap.com !", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }

            tb.SelectAll();
        }

        private void tbPass_Validating(object sender, CancelEventArgs e)
        {
           
                TextBox tb = (sender as TextBox);
                Regex regex = new Regex(@".*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z]).*$");
                Regex regex2 = new Regex(@".*[!@#$%^&*()-_=+,/?].*$");

                if (tb.Text.Trim() == "")
                {
                    MessageBoxForm mbf = new MessageBoxForm("Полето е задолжително!", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                }
                else if (!regex.IsMatch(tb.Text.Trim()) || !regex2.IsMatch(tb.Text.Trim()))
                {
                    MessageBoxForm mbf = new MessageBoxForm("Лозинката мора да содржи барем 1 буква, барем 1 цифра и барем 1 специјален знак и вкупно да има најмалку 8 знаци!", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                }

                tb.SelectAll();
            
        }

        private void tbkapacitet_Validating(object sender, CancelEventArgs e)
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

        private void tbBrMasi_Validating(object sender, CancelEventArgs e)
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

        private void tbDostava_Validating(object sender, CancelEventArgs e)
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

        private void btnOtkazi_Click(object sender, EventArgs e)
        {
            MessageBoxForm mbf = new MessageBoxForm("Промените ќе бидат отфрлени.\nДали сте сигурни дека сакате да ја напуштите формата?");
            if (mbf.ShowDialog() == DialogResult.Yes)
            {
                this.AutoValidate = AutoValidate.Disable;
                this.Close();
            }
        }

        private void btnDodadi_MouseEnter(object sender, EventArgs e)
        {
            this.btnDodadi.Image = Resources.LightButton___Copy;
            this.btnDodadi.ForeColor = Color.Sienna;
        }

        private void btnDodadi_MouseLeave(object sender, EventArgs e)
        {
            this.btnDodadi.Image = Resources.DarkButton___Copy;
            this.btnDodadi.ForeColor = Color.Khaki;
        }

        private void btnOtkazi_MouseEnter(object sender, EventArgs e)
        {
            this.btnOtkazi.Image = Resources.LightButton___Copy;
            this.btnOtkazi.ForeColor = Color.Sienna;
        }

        private void btnOtkazi_MouseLeave(object sender, EventArgs e)
        {
            this.btnOtkazi.Image = Resources.DarkButton___Copy;
            this.btnOtkazi.ForeColor = Color.Khaki;
        }

        private void tbDatum_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))");
            TextBox tb = (sender as TextBox);
          
            if (tb.Text.Trim() != "" && !reg.IsMatch(tb.Text.Trim()))
            {
                MessageBoxForm mbf = new MessageBoxForm("Датумот треба да е во формат dd/MM/yyyy\nПример: 01/02/2013", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }

            tb.SelectAll();
        }

        private void btnDodadi_Click(object sender, EventArgs e)
        {
            //proverka dali username e unique
            string sql = "Select KORISNICHKO_IME from KORISNIK";
            OracleCommand cmd = new OracleCommand(sql, Conn);
            bool uniqueUser = true;
            bool uniqueRest = true;

            try
            {

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader(); // C#
                dr = cmd.ExecuteReader();
              
                while (dr.Read())
                {
                    string usr = dr.GetString(0);
                    if (tbUser.Text.Trim() == usr)
                    {
                        uniqueUser = false;
                        break;
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

            // proverka dali imeto na restoranot e unique
             sql = "Select IME_RESTORAN from RESTORAN";
             cmd = new OracleCommand(sql, Conn);

            try
            {

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader(); // C#
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string ime = dr.GetString(0);
                    if (tbIme.Text.Trim() == ime)
                    {
                        uniqueRest = false;
                        break;
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

            if (!uniqueUser)
            {
                MessageBoxForm m = new MessageBoxForm("Корисничкото име веќе постои! Изберете друго.", false);
                m.ShowDialog();
                tbUser.SelectAll();
            }
            else if (!uniqueRest)
            {
                MessageBoxForm m = new MessageBoxForm("Името на ресторанот веќе постои! Изберете друго.", false);
                m.ShowDialog();
                tbIme.SelectAll();
            }
            else //ako e ok
            {

                MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да го додадете ресторанот во листата ресторани?");
                if (mbf.ShowDialog() == DialogResult.Yes)
                {

                    int br1 = this.DodadiVoDBRestoran();
                    int br2 = this.dodadiVoDBKorisnik();
                    int br3 = this.dodadiVoImenik();
                    int br4 = this.dodadiGlavnoMeni();

                    if (br1 == 1 && br2 == 1 && br3 == 1 && br4==1)
                    {
                        MessageBoxForm mbf1 = new MessageBoxForm("Ресторанот беше успешно додаден!", false);
                        mbf1.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBoxForm mbf1 = new MessageBoxForm("Грешка!\nРесторанот не може да се додаде!", false);
                        mbf1.ShowDialog();
                    }


                }
            }


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

        private void tbKategorija_Validating(object sender, CancelEventArgs e)
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
