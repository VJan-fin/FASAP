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
    public partial class InfoForma : Form
    {
        private OracleConnection Conn { get; set; }
        public InfoForma(OracleConnection conn)
        {
            InitializeComponent();
            Conn = conn;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login(Conn);
            login.Show();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register(Conn);
            register.Show();
        }
    }
}
