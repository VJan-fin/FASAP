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
using SmetkaZaNaracka.SearchFilterForm;

namespace SmetkaZaNaracka
{
    public partial class SearchFilter : BackgroundForm
    {
        public SortingState NoneSort { get; set; }
        public SortingState NameAscendingSort { get; set; }
        public SortingState NameDescendingSort { get; set; }
        public SortingState RejtingAscendingSort { get; set; }
        public SortingState RejtingDescendingSort { get; set; }

        public SortingState SortingState { get; set; }

        public Restoran Restoran { get; set; }
        public int CurrRestorani { get; set; }
        public int CurrKontakt { get; set; }
        public List<Restoran> Restorani { get; set; }
        public OracleConnection Conn { get; set; }
        List<PictureBox> lista;
        List<LabelFASAP> labeli;
        public SearchFilter(List<Restoran> restorani, OracleConnection conn)
        {
            InitializeComponent();
            DoubleBuffered = true;
            labeli = new List<LabelFASAP>();
            Restorani = restorani;
            Conn = conn;
            Opacity = 0;
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
            NoneSort = new NoneSort(this);
            NameAscendingSort = new NameAscendingSort(this);
            NameDescendingSort = new NameDescendingSort(this);
            RejtingAscendingSort = new RejtingAscendingSort(this);
            RejtingDescendingSort = new RejtingDescendingSort(this);

            SortingState = NoneSort;
            
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

            PostaviRestorani();
            
            postaviRejting(5);
        }

        public void PostaviRestorani()
        {
            for (int i = 0; i < Restorani.Count && i < labeli.Count; i++)
            {
                labeli[i].UpdateObject(Restorani[i + CurrRestorani]);
                if (Restoran != null && Restorani[i + CurrRestorani].Equals(Restoran))
                {
                    labeli[i].Image = Resources.LabelBackgroundSelected;
                    labeli[i].ForeColor = Color.SaddleBrown;
                }
                else
                {
                    labeli[i].Image = Resources.LabelBackground2;
                    labeli[i].ForeColor = Color.Gold;
                }
            }
        }

        private void lbl1_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                lb.Font = new Font("Trebuchet MS", 20, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lbl1_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                lb.Font = new Font("Trebuchet MS", 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                textBox1.Visible = false;
                this.Focus();
                textBox1.Visible = true;
                Restoran = lb.LblObject as Restoran;
                CurrKontakt = 0;
                foreach (var obj in labeli)
                {
                    obj.Image = Resources.LabelBackground2;
                    obj.ForeColor = Color.Gold;
                }
                lb.Image = Resources.LabelBackgroundSelected;
                lb.ForeColor = Color.SaddleBrown;
                PostaviLabeli();
            }
        }

        private void PostaviLabeli()
        {
            if (Restoran != null)
            {
                lblUlica.UpdateObject(Restoran.Ulica);
                lblGrad.UpdateObject(Restoran.Grad);
                lblKategorija.UpdateObject(Restoran.Kategorija);
                lblSlobodniMasi.UpdateObject(Restoran.SlobodniMasi);
                lblRabotnoVreme.UpdateObject(Restoran.RabotnoVreme);
                postaviRejting(Restoran.Rejting);
                CurrKontakt = 0;
                PostaviKontakt();
            }
        }

        private void lbl1_DoubleClick(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                IzvrsuvanjeNaracka naracka = new IzvrsuvanjeNaracka(Restoran, Conn);
                naracka.Show();
            }
            
        }

        private void SearchFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Restoran != null)
            {
                IzvrsuvanjeNaracka naracka = new IzvrsuvanjeNaracka(Restoran, Conn);
                naracka.Show();
            }
            
        }

        private void PostaviKontakt()
        {
            if (Restoran.Kontakt.Count != 0)
                lblKontakt.UpdateObject(Restoran.Kontakt[CurrKontakt]);
            else
            {
                lblKontakt.UpdateObject(null);
                lblKontakt.Text = ":                    : ";
            }
        }

        private void pbContactLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (Restoran != null && Restoran.Kontakt.Count != 0)
            {
                CurrKontakt++;
                CurrKontakt = CurrKontakt % Restoran.Kontakt.Count;
                PostaviKontakt();
            }
        }

        private void pbContactRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (Restoran != null && Restoran.Kontakt.Count != 0)
            {
                if (CurrKontakt == 0)
                    CurrKontakt = Restoran.Kontakt.Count - 1;
                else CurrKontakt--;
                PostaviKontakt();
            }
        }

        private void labelFASAP19_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            Cursor = Cursors.Hand;
            lb.Image = Resources.LightButton___Copy;
            lb.ForeColor = Color.SaddleBrown;
        }

        private void labelFASAP19_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            Cursor = Cursors.Default;
            lb.Image = Resources.DarkButton___Copy;
            lb.ForeColor = Color.Khaki;
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
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

        private void labelFASAP19_Click(object sender, EventArgs e)
        {
            SortingState.Rejting();
        }

        private void labelFASAP18_Click(object sender, EventArgs e)
        {
            SortingState.Name();
        }

        private void pbListUp_MouseDown(object sender, MouseEventArgs e)
        {
            int pom = Restorani.Count - 12;
            if (CurrRestorani < pom)
            {
                CurrRestorani++;
                PostaviRestorani();
            }
        }

        private void pbListDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrRestorani > 0)
            {
                CurrRestorani--;
                PostaviRestorani();
            }
            
        }
    }
}