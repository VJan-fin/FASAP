using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using SmetkaZaNaracka.Properties;
using System.Threading;

namespace SmetkaZaNaracka
{
    public partial class PregledMeni : BackgroundForm
    {
        public List<LabelFASAP> ListaStavki { get; set; }
        public Restoran Restoran { get; set; }
        public OracleConnection Conn { get; set; }
        public int indMeni { get; set; }
        public MenuComponent CurrMenu { get; set; }
        public MenuComponent SelectedComponent { get; set; }
        public bool ShowInactive { get; set; }
        public bool IsDecorator { get; set; }
        
        public PregledMeni()
        {
            InitializeComponent();
            Opacity = 0;
            Restoran = new Restoran();
            Restoran.Ime = "Ресторан Бигор - Вруток";
            Restoran.RestoranID = 2;
            string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
             + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
             + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            ShowInactive = false;
            IsDecorator = false;
            Conn.Open();
        }

        private void PregledMeni_Load(object sender, EventArgs e)
        {
            ListaStavki = new List<LabelFASAP>();
            this.ListaStavki.Add(this.lblStavka1);
            this.ListaStavki.Add(this.lblStavka2);
            this.ListaStavki.Add(this.lblStavka3);
            this.ListaStavki.Add(this.lblStavka4);
            this.ListaStavki.Add(this.lblStavka5);
            this.ListaStavki.Add(this.lblStavka6);
            this.ListaStavki.Add(this.lblStavka7);
            this.ListaStavki.Add(this.lblStavka8);
            this.ListaStavki.Add(this.lblStavka9);
            this.ListaStavki.Add(this.lblStavka10);

            Thread oThread = new Thread(new ThreadStart(KreirajMeni));
            oThread.Start();
            //KreirajMeni();
        }

        private void KreirajMeni()
        {
            Dictionary<string, Meni> Menus = new Dictionary<string, Meni>();
            Dictionary<StavkaKey, Stavka> Items = new Dictionary<StavkaKey, Stavka>();
            
            string sql = @"SELECT * FROM MENI WHERE RESTORAN_ID = :RES_ID";
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
            SetObject(lblOsnovnoMeni, Restoran.GlavnoMeni);
            CurrMenu = Restoran.GlavnoMeni;
            PostaviPateka();
            PopolniListaMenija();
        }

        private void PopolniListaMenija()
        {
            int ind = this.indMeni;
            for (int i = 0; i < this.ListaStavki.Count; i++)
                if (ind < CurrMenu.GetContent().Count)
                {

                    if (CurrMenu.GetContent()[ind] is Meni)
                    {
                        Meni mn = CurrMenu.GetContent()[ind] as Meni;
                        if (!ShowInactive && !mn.ValidnostMeni)
                        {
                            i--;
                            ind++;
                            continue;
                        }
                        SetObject(ListaStavki[i], CurrMenu.GetContent()[ind]);
                        SetFontLabel(ListaStavki[i], new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204))));
                        ListaStavki[i].ForeColor = Color.Gold;
                        if (!mn.ValidnostMeni)
                        {
                            if (mn.Equals(SelectedComponent))
                            {
                                ListaStavki[i].ForeColor = SystemColors.InactiveCaptionText;
                            }
                            else
                            {
                                ListaStavki[i].ForeColor = SystemColors.InactiveCaption;
                            }
                        }
                        else
                        {
                            if (mn.Equals(SelectedComponent))
                            {
                                ListaStavki[i].ForeColor = Color.SaddleBrown;
                            }
                            else
                            {
                                ListaStavki[i].ForeColor = Color.Gold;
                            }
                        }
                        
                    }
                    else
                    {
                        SetFontLabel(ListaStavki[i], new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204))));
                        SetObject(ListaStavki[i], CurrMenu.GetContent()[ind]);
                        if (CurrMenu.GetContent()[ind].Equals(SelectedComponent))
                        {
                            ListaStavki[i].ForeColor = Color.SaddleBrown;
                        }
                        else
                        {
                            ListaStavki[i].ForeColor = Color.White;
                        }
                        ListaStavki[i].Image = Resources.LabelBackground2;
                    }

                    if (CurrMenu.GetContent()[ind].Equals(SelectedComponent))
                    {
                        ListaStavki[i].Image = Resources.LabelBackgroundSelected;
                    }
                    else ListaStavki[i].Image = Resources.LabelBackground2;
                    ind++;
                }
                else
                {
                    ListaStavki[i].Image = Resources.LabelBackground2;
                    SetObject(ListaStavki[i], null);
                    SetFontLabel(ListaStavki[i], new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204))));
                    ListaStavki[i].ForeColor = Color.Khaki;
                }
        }

        private void lblStavka1_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                if (lb.LblObject is Meni)
                    lb.Font = new Font("Trebuchet MS", 17, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                else lb.Font = new Font("Trebuchet MS", 17, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lblStavka1_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                if (lb.LblObject is Meni)
                    lb.Font = new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                else lb.Font = new Font("Trebuchet MS", 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
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
            indMeni = 0;

            if (lb.LblObject != null && lb.LblObject is Meni)
            {
                CurrMenu = lb.LblObject as Meni;
                PostaviPateka();
                PopolniListaMenija();
            }
        }

        private void lblStavka1_DoubleClick(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            indMeni = 0;

            if (lb.LblObject != null && lb.LblObject is Meni)
            {
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
                PopolniListaMenija();
            }
        }

        private void buttonFASAP1_MouseEnter(object sender, EventArgs e)
        {
            ButtonFASAP bf = sender as ButtonFASAP;
            Cursor = Cursors.Hand;
            bf.Image = Resources.LightButton___Copy;
            bf.ForeColor = Color.SaddleBrown;
        }

        private void buttonFASAP1_MouseLeave(object sender, EventArgs e)
        {
            ButtonFASAP bf = sender as ButtonFASAP;
            Cursor = Cursors.Default;
            bf.Image = Resources.DarkButton___Copy;
            bf.ForeColor = Color.Khaki;
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            Thread oThread = new Thread(new ThreadStart(KreirajMeni));
            oThread.Start();
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            if (ShowInactive)
            {
                ShowInactive = false;
                buttonFASAP2.Text = "Сите менија";
            }
            else
            {
                ShowInactive = true;
                buttonFASAP2.Text = "Само активни";
            }
            PopolniListaMenija();
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

        delegate void SetFontLabelCallback(LabelFASAP fs, Font obj);

        private void SetFontLabel(LabelFASAP fs, Font obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                SetFontLabelCallback d = new SetFontLabelCallback(SetFontLabel);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Font = obj;
            }
        }

        private void buttonFASAP6_Click(object sender, EventArgs e)
        {
            if (IsDecorator)
            {
                IsDecorator = false;
                buttonFASAP6.Image = Resources.LightButton___Copy;
            }
            else
            {
                IsDecorator = true;
                buttonFASAP6.Image = Resources.LightCorrectButton;
            }
        }

        private void buttonFASAP6_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            if (IsDecorator)
            {
                buttonFASAP6.Image = Resources.LightCorrectButton;
            }
            else buttonFASAP6.Image = Resources.LightButton___Copy;
        }

        private void buttonFASAP6_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            if (IsDecorator)
            {
                buttonFASAP6.Image = Resources.DarkCorrectButton;
            }
            else buttonFASAP6.Image = Resources.DarkButton___Copy;
        }

        private void lblStavka1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            SelectedComponent = lb.LblObject as MenuComponent;
            PopolniListaMenija();
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

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            pictureBox8.Image = Resources.LightArrowUp;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            pictureBox8.Image = Resources.DarkArrowUp;
        }

        private void pictureBox9_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            pictureBox9.Image = Resources.LightArrowDown;
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            pictureBox9.Image = Resources.DarkArrowDown;
        }

        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            if (indMeni > 0)
            {
                indMeni--;
                PopolniListaMenija();
            }
        }

        private void pictureBox9_MouseDown(object sender, MouseEventArgs e)
        {
            int pom = CurrMenu.GetContent().Count - ListaStavki.Count;
            if (indMeni < pom)
            {
                indMeni++;
                PopolniListaMenija();
            }
        }
    }
}
