using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka
{
    public partial class Loading : BackgroundForm
    {
        public Loading()
        {
            InitializeComponent();
            Opacity = 0;
            DoubleBuffered = true;
            timer1.Start();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FasapPoceten fp = new FasapPoceten();
            timer1.Stop();
            fp.ShowDialog();
            Close();
        }
    }
}
