using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka
{
    class SearchButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(Resources.Resized_HTAHM, 5, 2);
        }
    }
}
