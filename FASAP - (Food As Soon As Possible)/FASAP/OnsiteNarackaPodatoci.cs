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

namespace SmetkaZaNaracka
{
    public partial class OnsiteNarackaPodatoci : BackgroundForm
    {
        private Naracka Naracka { get; set; }
        private Restoran Restoran { get; set; }
        private OracleConnection Conn { get; set; }
        private int brMasa;
        private int BrMasa
        {
            get
            {
                return brMasa;
            }
            set
            {
                brMasa = value;
                lblBrojMasa.Text = brMasa.ToString();
                if (brMasa == 1)
                    pbDown.Visible = false;
                else pbDown.Visible = true;
                if (brMasa == Restoran.BrojMasi)
                    pbUp.Visible = false;
                else pbUp.Visible = true;
            }
        }

        public OnsiteNarackaPodatoci(OracleConnection conn, Naracka nar, Restoran res)
        {
            InitializeComponent();
            Naracka = nar;
            Restoran = res;
            Conn = conn;
            BrMasa = 1;
            Opacity = 0;
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            Onsite online = new Onsite(Naracka.NarackaID, Naracka.VkupnaCena, DateTime.Now, BrMasa);
            online.Stavki = Naracka.Stavki;
            online.SqlInsert(Conn, Restoran.RestoranID);
            DialogResult = DialogResult.Yes;
        }

        private void pbUp_Click(object sender, EventArgs e)
        {
            BrMasa++;
        }

        private void pbDown_Click(object sender, EventArgs e)
        {
            BrMasa--;
        }

        private void pbUp_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            pbUp.Image = Resources.LightArrowUp;
        }

        private void pbUp_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            pbUp.Image = Resources.DarkArrowUp;
        }

        private void pbDown_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            pbDown.Image = Resources.LightArrowDown;
        }

        private void pbDown_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            pbDown.Image = Resources.DarkArrowDown;
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
    }
}
