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
    }
}
