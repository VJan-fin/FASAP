using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmetkaZaNaracka.Properties;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka
{
    public partial class SearchFilter : BackgroundForm
    {
        public List<Restoran> Restorani { get; set; }
        public OracleConnection Conn { get; set; }
        List<PictureBox> lista;
        List<LabelFASAP> labeli;
        public SearchFilter(List<Restoran> restorani, OracleConnection conn)
        {
            InitializeComponent();
            labeli = new List<LabelFASAP>();
            Restorani = restorani;
            Conn = conn;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!textBox1.Focused && textBox1.Font.Italic)
            {
                textBox1.BackColor = Color.Sienna;
                textBox1.ForeColor = Color.Khaki;
                pictureBox3.BackColor = Color.Sienna;
            }
        }

        private void postaviRejting(double rejting)
        {
            int i = 0;
            foreach (PictureBox pb in lista)
            {
                i += 2;
                if (i <= rejting || System.Math.Abs((i - rejting)) <= 0.5)
                    pb.Image = Resources.Polna_zvezda;
                else if (System.Math.Abs((i - rejting)) <= 1.5)
                    pb.Image = Resources.Pola_zvezda;
                else pb.Image = Resources.Prazna_zvezda;

            }

        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox1.Font.Italic)
            {
                textBox1.Text = "";
                textBox1.BackColor = Color.Khaki;
                textBox1.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                textBox1.ForeColor = Color.Sienna;
                pictureBox3.BackColor = Color.Khaki;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "Пребарај FASAP - Ресторани";
                textBox1.BackColor = Color.Sienna;
                textBox1.ForeColor = Color.Khaki;
                pictureBox3.BackColor = Color.Sienna;
                textBox1.Font = new Font("Trebuchet MS", 12, FontStyle.Italic);
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            if (textBox1.Font.Italic)
            {
                textBox1.BackColor = Color.SaddleBrown;
                pictureBox3.BackColor = Color.SaddleBrown ;
            }
        }

        private void pbListUp_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowUp;
        }

        private void pbListUp_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowUp;
        }

        private void pbListDown_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowDown;
        }

        private void pbListDown_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowDown;
        }

        private void pbContactLeft_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pbContactLeft_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowLeft;
        }

        private void pbContactRight_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pbContactRight_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowRight;
        }

        private void SearchFilter1_Load(object sender, EventArgs e)
        {
            lista = new List<PictureBox>();
            lista.Add(pbZvezda1);
            lista.Add(pbZvezda2);
            lista.Add(pbZvezda3);
            lista.Add(pbZvezda4);
            lista.Add(pbZvezda5);

            labeli.Add(lbl1);
            labeli.Add(lbl2);
            labeli.Add(lbl3);
            labeli.Add(lbl4);
            labeli.Add(lbl5);
            labeli.Add(lbl6);
            labeli.Add(lbl7);
            labeli.Add(lbl8);
            labeli.Add(lbl9);
            labeli.Add(lbl10);
            labeli.Add(lbl11);
            labeli.Add(lbl12);

            for (int i = 0; i < Restorani.Count && i < labeli.Count; i++)
                labeli[i].UpdateObject(Restorani[i]);
            
            postaviRejting(0);
        }

        private void lbl1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            LabelFASAP lb = sender as LabelFASAP;
            lb.Font = new Font("Trebuchet MS", 20, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void lbl1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            LabelFASAP lb = sender as LabelFASAP;
            lb.Font = new Font("Trebuchet MS", 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }
    }
}