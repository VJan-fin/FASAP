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
    public partial class Register : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private int RestoranID { get; set; }

        
        public Register(OracleConnection conn)
        {
            InitializeComponent();
            Conn = conn;
            NajdiID();
        }
        private void NajdiID()
        {
            string sqlID = @"SELECT MAX(RESTORAN_ID) FROM RESTORAN";
            OracleCommand cmd = new OracleCommand(sqlID, this.Conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                this.RestoranID = dr.GetInt32(0) + 1;
            //MessageBox.Show(string.Format("{0}",RestoranID));
        }

        private void DodadiVoDBRestoran(){
            string insertRest = @"INSERT INTO RESTORAN (RESTORAN_ID, IME_RESTORAN, KATEGORIJA, ULICA, GRAD, RABOTNO_VREME, KAPACITET, BROJ_MASI, CENA_ZA_DOSTAVA, DATUM_NA_OTVORANJE) VALUES (:REST_ID, :IME, :KAT, :UL, :GRAD, :RAB, :KAP, :MASI, :DOSTAVA, :DATUM)";
            OracleCommand cmd = new OracleCommand(insertRest, Conn);

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
            if(tbUlica.Text.Trim()!="") prm.Value = this.tbUlica.Text.Trim();
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
            prm = new OracleParameter("DATUM", OracleDbType.Date);
            if (tbDatum.Text.Trim() != "")
            {
                char[] sepa={'/'};
                String[] dates = tbDatum.Text.Trim().Split(sepa);
                DateTime dt=new DateTime(int.Parse(dates[2]),int.Parse(dates[1]),int.Parse(dates[0]));
                 prm.Value = dt;
            }
               
            else
                prm.Value = null;
            cmd.Parameters.Add(prm);
            MessageBox.Show(tbDatum.Text);
            cmd.ExecuteNonQuery();
          
           /*
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
           */
        }

        private void dodadiVoDBKorisnik()
        {
            string insertRest = @"INSERT INTO KORISNIK  VALUES (:KIME, :REST_ID, :LOZ, :VRAB_ID)";
            OracleCommand cmd = new OracleCommand(insertRest, Conn);

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

            cmd.ExecuteNonQuery();
           /* int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                br = -1;
            }

            return br;
            * */
        }
       
        private void tbUser_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            string sql = "Select KORISNICHKO_IME from KORISNIK";
            OracleCommand cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader(); // C#
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string usr=dr.GetString(0);
                if (tb.Text.Trim() == usr)
                {
                    MessageBoxForm mbf = new MessageBoxForm("Корисничкото име веќе постои! Изберете друго.", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                    break;
                }
            }
                
            Regex regex=new Regex( @"[A-Z0-9._%+-]*@fasap.com$");
             
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
            string txt=tb.Text.Trim();

            if (txt == "")
            {
                MessageBoxForm mbf = new MessageBoxForm("Полето е задолжително!", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }
            else if (!regex.IsMatch(txt))
            {
                MessageBoxForm mbf = new MessageBoxForm("Лозинката мора да содржи барем 1 буква, барем 1 цифра и барем 1 специјален знак и вкупно да има најмалку 8 знаци  !", false);
                e.Cancel = true;
                mbf.ShowDialog();
            }

            tb.SelectAll();
        }

        private void tbkapacitet_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            int res;
            if (tb.Text.Trim()!="" && !int.TryParse(tb.Text, out res))
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
            int res;
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
            MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да го додадете ресторанот во листата ресторани?");
            if (mbf.ShowDialog() == DialogResult.Yes)
            {
                //int br1 = this.DodadiVoDBRestoran();
              //  int br2 = this.dodadiVoDBKorisnik();
                /*
                if (br1 == 1 && br2 == 1)
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
                 * */
                DodadiVoDBRestoran();
                dodadiVoDBKorisnik();
              
            }
        }

        

    

      
    }
}
