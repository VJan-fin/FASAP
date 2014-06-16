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
    /// <summary>
    /// Gi oznacuva site mozni parametri po koi moze
    /// da se vrsi podreduvanjeto
    /// </summary>
    public enum SortingArg
    {
        Broj,
        Ime,
        Prezime,
        Plata
    }

    /// <summary>
    /// Go oznacuva redosledot po koj se vrsi podreduvanjeto
    /// (Asc - rastecki; Desc - opagacki)
    /// </summary>
    public enum SortingOrder
    {
        Asc,
        Desc
    }

    public partial class ListaVraboteni : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }
        /// <summary>
        /// Site mozni vraboteni koi se vo bazata, a se povrzani
        /// so soodvetniot restoran
        /// </summary>
        private List<VrabotenInfo> AllEmployees { get; set; }
        /// <summary>
        /// Vrabotenite koi se prikazuvaat vo listata na formata
        /// </summary>
        private List<VrabotenInfo> ShowingEmployees { get; set; }
        /// <summary>
        /// Tekovniot vraboten cii podetalni podatoci se ispisani
        /// na formata
        /// </summary>
        private VrabotenInfo CurrentEmp { get; set; }

        /// <summary>
        /// Pomosna lista koja gi sodrzi labelite vo koi se 
        /// prikazuvaat iminjata i preziminjata na vrabotenite
        /// </summary>
        private List<LabelFASAP> ListaVrab { get; set; }
        private int indVrab { get; set; }
        private List<string> Pozicii { get; set; }
        private int PozInd { get; set; }
        private List<string> Statusi { get; set; }
        private int indStatus { get; set; }

        private SortingArg SortingParam { get; set; }
        private SortingOrder SortingStat { get; set; }
        /// <summary>
        /// Pomosna lista koja gi sodrzi kopcinjata koi go oznacuvaat
        /// nacinot na podreduvanje po soodvetniot atribut
        /// </summary>
        private List<PictureBox> OrderPics { get; set; }

        /*
        // samo za primer
        public ListaVraboteni()
        {
            InitializeComponent();
            this.Restoran = new Restoran() { RestoranID = 1, Ime = "Гостилница Лира" };
            string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
             + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
             + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            this.Init();
        }
        */

        public ListaVraboteni(Restoran restoran, OracleConnection conn)
        {
            InitializeComponent();
            this.Restoran = restoran;
            this.Conn = conn;
            this.Init();
        }

        /// <summary>
        /// Inicijalizacija na potrebnite kontroli
        /// </summary>
        public void Init()
        {
            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.lblImeRestoran.Text = this.Restoran.Ime + " ";
            this.AllEmployees = new List<VrabotenInfo>();
            this.ShowingEmployees = new List<VrabotenInfo>();
            this.SortingParam = SortingArg.Broj;
            this.SortingStat = SortingOrder.Asc;

            this.ListaVrab = new List<LabelFASAP>();
            this.ListaVrab.Add(lbl1);
            this.ListaVrab.Add(lbl2);
            this.ListaVrab.Add(lbl3);
            this.ListaVrab.Add(lbl4);
            this.ListaVrab.Add(lbl5);
            this.ListaVrab.Add(lbl6);
            this.ListaVrab.Add(lbl7);
            this.ListaVrab.Add(lbl8);
            this.ListaVrab.Add(lbl9);
            this.ListaVrab.Add(lbl10);
            this.indVrab = 0;

            this.OrderPics = new List<PictureBox>();
            this.OrderPics.Add(pbBroj);
            this.OrderPics.Add(pbIme);
            this.OrderPics.Add(pbPrezime);
            this.OrderPics.Add(pbPlata);

            this.VcitajPozicii();
            this.VcitajStatusi();
            this.VcitajVraboteni();
            this.PopolniVraboten();
        }

        /// <summary>
        /// Popolnuvanje na listata statusi koi moze da se
        /// pojavat kaj vrabotenite
        /// </summary>
        public void VcitajStatusi()
        {
            this.Statusi = new List<string>();
            this.indStatus = 0;

            this.Statusi.Add("1");
            this.Statusi.Add("0");
            this.Statusi.Add("Сите");

            this.UpdateStatusi();
        }

        /// <summary>
        /// Prezemanje na site mozni funkcii koi postojat vo bazata
        /// </summary>
        public void VcitajPozicii()
        {
            this.Pozicii = new List<string>();
            this.PozInd = 0;
            this.Pozicii.Add("Сите");

            string sqlPozicii = @"SELECT * FROM FUNKCIJA ORDER BY POZICIJA";
            OracleCommand cmd = new OracleCommand(sqlPozicii, this.Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    this.Pozicii.Add(dr.GetString(0));
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (this.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            this.UpdatePozicii();
        }

        /// <summary>
        /// Prezemanje na site potrebni podatoci za vrabotenite
        /// od bazata na podatoci
        /// </summary>
        public void VcitajVraboteni()
        {
            this.AllEmployees = new List<VrabotenInfo>();
            this.indVrab = 0;

            string sqlVraboteni = @"SELECT VRABOTEN_ID, IME_VRABOTEN, PREZIME_VRABOTEN, POZICIJA, PLATA, STATUS, IZVRSHENI_NARACHKI FROM VRABOTEN NATURAL JOIN IZVRSHUVA WHERE RESTORAN_ID = :RES_ID ORDER BY VRABOTEN_ID";
            OracleCommand cmd = new OracleCommand(sqlVraboteni, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int64);
                prm.Value = Restoran.RestoranID;
                cmd.Parameters.Add(prm);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    VrabotenInfo vr = new VrabotenInfo();
                    vr.VrabotenID = dr.GetInt32(0);
                    vr.Ime = dr.GetString(1);
                    vr.Prezime = dr.GetString(2);
                    vr.Pozicija = dr.GetString(3);
                    vr.Plata = dr.GetInt32(4);
                    int st;
                    if (int.TryParse(dr.GetString(5), out st))
                        vr.Status = st;
                    vr.Naracki = dr.GetInt32(6);
                    this.AllEmployees.Add(vr);
                    //samo privremeno
                    //this.ShowingEmployees.Add(vr);
                }
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (this.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            //this.FiltrirajVraboteni();
            this.UpdateScreen();
        }

        /// <summary>
        /// Azuriranje na prikazot na labelata so soodvetna sodrzina
        /// od listata pozicii
        /// </summary>
        private void UpdatePozicii()
        {
            if (this.Pozicii.Count != 0)
                this.lblPozicijaFilter.Text = this.Pozicii[this.PozInd];
            else
                this.lblPozicijaFilter.Text = " ";

            //this.FiltrirajVraboteni();
            this.UpdateScreen();
        }

        /// <summary>
        /// Azuriranje na prikazot na labelata so soodvetna sodrzina
        /// od listata statusi na vrabotenite
        /// </summary>
        private void UpdateStatusi()
        {
            if (this.Statusi.Count != 0)
            {
                if (this.Statusi[this.indStatus] == "0")
                    this.lblStatusFilter.Text = "Неактивен";
                else if (this.Statusi[this.indStatus] == "1")
                    this.lblStatusFilter.Text = "Активен";
                else
                    this.lblStatusFilter.Text = this.Statusi[this.indStatus];
            } 
            else
                this.lblStatusFilter.Text = " ";

            //this.FiltrirajVraboteni();
            this.UpdateScreen();
        }

        /// <summary>
        /// Popolnuvanje na listata vraboteni so objektite
        /// koi gi sodrzat informaciite za istite
        /// </summary>
        public void UpdateVrab()
        {
            int ind = this.indVrab;
            for (int i = 0; i < this.ListaVrab.Count; i++)
            {
                if (ind < this.ShowingEmployees.Count)
                {
                    this.ListaVrab[i].UpdateObject(this.ShowingEmployees[ind]);
                    ind++;
                }
                else
                    this.ListaVrab[i].UpdateObject(null);
            }

            //this.MarkSelection();
        }

        /// <summary>
        /// Metod kojsto vrsi obnovuvanje na podatocite na ekranot
        /// </summary>
        public void UpdateScreen()
        {
            this.FiltrirajVraboteni();
            //OrderEmployees.OrderByName(this.ShowingEmployees, false);
            this.PodrediVrab();
            this.UpdateVrab();
            this.MarkSelection();
        }

        /// <summary>
        /// Popolnuvanje na poziciite so soodvetnite
        /// informacii za selektiraniot vraboten
        /// </summary>
        public void PopolniVraboten()
        {
            if (this.CurrentEmp != null)
            {
                this.lblBrVraboten.Text = this.CurrentEmp.VrabotenID.ToString();
                this.lblImePrezime.Text = this.CurrentEmp.Ime + " " + this.CurrentEmp.Prezime + " ";
                this.lblPozicija.Text = this.CurrentEmp.Pozicija + " ";
                this.lblPlata.Text = this.CurrentEmp.Plata.ToString();
                if (this.CurrentEmp.Status == 0)
                    this.lblStatus.Text = "Неактивен ";
                else
                    this.lblStatus.Text = "Активен ";
            }
            else
            {
                this.lblBrVraboten.Text = ": :";
                this.lblImePrezime.Text = ": :";
                this.lblPozicija.Text = ": :";
                this.lblPlata.Text = ": :";
                this.lblStatus.Text = ": :";
            }
        }

        /// <summary>
        /// Oznacuvanje na poleto od listata vraboteni koe
        /// e tekovno selektirano, nezavisno od toa kade se
        /// naoga vo listata - vo zavisnost od vraboteniot cii
        /// podatoci se ispisuvaat
        /// </summary>
        private void MarkSelection()
        {
            foreach (var item in this.ListaVrab)
                if (this.CurrentEmp != null && this.CurrentEmp.Equals(item.LblObject as VrabotenInfo))
                {
                    item.Image = Resources.LabelBackgroundSelected;
                    if (item.LblObject != null && (item.LblObject as VrabotenInfo).Status == 1)
                        item.ForeColor = Color.SaddleBrown;
                    else
                        item.ForeColor = Color.Firebrick;
                    //item.ForeColor = SystemColors.InactiveCaptionText;
                }
                else
                {
                    item.Image = Resources.LabelBackground2;
                    if (item.LblObject != null && (item.LblObject as VrabotenInfo).Status == 1)
                        item.ForeColor = Color.Gold;
                    else if (item.LblObject == null)
                        item.ForeColor = Color.Gold;
                    else
                        item.ForeColor = Color.FromArgb(250, 20, 20);
                        //item.ForeColor = SystemColors.InactiveCaption;
                }
        }

        /// <summary>
        /// Prikazuvanje na vrabotenite spored odbranite filtri
        /// </summary>
        private void FiltrirajVraboteni()
        {
            this.ShowingEmployees = new List<VrabotenInfo>();
            foreach (var item in this.AllEmployees)
                this.ShowingEmployees.Add(item);

            if (this.lblPozicijaFilter.Text != "Сите")
                this.ShowingEmployees.RemoveAll(w => w.Pozicija != this.lblPozicijaFilter.Text);
            if (this.lblStatusFilter.Text != "Сите")
                this.ShowingEmployees.RemoveAll(w => w.Status.ToString() != this.Statusi[indStatus]);

            //MessageBox.Show(this.ShowingEmployees.Count.ToString());

            //this.UpdateVrab();
        }

        /// <summary>
        /// Sortiranje na listata vraboteni spored odbraniot parametar
        /// </summary>
        public void PodrediVrab()
        {
            if (this.SortingParam == SortingArg.Broj)
                OrderEmployees.OrderByID(this.ShowingEmployees, this.SortingStat == SortingOrder.Asc);
            else if (this.SortingParam == SortingArg.Ime)
                OrderEmployees.OrderByName(this.ShowingEmployees, this.SortingStat == SortingOrder.Asc);
            else if (this.SortingParam == SortingArg.Prezime)
                OrderEmployees.OrderBySurname(this.ShowingEmployees, this.SortingStat == SortingOrder.Asc);
            else if (this.SortingParam == SortingArg.Plata)
                OrderEmployees.OrderBySalary(this.ShowingEmployees, this.SortingStat == SortingOrder.Asc);
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                this.CurrentEmp = lb.LblObject as VrabotenInfo;
                this.MarkSelection();
                this.PopolniVraboten();
            }
        }

        private void pictureBoxUp_Click(object sender, EventArgs e)
        {
            if (this.ShowingEmployees.Count > this.ListaVrab.Count)
            {
                if (this.indVrab != 0)
                    this.indVrab--;
                //this.FiltrirajVraboteni();
                //this.MarkSelection();
                this.UpdateScreen();
            }
        }

        private void pictureBoxDown_Click(object sender, EventArgs e)
        {
            if (this.ShowingEmployees.Count > this.ListaVrab.Count)
            {
                if (this.indVrab < this.ShowingEmployees.Count - this.ListaVrab.Count)
                    this.indVrab++;
                //this.FiltrirajVraboteni();
                //this.MarkSelection();
                this.UpdateScreen();
            }
        }

        private void pictureBoxUp_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Hand;
            pb.Image = Resources.LightArrowUp;
        }

        private void pictureBoxUp_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Default;
            pb.Image = Resources.DarkArrowUp;
        }

        private void pictureBoxDown_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Hand;
            pb.Image = Resources.LightArrowDown;
        }

        private void pictureBoxDown_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Default;
            pb.Image = Resources.DarkArrowDown;
        }

        private void lblMeni1_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Hand;
                lb.Font = new Font("Trebuchet MS", 20, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void lblMeni1_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                Cursor = Cursors.Default;
                lb.Font = new Font("Trebuchet MS", 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void buttonOtkazi_MouseEnter(object sender, EventArgs e)
        {
            ButtonFASAP btn = sender as ButtonFASAP;
            Cursor = Cursors.Hand;
            btn.Image = Resources.LightButton___Copy;
            btn.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void buttonOtkazi_MouseLeave(object sender, EventArgs e)
        {
            ButtonFASAP btn = sender as ButtonFASAP;
            Cursor = Cursors.Default;
            btn.Image = Resources.DarkButton___Copy;
            btn.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void buttonOtkazi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDodadiVrab_Click(object sender, EventArgs e)
        {
            DodavanjeVraboten addEmpForm = new DodavanjeVraboten(this.Restoran, this.Conn);
            if (addEmpForm.ShowDialog() == DialogResult.Yes)
            {
                this.VcitajVraboteni();
                this.CurrentEmp = null;
                this.PopolniVraboten();
                this.MarkSelection();
            }
        }

        private void buttonPregledVrab_Click(object sender, EventArgs e)
        {
            if (this.CurrentEmp != null)
            {
                PregledVraboten viewEmpForm = new PregledVraboten(this.CurrentEmp.VrabotenID, this.Restoran, this.Conn);
                if (viewEmpForm.ShowDialog() == DialogResult.Yes)
                {
                    this.VcitajVraboteni();
                    this.CurrentEmp = null;
                    this.PopolniVraboten();
                    this.MarkSelection();
                }
                else
                {
                    this.VcitajVraboteni();
                    this.CurrentEmp = null;
                    this.PopolniVraboten();
                    this.MarkSelection();
                }
            }
            else
            {
                MessageBoxForm mbf = new MessageBoxForm("Немате одбрано вработен!\nОдберете вработен и обидете се повторно", false);
                mbf.ShowDialog();
            }
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Hand;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Default;
            pb.Image = Resources.DarkArrowLeft;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Hand;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Cursor = Cursors.Default;
            pb.Image = Resources.DarkArrowRight;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (this.Pozicii.Count > 1)
            {
                this.PozInd = (this.PozInd + 1) % this.Pozicii.Count;
                this.UpdatePozicii();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.Pozicii.Count > 1)
            {
                if (this.PozInd == 0)
                    this.PozInd = this.Pozicii.Count;
                this.PozInd--;
                this.UpdatePozicii();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (this.Statusi.Count > 1)
            {
                if (this.indStatus == 0)
                    this.indStatus = this.Statusi.Count;
                this.indStatus--;
                this.UpdateStatusi();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (this.Statusi.Count > 1)
            {
                this.indStatus = (this.indStatus + 1) % this.Statusi.Count;
                this.UpdateStatusi();
            }
        }

        private void labelFASAP18_MouseEnter(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            Cursor = Cursors.Hand;
            lb.Image = Resources.LightButton___Copy;
            lb.ForeColor = Color.SaddleBrown;
        }

        private void labelFASAP18_MouseLeave(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            Cursor = Cursors.Default;
            lb.Image = Resources.DarkButton___Copy;
            lb.ForeColor = Color.Khaki;
        }

        /// <summary>
        /// Promena na redosledot na podreduvanje
        /// </summary>
        private void ChangeOrder()
        {
            if (this.SortingStat == SortingOrder.Asc)
                this.SortingStat = SortingOrder.Desc;
            else
                this.SortingStat = SortingOrder.Asc;
        }

        /*
        /// <summary>
        /// Promena na oznakata za redosledot na podreduvanje
        /// na soodvetniot parametar po koj se vrsi istoto
        /// </summary>
        /// <param name="sender"></param>
        private void ChangeImage(object sender)
        {
            PictureBox pb = sender as PictureBox;
            if (this.SortingStat == SortingOrder.Asc)
                pb.Image = Resources.LightArrowUp;
            else
                pb.Image = Resources.LightArrowDown;
        }
        */

        /// <summary>
        /// Postavuvanje na predodreden izgled na kontrolite
        /// za podreduvanje na vrabotenite
        /// </summary>
        private void UpdateImages(PictureBox pb)
        {
            //PictureBox pb = sender as PictureBox;
            foreach (var item in this.OrderPics)
            {
                if (pb == item && this.SortingStat == SortingOrder.Asc)
                    item.Image = Resources.LightArrowUp;
                else if (pb == item && this.SortingStat == SortingOrder.Desc)
                    item.Image = Resources.LightArrowDown;
                else if (pb != item)
                    item.Image = Resources.DarkArrowUp;
            }
            /*
            this.pbBroj.Image = Resources.DarkArrowUp;
            this.pbIme.Image = Resources.DarkArrowUp;
            this.pbPrezime.Image = Resources.DarkArrowUp;
            this.pbPlata.Image = Resources.DarkArrowUp;
            */
        }

        private void labelFASAP18_Click(object sender, EventArgs e)
        {
            if (this.SortingParam == SortingArg.Broj)
                this.ChangeOrder();
            else
            {
                this.SortingParam = SortingArg.Broj;
                this.SortingStat = SortingOrder.Asc;
            }
            
            //MessageBox.Show(this.SortingStat.ToString());
            this.UpdateImages(pbBroj);
            this.UpdateScreen();
        }

        private void labelFASAP19_Click(object sender, EventArgs e)
        {
            if (this.SortingParam == SortingArg.Ime)
                this.ChangeOrder();
            else
            {
                this.SortingParam = SortingArg.Ime;
                this.SortingStat = SortingOrder.Asc;
            }

            //MessageBox.Show(this.SortingStat.ToString());
            this.UpdateImages(pbIme);
            this.UpdateScreen();
        }

        private void labelFASAP1_Click(object sender, EventArgs e)
        {
            if (this.SortingParam == SortingArg.Prezime)
                this.ChangeOrder();
            else
            {
                this.SortingParam = SortingArg.Prezime;
                this.SortingStat = SortingOrder.Asc;
            }

            //MessageBox.Show(this.SortingStat.ToString());
            this.UpdateImages(pbPrezime);
            this.UpdateScreen();
        }

        private void labelFASAP2_Click(object sender, EventArgs e)
        {
            if (this.SortingParam == SortingArg.Plata)
                this.ChangeOrder();
            else
            {
                this.SortingParam = SortingArg.Plata;
                this.SortingStat = SortingOrder.Asc;
            }

            //MessageBox.Show(this.SortingStat.ToString());
            this.UpdateImages(pbPlata);
            this.UpdateScreen();
        }
    }
}
