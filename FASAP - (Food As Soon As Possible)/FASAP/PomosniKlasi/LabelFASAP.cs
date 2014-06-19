using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka
{
    public partial class LabelFASAP : Label
    {
        public Object LblObject { get; set; }

        public LabelFASAP()
        {
            InitializeComponent();
            this.Init();
        }

        

        public LabelFASAP(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.Init();
        }

        public void Init()
        {
            this.BackColor = Color.Transparent;
            this.Text = "";
            this.Image = Resources.LabelBackground2;
            this.Font = new Font("Trebuchet MS", 16, FontStyle.Bold);
            this.ForeColor = Color.White;
        }

        

        /// <summary>
        /// Updates the value of the object instance
        /// and sets the text of the label
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateObject(Object obj)
        {
            this.LblObject = obj;
            if (this.LblObject != null)
                Text = String.Format(":{0}: ",this.LblObject.ToString());
            else
                this.Text = ": : ";
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

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(ForeColor), rectangle, format);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
