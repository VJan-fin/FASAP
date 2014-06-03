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
    public partial class Register : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        public Register(OracleConnection conn)
        {
            InitializeComponent();
        }
    }
}
