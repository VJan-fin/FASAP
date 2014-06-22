using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmetkaZaNaracka.Properties;
using SmetkaZaNaracka.Naracki;
using SmetkaZaNaracka.Narachki;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka
{
    public partial class OnlineNarackaPodatoci : BackgroundForm
    {
        private Naracka Naracka { get; set; }
        private Restoran Restoran { get; set; }
        private OracleConnection Conn { get; set; }
        public OnlineNarackaPodatoci(OracleConnection conn, Naracka nar, Restoran res)
        {
            InitializeComponent();
            Naracka = nar;
            Restoran = res;
            Conn = conn;
            lblPragZaDostava.Text = Restoran.PragZaDostava.ToString();
            if (nar.VkupnaCena < Restoran.PragZaDostava)
            {
                lblNaplataZaDostava.Text = ((int)Restoran.CenaZaDostava).ToString();
                lblVkupno.Text = ((int)Restoran.CenaZaDostava + Naracka.VkupnaCena).ToString(); ;
            }
            else
            {
                lblNaplataZaDostava.Text = "0";
                lblVkupno.Text = Naracka.VkupnaCena.ToString();
            }
            lblNaracano.Text = Naracka.VkupnaCena.ToString();
            Opacity = 0;
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            Online online = new Online(Naracka.NarackaID, Naracka.VkupnaCena, DateTime.Now, String.Format("{0} \"{1}\"", tbNaselenoMesto.Text, tbAdresa.Text), tbKontakt.Text, tbIme.Text, tbPrezime.Text);
            online.Stavki = Naracka.Stavki;
            if (online.VkupnaCena < Restoran.PragZaDostava)
            {
                online.VkupnaCena += (int)Restoran.CenaZaDostava;
                online.CenaZaDostava = (int)Restoran.CenaZaDostava;
            }
            else
            {
                online.CenaZaDostava = 0;
            }
            online.SqlInsert(Conn, Restoran.RestoranID);
            DialogResult = DialogResult.Yes;
        }

        private void buttonFASAP2_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            Label lb = sender as Label;
            lb.Image = Resources.LightButton___Copy;
            lb.ForeColor = Color.SaddleBrown;
        }

        private void buttonFASAP2_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            Label lb = sender as Label;
            lb.Image = Resources.DarkButton___Copy;
            lb.ForeColor = Color.Khaki;
        }

        private void tbIme_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                errorProvider1.SetError(tb, "Ова поле не смее да биде празно!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tb, "");
                e.Cancel = false;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                errorProvider1.SetError(tb, "Ова поле не смее да биде празно!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tb, "");
                e.Cancel = false;
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                errorProvider1.SetError(tb, "Ова поле не смее да биде празно!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tb, "");
                e.Cancel = false;
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                errorProvider1.SetError(tb, "Ова поле не смее да биде празно!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tb, "");
                e.Cancel = false;
            }
        }
    }
}
