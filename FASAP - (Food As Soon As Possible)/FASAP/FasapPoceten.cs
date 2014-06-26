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

namespace SmetkaZaNaracka
{
    public partial class FasapPoceten : BackgroundForm
    {
        private OracleConnection Conn { get; set; }

        private List<Restoran> Restorani { get; set; }
        private List<Restoran> ShowRestorani { get; set; }
        private List<String> Najbarani { get; set; }
        private List<String> Najbarani5 { get; set; }
        private List<String> Gradovi { get; set; }
        private List<String> Kategorija { get; set; }

        private List<LabelFASAP> labeliNajbarani { get; set; }
        private List<LabelFASAP> labeliGrad { get; set; }
        private List<LabelFASAP> labeliKategorija { get; set; }
        private List<LabelFASAP> labeliRestorani { get; set; }

        private int indexG { get; set; }
        private int indexK { get; set; }
        private int indexR { get; set; }

        private String tekovenGrad { get; set; }
        private String tekovnaKat { get; set; }

        public FasapPoceten()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            DoubleBuffered = true;
            Opacity = 0;
            Restorani = new List<Restoran>();
            Najbarani = new List<String>();
            Najbarani5 = new List<String>();
            Gradovi = new List<String>();
            Kategorija = new List<String>();

            Kategorija.Add("Сите");
            Gradovi.Add("Сите");

            labeliNajbarani = new List<LabelFASAP>();
            labeliNajbarani.Add(lblNaj1);
            labeliNajbarani.Add(lblNaj2);
            labeliNajbarani.Add(lblNaj3);
            labeliNajbarani.Add(lblNaj4);
            labeliNajbarani.Add(lblNaj5);

            labeliGrad = new List<LabelFASAP>();
            labeliGrad.Add(lblGrad1);
            labeliGrad.Add(lblGrad2);
            labeliGrad.Add(lblGrad3);
            labeliGrad.Add(lblGrad4);

            labeliKategorija = new List<LabelFASAP>();
            labeliKategorija.Add(lblKat1);
            labeliKategorija.Add(lblKat2);
            labeliKategorija.Add(lblkat3);
            labeliKategorija.Add(lblKat4);
            labeliKategorija.Add(lblKat5);

            labeliRestorani = new List<LabelFASAP>();
            labeliRestorani.Add(lblRest1);
            labeliRestorani.Add(lblRest2);
            labeliRestorani.Add(lblRest3);
            labeliRestorani.Add(lblRest4);
            labeliRestorani.Add(lblRest5);
            labeliRestorani.Add(lblRest6);
            labeliRestorani.Add(lblRest7);
            labeliRestorani.Add(lblRest8);

            indexG = 0;
            indexK = 0;
            indexR = 0;

            tekovenGrad = "Сите";
            tekovnaKat = "Сите";
        }
        private void FasapPoceten_Load(object sender, EventArgs e)
        {
            string oradb = "Data Source=(DESCRIPTION="
           + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
           + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
           + "User Id=DBA_20132014L_GRP_020;Password=7734924;";
            try
            {
                Conn = new OracleConnection();
                Conn.ConnectionString = oradb;
                Conn.Open();
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!Проверете ја конекцијата", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }
            vcituvanje();
            popolniLabeli();
            MarkSelection();

        }
        public void refresh()
        {
            indexG = 0;
            indexK = 0;
            indexR = 0;
            tekovenGrad = "Сите";
            tekovnaKat = "Сите";
            vcituvanje();
            MarkSelection();
            popolniLabeli();
            //MessageBox.Show("I'm refreshing :p");

        }
        private void FiltrirajRestorani()
        {
            this.ShowRestorani = new List<Restoran>(Restorani);

            if (this.tekovenGrad != "Сите")
                this.ShowRestorani.RemoveAll(w => w.Grad != tekovenGrad);
            if (this.tekovnaKat != "Сите")
                this.ShowRestorani.RemoveAll(w => w.Kategorija != this.tekovnaKat);

            this.popolniLabeliRestorani();
        }
        public void popolniLabeli()
        {
            popolniLabeliNajbarani();
            popolniLabeliGrad();
            popolniLabeliKategorija();
            popolniLabeliRestorani();
        }
        public void popolniLabeliNajbarani()
        {

            int ind = 0;
            for (int i = 0; i < this.labeliNajbarani.Count; i++)
            {
                if (ind < this.Najbarani5.Count)
                {
                    this.labeliNajbarani[i].UpdateObject(this.Najbarani5[ind]);
                    ind++;
                }
                else
                    this.labeliNajbarani[i].UpdateObject(null);
            }
        }
        public void popolniLabeliGrad()
        {
            int ind = this.indexG;
            for (int i = 0; i < this.labeliGrad.Count; i++)
            {
                if (ind < this.Gradovi.Count)
                {
                    if (Gradovi[ind].Equals(tekovenGrad))
                    {
                        this.labeliGrad[i].Image = Resources.LabelBackgroundSelected;
                        this.labeliGrad[i].ForeColor = Color.SaddleBrown;
                    }
                    else
                    {
                        this.labeliGrad[i].Image = Resources.LabelBackground2;
                        this.labeliGrad[i].ForeColor = Color.White;
                    }
                    this.labeliGrad[i].UpdateObject(this.Gradovi[ind]);
                    ind++;
                }
                else
                {
                    this.labeliGrad[i].UpdateObject(null);
                    this.labeliGrad[i].Image = Resources.LabelBackground2;
                    this.labeliGrad[i].ForeColor = Color.White;
                }
            }
            //clearLabels();
            // for (int i = indexG; i < indexG + 4; i++)
            // {
            //  if (i >= Gradovi.Count) break;
            // labeliGrad[i - indexG].Text = String.Format(": {0} :", Gradovi[i]);
            //}
        }
        public void popolniLabeliKategorija()
        {
            int ind = this.indexK;
            for (int i = 0; i < this.labeliKategorija.Count; i++)
            {
                if (ind < this.Kategorija.Count)
                {
                    if (Kategorija[ind] == tekovnaKat)
                    {
                        this.labeliKategorija[i].Image = Resources.LabelBackgroundSelected;
                        this.labeliKategorija[i].ForeColor = Color.SaddleBrown;
                    }
                    else
                    {
                        this.labeliKategorija[i].Image = Resources.LabelBackground2;
                        this.labeliKategorija[i].ForeColor = Color.White;
                    }
                    this.labeliKategorija[i].UpdateObject(this.Kategorija[ind]);
                    ind++;
                }
                else
                {
                    this.labeliKategorija[i].UpdateObject(null);
                    this.labeliKategorija[i].Image = Resources.LabelBackground2;
                    this.labeliKategorija[i].ForeColor = Color.White;
                }
            }

            // for (int i = indexK; i < indexK + 5; i++)
            // {
            //    if (i >= Kategorija.Count) break;
            //  labeliKategorija[i - indexK].Text = String.Format(": {0} :", Kategorija[i]);
            //  }
        }
        public void popolniLabeliRestorani()
        {
            int ind = this.indexR;
            for (int i = 0; i < this.labeliRestorani.Count; i++)
            {
                if (ind < this.ShowRestorani.Count)
                {
                    this.labeliRestorani[i].UpdateObject(this.ShowRestorani[ind]);
                    ind++;
                }
                else
                    this.labeliRestorani[i].UpdateObject(null);
            }
            // for (int i = indexR; i < indexR + 8; i++)
            // {
            //     if (i >= ShowRestorani.Count) break;
            //     labeliRestorani[i - indexR].Text = String.Format(": {0} :", ShowRestorani[i]);
            // }
        }
        public void vcituvanje()
        {
            vcitajNajbarani();
            popolniNajbarani5();
            vcitajKategorija();
            vcitajGrad();
            vcitajRestorani();
            ShowRestorani = new List<Restoran>(Restorani);

        }
        public void vcitajNajbarani()
        {
            Najbarani = new List<String>();
            string sql = "SELECT IME_RESTORAN, ONLINE_SMETKI, KLIENTI FROM ( SELECT R.IME_RESTORAN, G.SMETKI ONLINE_SMETKI, G.KLIENTI FROM RESTORAN R JOIN ( SELECT RES.RESTORAN_ID, COUNT(ONL.RESTORAN_ID) AS SMETKI, COUNT(DISTINCT ONL.IME_KLIENT || ONL.PREZIME_KLIENT || ADRESA_ZA_DOSTAVA) AS KLIENTI FROM RESTORAN RES LEFT OUTER JOIN NARACHKA NAR ON RES.RESTORAN_ID = NAR.RESTORAN_ID AND MONTHS_BETWEEN(SYSDATE, NAR.VREME) <= 1 LEFT OUTER JOIN ONLINE_NARACHKA ONL ON NAR.RESTORAN_ID = ONL.RESTORAN_ID AND NAR.NARACHKA_ID = ONL.NARACHKA_ID GROUP BY RES.RESTORAN_ID ) G ON R.RESTORAN_ID = G.RESTORAN_ID ORDER BY G.SMETKI DESC, G.KLIENTI DESC, R.IME_RESTORAN ASC )"; // C#
            OracleCommand cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleDataReader dr = cmd.ExecuteReader(); // C#
                int i = 0;
                while (dr.Read())
                {
                    //
                    // lbNajbarani.Items.Add((i + 1) + ". " + dr.GetString(0));
                    Najbarani.Add( dr.GetString(0));
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

        }
        public void popolniNajbarani5()
        {
            Najbarani5 = new List<String>();
            for (int i = 0; i < 5; i++)
            {
                if (i >= Najbarani.Count) break;
                Najbarani5.Add(Najbarani[i]);
            }
        }
        public void vcitajKategorija()
        {
            Kategorija = new List<String>();
            Kategorija.Add("Сите");
            string sql = "Select distinct KATEGORIJA from RESTORAN ORDER BY KATEGORIJA"; // C#
            OracleCommand cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Kategorija.Add(dr.GetString(0));
                }
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

        }
        public void vcitajGrad()
        {
            Gradovi = new List<String>();
            Gradovi.Add("Сите");
            string sql = "Select distinct GRAD from RESTORAN ORDER BY GRAD"; // C#
            OracleCommand cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        Gradovi.Add(dr.GetString(0));
                    }

                }
            }
            catch
                (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }
        }
        public void vcitajRestorani()
        {
            Restorani = new List<Restoran>();
            string sql = "Select * from RESTORAN"; // C#
            OracleCommand cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                Restoran res;
                while (dr.Read())
                {
                    res = new Restoran();
                    Object LogoUrl = dr.GetValue(13);
                    if (LogoUrl == null)
                        res.LogoUrl = null;
                    else res.LogoUrl = LogoUrl as String;
                    res.RestoranID = (int)dr.GetValue(0);
                    res.Ime = dr.GetString(2);
                    if (!dr.IsDBNull(3))
                    {
                        res.Ulica = dr.GetString(3);
                    }
                    else res.Ulica = "";
                    if (!dr.IsDBNull(4))
                    {
                        res.Grad = dr.GetString(4);
                    }
                    else res.Grad = "";

                    res.Rejting = (float)dr.GetValue(5);
                    if (!dr.IsDBNull(6))
                    {
                        res.RabotnoVreme = dr.GetString(6);
                    }
                    else res.RabotnoVreme = "";
                    if (!dr.IsDBNull(7))
                    {
                        res.Kapacitet = dr.GetInt16(7);
                    }
                    else res.Kapacitet = null;
                    if (!dr.IsDBNull(8))
                    {
                        res.BrojMasi = dr.GetInt16(8);
                    }
                    else res.BrojMasi = null;
                    if (!dr.IsDBNull(9))
                    {
                        res.CenaZaDostava = dr.GetInt16(9);
                    }
                    else res.CenaZaDostava = null;
                    if (!dr.IsDBNull(10))
                    {
                        res.PragZaDostava = dr.GetInt16(10);
                    }
                    else res.PragZaDostava = null;
                    if (!dr.IsDBNull(11))
                    {
                        res.DatumNaOtvoranje = dr.GetDateTime(11);
                    }
                    else res.DatumNaOtvoranje = null;

                    res.Kategorija = dr.GetString(12);

                    Restorani.Add(res);

                }
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }


            sql = "Select * from IMENIK"; // C#
            cmd = new OracleCommand(sql, Conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                OracleDataReader drr = cmd.ExecuteReader();

                while (drr.Read())
                    foreach (var obj in Restorani)
                        if (obj.RestoranID == (int)drr.GetValue(0))
                            obj.Kontakt.Add(new Telefon(drr.GetString(1)));
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }
        }
        private void MarkSelection()
        {
            foreach (var item in this.labeliGrad)
            {
                if (this.tekovenGrad != null && this.tekovenGrad.Equals(item.LblObject as String))
                {
                    item.Image = Resources.LabelBackgroundSelected;
                    item.ForeColor = Color.SaddleBrown;

                }
                else
                {
                    item.Image = Resources.LabelBackground2;
                    item.ForeColor = Color.White;
                }
            }
            foreach (var item in this.labeliKategorija)
            {
                if (this.tekovnaKat != null && this.tekovnaKat.Equals(item.LblObject as String))
                {
                    item.Image = Resources.LabelBackgroundSelected;
                    item.ForeColor = Color.SaddleBrown;

                }
                else
                {
                    item.Image = Resources.LabelBackground2;
                    item.ForeColor = Color.White;
                }
            }
        }

        private void lblGrad1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                this.tekovenGrad = lb.LblObject as String;
                MarkSelection();
                indexR = 0;
                FiltrirajRestorani();
            }
            //  MessageBox.Show("Tekoven grad: " +tekovenGrad+"tekovna k: "+tekovnaKat);

        }

        private void lblKat1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                this.tekovnaKat = lb.LblObject as String;
                MarkSelection();
                indexR = 0;
                FiltrirajRestorani();
            }
            // MessageBox.Show("Tekoven grad: " + tekovenGrad + "tekovna k: " + tekovnaKat);

        }

        private void lblMeni1_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb == null)
                MessageBox.Show("Ednakvo na null vo MouseEnter");
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                if(lb.LblObject is Restoran)
                    lb.Font = new Font("Trebuchet MS", 19, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                else lb.Font = new Font("Trebuchet MS", 19, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lblMeni1_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb == null)
                MessageBox.Show("Ednakvo na null vo MouseLeave");
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                if(lb.LblObject is Restoran)
                    lb.Font = new Font("Trebuchet MS", 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                else lb.Font = new Font("Trebuchet MS", 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lblRest1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Restoran obj = lb.LblObject as Restoran;
                IzvrsuvanjeNaracka fasapNaracka = new IzvrsuvanjeNaracka(obj, Conn);
                if (fasapNaracka.ShowDialog() == DialogResult.OK)
                {
                    refresh();

                }
                else
                {
                    refresh();

                }

            }
        }


        private void btnInfo_Click(object sender, EventArgs e)
        {
            InfoForma i = new InfoForma(Conn);
            i.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login l = new Login(Conn);
            if (l.ShowDialog() == DialogResult.OK)
            {
                refresh();

            }
            else
            {
                refresh();

            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            DodavanjeRestoran r = new DodavanjeRestoran(Conn);
            if (r.ShowDialog() == DialogResult.OK)
            {
                refresh();
              
            }
            else
            {
                refresh();
               
            }
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            SearchFilter sf = new SearchFilter(Restorani, Conn);
            sf.Show();
        }

        private void btnInfo_MouseEnter(object sender, EventArgs e)
        {
            ButtonFASAP btn = sender as ButtonFASAP;
            Cursor = Cursors.Hand;
            btn.Image = Resources.LightButton___Copy;
            btn.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnInfo_MouseLeave(object sender, EventArgs e)
        {
            ButtonFASAP btn = sender as ButtonFASAP;
            Cursor = Cursors.Default;
            btn.Image = Resources.DarkButton___Copy;
            btn.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void pbGradUp_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Hand;
            pb.Image = Resources.LightArrowUp;
        }

        private void pbGradUp_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Default;
            pb.Image = Resources.DarkArrowUp;
        }

        private void pbGradDown_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Hand;
            pb.Image = Resources.LightArrowDown;
        }

        private void pbGradDown_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Default;
            pb.Image = Resources.DarkArrowDown;
        }

        private void pbGradUp_Click(object sender, EventArgs e)
        {
            if (this.Gradovi.Count > this.labeliGrad.Count)
            {
                if (this.indexG != 0)
                    this.indexG--;
                popolniLabeliGrad();
            }
        }

        private void pbGradDown_Click(object sender, EventArgs e)
        {
            if (this.Gradovi.Count > this.labeliGrad.Count)
            {
                if (this.indexG < this.Gradovi.Count - this.labeliGrad.Count)
                    this.indexG++;
                popolniLabeliGrad();
            }
        }

        private void pbKatUp_Click(object sender, EventArgs e)
        {
            if (this.Kategorija.Count > this.labeliKategorija.Count)
            {
                if (this.indexK != 0)
                    this.indexK--;
                popolniLabeliKategorija();
            }
        }

        private void pbKatDown_Click(object sender, EventArgs e)
        {
            if (this.Kategorija.Count > this.labeliKategorija.Count)
            {
                if (this.indexK < this.Kategorija.Count - this.labeliKategorija.Count)
                    this.indexK++;
                popolniLabeliKategorija();
            }
        }

        private void pbRestDown_Click(object sender, EventArgs e)
        {
            if (this.ShowRestorani.Count > this.labeliRestorani.Count)
            {
                if (this.indexR < this.ShowRestorani.Count - this.labeliRestorani.Count)
                    this.indexR++;
                popolniLabeliRestorani();
            }
        }

        private void pbRestUp_Click(object sender, EventArgs e)
        {
            if (this.ShowRestorani.Count > this.labeliRestorani.Count)
            {
                if (this.indexR != 0)
                    this.indexR--;
                popolniLabeliRestorani();
            }
        }

        // labelite za Najbarani restrani sodrzat samo iminja na restorani
        // zatoa vo listata so restorani go baram restoranot koj go ima imeto na labelata sto e kliknata
        
        private void lblNaj1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                String restIme = lb.LblObject as String;
                Restoran obj=null;
                foreach (Restoran r in Restorani)
                {
                    if(restIme.Equals(r.Ime))
                    {
                        obj = r; break;
                    }
                }
                IzvrsuvanjeNaracka fasapNaracka = new IzvrsuvanjeNaracka(obj, Conn);
                if (fasapNaracka.ShowDialog() == DialogResult.OK)
                {
                    refresh();

                }
                else
                {
                    refresh();

                }

            }
        }

        private void lblNaj1_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb == null)
                MessageBox.Show("Ednakvo na null vo MouseLeave");
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                lb.Font = new Font("Trebuchet MS", 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lblNaj5_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb == null)
                MessageBox.Show("Ednakvo na null vo MouseEnter");
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                lb.Font = new Font("Trebuchet MS", 19, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }




    }



}
