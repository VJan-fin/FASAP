﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

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
            Gradovi=new List<String>();
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

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            vcituvanje();
            popolniLabeli();


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
            for (int i = 0; i < 5; i++)
            {
                if (i >= Najbarani5.Count) break;
                labeliNajbarani[i].Text =String.Format(": {0} :", Najbarani5[i]);
            }
        }
        public void popolniLabeliGrad()
        {
            int ind = this.indexG;
            for (int i = 0; i < this.labeliGrad.Count; i++)
            {
                if (ind < this.Gradovi.Count)
                {
                    this.labeliGrad[i].UpdateObject(this.Gradovi[ind]);
                    ind++;
                }
                else
                    this.labeliGrad[i].UpdateObject(null);
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
                    this.labeliKategorija[i].UpdateObject(this.Kategorija[ind]);
                    ind++;
                }
                else
                    this.labeliKategorija[i].UpdateObject(null);
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
                    Najbarani.Add(String.Format("{0}.{1}", i + 1, dr.GetString(0)));
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
            for (int i = 0; i < 5; i++)
            {
                if (i >= Najbarani.Count) break;
                Najbarani5.Add(Najbarani[i]);
            }
        }
        public void vcitajKategorija()
        {
            
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
           string  sql = "Select distinct GRAD from RESTORAN ORDER BY GRAD"; // C#
           OracleCommand  cmd = new OracleCommand(sql, Conn);
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

        private void lblGrad1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
                //this.CurrentEmp = lb.LblObject as VrabotenInfo;
               // this.MarkSelection();
               // this.PopolniVraboten();
            }
        }

        private void lblKat1_Click(object sender, EventArgs e)
        {
            LabelFASAP lb = sender as LabelFASAP;
            if (lb.LblObject != null)
            {
               // this.CurrentEmp = lb.LblObject as VrabotenInfo;
               // this.MarkSelection();
                //this.PopolniVraboten();
            }
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
           
    }
}