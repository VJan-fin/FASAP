﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmetkaZaNaracka.Properties;
using Oracle.DataAccess.Client;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Net;
using SmetkaZaNaracka.Narachki;

namespace SmetkaZaNaracka
{
    public partial class IzvrsuvanjeNaracka : BackgroundForm
    {
        private MenuComponent CurrMenu { get; set; }
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        public MenuComponent CurrItem { get; set; }
        public List<OrderComponent> OrderList { get; set; }
        private Naracka naracka;
        public Naracka Naracka
        {
            get
            {
                return naracka;
            }
            set
            {
                naracka = value;
                PostaviNaracka();
                if (naracka.Stavki.Count == 0)
                {
                    btnOtkazi.Visible = false;
                    btnPotvrdi.Visible = false;
                }
                else
                {
                    btnOtkazi.Visible = true;
                    btnPotvrdi.Visible = true;
                }
            }
        }
        private List<LabelFASAP> ListaMeni { get; set; }
        private List<LabelFASAP> ListaStavki { get; set; }
        private List<LabelFASAP> ListaKupeni { get; set; }
        private int indKontakt { get; set; }
        private int indMeni { get; set; }
        private int indStavka { get; set; }
        private int indKupeni { get; set; }
        public Semaphore LoadingSemaphore { get; set; }
        int errorMessageTime = 3;
        public OrderComponent CurrOrderComponent { get; set; }

        public IzvrsuvanjeNaracka(Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            LoadingSemaphore = new Semaphore(0, 100);
            Restoran = restoran;
            Conn = conn;
            OrderList = new List<OrderComponent>();
            Opacity = 0;

            // Вчитување на мени за ресторанот
            Thread oThread = new Thread(new ThreadStart(KreirajMeni));
            oThread.Start();

            // Вчитување на лого за ресторанот
            oThread = new Thread(new ThreadStart(LoadLogo));
            oThread.Start();

            this.AddButtons();

            Naracka = new Naracka(-1, 0, DateTime.Now);

            this.lblImeRestoran.Text = String.Format("{0} ", Restoran.Ime);
            if (this.Restoran.Ulica != null && this.Restoran.Grad != null)
                this.lblAdresa.Text = String.Format("{0}, {1} ", Restoran.Ulica, Restoran.Grad);
            if (this.Restoran.RabotnoVreme != null)
                this.lblRabVreme.Text = this.Restoran.RabotnoVreme + " ";
            if (Restoran.PicturePath == null)
                pictureBoxLogo.Image = Resources.FASAP_LOGO;
        }

        public IzvrsuvanjeNaracka()
        {
            InitializeComponent();
            this.AddButtons();
        }

        private void AddButtons()
        {
            this.ListaMeni = new List<LabelFASAP>();
            this.ListaStavki = new List<LabelFASAP>();
            this.ListaKupeni = new List<LabelFASAP>();

            this.ListaMeni.Add(this.lblMeni1);
            this.ListaMeni.Add(this.lblMeni2);
            this.ListaMeni.Add(this.lblMeni3);
            this.ListaMeni.Add(this.lblMeni4);
            this.ListaMeni.Add(this.lblMeni5);
            this.ListaMeni.Add(this.lblMeni6);

            this.ListaStavki.Add(this.lblStavka1);
            this.ListaStavki.Add(this.lblStavka2);
            this.ListaStavki.Add(this.lblStavka3);
            this.ListaStavki.Add(this.lblStavka4);
            this.ListaStavki.Add(this.lblStavka5);
            this.ListaStavki.Add(this.lblStavka6);

            this.ListaKupeni.Add(this.lblKupeno1);
            this.ListaKupeni.Add(this.lblKupeno2);
            this.ListaKupeni.Add(this.lblKupeno3);
            this.ListaKupeni.Add(this.lblKupeno4);
            this.ListaKupeni.Add(this.lblKupeno5);
            this.ListaKupeni.Add(this.lblKupeno6);
        }

        private void IzvrsuvanjeNaracka_Load(object sender, EventArgs e)
        {
        }

        private void LoadLogo()
        {
            // Доколку нема линк постави ја предодредената слика
            if (Restoran.LogoUrl == null)
                return;
            Image img;
            try
            {
                // Пробај да ја вчиташ сликата
                img = ImageFromURL(Restoran.LogoUrl);

                // Забелешка: Вчитување со посебен метод за да се земе Image објект. Можно е и директно во PictureBox
                // но така не е можно да се обезбеди паралелизам, а да не се примети кочење, бидејќи самите контроли
                // синхронизираат и не дозволуваат во исто време две нишки да прават измени.
            }
            catch (Exception)
            {
                // Доколку не може да се вчита постави предодредена слика
                img = Resources.FASAP_LOGO;
            }

            // Чекај додека не заврши почетното исцртување на формата (важно за да не се приметува сецкање при појавувањето на формата)
            LoadingSemaphore.WaitOne();

            // Постави ја сликата на pictureBoxLogo
            SetPbDefaultLogo(pictureBoxLogo, img);
        }

        /// <summary>
        /// Креирање на мени со помош на хеш мапи.
        /// Идеја: Да се овозможи еднократно читање од база и потоа линеарно креирање на дрвото на менија.
        /// 
        /// Ги поставува: Restoran.GlavnoMeni и CurrMenu (иста истанца)
        /// </summary>
        private void KreirajMeni()
        {
            Dictionary<string, Meni> Menus = new Dictionary<string, Meni>();
            Dictionary<StavkaKey, Stavka> Items = new Dictionary<StavkaKey, Stavka>();

            // Вчитување на менија
            string sql = @"SELECT * FROM MENI WHERE RESTORAN_ID = :RES_ID AND VALIDNOST_MENI = 1";
            OracleCommand cmd = new OracleCommand(sql, Conn);

            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int64);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            Meni meni;
            while (dr.Read())
            {
                meni = new Meni(dr.GetString(1));
                Object obj = dr.GetValue(3);
                if (obj == null)
                    meni.ImeGlavno = null;
                else meni.ImeGlavno = obj as String;
                String IsValid = (string)dr.GetValue(2);
                if (IsValid == "1")
                    meni.ValidnostMeni = true;
                else meni.ValidnostMeni = false;
                Menus.Add(meni.Ime, meni);
            }

            // Вчитување на ставки
            sql = @"SELECT * FROM STAVKA WHERE VALIDNOST_STAVKA LIKE '1' AND RESTORAN_ID = :RES_ID";
            cmd = new OracleCommand(sql, Conn);

            prm = new OracleParameter("RES_ID", OracleDbType.Int64);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();

            Dodatok dodatok;
            Stavka stavka;
            while (dr.Read())
            {
                String IsDecorator = (string)dr.GetValue(5);
                Object ImeGlavno = dr.GetValue(1);
                Object OpisStavka = dr.GetValue(3);
                if (IsDecorator == "1")
                {
                    dodatok = new Dodatok((int)dr.GetValue(2), dr.GetString(7), (decimal)dr.GetValue(4));
                    if (ImeGlavno == null)
                        dodatok.ImeGlavno = null;
                    else dodatok.ImeGlavno = ImeGlavno as String;
                    if (OpisStavka == null)
                        dodatok.Opis = null;
                    else dodatok.Opis = OpisStavka as String;
                    Items.Add(dodatok.GetStavkaKey(), dodatok);
                }
                else
                {

                    stavka = new Stavka((int)dr.GetValue(2), dr.GetString(7), (decimal)dr.GetValue(4));
                    if (ImeGlavno == null)
                        stavka.ImeGlavno = null;
                    else stavka.ImeGlavno = ImeGlavno as String;
                    if (OpisStavka == null)
                        stavka.Opis = null;
                    else stavka.Opis = OpisStavka as String;
                    Items.Add(stavka.GetStavkaKey(), stavka);
                }

            }

            foreach (var obj in Menus)
            {
                Meni menu;
                if (obj.Value.ImeGlavno != null && Menus.TryGetValue(obj.Value.ImeGlavno, out menu))
                {
                    menu.AddComp(obj.Value);
                    obj.Value.Parent = menu;
                }
                else
                {
                    Restoran.GlavnoMeni = obj.Value;
                }
            }

            foreach (var obj in Items)
            {
                Meni menu;
                if (Menus.TryGetValue(obj.Value.ImeGlavno, out menu))
                {
                    menu.AddComp(obj.Value);
                    obj.Value.Parent = menu;
                }
            }
            //lblOsnovnoMeni.UpdateObject(Restoran.GlavnoMeni);
            LoadingSemaphore.WaitOne();
            SetObject(lblOsnovnoMeni, Restoran.GlavnoMeni);
            CurrMenu = Restoran.GlavnoMeni;
            PopolniListaMenija();
        }

        private void lblOsnovnoMeni_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;

            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                lb.Image = Resources.LabelBackgroundSelected;
                lb.ForeColor = Color.SaddleBrown;
            }
        }

        private void lblOsnovnoMeni_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;

            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                lb.Image = Resources.LabelBackground2;
                lb.ForeColor = Color.Gold;
            }
        }

        private void lblOsnovnoMeni_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            indStavka = 0;

            if (lb.LblObject != null && lb.LblObject is Meni)
            {
                CurrMenu = lb.LblObject as Meni;
                PostaviPateka();
                PopolniListaStavki();
            }
        }

        private void PostaviPateka()
        {
            for (int i = flowLayoutPanelFasap1.Controls.Count - 1; i >= 1; i--)
                if (CurrMenu.Equals((flowLayoutPanelFasap1.Controls[i] as LabelFASAP).LblObject))
                    break;
                else RemoveFlowLayoutPanelControl(flowLayoutPanelFasap1, flowLayoutPanelFasap1.Controls[i]);
        }

        delegate void RemFlowLayoutpanelControl(FlowLayoutPanel fs, Control obj);

        private void RemoveFlowLayoutPanelControl(FlowLayoutPanel fs, Control obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                RemFlowLayoutpanelControl d = new RemFlowLayoutpanelControl(RemoveFlowLayoutPanelControl);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Controls.Remove(obj);
            }
        }

        private void PopolniListaMenija()
        {
            MenuComponent mc = this.Restoran.GlavnoMeni;
            if (mc == null)
                return;
            int ind = this.indMeni;
            for (int i = 0; i < this.ListaMeni.Count; i++)
                if (ind < mc.GetContent().Count)
                {
                    SetObject(ListaMeni[i], mc.GetContent()[ind]);
                    ind++;
                }
                else
                    SetObject(ListaMeni[i], null);
        }

        private void PopolniListaStavki()
        {
            if (CurrMenu == null)
                return;
            int ind = this.indStavka;
            for (int i = 0; i < this.ListaStavki.Count; i++)
                if (ind < this.CurrMenu.GetContent().Count)
                {
                    this.ListaStavki[i].UpdateObject(CurrMenu.GetContent()[ind]);
                    if (this.CurrMenu.GetContent()[ind] is Meni)
                    {
                        this.ListaStavki[i].ForeColor = Color.Gold;
                        ListaStavki[i].Font = new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    }
                    else
                    {
                        this.ListaStavki[i].ForeColor = Color.White;
                        ListaStavki[i].Font = new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    }
                    ind++;
                }
                else
                {
                    ListaStavki[i].UpdateObject(null);
                }
        }

        private void UpdatePhone()
        {
            if (this.Restoran.Kontakt.Count != 0)
                this.lblKontakt.Text = this.Restoran.Kontakt[this.indKontakt].ToString();
            else
                this.lblKontakt.Text = " ";
        }

        private void IzvrsuvanjeNaracka_Paint(object sender, PaintEventArgs e)
        {
            this.UpdatePhone();
            this.PopolniListaMenija();
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowLeft;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowRight;
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowUp;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowUp;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowDown;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowDown;
        }

        private void pictureBox12_MouseEnter(object sender, EventArgs e)
        {
            pictureBox12.Image = Resources.LightArrowRight___Copy;
            pictureBox14.Image = Resources.LightArrowRight___Copy;
        }

        private void pictureBox12_MouseLeave(object sender, EventArgs e)
        {
            pictureBox12.Image = Resources.DarkArrowRight;
            pictureBox14.Image = Resources.DarkArrowRight;
        }

        private void pictureBox15_MouseEnter(object sender, EventArgs e)
        {
            pictureBox15.Image = Resources.LightArrowLeft;
            pictureBox13.Image = Resources.LightArrowLeft;
        }

        private void pictureBox15_MouseLeave(object sender, EventArgs e)
        {
            pictureBox15.Image = Resources.DarkArrowLeft;
            pictureBox13.Image = Resources.DarkArrowLeft;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (this.Restoran.Kontakt.Count > 1)
            {
                this.indKontakt = (this.indKontakt + 1) % this.Restoran.Kontakt.Count;
                Invalidate(true);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.Restoran.Kontakt.Count > 1)
            {
                if (this.indKontakt == 0)
                    this.indKontakt = this.Restoran.Kontakt.Count;
                this.indKontakt--;
                Invalidate(true);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (Restoran.GlavnoMeni != null && this.Restoran.GlavnoMeni.GetContent().Count > this.ListaMeni.Count)
            {
                if (this.indMeni != 0)
                    this.indMeni--;
                Invalidate(true);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (Restoran.GlavnoMeni != null && this.Restoran.GlavnoMeni.GetContent().Count > this.ListaMeni.Count)
            {
                if (this.indMeni < this.Restoran.GlavnoMeni.GetContent().Count - this.ListaMeni.Count)
                    this.indMeni++;
                Invalidate(true);
            }
        }

        private void lblMeni1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;

            if (lb.LblObject != null && lb.LblObject is Meni)
            {
                CurrMenu = Restoran.GlavnoMeni;
                PostaviPateka();
                indStavka = 0;
                CurrMenu = lb.LblObject as Meni;
                LabelFASAP label1 = new LabelFASAP();
                LabelFASAP label2 = new LabelFASAP();
                flowLayoutPanelFasap1.Controls.Add(label2);
                flowLayoutPanelFasap1.Controls.Add(label1);
                label1.Font = new Font("Trebuchet MS", 12, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                label1.UpdateObject(CurrMenu);
                label1.ForeColor = Color.Gold;
                label1.MouseEnter += new EventHandler(lblOsnovnoMeni_MouseEnter);
                label1.MouseLeave += new EventHandler(lblOsnovnoMeni_MouseLeave);
                label1.Click += new EventHandler(lblOsnovnoMeni_Click);
                label1.AutoSize = true;
                label2.Font = new Font("Trebuchet MS", 12, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                label2.ForeColor = Color.White;
                label2.Text = ">>";
                label2.AutoSize = true;
                flowLayoutPanelFasap1.Invalidate(true);
            }
            this.PopolniListaStavki();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (CurrMenu != null && this.CurrMenu.GetContent().Count > this.ListaStavki.Count)
            {
                if (this.indStavka != 0)
                    this.indStavka--;
                this.PopolniListaStavki();
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (CurrMenu != null && this.CurrMenu.GetContent().Count > this.ListaStavki.Count)
            {
                if (this.indStavka < this.CurrMenu.GetContent().Count - this.ListaStavki.Count)
                    this.indStavka++;
                this.PopolniListaStavki();
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (this.lblKolicina.Text != "1")
                this.lblKolicina.Text = (int.Parse(this.lblKolicina.Text) - 1).ToString();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.lblKolicina.Text = (int.Parse(this.lblKolicina.Text) + 1).ToString();
        }

        private void lblStavka1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                MenuComponent mc = lb.LblObject as MenuComponent;

                if (mc is Meni)
                {
                    CurrMenu = mc;
                    if (lb.LblObject != null && lb.LblObject is Meni)
                    {
                        indMeni = 0;
                        CurrMenu = lb.LblObject as Meni;
                        LabelFASAP label1 = new LabelFASAP();
                        LabelFASAP label2 = new LabelFASAP();
                        flowLayoutPanelFasap1.Controls.Add(label2);
                        flowLayoutPanelFasap1.Controls.Add(label1);
                        label1.Font = new Font("Trebuchet MS", 12, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        label1.UpdateObject(CurrMenu);
                        label1.ForeColor = Color.Gold;
                        label1.MouseEnter += new EventHandler(lblOsnovnoMeni_MouseEnter);
                        label1.MouseLeave += new EventHandler(lblOsnovnoMeni_MouseLeave);
                        label1.Click += new EventHandler(lblOsnovnoMeni_Click);
                        label1.AutoSize = true;
                        label2.Font = new Font("Trebuchet MS", 12, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        label2.ForeColor = Color.White;
                        label2.Text = ">>";
                        label2.AutoSize = true;
                        flowLayoutPanelFasap1.Invalidate(true);
                    }
                    PopolniListaStavki();
                    return;
                }
                try
                {
                    CurrItem = mc.GetReference(CurrItem);

                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = String.Format("{0}   ", ex.Message);
                    timer1.Stop();
                    errorMessageTime = 3;
                    lblErrorMessage.Visible = true;
                    timer1.Start();
                }
            }
            if (CurrItem != null)
            {
                lblImeStavka.UpdateObject(CurrItem);
                try
                {
                    lblCenaProizvod.Text = String.Format("{0} ден.", CurrItem.ComputeCost().ToString());
                }
                catch (Exception)
                {
                }
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (CurrItem == null)
                return;
            OrderComponent oc = new OrderComponent(CurrItem, int.Parse(lblKolicina.Text));
            Naracka.Add(oc);
            Naracka = Naracka;
        }

        private void PostaviNaracka()
        {
            for (int i = 0; i < ListaKupeni.Count; i++)
                if (i < Naracka.Stavki.Count)
                {
                    ListaKupeni[i].UpdateObject(Naracka.Stavki[i + indKupeni]);
                    if (Naracka.Stavki[i + indKupeni].Equals(CurrOrderComponent))
                    {
                        ListaKupeni[i].Image = Resources.LabelBackgroundSelected;
                        ListaKupeni[i].ForeColor = Color.SaddleBrown;
                    }
                    else
                    {
                        ListaKupeni[i].Image = Resources.LabelBackground2;
                        ListaKupeni[i].ForeColor = Color.White;
                    }
                }
                else
                {
                    ListaKupeni[i].UpdateObject(null);
                    ListaKupeni[i].Image = Resources.LabelBackground2;
                    ListaKupeni[i].ForeColor = Color.White;
                }
            lblCena.Text = ((int)Naracka.VkupnaCena).ToString();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            if (indKupeni > 0)
            {
                indKupeni--;
                PostaviNaracka();
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            int pom = Naracka.Stavki.Count - ListaKupeni.Count;
            if (indKupeni < pom)
            {
                indKupeni++;
                PostaviNaracka();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            errorMessageTime--;
            if (errorMessageTime == 0)
            {
                lblErrorMessage.Visible = false;
                timer1.Stop();
            }
        }

        private void lblMeni1_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                if (lb.LblObject is Meni)
                    lb.Font = new Font("Trebuchet MS", 17, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                else lb.Font = new Font("Trebuchet MS", 17, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lblMeni1_MouseLeave(object sender, EventArgs e)
        {
            lblDescription.Visible = false;
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                if (lb.LblObject is Meni)
                    lb.Font = new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                else lb.Font = new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        public override void LoadingMethod()
        {
            LoadingSemaphore.Release(2);
        }

        delegate void SetObjectCallback(LabelFASAP fs, Object obj);

        private void SetObject(LabelFASAP fs, Object obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                SetObjectCallback d = new SetObjectCallback(SetObject);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.UpdateObject(obj);
            }
        }

        private void IzvrsuvanjeNaracka_FormClosing(object sender, FormClosingEventArgs e)
        {
            Restoran.GlavnoMeni = null;
        }

        delegate void SetLogoMethod(PictureBox fs, String obj);

        private void SetPbLogo(PictureBox fs, String obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                SetLogoMethod d = new SetLogoMethod(SetPbLogo);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Load(obj);
            }
        }

        delegate void SetDefaultLogoMethod(PictureBox fs, Image obj);

        private void SetPbDefaultLogo(PictureBox fs, Image obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                SetDefaultLogoMethod d = new SetDefaultLogoMethod(SetPbDefaultLogo);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Image = obj;
            }
        }


        private Image ImageFromURL(string Url)
        {
            byte[] imageData = DownloadData(Url);
            Image img = null;

            MemoryStream stream = new MemoryStream(imageData);
            img = Image.FromStream(stream);


            stream.Close();

            return img;
        }

        private byte[] DownloadData(string Url)
        {
            string empty = string.Empty;
            return DownloadData(Url, out empty);
        }

        private byte[] DownloadData(string Url, out string responseUrl)
        {
            byte[] downloadedData = new byte[0];
            try
            {
                //Get a data stream from the url  
                WebRequest req = WebRequest.Create(Url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                responseUrl = response.ResponseUri.ToString();

                //Download in chuncks  
                byte[] buffer = new byte[1024];

                //Get Total Size  
                int dataLength = (int)response.ContentLength;

                //Download to memory  
                //Note: adjust the streams here to download directly to the hard drive  
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    //Try to read the data  
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        //Write the downloaded data  
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }

                //Convert the downloaded stream to a byte array  
                downloadedData = memStream.ToArray();

                //Clean up  
                stream.Close();
                memStream.Close();
            }
            catch (Exception)
            {
                responseUrl = string.Empty;
                return new byte[0];
            }

            return downloadedData;
        }

        private void btnOtkazi_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            Label lb = sender as Label;
            lb.Image = Resources.LightButton___Copy;
            lb.ForeColor = Color.SaddleBrown;
        }

        private void btnOtkazi_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            Label lb = sender as Label;
            lb.Image = Resources.DarkButton___Copy;
            lb.ForeColor = Color.Khaki;
        }

        private void btnPotvrdi_Click(object sender, EventArgs e)
        {
            MessageBoxForm mbf = new MessageBoxForm("Дали сакате достава?");
            if (mbf.ShowDialog() == DialogResult.Yes)
            {
                OnlineNarackaPodatoci onp = new OnlineNarackaPodatoci(Conn, Naracka, Restoran);
                if (onp.ShowDialog() == DialogResult.Yes)
                {
                    Naracka = new Naracka(-1, 0, DateTime.Now);
                }
            }
            else
            {
                OnsiteNarackaPodatoci onp = new OnsiteNarackaPodatoci(Conn, Naracka, Restoran);
                if (onp.ShowDialog() == DialogResult.Yes)
                {
                    Naracka = new Naracka(-1, 0, DateTime.Now);
                }
            }
        }

        private void btnOtkazi_Click(object sender, EventArgs e)
        {
            MessageBoxForm mbf = new MessageBoxForm("Дали навистина сакате да ја откажете нарачката?");
            if (mbf.ShowDialog() == DialogResult.Yes)
            {
            }
            Naracka = new Naracka(-1, 0, DateTime.Now);
        }

        private void lblKupeno5_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                foreach (var obj in ListaKupeni)
                {
                    obj.Image = Resources.LabelBackground2;
                    obj.ForeColor = Color.White;
                }
                CurrOrderComponent = lb.LblObject as OrderComponent;
                lb.Image = Resources.LabelBackgroundSelected;
                lb.ForeColor = Color.SaddleBrown;
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (CurrOrderComponent != null)
            {
                Naracka.Remove(CurrOrderComponent);
                CurrOrderComponent = null;
                Naracka = Naracka;
            }
        }

        private void lblStavka1_MouseHover(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                MenuComponent mc = lb.LblObject as MenuComponent;
                if (mc.GetDescription() != null && mc.GetDescription().Trim() != "")
                {
                    lblDescription.Location = Cursor.Position + new Size(10, 30);
                    lblDescription.Text = mc.GetDescription() + "  ";
                    lblDescription.Visible = true;
                }
            }
        }

        private void lblImeStavka_MouseLeave(object sender, EventArgs e)
        {
            lblDescription.Visible = false;
        }

        private void lblKupeno6_MouseHover(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                OrderComponent mc = lb.LblObject as OrderComponent;
                if (mc.Item.GetDescription() != null && mc.Item.GetDescription().Trim() != "")
                {
                    lblDescription.Location = Cursor.Position + new Size(10, 30);
                    lblDescription.Text = mc.Item.GetDescription() + "  ";
                    lblDescription.Visible = true;
                }
            }
        }

        private void lblImeStavka_MouseHover(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                MenuComponent mc = lb.LblObject as MenuComponent;
                if (mc.GetDescription() != null && mc.GetDescription().Trim() != "")
                {
                    lblDescription.Location = Cursor.Position + new Size(10, 30);
                    lblDescription.Text = mc.GetDescription() + "  ";
                    lblDescription.Visible = true;
                }
            }
        }
    }
}
