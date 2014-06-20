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
    public partial class DodavanjePozicija : BackgroundForm
    {
        private OracleConnection Conn { get; set; }

        /// <summary>
        /// Site mozni pozicii koi se vo bazata
        /// </summary>
        private List<string> AllFunctions { get; set; }
        /// <summary>
        /// Pomosna lista koja gi sodrzi labelite vo koi se 
        /// prikazuvaat poziciite
        /// </summary>
        private List<LabelFASAP> Pozicii { get; set; }
        private int PozInd { get; set; }
        /// <summary>
        /// Pomosna promenliva koja oznacuva koj e tekovniot
        /// rezim na rabota
        /// (true - Moze da se menuvaat podatocite; false)
        /// </summary>
        private bool ModifyMode { get; set; }
        private string CurrentPoz { get; set; }

        public DodavanjePozicija(OracleConnection conn)
        {
            InitializeComponent();
            this.Conn = conn;
            this.Init();
        }

        // samo za primer
        public DodavanjePozicija()
        {
            InitializeComponent();
            string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
             + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
             + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            this.Init();
        }

        /// <summary>
        /// Inicijalizacija na potrebnite kontroli
        /// </summary>
        public void Init()
        {
            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.ModifyMode = false;

            this.ChangeVisibility();

            this.Pozicii = new List<LabelFASAP>();
            this.Pozicii.Add(this.lbl1);
            this.Pozicii.Add(this.lbl2);
            this.Pozicii.Add(this.lbl3);
            this.Pozicii.Add(this.lbl4);
            this.Pozicii.Add(this.lbl5);
            this.Pozicii.Add(this.lbl6);

            this.VcitajPozicii();
        }

        /// <summary>
        /// Promena na vidlivosta na polinjata koi se skrieni
        /// </summary>
        private void ChangeVisibility()
        {
            this.lblIme.Visible = this.ModifyMode;
            this.tbImePozicija.Visible = this.ModifyMode;
            this.lblZab.Visible = this.ModifyMode;
            this.lblZad1.Visible = this.ModifyMode;
            this.btnDodadi.Visible = this.ModifyMode;
            this.btnDodadiPoz.Visible = !this.ModifyMode;
            this.btnOtstraniPoz.Visible = !this.ModifyMode;
            this.dbLayoutPanel2.Visible = !this.ModifyMode;
        }

        /// <summary>
        /// Prezemanje na site mozni funkcii koi postojat vo bazata
        /// </summary>
        public void VcitajPozicii()
        {
            this.AllFunctions = new List<string>();
            this.PozInd = 0;

            string sqlPozicii = @"SELECT * FROM FUNKCIJA ORDER BY POZICIJA";
            OracleCommand cmd = new OracleCommand(sqlPozicii, this.Conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    this.AllFunctions.Add(dr.GetString(0));
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            //MessageBox.Show(this.AllFunctions.Count.ToString());
            this.UpdatePozicii();
        }

        /// <summary>
        /// Azuriranje na prikazot na labelata so soodvetna sodrzina
        /// od listata pozicii
        /// </summary>
        private void UpdatePozicii()
        {
            int ind = this.PozInd;
            for (int i = 0; i < this.Pozicii.Count; i++)
            {
                if (ind < this.AllFunctions.Count)
                {
                    this.Pozicii[i].UpdateObject(this.AllFunctions[ind]);
                    ind++;
                }
                else
                    this.Pozicii[i].UpdateObject(null);
            }

            this.MarkSelection();
        }

        /// <summary>
        /// Oznacuvanje na poleto od listata pozicii koe
        /// e tekovno selektirano, nezavisno od toa kade se
        /// naoga vo listata
        /// </summary>
        private void MarkSelection()
        {
            foreach (var item in this.Pozicii)
            {
                if (this.CurrentPoz != null && this.CurrentPoz == (item.LblObject as string))
                {
                    item.Image = Resources.LabelBackgroundSelected;
                    item.ForeColor = Color.SaddleBrown;
                }
                else
                {
                    item.Image = Resources.LabelBackground2;
                    item.ForeColor = Color.Gold;
                }
            }
        }

        /// <summary>
        /// Gi azurira podatocite za funkcijata so menuvanje
        /// na vrednostite na soodvetnite atributi vo bazata
        /// </summary>
        private bool AzurirajPodatoci()
        {
            string sqlPoz = @"INSERT INTO FUNKCIJA (POZICIJA) VALUES (:POZ)";
            OracleCommand cmd = new OracleCommand(sqlPoz, this.Conn);

            try
            {
                OracleParameter prm = new OracleParameter("POZ", OracleDbType.Varchar2);
                prm.Value = this.tbImePozicija.Text;
                cmd.Parameters.Add(prm);
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            //cmd.ExecuteNonQuery();

            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                br = -1;
            }

            if (br == -1)
                return false;

            //this.PopolniInfo();
            return true;
        }

        /// <summary>
        /// Otstranuvanje na izbranata pozicija od bazata
        /// </summary>
        /// <returns></returns>
        private bool OtstraniPozicija()
        {
            string sqlPoz = @"DELETE FROM FUNKCIJA WHERE POZICIJA LIKE :POZ";
            OracleCommand cmd = new OracleCommand(sqlPoz, this.Conn);

            try
            {
                OracleParameter prm = new OracleParameter("POZ", OracleDbType.Varchar2);
                prm.Value = this.CurrentPoz;
                cmd.Parameters.Add(prm);
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата!", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }

            //cmd.ExecuteNonQuery();

            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                br = -1;
            }

            if (br == -1)
                return false;

            //this.PopolniInfo();
            return true;
        }

        private void pbListUp_Click(object sender, EventArgs e)
        {
            if (this.AllFunctions.Count > this.Pozicii.Count)
            {
                if (this.PozInd != 0)
                    this.PozInd--;
                //this.FiltrirajVraboteni();
                //this.MarkSelection();
                this.UpdatePozicii();
            }
        }

        private void pbListDown_Click(object sender, EventArgs e)
        {
            if (this.AllFunctions.Count > this.Pozicii.Count)
            {
                if (this.PozInd < this.AllFunctions.Count - this.Pozicii.Count)
                    this.PozInd++;
                //this.FiltrirajVraboteni();
                //this.MarkSelection();
                this.UpdatePozicii();
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

        private void lbl1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                this.CurrentPoz = lb.LblObject as string;
                this.MarkSelection();
                //this.PopolniVraboten();
            }
        }

        private void buttonDodadiVrab_Click(object sender, EventArgs e)
        {
            this.ModifyMode = true;
            this.ChangeVisibility();
        }

        private void tbImePozicija_Validating(object sender, CancelEventArgs e)
        {
            if (this.ModifyMode)
            {
                TextBox tb = (sender as TextBox);
                if (tb.Text.Trim() == "")
                {
                    MessageBoxForm mbf = new MessageBoxForm("Полето е задолжително!", false);
                    e.Cancel = true;
                    mbf.ShowDialog();
                }
                tb.SelectAll();
            }
        }

        private void buttonOtkazi_Click(object sender, EventArgs e)
        {
            if (this.ModifyMode)
            {
                MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да ги отфрлите промените?");
                if (mbf.ShowDialog() == DialogResult.Yes)
                {
                    //this.AutoValidate = AutoValidate.Disable;
                    this.ModifyMode = false;
                    this.ChangeVisibility();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void btnDodadi_Click(object sender, EventArgs e)
        {
            MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да ја додадете позицијата?");
            if (mbf.ShowDialog() == DialogResult.Yes)
            {
                if (this.AzurirajPodatoci())
                {
                    MessageBoxForm mbf1 = new MessageBoxForm("Позицијата беше успешно додадена!", false);
                    mbf1.ShowDialog();
                    this.ModifyMode = false;
                }
                else
                {
                    MessageBoxForm mbf1 = new MessageBoxForm("Позицијата не можеше да се додаде.\nОбидете се повторно.", false);
                    mbf1.ShowDialog();
                }
            }

            this.ChangeVisibility();
            this.VcitajPozicii();
        }

        private void btnOtstraniPoz_Click(object sender, EventArgs e)
        {
            if (this.CurrentPoz == null)
            {
                MessageBoxForm mbf = new MessageBoxForm("Немате одбрано позиција!\nОдберете позиција и обидете се повторно", false);
                mbf.ShowDialog();
            }
            else
            {
                MessageBoxForm mbf = new MessageBoxForm("Дали сте сигурни дека сакате да ја отстраните позицијата " + this.CurrentPoz);
                if (mbf.ShowDialog() == DialogResult.Yes)
                {
                    if (this.OtstraniPozicija())
                    {
                        MessageBoxForm mbf1 = new MessageBoxForm("Позицијата беше успешно отстранета!", false);
                        mbf1.ShowDialog();
                    }
                    else
                    {
                        MessageBoxForm mbf1 = new MessageBoxForm("Обидот за отстранување на позицијата не беше успешен.\nПостојат вработени кои ја извршуваат таа функција.", false);
                        mbf1.ShowDialog();
                    }
                }
            }

            this.VcitajPozicii();
        }
    }
}
