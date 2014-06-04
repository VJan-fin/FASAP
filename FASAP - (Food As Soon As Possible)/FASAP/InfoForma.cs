using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka
{
    public partial class InfoForma : BackgroundForm
    {
        private OracleConnection Conn { get; set; }

        List<LabelFASAP> labeli;
        public InfoForma(OracleConnection conn)
        {
            InitializeComponent();
            Conn = conn;
            labeli = new List<LabelFASAP>();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

      

        private void InfoForma_Load(object sender, EventArgs e)
        {
            //labeli.Add(this.label2);
        }

       

        private void lbRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register(Conn);
            register.Show();
        }

        private void lbLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login(Conn);
            login.Show();
        }
    }
}
