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
using System.Globalization;

namespace SmetkaZaNaracka.Izvestai
{
    public partial class PregledPoRegioni : BackgroundForm
    {
        public Restoran Restoran { get; set; }
        public OracleConnection Conn { get; set; }
        public Semaphore LoadingSemaphore { get; set; }
        public List<Naracki.Region> Regioni { get; set; }
        public List<Naracki.Region> RegioniPom { get; set; }
        public int Ind { get; set; }
        public List<LabelFASAP> LabeliRegioni { get; set; }
        public List<LabelFASAP> LabeliNaracki { get; set; }
        public bool Postavuva { get; set; }
        public Comparer<Naracki.Region> CurrComparer { get; set; }
        public Comparer<Naracki.Region> NameAsc { get; set; }
        public Comparer<Naracki.Region> NameDesc { get; set; }
        public Comparer<Naracki.Region> ProcentAsc { get; set; }
        public Comparer<Naracki.Region> ProcentDesc { get; set; }

        public PregledPoRegioni(OracleConnection conn, Restoran restoran)
        {
            InitializeComponent();
            Ind = 0;
            Opacity = 0;
            Restoran = restoran;
            Conn = conn;
            LoadingSemaphore = new Semaphore(0, 10);
            Regioni = new List<Naracki.Region>();
            RegioniPom = new List<Naracki.Region>();
            NameAsc = new Naracki.RegionNameAscCoparer();
            NameDesc = new Naracki.RegionNameDescCoparer();
            ProcentAsc = new Naracki.RegionProcentAscCoparer();
            ProcentDesc = new Naracki.RegionProcentDescCoparer();
            CurrComparer = ProcentDesc;
            lblIme.UpdateObject(null);
            lblPrezime.UpdateObject(null);
            lblSuma.UpdateObject(null);
            lblAdresa.UpdateObject(null);
            LabeliRegioni = new List<LabelFASAP>();
            LabeliNaracki = new List<LabelFASAP>();
            LabeliRegioni.Add(labelFASAP41);
            LabeliRegioni.Add(labelFASAP42);
            LabeliRegioni.Add(labelFASAP43);
            LabeliRegioni.Add(labelFASAP44);
            LabeliRegioni.Add(labelFASAP45);
            LabeliRegioni.Add(labelFASAP46);
            LabeliRegioni.Add(labelFASAP47);
            LabeliRegioni.Add(labelFASAP48);

            LabeliNaracki.Add(labelFASAP31);
            LabeliNaracki.Add(labelFASAP32);
            LabeliNaracki.Add(labelFASAP33);
            LabeliNaracki.Add(labelFASAP34);
            LabeliNaracki.Add(labelFASAP35);
            LabeliNaracki.Add(labelFASAP36);
            LabeliNaracki.Add(labelFASAP37);
            LabeliNaracki.Add(labelFASAP38);
            Thread oThread = new Thread(new ThreadStart(PostaviNajverenKlient));
            oThread.Start();
            oThread = new Thread(new ThreadStart(PrevzemiPodatociZaRegioni));
            if(ValidateChildren())
                oThread.Start();
        }

        public void PostaviNajverenKlient()
        {
            string sql = @"SELECT *
                        FROM (
                        SELECT ONL.IME_KLIENT AS IME, ONL.PREZIME_KLIENT AS PREZIME, ONL.ADRESA_ZA_DOSTAVA AS ADRESA, SUM(VKUPNA_CENA) AS SUMA
                        FROM RESTORAN RES JOIN NARACHKA NAR ON RES.RESTORAN_ID = NAR.RESTORAN_ID
                        JOIN ONLINE_NARACHKA ONL ON NAR.RESTORAN_ID = ONL.RESTORAN_ID AND NAR.NARACHKA_ID = ONL.NARACHKA_ID
                        WHERE RES.RESTORAN_ID = :RES_ID
                        GROUP BY (RES.RESTORAN_ID, ONL.ADRESA_ZA_DOSTAVA, ONL.IME_KLIENT, ONL.PREZIME_KLIENT)
                        ORDER BY SUM(VKUPNA_CENA) DESC
                        )";
            OracleCommand cmd = new OracleCommand(sql, Conn);

            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int64);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            string ime = null;
            string prezime = null;
            string suma = null;
            string adresa = null;
            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ime = dr.GetString(0);
                    prezime = dr.GetString(1);
                    suma = dr.GetInt32(3).ToString();
                    adresa = dr.GetString(2).ToString();
                    break;
                }
            }
            catch (Exception)
            {
                SetText(lblErrorMessage, "Имате проблем со поврзување на конекцијата");
                SetVisible(lblErrorMessage, true);
            }
            LoadingSemaphore.WaitOne();
            SetObject(lblIme, ime);
            SetObject(lblPrezime, prezime);
            SetObject(lblSuma, suma);
            SetObject(lblAdresa, adresa);
        }

        private void buttonFASAP4_MouseEnter(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            Cursor = Cursors.Hand;
            lb.Image = Resources.LightButton___Copy;
            lb.ForeColor = Color.SaddleBrown;
        }

        private void buttonFASAP4_MouseLeave(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            Cursor = Cursors.Default;
            lb.Image = Resources.DarkButton___Copy;
            lb.ForeColor = Color.Khaki;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Image = Resources.LightArrowUp;
            Cursor = Cursors.Hand;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = Resources.DarkArrowUp;
            Cursor = Cursors.Default;
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.Image = Resources.LightArrowDown;
            Cursor = Cursors.Hand;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Image = Resources.DarkArrowDown;
            Cursor = Cursors.Default;
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

        public override void LoadingMethod()
        {
            LoadingSemaphore.Release(2);
        }

        public void PrevzemiPodatociZaRegioni()
        {
            DateTime Od = DateTime.ParseExact(tbDatumOd.Text + " 00:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime Do = DateTime.ParseExact(tbDatumDo.Text + " 23:59", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            string sql = @"select 
	                        r.region, 
	                        NVL(period_rg.period, 0) as BROJ_NARACHKI, ROUND(nvl(period_rg.period/period_vk.period * 100, 0), 2) AS PROCENT
                        from
	                        (
		                        select distinct SUBSTR(adresa_za_dostava, 1, INSTR(adresa_za_dostava,'""', 1, 1) - 2) AS REGION
		                        from online_narachka
		                        where restoran_id = :RES_ID and adresa_za_dostava is not null
	                        ) r
	                        natural left outer join
	                        (
		                        select count(*) as period
		                        from
		                        (
			                        select narachka_id
			                        from online_narachka
			                        where restoran_id = :RES_ID1 and adresa_za_dostava is not null
		                        ) n
		                        natural join
		                        (
			                        select narachka_id
			                        from narachka
			                        where restoran_id = :RES_ID2 AND vreme >= :DatumOd AND vreme <= :DatumDo
		                        ) o
	                        ) period_vk
	                        left outer join
	                        (
		                        select n.region, count(*) as period
		                        from
		                        (
			                        select narachka_id, SUBSTR(adresa_za_dostava, 1, INSTR(adresa_za_dostava,'""', 1, 1) - 2) AS REGION
			                        from online_narachka
                                                where restoran_id = :RES_ID3 and adresa_za_dostava is not null
		                        ) n
		                        natural join
		                        (
			                        select narachka_id
			                        from narachka
			                        where restoran_id = :RES_ID4 AND vreme >= :Od AND vreme <= :Do
		                        ) o
		                        group by n.region
	                        ) period_rg on period_rg.region = r.region
                        order by ROUND(nvl(period_rg.period/period_vk.period * 100, 0), 2) desc, r.region asc";
		
            OracleCommand cmd = new OracleCommand(sql, Conn);

            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int32);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("RES_ID1", OracleDbType.Int32);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("RES_ID2", OracleDbType.Int32);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("DatumOd", OracleDbType.Date);
            prm.Value = Od;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("DatumDo", OracleDbType.Date);
            prm.Value = Do;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("RES_ID3", OracleDbType.Int32);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("RES_ID4", OracleDbType.Int32);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Od", OracleDbType.Date);
            prm.Value = Od;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Do", OracleDbType.Date);
            prm.Value = Do;
            cmd.Parameters.Add(prm);

            Regioni = new List<Naracki.Region>();
            try
            {
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Naracki.Region reg = new Naracki.Region(dr.GetString(0), dr.GetInt32(1), dr.GetDecimal(2));
                    Regioni.Add(reg);
                }
            }
            catch (Exception ex)
            {
                SetText(lblErrorMessage, "Имате проблем со поврзување на конекцијата");
                SetVisible(lblErrorMessage, true);
            }
            LoadingSemaphore.WaitOne();
            PostaviRegioni();
        }

        public void PostaviRegioni()
        {
            RegioniPom = new List<Naracki.Region>();
            foreach (var obj in Regioni)
                if (textBox1.Text.Trim().Length == 0 || textBox1.ForeColor.Equals(Color.Khaki))
                    RegioniPom.Add(obj);
                else if (obj.Sodrzi(textBox1.Text))
                    RegioniPom.Add(obj);
            RegioniPom.Sort(CurrComparer);

            int ind = this.Ind;
            for (int i = 0; i < LabeliRegioni.Count; i++)
                if (ind < RegioniPom.Count)
                {
                    SetObject(LabeliRegioni[i], RegioniPom[ind].Ime);
                    SetObject(LabeliNaracki[i], String.Format("{0} нарачки -- {1}%", RegioniPom[ind].BrojNaracki, RegioniPom[ind].Procent));
                    ind++;
                }
                else
                {
                    SetObject(LabeliRegioni[i], null);
                    SetObject(LabeliNaracki[i], null);
                }
            Postavuva = false;
        }

        public void textBox1Focus()
        {
            textBox1.Visible = false;
            this.Focus();
            textBox1.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.ForeColor == Color.Khaki)
                return;
            PostaviRegioni();
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
                textBox1.Text = "Пребарај  - Региони";
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
                pictureBox3.BackColor = Color.SaddleBrown;
            }
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.ForeColor = Color.Khaki;
                textBox1.BackColor = Color.Sienna;
                pictureBox3.BackColor = Color.Sienna;
                textBox1.Font = new Font("Trebuchet MS", 12, FontStyle.Italic);
                textBox1.Text = "Пребарај - Региони";
            }
        }

        private void btnSortIme_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (CurrComparer is Naracki.RegionNameDescCoparer)
            {
                pbRegion.Image = Resources.LightArrowUp;
                CurrComparer = NameAsc;
            }
            else
            {
                pbProcent.Image = Resources.DarkArrowDown;
                pbRegion.Image = Resources.LightArrowDown;
                CurrComparer = NameDesc;
            }
            PostaviRegioni();
        }

        private void btnProcent_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (CurrComparer is Naracki.RegionProcentDescCoparer)
            {
                pbProcent.Image = Resources.LightArrowUp;
                CurrComparer = ProcentAsc;
            }
            else
            {
                pbRegion.Image = Resources.DarkArrowDown;
                pbProcent.Image = Resources.LightArrowDown;
                CurrComparer = ProcentDesc;
            }
            PostaviRegioni();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (this.Ind != 0)
            {
                this.Ind--;
                this.PostaviRegioni();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (this.RegioniPom.Count > this.LabeliRegioni.Count)
            {
                if (this.Ind < this.RegioniPom.Count - this.LabeliRegioni.Count)
                    this.Ind++;
                this.PostaviRegioni();
            }
        }

        private void buttonFASAP4_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (!ValidateChildren())
                return;
            PostaviDatum();
        }

        public void PostaviDatum()
        {
            if (!ValidateChildren() || Postavuva)
                return;
            Postavuva = true;
            pbProcent.Image = Resources.LightArrowDown;
            pbRegion.Image = Resources.DarkArrowDown;
            Thread oThread = new Thread(new ThreadStart(PrevzemiPodatociZaRegioni));
            oThread.Start();
            LoadingSemaphore.Release();
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            tbDatumOd.Text = String.Format("{0:00}.{1:00}.{2}", 1, DateTime.Now.Month, DateTime.Now.Year);
            tbDatumDo.Text = String.Format("{0:00}.{1:00}.{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            PostaviDatum();
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            tbDatumOd.Text = String.Format("{0:00}.{1:00}.{2}", 1, 1, DateTime.Now.Year);
            tbDatumDo.Text = String.Format("{0:00}.{1:00}.{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            PostaviDatum();
        }

        private void buttonFASAP3_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (Restoran.DatumNaOtvoranje == null)
            {
                MessageBoxForm mbf = new MessageBoxForm("Нема информација за датумот на отворање на ресторанот, ве молиме поставете ја таа информација во другите форми", false);
                mbf.ShowDialog();
                return;
            }
            tbDatumOd.Text = String.Format("{0:00}.{1:00}.{2}", Restoran.DatumNaOtvoranje.Value.Day, Restoran.DatumNaOtvoranje.Value.Month, Restoran.DatumNaOtvoranje.Value.Year);
            tbDatumDo.Text = String.Format("{0:00}.{1:00}.{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            PostaviDatum();
        }

        private void tbDatumOd_Validating(object sender, CancelEventArgs e)
        {
            MaskedTextBox tb = sender as MaskedTextBox;
            DateTime datum = DateTime.Now;
            try
            {
                datum = DateTime.ParseExact(tbDatumOd.Text + " 00:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                if (datum > DateTime.Now)
                {
                    errorProvider1.SetError(tb, "Датумот кој го внесовте го надминува тековниот датум");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(tb, "");
                    e.Cancel = false;
                }
            }
            catch
            {
                errorProvider1.SetError(tb, "Ве молиме внесете валиден датум \"ден.месец.година\"");
                e.Cancel = true;
            }
        }

        private void buttonFASAP5_Click(object sender, EventArgs e)
        {
            Close();
        }

        delegate void SetTextCallback(Label fs, String obj);

        private void SetText(Label fs, String obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Text = obj;
            }
        }

        delegate void SetVisibleCallback(Label fs, bool obj);

        private void SetVisible(Label fs, bool obj)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (fs.InvokeRequired)
            {
                SetVisibleCallback d = new SetVisibleCallback(SetVisible);
                this.Invoke(d, new object[] { fs, obj });
            }
            else
            {
                fs.Visible = obj;
            }
        }
    }
}
