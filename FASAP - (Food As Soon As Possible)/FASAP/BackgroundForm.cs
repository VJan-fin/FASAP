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
    public partial class BackgroundForm : Form
    {
        public BackgroundForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.pictureBox1.Image = Resources.MinimizeDarkLeft;
            this.pictureBox2.Image = Resources.ExitDark;
            Opacity = 0;
            timer1.Start();
            
        }

        public void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Resources.MinimizeLightLeft;
        }

        public void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox2.Image = Resources.ExitLight;
        }

        public void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Resources.MinimizeDarkLeft;
        }

        public void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox2.Image = Resources.ExitDark;
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1)
                Opacity += 0.07;
            else
            {
                timer1.Stop();
                LoadingMethod();
            }
        }

        public virtual void LoadingMethod()
        {

        }
    }
}
