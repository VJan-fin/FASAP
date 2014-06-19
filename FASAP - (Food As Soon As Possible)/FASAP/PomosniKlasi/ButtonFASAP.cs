using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka
{
    public partial class ButtonFASAP : Label
    {
        public ButtonFASAP()
        {
            InitializeComponent();
            Init();
        }

        public ButtonFASAP(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.BackColor = Color.Transparent;
            this.Image = Resources.DarkButton___Copy;
            this.AutoSize = false;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.ForeColor = Color.Khaki;
            this.Font = new Font("Trebuchet MS", 18, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw image, sized to control
            RectangleF rectangle = new RectangleF(0, 0, this.Width, this.Height);
            if (this.Image != null)
            {
                e.Graphics.DrawImage(this.Image, rectangle);
            }

            // Draw text, centered vertically and horizontally
            rectangle = new RectangleF(0, 0, this.Width, this.Height);

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(ForeColor), rectangle, format);
        }

    }
}
