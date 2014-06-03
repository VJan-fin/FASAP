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
    public partial class FasapNaracka : Form
    {
        private MenuComponent CurrComponent { get; set; }
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        public FasapNaracka(Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            Restoran = restoran;
            Conn = conn;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Restoran.Ime;
            labelImeRestoran.Text = Restoran.Ime;
            labelLokacija.Text = Restoran.Ulica + ", " + Restoran.Grad;
            labelRabVreme.Text = Restoran.RabotnoVreme;
            foreach (var obj in Restoran.Kontakt)
                lbKontakt.Items.Add(obj);
            if (Restoran.PicturePath == null)
            {
                pictureBoxLogo.Image = Resources.FASAP_LOGO;
            }
            
            string sql = @"SELECT * FROM MENI WHERE VALIDNOST_MENI LIKE '1' AND RESTORAN_ID = :RES_ID AND IME_GLAVNO IS NULL"; // C#
            OracleCommand cmd = new OracleCommand(sql, Conn);
            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int64);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
                Restoran.GlavnoMeni = new Meni(dr.GetString(1));
            KreirajMeni(Restoran.GlavnoMeni);
            PopolniListaMenija(Restoran.GlavnoMeni);
        }

        private void KreirajMeni(MenuComponent mc)
        {
            string sql = @"SELECT * FROM MENI WHERE VALIDNOST_MENI LIKE '1' AND RESTORAN_ID = :RES_ID AND IME_GLAVNO LIKE :MENU_II"; // C#
            OracleCommand cmd = new OracleCommand(sql, Conn);
            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int64); prm.Value = Restoran.RestoranID; cmd.Parameters.Add(prm);
            prm = new OracleParameter("MENU_II", OracleDbType.Varchar2); prm.Value = mc.GetName(); cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            Meni meni;
            while (dr.Read())
            {
                meni = new Meni(dr.GetString(1));
                mc.AddComp(meni);
                KreirajMeni(meni); 
            }
            
            sql = @"SELECT * FROM STAVKA WHERE VALIDNOST_STAVKA LIKE '1' AND RESTORAN_ID = :RES_ID AND IME_MENI LIKE :MENU_II";
            cmd = new OracleCommand(sql, Conn);
            prm = new OracleParameter("RES_ID", OracleDbType.Int64); prm.Value = Restoran.RestoranID; cmd.Parameters.Add(prm);
            prm = new OracleParameter("MENU_II", OracleDbType.Varchar2); prm.Value = mc.GetName(); cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader(); 
            Dodatok dodatok;
            Stavka stavka;

            while (dr.Read())
            { 
                String IsDecorator = (string)dr.GetValue(5);
                if ( IsDecorator == "1")
                {
                    dodatok = new Dodatok((int)dr.GetValue(2), dr.GetString(7), (decimal)dr.GetValue(4));
                    mc.AddComp(dodatok);
                }
                else
                {
                    
                   stavka = new Stavka((int)dr.GetValue(2), dr.GetString(7), (decimal)dr.GetValue(4));
                   mc.AddComp(stavka);
                }  
            }
        }

        private void PopolniListaMenija(MenuComponent mc)
        {
            lbMenus.Items.Add(mc);
            foreach (var obj in mc.GetContent())
                if(obj is Meni)
                    PopolniListaMenija(obj);
        }

        private void lbMenus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMenus.SelectedItem != null)
            {
                MenuComponent mc = lbMenus.SelectedItem as MenuComponent;
                listBoxStavki.Items.Clear();
                foreach (var obj in mc.GetContent())
                    if (!(obj is Meni))
                        listBoxStavki.Items.Add(obj);
            }
        }

        private void FasapNaracka_Paint(object sender, PaintEventArgs e)
        {
            int Total = 0;
            foreach (var obj in listBoxNaracano.Items)
                Total += ((OrderComponent)obj).ComputePrice();
            label4.Text = Total.ToString();
            if (CurrComponent != null)
            {
                labelIme.Text = CurrComponent.GetName();
                try
                {
                    labelCena.Text = String.Format("{0} ден.", CurrComponent.ComputeCost());
                }
                catch (Exception ex)
                {
                }
                labelOpis.Text = CurrComponent.GetDescription();
            }
            else
            {
                labelIme.Text = "";
                labelCena.Text = "";
                labelOpis.Text = "";
            }
        }

        private void listBoxStavki_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxStavki.SelectedItem != null)
            {
                MenuComponent mc = listBoxStavki.SelectedItem as MenuComponent;
                try
                {
                    CurrComponent = mc.GetReference(CurrComponent);
                }
                catch (Exception ex)
                {
                }
            }
            Invalidate();
        }

        private void buttonDodajVoNaracka_Click(object sender, EventArgs e)
        {
            OrderComponent oc = new OrderComponent(CurrComponent, (int)numericUpDownKolicina.Value);
            listBoxNaracano.Items.Add(oc);
            Invalidate();
        }

        private void buttonOtstraniOdNaracka_Click(object sender, EventArgs e)
        {
            if (listBoxNaracano.SelectedItem != null)
                listBoxNaracano.Items.Remove(listBoxNaracano.SelectedItem);
            Invalidate();
        }
    }
}
