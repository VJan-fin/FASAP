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
        private double Rejting { get; set; }
        private bool isVoted { get; set; }
        private List<PictureBox> lista { get; set; }

        public OnlineNarackaPodatoci(OracleConnection conn, Naracka nar, Restoran res)
        {
            InitializeComponent();
            Naracka = nar;
            Restoran = res;
            Rejting = Restoran.Rejting;
            lista = new List<PictureBox>();
            lista.Add(pbZvezda1);
            lista.Add(pbZvezda2);
            lista.Add(pbZvezda3);
            lista.Add(pbZvezda4);
            lista.Add(pbZvezda5);
            postaviRejting(Rejting);
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

        private void postaviRejting(double rejting)
        {
            int i = 0;
            foreach (PictureBox pb in lista)
            {
                i += 2;
                if (i <= rejting || System.Math.Abs((i - rejting)) <= 0.5)
                    pb.Image = Resources.Polna_zvezda;
                else if (System.Math.Abs((i - rejting)) <= 1.5)
                    pb.Image = Resources.Pola_zvezda;
                else pb.Image = Resources.Prazna_zvezda;

            }

        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;
            if (isVoted)
            {
                string updateOnsite = @"UPDATE RESTORAN
                                        SET REJTING = ((REJTING * PRIMEROK) + :Rejting) / (PRIMEROK + 1), PRIMEROK = PRIMEROK + 1
                                        WHERE RESTORAN_ID = :ResID";

                OracleCommand cmd = new OracleCommand(updateOnsite, Conn);

                OracleParameter prm = new OracleParameter("Rejting", OracleDbType.Double);
                prm.Value = Rejting;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("ResID", OracleDbType.Int64);
                prm.Value = Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                int br;
                try
                {
                    br = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }
            }
            Online online = new Online(Naracka.NarackaID, Naracka.VkupnaCena, DateTime.Now, String.Format("{0} \"{1}\"", tbNaselenoMesto.Text, tbAdresa.Text), tbKontakt.Text, tbIme.Text, tbPrezime.Text, 0);
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
            UspesnostNaNaracka up = new UspesnostNaNaracka();
            up.ShowDialog();
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

        private void pbZvezda1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            if (lista[0].Equals(sender))
                postaviRejting(2);
            else if (lista[1].Equals(sender))
                postaviRejting(4);
            else if (lista[2].Equals(sender))
                postaviRejting(6);
            else if (lista[3].Equals(sender))
                postaviRejting(8);
            else if (lista[4].Equals(sender))
                postaviRejting(10);
        }

        private void pbZvezda1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            postaviRejting(Rejting);
        }

        private void pbZvezda1_Click(object sender, EventArgs e)
        {
            if (lista[0].Equals(sender))
                Rejting = 2;
            else if (lista[1].Equals(sender))
                Rejting = 4;
            else if (lista[2].Equals(sender))
                Rejting = 6;
            else if (lista[3].Equals(sender))
                Rejting = 8;
            else if (lista[4].Equals(sender))
                Rejting = 10;
            isVoted = true;
        }
    }
}
