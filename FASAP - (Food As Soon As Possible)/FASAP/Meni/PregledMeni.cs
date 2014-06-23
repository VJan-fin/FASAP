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
        public bool ShowInactive { get; set; }
        public bool IsDecorator { get; set; }
        public Semaphore LoadingSemaphore { get; set; }
        public Vraboten Vraboten { get; set; }
        public bool DodadiStavka { get; set; }
        private bool isChanged;
        public bool IsChanged
        {
            get
            {
                return isChanged;
            }
            set
            {
                isChanged = value;
                if (isChanged)
                    ButtonFasapSetVisible(btnSocuvajPromeni, true);
                else ButtonFasapSetVisible(btnSocuvajPromeni, false);
            }
        }
        private MenuComponent selectedComponent;
        public MenuComponent SelectedComponent
        {
            get { return selectedComponent; }
            set
            {
                selectedComponent = value;
                if (value != null)
                {
                    ButtonFasapSetVisible(btnBrisi, true);
                    TextBoxSetText(tbIme, selectedComponent.GetName());
                    if (selectedComponent is Meni)
                    {
                        if ((selectedComponent as Meni).ValidnostMeni)
                        {
                            btnBrisi.ForeColor = Color.FromArgb(250, 20, 20);
                            ButtonFasapSetText(btnBrisi, "Бриши");
                        }
                        else
                        {
                            btnBrisi.ForeColor = Color.Lime;
                            ButtonFasapSetText(btnBrisi, "Активирај");
                        }
                        ButtonFasapSetVisible(lblIme, true);
                        ButtonFasapSetVisible(tbIme, true);
                        ButtonFasapSetVisible(lblOpis, false);
                        ButtonFasapSetVisible(lblDodatok, false);
                        ButtonFasapSetVisible(lblCena, false);
                        ButtonFasapSetVisible(tbOpis, false);
                        ButtonFasapSetVisible(btnDodatok, false);
                        ButtonFasapSetVisible(tbCena, false);
                    }
                    else
                    {
                        btnBrisi.ForeColor = Color.FromArgb(250, 20, 20);
                        ButtonFasapSetText(btnBrisi, "Бриши");
                        if (selectedComponent.GetDescription() != null)
                            TextBoxSetText(tbOpis, selectedComponent.GetDescription());
                        else TextBoxSetText(tbOpis, "");
                        if (selectedComponent is Dodatok)
                        {
                            IsDecorator = true;
                            btnDodatok.Image = Resources.DarkCorrectButton;
                        }
                        else
                        {
                            btnDodatok.Image = Resources.DarkButton___Copy;
                            IsDecorator = false;
                        }
                        TextBoxSetText(tbCena, selectedComponent.ComputeCost().ToString());
                        ButtonFasapSetVisible(lblIme, true);
                        ButtonFasapSetVisible(tbIme, true);
                        ButtonFasapSetVisible(lblOpis, true);
                        ButtonFasapSetVisible(lblDodatok, true);
                        ButtonFasapSetVisible(lblCena, true);
                        ButtonFasapSetVisible(tbOpis, true);
                        ButtonFasapSetVisible(btnDodatok, true);
                        ButtonFasapSetVisible(tbCena, true);
                    }
                }
                else
                {
                    TextBoxSetText(tbIme, "");
                    TextBoxSetText(tbOpis, "");
                    TextBoxSetText(tbCena, "0");
                    btnDodatok.Image = Resources.DarkButton___Copy;
                    ButtonFasapSetVisible(btnBrisi, false);
                }
            }
        }

        public PregledMeni(Restoran restoran, Vraboten vrab, OracleConnection conn)
        {
            InitializeComponent();
            LoadingSemaphore = new Semaphore(0, 100);
            IsChanged = false;
            Conn = conn;
            Restoran = restoran;
            Vraboten = vrab;
            Opacity = 0;
            lblImeVraboten.Text = String.Format("{0} {1} ", Vraboten.Ime, Vraboten.Prezime);
            lblImeRestoran.Text = String.Format("{0} ", Restoran.Ime);
        }

        public PregledMeni()
        {
            InitializeComponent();
            LoadingSemaphore = new Semaphore(0, 1);
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
            LoadingSemaphore.WaitOne();
            SetObject(lblOsnovnoMeni, Restoran.GlavnoMeni);
            CurrMenu = Restoran.GlavnoMeni;
            PostaviPateka();
            PopolniListaMenija();
        }

        private void PopolniListaMenija()
        {
            if (CurrMenu == null)
                return;
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
                                ListaStavki[i].ForeColor = Color.Firebrick;
                            }
                            else
                            {
                                ListaStavki[i].ForeColor = Color.FromArgb(250, 20, 20);
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
            buttonFASAP1.Enabled = false;
            Thread oThread = new Thread(new ThreadStart(KreirajMeni));
            oThread.Start();
            indMeni = 0;
            LoadingSemaphore.Release();
            buttonFASAP1.Enabled = true;
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            if (ShowInactive)
            {
                ShowInactive = false;
                if (SelectedComponent is Meni && !(SelectedComponent as Meni).ValidnostMeni)
                    SelectedComponent = null;
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
                btnDodatok.Image = Resources.LightButton___Copy;
            }
            else
            {
                IsDecorator = true;
                btnDodatok.Image = Resources.LightCorrectButton;
            }
        }

        private void buttonFASAP6_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            if (IsDecorator)
            {
                btnDodatok.Image = Resources.LightCorrectButton;
            }
            else btnDodatok.Image = Resources.LightButton___Copy;
        }

        private void buttonFASAP6_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            if (IsDecorator)
            {
                btnDodatok.Image = Resources.DarkCorrectButton;
            }
            else btnDodatok.Image = Resources.DarkButton___Copy;
        }

        private void lblStavka1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                if (IsChanged)
                {
                    MessageBoxForm mbf = new MessageBoxForm("Имате несочувани промени. Дали сакате да ги сочувате?");
                    if (mbf.ShowDialog() == DialogResult.Yes)
                        SocuvajPromeni();
                    else
                        IsChanged = false;
                }
                SelectedComponent = lb.LblObject as MenuComponent;
                PopolniListaMenija();
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

        public override void LoadingMethod()
        {
            LoadingSemaphore.Release();
        }

        private void PregledMeni_FormClosing(object sender, FormClosingEventArgs e)
        {
            Restoran.GlavnoMeni = null;
        }

        private void buttonFASAP5_Click(object sender, EventArgs e)
        {
            if (SelectedComponent == null)
                return;
            if (SelectedComponent is Meni && !(SelectedComponent as Meni).ValidnostMeni)
            {
                MessageBoxForm mb = new MessageBoxForm(String.Format("Активирај \"{0}\"?", SelectedComponent.GetName()));
                if (mb.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        SelectedComponent.SqlActivate(Conn, Restoran.RestoranID);
                        SelectedComponent = SelectedComponent;
                        PopolniListaMenija();
                    }
                    catch (Exception ex)
                    {
                        timer1.Stop();
                        lblErrorMessage.Text = ex.Message;
                        lblErrorMessage.Visible = true;
                        timer1.Start();
                    }
                }
            }
            else
            {
                MessageBoxForm mb = new MessageBoxForm(String.Format("Бриши \"{0}\"?", SelectedComponent.GetName()));
                if (mb.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        SelectedComponent.SqlDelete(Conn, Restoran.RestoranID);
                        if (!ShowInactive)
                            SelectedComponent = null;
                        else SelectedComponent = SelectedComponent;
                        PopolniListaMenija();
                    }
                    catch (Exception ex)
                    {
                        lblErrorMessage.Text = ex.Message;
                        lblErrorMessage.Visible = true;
                        timer1.Start();
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ButtonFasapSetVisible(lblErrorMessage, false);
            timer1.Stop();
        }

        delegate void BtnBrisiSetVisibleMethod(Control fs, bool obj);

        private void ButtonFasapSetVisible(Control fs, bool obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                BtnBrisiSetVisibleMethod d = new BtnBrisiSetVisibleMethod(ButtonFasapSetVisible);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Visible = obj;
            }
        }

        delegate void ButtonFasapSetTextMethod(Control fs, string obj);

        private void ButtonFasapSetText(Control fs, string obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                ButtonFasapSetTextMethod d = new ButtonFasapSetTextMethod(ButtonFasapSetText);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Text = obj;
            }
        }

        private void btnBrisi_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            if (btnBrisi.ForeColor == Color.Lime)
            {
                btnBrisi.ForeColor = Color.Green;
            }
            else
            {
                btnBrisi.ForeColor = Color.Firebrick;
            }
            btnBrisi.Image = Resources.LightButton___Copy;
        }

        private void btnBrisi_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            if (btnBrisi.ForeColor == Color.Green)
            {
                btnBrisi.ForeColor = Color.Lime;
            }
            else
            {
                btnBrisi.ForeColor = Color.FromArgb(250, 20, 20);
            }
            btnBrisi.Image = Resources.DarkButton___Copy;
        }

        delegate void TextBoxSetTextMethod(TextBox fs, String obj);

        private void TextBoxSetText(TextBox fs, String obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                TextBoxSetTextMethod d = new TextBoxSetTextMethod(TextBoxSetText);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Text = obj;
            }
        }

        private void btnSocuvajPromeni_Click(object sender, EventArgs e)
        {
            ValidateChildren();
            btnSocuvajPromeni.Enabled = false;
            SocuvajPromeni();
            btnSocuvajPromeni.Enabled = true;
        }

        private void SocuvajPromeni()
        {
            if (SelectedComponent != null)
            {
                SelectedComponent.SetName(tbIme.Text);
                SelectedComponent.SetDescription(tbOpis.Text);
                SelectedComponent.SetCost(int.Parse(tbCena.Text));
                try
                {
                    SelectedComponent.SQLUpdate(Conn, Restoran.RestoranID);
                    isChanged = false;
                    Thread oThread = new Thread(new ThreadStart(KreirajMeni));
                    oThread.Start();
                    LoadingSemaphore.Release();
                }
                catch (Exception ex)
                {
                    timer1.Stop();
                    ButtonFasapSetText(lblErrorMessage, ex.Message);
                    ButtonFasapSetVisible(lblErrorMessage, true);
                    timer1.Start();
                }
            }
            else
            {
                if (DodadiStavka)
                {
                    MenuComponent mc;
                    if (IsDecorator)
                        mc = new Dodatok(-1, tbIme.Text, int.Parse(tbCena.Text), tbOpis.Text);
                    else
                        mc = new Stavka(-1, tbIme.Text, int.Parse(tbCena.Text), tbOpis.Text);
                    mc.Parent = CurrMenu;
                    try
                    {
                        mc.SqlInsert(Conn, Restoran.RestoranID);
                        IsChanged = false;
                        Thread oThread = new Thread(new ThreadStart(KreirajMeni));
                        oThread.Start();
                        LoadingSemaphore.Release();
                        tbIme.Visible = false;
                        lblIme.Visible = false;
                        lblOpis.Visible = false;
                        tbOpis.Visible = false;
                        lblDodatok.Visible = false;
                        btnDodatok.Visible = false;
                        lblCena.Visible = false;
                        tbCena.Visible = false;
                    }
                    catch (DuplicatePrimaryKeyException ex)
                    {
                        MessageBoxForm mf = new MessageBoxForm(ex.Message, false);
                        if (mf.ShowDialog() == DialogResult.Yes)
                            tbIme.Text = "";
                        else
                        {
                            tbIme.Visible = false;
                            lblIme.Visible = false;
                            lblOpis.Visible = false;
                            tbOpis.Visible = false;
                            lblDodatok.Visible = false;
                            btnDodatok.Visible = false;
                            lblCena.Visible = false;
                            tbCena.Visible = false;
                            IsChanged = false;
                            SelectedComponent = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        timer1.Stop();
                        ButtonFasapSetText(lblErrorMessage, ex.Message);
                        ButtonFasapSetVisible(lblErrorMessage, true);
                        timer1.Start();
                    }
                }
                else
                {
                    MenuComponent mc = new Meni(tbIme.Text);
                    mc.Parent = CurrMenu;
                    try
                    {
                        mc.SqlInsert(Conn, Restoran.RestoranID);
                        IsChanged = false;
                        Thread oThread = new Thread(new ThreadStart(KreirajMeni));
                        oThread.Start();
                        LoadingSemaphore.Release();
                        tbIme.Visible = false;
                        lblIme.Visible = false;
                    }
                    catch (DuplicatePrimaryKeyException ex)
                    {
                        MessageBoxForm mf = new MessageBoxForm(ex.Message, false);
                        if (mf.ShowDialog() == DialogResult.Yes)
                            tbIme.Text = "";
                        else
                        {
                            tbIme.Visible = false;
                            lblIme.Visible = false;
                            IsChanged = false;
                            SelectedComponent = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        timer1.Stop();
                        ButtonFasapSetText(lblErrorMessage, ex.Message);
                        ButtonFasapSetVisible(lblErrorMessage, true);
                        timer1.Start();
                    }
                }
            }
        }

        private void buttonFASAP4_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                MessageBoxForm mbf = new MessageBoxForm("Имате несочувани промени. Дали сакате да ги сочувате?");
                if (mbf.ShowDialog() == DialogResult.Yes)
                    SocuvajPromeni();
                else
                    IsChanged = false;
            }
            ButtonFasapSetVisible(lblIme, true);
            ButtonFasapSetVisible(tbIme, true);
            ButtonFasapSetVisible(lblOpis, true);
            ButtonFasapSetVisible(lblDodatok, true);
            ButtonFasapSetVisible(lblCena, true);
            ButtonFasapSetVisible(tbOpis, true);
            ButtonFasapSetVisible(btnDodatok, true);
            ButtonFasapSetVisible(tbCena, true);
            SelectedComponent = null;
            IsChanged = true;
            DodadiStavka = true;
        }

        private void buttonFASAP3_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                MessageBoxForm mbf = new MessageBoxForm("Имате несочувани промени. Дали сакате да ги сочувате?");
                if (mbf.ShowDialog() == DialogResult.Yes)
                    SocuvajPromeni();
                else
                    IsChanged = false;
            }
            ButtonFasapSetVisible(lblIme, true);
            ButtonFasapSetVisible(tbIme, true);
            ButtonFasapSetVisible(lblOpis, false);
            ButtonFasapSetVisible(lblDodatok, false);
            ButtonFasapSetVisible(lblCena, false);
            ButtonFasapSetVisible(tbOpis, false);
            ButtonFasapSetVisible(btnDodatok, false);
            ButtonFasapSetVisible(tbCena, false);
            SelectedComponent = null;
            IsChanged = true;
            DodadiStavka = false;
        }

        private void tbIme_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                errorProvider1.SetError(tb, "Ова поле не смее да биде празно!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tb, "");
                if (SelectedComponent != null && !SelectedComponent.GetName().Equals(tbIme.Text))
                    IsChanged = true;
                e.Cancel = false;
            }
        }

        private void tbOpis_Validating(object sender, CancelEventArgs e)
        {
            if (SelectedComponent != null && SelectedComponent.GetDescription() != null && !SelectedComponent.GetDescription().Equals(tbOpis.Text))
                IsChanged = true;
            if (SelectedComponent != null && SelectedComponent.GetDescription() == null && tbOpis.Text.Trim() != "")
                IsChanged = true;
        }

        private void tbCena_Validating(object sender, CancelEventArgs e)
        {
            if (tbIme.Text == "")
            {
                errorProvider1.SetError(tbIme, "Ова поле не смее да биде празно!");
                e.Cancel = true;
            }
            else
            {
                int pom;
                bool pom1 = int.TryParse(tbCena.Text, out pom);
                if (!pom1)
                {
                    errorProvider1.SetError(tbIme, "Ова поле може да биде само цел број!");
                    e.Cancel = true;
                }
                else
                {
                    if (SelectedComponent != null && SelectedComponent.ComputeCost() != pom)
                        IsChanged = true;
                    errorProvider1.SetError(tbIme, "");
                    e.Cancel = false;
                }
            }
        }
    }
}
