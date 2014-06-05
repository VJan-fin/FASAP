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
using System.Globalization;

namespace SmetkaZaNaracka
{
    public partial class IzvrsuvanjeNaracka : BackgroundForm
    {
        private MenuComponent CurrMenu { get; set; }
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        public MenuComponent CurrItem { get; set; }
        public List<OrderComponent> OrderList { get; set; }

        private List<LabelFASAP> ListaMeni { get; set; }
        private List<LabelFASAP> ListaStavki { get; set; }
        private List<LabelFASAP> ListaKupeni { get; set; }
        private int indKontakt { get; set; }
        private int indMeni { get; set; }
        private int indStavka { get; set; }
        private int indKupeni { get; set; }
        int errorMessageTime = 3;

        public IzvrsuvanjeNaracka(Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            Restoran = restoran;
            Conn = conn;
            OrderList = new List<OrderComponent>();
            Opacity = 0;
            this.AddButtons();
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
            // Vcituvanje podatoci za restoranot
            this.indKontakt = 0;
            this.indMeni = 0;
            this.indStavka = 0;
            this.lblImeRestoran.Text = this.Restoran.Ime + " ";
            if (this.Restoran.Ulica != null && this.Restoran.Grad != null)
                this.lblAdresa.Text = this.Restoran.Ulica + ", " + Restoran.Grad + " ";
            else
                this.lblAdresa.Text = " ";
            if (this.Restoran.RabotnoVreme != null)
                this.lblRabVreme.Text = this.Restoran.RabotnoVreme + " ";
            else
                this.lblRabVreme.Text = " ";
            if (Restoran.PicturePath == null)
                pictureBoxLogo.Image = Resources.FASAP_LOGO;

            // Vcituvanje na osnovnite menija na restoranot
            string sqlMeni = @"SELECT * FROM MENI WHERE VALIDNOST_MENI LIKE '1' AND RESTORAN_ID = :RES_ID AND IME_GLAVNO IS NULL";
            OracleCommand cmd = new OracleCommand(sqlMeni, Conn);
            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int64);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                Restoran.GlavnoMeni = new Meni(dr.GetString(1));
            KreirajMeni(Restoran.GlavnoMeni);
            PopolniListaMenija();

            foreach (var item in this.ListaStavki)
                item.Text = " ";
        }

        private void KreirajMeni(MenuComponent mc)
        {
            string sql = @"SELECT * FROM MENI WHERE VALIDNOST_MENI LIKE '1' AND RESTORAN_ID = :RES_ID AND IME_GLAVNO LIKE :MENU_II";
            OracleCommand cmd = new OracleCommand(sql, Conn);

            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int64); 
            prm.Value = Restoran.RestoranID; 
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("MENU_II", OracleDbType.Varchar2); 
            prm.Value = mc.GetName(); 
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            Meni meni;
            while (dr.Read())
            {
                meni = new Meni(dr.GetString(1));
                meni.Parent = mc;
                mc.AddComp(meni);
                KreirajMeni(meni);
            }

            sql = @"SELECT * FROM STAVKA WHERE VALIDNOST_STAVKA LIKE '1' AND RESTORAN_ID = :RES_ID AND IME_MENI LIKE :MENU_II";
            cmd = new OracleCommand(sql, Conn);

            prm = new OracleParameter("RES_ID", OracleDbType.Int64); 
            prm.Value = Restoran.RestoranID; 
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("MENU_II", OracleDbType.Varchar2); 
            prm.Value = mc.GetName(); 
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();

            Dodatok dodatok;
            Stavka stavka;
            while (dr.Read())
            {
                String IsDecorator = (string)dr.GetValue(5);
                if (IsDecorator == "1")
                {
                    dodatok = new Dodatok((int)dr.GetValue(2), dr.GetString(7), (decimal)dr.GetValue(4));
                    dodatok.Parent = mc;
                    mc.AddComp(dodatok);
                }
                else
                {

                    stavka = new Stavka((int)dr.GetValue(2), dr.GetString(7), (decimal)dr.GetValue(4));
                    stavka.Parent = mc;
                    mc.AddComp(stavka);
                }
            }
        }

        private void PopolniListaMenija()
        {
            MenuComponent mc = this.Restoran.GlavnoMeni;
            int ind = this.indMeni;
            for (int i = 0; i < this.ListaMeni.Count; i++)
                if (ind < mc.GetContent().Count)
                {
                    this.ListaMeni[i].UpdateObject(mc.GetContent()[ind]);
                    ind++;                  
                }
                else
                    this.ListaMeni[i].Text = ": : ";
        }

        private void PopolniListaStavki()
        {
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
                    ListaStavki[i].Text = ": : ";
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
            if (this.Restoran.GlavnoMeni.GetContent().Count > this.ListaMeni.Count)
            {
                if (this.indMeni != 0)
                    this.indMeni--;
                Invalidate(true);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (this.Restoran.GlavnoMeni.GetContent().Count > this.ListaMeni.Count)
            {
                if (this.indMeni < this.Restoran.GlavnoMeni.GetContent().Count - this.ListaMeni.Count)
                    this.indMeni++;
                Invalidate(true);
            }
        }

        private void lblMeni1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
                CurrMenu = lb.LblObject as MenuComponent;
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
                    PopolniListaStavki();
                    return;
                }
                try
                {
                    CurrItem = mc.GetReference(CurrItem);
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = String.Format("{0}   ",ex.Message);
                    timer1.Stop();
                    errorMessageTime = 3;
                    lblErrorMessage.Visible = true;
                    timer1.Start();
                }
            }
            if (CurrItem != null)
            {
                lblImeStavka.Text = CurrItem.GetName();
                try
                {
                    lblCenaProizvod.Text = String.Format("{0} ден.",CurrItem.ComputeCost().ToString());
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            OrderComponent oc = new OrderComponent(CurrItem, int.Parse(lblKolicina.Text));
            int i = 0;
            for (i = 0; i < OrderList.Count; i++)
                if (oc.Equals(OrderList[i]))
                {
                    OrderList[i].Quantity += oc.Quantity;
                    break;
                }
            if(i == OrderList.Count)
                OrderList.Add(oc);
            PostaviNaracka();
        }

        private void PostaviNaracka()
        {
            int Vkupno = 0;
            foreach (var obj in OrderList)
                Vkupno += obj.ComputePrice();
            for (int i = 0; i < OrderList.Count && i < ListaKupeni.Count; i++)
            {
                ListaKupeni[i].UpdateObject(OrderList[i + indKupeni]);
            }
            lblCena.Text = Vkupno.ToString();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            int pom = OrderList.Count - ListaKupeni.Count;
            if (indKupeni < pom)
            {
                indKupeni++;
                PostaviNaracka();
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if (indKupeni > 0)
            {
                indKupeni--;
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
    }
}
