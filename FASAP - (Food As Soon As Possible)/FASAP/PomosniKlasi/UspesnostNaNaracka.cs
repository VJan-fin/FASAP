using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmetkaZaNaracka
{
    public partial class UspesnostNaNaracka : BackgroundForm
    {
        public UspesnostNaNaracka()
        {
            InitializeComponent();
            Opacity = 0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
