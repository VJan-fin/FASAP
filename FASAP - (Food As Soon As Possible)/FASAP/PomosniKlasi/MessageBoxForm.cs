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
    public partial class MessageBoxForm : Form
    {
        public MessageBoxForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Opacity = 0;
            timer1.Start();
        }

        public MessageBoxForm(string txt, bool cancel = true)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.lblContents.Text = txt;
            if (!cancel)
            {
                this.buttonFASAPPotvrdi.Size = new Size(this.buttonFASAPPotvrdi.Width * 2, this.buttonFASAPPotvrdi.Height);
                this.buttonFASAPOtkazi.Visible = false;
            }
            this.Opacity = 0;
            timer1.Start();
        }

        private void buttonFASAPPotvrdi_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonFASAPPotvrdi_MouseEnter(object sender, EventArgs e)
        {
            this.buttonFASAPPotvrdi.Image = Resources.LightButton___Copy;
            this.buttonFASAPPotvrdi.ForeColor = Color.Sienna;
        }

        private void buttonFASAPPotvrdi_MouseLeave(object sender, EventArgs e)
        {
            this.buttonFASAPPotvrdi.Image = Resources.DarkButton___Copy;
            this.buttonFASAPPotvrdi.ForeColor = Color.Khaki;
        }

        private void buttonFASAP2_MouseEnter(object sender, EventArgs e)
        {
            this.buttonFASAPOtkazi.Image = Resources.LightButton___Copy;
            this.buttonFASAPOtkazi.ForeColor = Color.Sienna;
        }

        private void buttonFASAP2_MouseLeave(object sender, EventArgs e)
        {
            this.buttonFASAPOtkazi.Image = Resources.DarkButton___Copy;
            this.buttonFASAPOtkazi.ForeColor = Color.Khaki;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1)
                Opacity += 0.1;
            else 
                timer1.Stop();
        }
    }
}
