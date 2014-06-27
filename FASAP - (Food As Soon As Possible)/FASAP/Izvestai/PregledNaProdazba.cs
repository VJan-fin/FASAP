using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Threading;
using SmetkaZaNaracka.Naracki;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka.Izvestai
{
    public partial class PregledNaProdazba : BackgroundForm
    {
        public enum Sortiranje
        {
            ImeRastecki,
            ImeOpagjacki,
            StaraRastecki,
            StaraOpagjacki,
            NovaRastecki,
            NovaOpagjacki,
            ProcentRastecki,
            ProcentOpagjacki
        };

        public Restoran Restoran { get; set; }
        public OracleConnection Conn { get; set; }
        public Semaphore LoadingSemaphore { get; set; }
        public Sortiranje CurrSortiranje { get; set; }
        public List<StavkaPorast> Stavki { get; set; }
        public List<StavkaPorast> StavkiPom { get; set; }
        public int Ind { get; set; }
        public List<LabelFASAP> LabeliIme { get; set; }
        public List<LabelFASAP> LabeliStara { get; set; }
        public List<LabelFASAP> LabeliNova { get; set; }
        public List<LabelFASAP> LabeliCena { get; set; }
        public List<LabelFASAP> LabeliProcent { get; set; }
        public bool Prevzema { get; set; }

        public PregledNaProdazba(OracleConnection conn, Restoran restoran)
        {
            InitializeComponent();
            Opacity = 0;
            CurrSortiranje = Sortiranje.ProcentOpagjacki;
            Stavki = new List<StavkaPorast>();
            StavkiPom = new List<StavkaPorast>();
            LoadingSemaphore = new Semaphore(0, 10);
            Restoran = restoran;
            Conn = conn;
            KreirajListiLabeli();
            Thread oThread = new Thread(new ThreadStart(PostaviNarackiPorast));
            oThread.Start();
            oThread = new Thread(new ThreadStart(PrevzemiStavki));
            oThread.Start();
        }

        public void KreirajListiLabeli()
        {
            LabeliIme = new List<LabelFASAP>();
            LabeliStara = new List<LabelFASAP>();
            LabeliNova = new List<LabelFASAP>();
            LabeliCena = new List<LabelFASAP>();
            LabeliProcent = new List<LabelFASAP>();

            LabeliIme.Add(labelFASAP11);
            LabeliIme.Add(labelFASAP12);
            LabeliIme.Add(labelFASAP13);
            LabeliIme.Add(labelFASAP14);
            LabeliIme.Add(labelFASAP15);
            LabeliIme.Add(labelFASAP16);
            LabeliIme.Add(labelFASAP17);
            LabeliIme.Add(labelFASAP18);

            LabeliStara.Add(labelFASAP21);
            LabeliStara.Add(labelFASAP22);
            LabeliStara.Add(labelFASAP23);
            LabeliStara.Add(labelFASAP24);
            LabeliStara.Add(labelFASAP25);
            LabeliStara.Add(labelFASAP26);
            LabeliStara.Add(labelFASAP27);
            LabeliStara.Add(labelFASAP28);

            LabeliNova.Add(labelFASAP31);
            LabeliNova.Add(labelFASAP32);
            LabeliNova.Add(labelFASAP33);
            LabeliNova.Add(labelFASAP34);
            LabeliNova.Add(labelFASAP35);
            LabeliNova.Add(labelFASAP36);
            LabeliNova.Add(labelFASAP37);
            LabeliNova.Add(labelFASAP38);

            LabeliCena.Add(labelFASAP41);
            LabeliCena.Add(labelFASAP42);
            LabeliCena.Add(labelFASAP43);
            LabeliCena.Add(labelFASAP44);
            LabeliCena.Add(labelFASAP45);
            LabeliCena.Add(labelFASAP46);
            LabeliCena.Add(labelFASAP47);
            LabeliCena.Add(labelFASAP48);

            LabeliProcent.Add(labelFASAP51);
            LabeliProcent.Add(labelFASAP52);
            LabeliProcent.Add(labelFASAP53);
            LabeliProcent.Add(labelFASAP54);
            LabeliProcent.Add(labelFASAP55);
            LabeliProcent.Add(labelFASAP56);
            LabeliProcent.Add(labelFASAP57);
            LabeliProcent.Add(labelFASAP58);
        }

        public void PrevzemiStavki()
        {
            string sql = @"SELECT A.IME_STAVKA AS STAVKA, A.CENA_STAVKA || ' ден.' AS CENA, NVL(A.PROSEK, 0) || ' ком/ден' AS NOV, NVL(B.PROSEK, 0) || ' ком/ден' AS STAR, ROUND((A.PROSEK - B.PROSEK)*100/B.PROSEK, 2) || '%' AS PROCENT_NA_PORAST
                            FROM
	                            (
	                            SELECT S.IME_MENI, S.IME_STAVKA, S.OPIS_STAVKA, S.CENA_STAVKA, VALIDNOST_STAVKA, ROUND(SUM(V.KOLICHINA_STAVKA) / TO_NUMBER(MAX(N.VREME) - MIN(N.VREME) + 1), 1) AS PROSEK
	                            FROM
	                            (
		                            SELECT RESTORAN_ID, IME_MENI, STAVKA_ID, IME_STAVKA, VALIDNOST_STAVKA, OPIS_STAVKA, CENA_STAVKA
		                            FROM 
			                            (
			                            SELECT 
				                            RESTORAN_ID, IME_MENI, STAVKA_ID, IME_STAVKA, VALIDNOST_STAVKA, OPIS_STAVKA, CENA_STAVKA
			                            FROM 
				                            STAVKA
			                            WHERE 
				                            RESTORAN_ID = :RES_ID AND VALIDNOST_STAVKA = 1
			                            )
		                            WHERE 
			                            (RESTORAN_ID, IME_MENI, IME_STAVKA) IN
				                            (
				                            SELECT 
					                            RESTORAN_ID, IME_MENI, IME_STAVKA
				                            FROM 
					                            STAVKA
				                            WHERE RESTORAN_ID = :RES_ID1 AND VALIDNOST_STAVKA = 0
				                            )
		                            ) S
		                            LEFT OUTER JOIN
		                            VKLUCHUVA V
			                            ON 
				                            S.RESTORAN_ID = V.RESTORAN_ID AND S.IME_MENI = V.IME_MENI AND S.STAVKA_ID = V.STAVKA_ID
		                            LEFT OUTER JOIN
		                            NARACHKA N
			                            ON
				                            V.RESTORAN_ID = N.RESTORAN_ID AND V.NARACHKA_ID = N.NARACHKA_ID
		                            GROUP BY
			                            (S.RESTORAN_ID, S.IME_MENI, S.STAVKA_ID, S.IME_STAVKA, S.VALIDNOST_STAVKA, S.OPIS_STAVKA, S.CENA_STAVKA)
	                            ) A
	                            JOIN
	                            (
		                            SELECT IME_MENI, IME_STAVKA, OPIS_STAVKA, AVG(PROSEK) AS PROSEK
		                            FROM
			                            (
			                            SELECT S.IME_MENI, S.IME_STAVKA, S.OPIS_STAVKA, VALIDNOST_STAVKA, ROUND(SUM(V.KOLICHINA_STAVKA) / TO_NUMBER(MAX(N.VREME) - MIN(N.VREME) + 1), 1) AS PROSEK
			                            FROM
			                            (
				                            SELECT RESTORAN_ID, IME_MENI, STAVKA_ID, IME_STAVKA, VALIDNOST_STAVKA, OPIS_STAVKA
				                            FROM 
					                            (
					                            SELECT 
						                            RESTORAN_ID, IME_MENI, STAVKA_ID, IME_STAVKA, VALIDNOST_STAVKA, OPIS_STAVKA
					                            FROM 
						                            STAVKA
					                            WHERE 
						                            RESTORAN_ID = :RES_ID2 AND VALIDNOST_STAVKA = 0
					                            )
				                            WHERE 
					                            (RESTORAN_ID, IME_MENI, IME_STAVKA) IN
						                            (
						                            SELECT 
							                            RESTORAN_ID, IME_MENI, IME_STAVKA
						                            FROM 
							                            STAVKA
						                            WHERE RESTORAN_ID = :RES_ID3 AND VALIDNOST_STAVKA = 0
						                            )
				                            ) S
				                            LEFT OUTER JOIN
				                            VKLUCHUVA V
					                            ON 
						                            S.RESTORAN_ID = V.RESTORAN_ID AND S.IME_MENI = V.IME_MENI AND S.STAVKA_ID = V.STAVKA_ID
				                            LEFT OUTER JOIN
				                            NARACHKA N
					                            ON
						                            V.RESTORAN_ID = N.RESTORAN_ID AND V.NARACHKA_ID = N.NARACHKA_ID
				                            GROUP BY
					                            (S.RESTORAN_ID, S.IME_MENI, S.STAVKA_ID, S.IME_STAVKA, S.VALIDNOST_STAVKA, S.OPIS_STAVKA)
			                            )
		                            GROUP BY (IME_MENI, IME_STAVKA, OPIS_STAVKA)
	                            ) B 
	                            ON
		                            A.IME_STAVKA = B.IME_STAVKA AND A.IME_MENI = B.IME_MENI AND NVL(A.OPIS_STAVKA, 0) = NVL(B.OPIS_STAVKA, 0)";

            if (CurrSortiranje.Equals(Sortiranje.ImeOpagjacki))
                sql += "\nORDER BY STAVKA desc";
            else if (CurrSortiranje.Equals(Sortiranje.ImeRastecki))
                sql += "\nORDER BY STAVKA asc";
            else if (CurrSortiranje.Equals(Sortiranje.ProcentOpagjacki))
                sql += "\nORDER BY ROUND((A.PROSEK - B.PROSEK)*100/B.PROSEK, 2) desc, STAVKA";
            else if (CurrSortiranje.Equals(Sortiranje.ProcentRastecki))
                sql += "\nORDER BY ROUND((A.PROSEK - B.PROSEK)*100/B.PROSEK, 2) asc, STAVKA";
            else if (CurrSortiranje.Equals(Sortiranje.StaraOpagjacki))
                sql += "\nORDER BY NVL(B.PROSEK, 0) desc, STAVKA";
            else if (CurrSortiranje.Equals(Sortiranje.StaraRastecki))
                sql += "\nORDER BY NVL(B.PROSEK, 0) asc, STAVKA";
            else if (CurrSortiranje.Equals(Sortiranje.NovaOpagjacki))
                sql += "\nORDER BY NVL(A.PROSEK, 0) desc, STAVKA";
            else if (CurrSortiranje.Equals(Sortiranje.NovaRastecki))
                sql += "\nORDER BY NVL(A.PROSEK, 0) asc, STAVKA";


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

            prm = new OracleParameter("RES_ID3", OracleDbType.Int32);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            Stavki = new List<StavkaPorast>();
            try
            {
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    StavkaPorast st = new StavkaPorast(dr.GetString(0), dr.GetString(3), dr.GetString(2), dr.GetString(1), dr.GetString(4));
                    Stavki.Add(st);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                throw new Exception("Ве молиме проверете ја вашата конекција");
            }

            LoadingSemaphore.WaitOne();
            PostaviTabela();
        }

        private void PostaviTabela()
        {
            StavkiPom = new List<StavkaPorast>();
            foreach (var obj in Stavki)
                if (textBox1.Text.Trim().Length == 0 || textBox1.ForeColor.Equals(Color.Khaki))
                {
                    StavkiPom.Add(obj);
                }
                else if (obj.Sodrzi(textBox1.Text))
                {
                    StavkiPom.Add(obj);
                }
            int ind = this.Ind;
            for (int i = 0; i < LabeliIme.Count; i++)
                if (ind < StavkiPom.Count)
                {
                    SetObject(LabeliIme[i], StavkiPom[ind].ime);
                    SetObject(LabeliStara[i], StavkiPom[ind].stara);
                    SetObject(LabeliNova[i], StavkiPom[ind].nova);
                    SetObject(LabeliCena[i], StavkiPom[ind].cena);
                    SetObject(LabeliProcent[i], StavkiPom[ind].procent);
                    ind++;
                }
                else
                {
                    SetObject(LabeliIme[i], null);
                    SetObject(LabeliStara[i], null);
                    SetObject(LabeliNova[i], null);
                    SetObject(LabeliCena[i], null);
                    SetObject(LabeliProcent[i], null);
                }
            Prevzema = false;
        }

        public void PostaviNarackiPorast()
        {
            string sql = @"with t as (select r.restoran_id, r.ime_restoran, pret.pret_mesec, tek.tek_mesec, tek.tek_mesec / pret.pret_mesec as koef
		                               from restoran r left outer join
				                            (select r.restoran_id, r.ime_restoran, count(*) as tek_mesec
				                             from narachka n, restoran r
				                             where n.restoran_id = r.restoran_id and to_char(n.vreme, 'mm.yyyy') like to_char(sysdate, 'mm.yyyy')
				                             group by r.restoran_id, r.ime_restoran) tek on r.ime_restoran = tek.ime_restoran 
				                            left outer join
				                             (select r.restoran_id, r.ime_restoran, count(*) as pret_mesec
				                              from narachka n, restoran r
				                              where n.restoran_id = r.restoran_id and to_char(n.vreme, 'mm.yyyy') like to_char( add_months(sysdate, -1), 'mm.yyyy')
				                              group by r.restoran_id, r.ime_restoran) pret on tek.ime_restoran = pret.ime_restoran
		                              )
                            select Претходен_месец, Тековен_месец, Пораст
                            from
	                            ((select t.restoran_id, t.ime_restoran as Ресторан, NVL(t.pret_mesec, 0) || ' нарачки' as Претходен_месец, NVL(t.tek_mesec, 0) || ' нарачки' as Тековен_месец, round((koef - 1) * 100, 1) || '%' as Пораст
	                            from t
	                            where t.koef <= 1 or t.koef is null)
	                            union
	                            (select t.restoran_id, t.ime_restoran as Ресторан, NVL(t.pret_mesec, 0) || ' нарачки' as Претходен_месец, NVL(t.tek_mesec, 0) || ' нарачки' as Тековен_месец, round(koef * 100, 1) || '%' as Пораст
	                            from t
	                            where t.koef > 1 or t.koef is null)) tmp
                            where restoran_id = :RES_ID";

            OracleCommand cmd = new OracleCommand(sql, Conn);

            OracleParameter prm = new OracleParameter("RES_ID", OracleDbType.Int32);
            prm.Value = Restoran.RestoranID;
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            string predhoden = null;
            string tekoven = null;
            string procent = null;
            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    predhoden = dr.GetString(0);
                    tekoven = dr.GetString(1);
                    procent = dr.GetString(2);
                    break;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ве молиме проверете ја вашата конекција");
            }

            LoadingSemaphore.WaitOne();
            SetObject(lblPredhoden, predhoden);
            SetObject(lblTekoven, tekoven);
            SetObject(lblProcent, procent);
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

        private void btnProcent_MouseEnter(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            Cursor = Cursors.Hand;
            lb.Image = Resources.LightButton___Copy;
            lb.ForeColor = Color.SaddleBrown;
        }

        private void btnProcent_MouseLeave(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            Cursor = Cursors.Default;
            lb.Image = Resources.DarkButton___Copy;
            lb.ForeColor = Color.Khaki;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (this.Ind != 0)
            {
                this.Ind--;
                this.PostaviTabela();
            }
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

        private void btnSortIme_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (Prevzema)
                return;
            Prevzema = true;
            if (CurrSortiranje.Equals(Sortiranje.ImeOpagjacki))
            {
                pbIme.Image = Resources.LightArrowUp;
                CurrSortiranje = Sortiranje.ImeRastecki;
            }
            else
            {
                pbProcent.Image = Resources.DarkArrowDown;
                pbStara.Image = Resources.DarkArrowDown;
                pbNova.Image = Resources.DarkArrowDown;
                pbIme.Image = Resources.LightArrowDown;
                CurrSortiranje = Sortiranje.ImeOpagjacki;
            }
            Thread oThread = new Thread(new ThreadStart(PrevzemiStavki));
            oThread.Start();
            LoadingSemaphore.Release();
        }

        public void textBox1Focus()
        {
            textBox1.Visible = false;
            this.Focus();
            textBox1.Visible = true;
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (Prevzema)
                return;
            Prevzema = true;
            if (CurrSortiranje.Equals(Sortiranje.StaraOpagjacki))
            {
                pbStara.Image = Resources.LightArrowUp;
                CurrSortiranje = Sortiranje.StaraRastecki;
            }
            else
            {
                pbProcent.Image = Resources.DarkArrowDown;
                pbStara.Image = Resources.LightArrowDown;
                pbNova.Image = Resources.DarkArrowDown;
                pbIme.Image = Resources.DarkArrowDown;
                CurrSortiranje = Sortiranje.StaraOpagjacki;
            }
            Thread oThread = new Thread(new ThreadStart(PrevzemiStavki));
            oThread.Start();
            LoadingSemaphore.Release();
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (Prevzema)
                return;
            Prevzema = true;
            if (CurrSortiranje.Equals(Sortiranje.NovaOpagjacki))
            {
                pbNova.Image = Resources.LightArrowUp;
                CurrSortiranje = Sortiranje.NovaRastecki;
            }
            else
            {
                pbProcent.Image = Resources.DarkArrowDown;
                pbStara.Image = Resources.DarkArrowDown;
                pbNova.Image = Resources.LightArrowDown;
                pbIme.Image = Resources.DarkArrowDown;
                CurrSortiranje = Sortiranje.NovaOpagjacki;
            }
            Thread oThread = new Thread(new ThreadStart(PrevzemiStavki));
            oThread.Start();
            LoadingSemaphore.Release();
        }

        private void btnProcent_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (Prevzema)
                return;
            Prevzema = true;
            if (CurrSortiranje.Equals(Sortiranje.ProcentOpagjacki))
            {
                pbProcent.Image = Resources.LightArrowUp;
                CurrSortiranje = Sortiranje.ProcentRastecki;
            }
            else
            {
                pbProcent.Image = Resources.LightArrowDown;
                pbStara.Image = Resources.DarkArrowDown;
                pbNova.Image = Resources.DarkArrowDown;
                pbIme.Image = Resources.DarkArrowDown;
                CurrSortiranje = Sortiranje.ProcentOpagjacki;
            }
            Thread oThread = new Thread(new ThreadStart(PrevzemiStavki));
            oThread.Start();
            LoadingSemaphore.Release();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.ForeColor == Color.Khaki)
                return;
            PostaviTabela();
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            textBox1Focus();
            if (this.StavkiPom.Count > this.LabeliIme.Count)
            {
                if (this.Ind < this.StavkiPom.Count - this.LabeliIme.Count)
                    this.Ind++;
                this.PostaviTabela();
            }
        }

        
    }
}
