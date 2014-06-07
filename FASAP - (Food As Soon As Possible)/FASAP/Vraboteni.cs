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
    public partial class Vraboteni : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private int RestoranID { get; set; }
        public Vraboteni()
        {
            InitializeComponent();
        }
        public Vraboteni(OracleConnection conn, int restoranID)
        {
            InitializeComponent();
            Conn = conn;
            RestoranID = restoranID;

        }

        private void btnDodadi_MouseEnter(object sender, EventArgs e)
        {
            this.btnDodadi.Image = Resources.LightButton___Copy;
            this.btnDodadi.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnDodadi_MouseLeave(object sender, EventArgs e)
        {
            this.btnDodadi.Image = Resources.DarkButton___Copy;
            this.btnDodadi.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnDodadi_Click(object sender, EventArgs e)
        {
            //samo za proba
            DodavanjeVraboten dv = new DodavanjeVraboten();
            dv.Show();
        }
    }
}
