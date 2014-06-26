using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka.Izvestai
{
    public partial class PregledNaProdazba : BackgroundForm
    {
        public Restoran Restoran { get; set; }
        public OracleConnection Conn { get; set; }

        public PregledNaProdazba(OracleConnection conn, Restoran restoran)
        {
            InitializeComponent();
            Opacity = 0;
            Restoran = restoran;
            Conn = conn;
        }
    }
}
