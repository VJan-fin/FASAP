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
    public partial class PregledPoRegioni : BackgroundForm
    {
        public Restoran Restoran { get; set; }
        public OracleConnection Conn { get; set; }

        public PregledPoRegioni(OracleConnection conn, Restoran restoran)
        {
            InitializeComponent();
            Opacity = 0;
            Restoran = restoran;
            Conn = conn;
        }
    }
}
