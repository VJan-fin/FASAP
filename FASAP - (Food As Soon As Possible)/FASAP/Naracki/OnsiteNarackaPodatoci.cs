﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using SmetkaZaNaracka.Narachki;
using SmetkaZaNaracka.Properties;
using SmetkaZaNaracka.Naracki;

namespace SmetkaZaNaracka
{
    public partial class OnsiteNarackaPodatoci : BackgroundForm
    {
        private Naracka Naracka { get; set; }
        private Restoran Restoran { get; set; }
        private OracleConnection Conn { get; set; }
        private int brMasa;
        private int BrMasa
        {
            get
            {
                return brMasa;
            }
            set
            {
                brMasa = value;
                lblBrojMasa.Text = brMasa.ToString();
                if (brMasa == 1)
                    pbDown.Visible = false;
                else pbDown.Visible = true;
                if (brMasa == Restoran.BrojMasi)
                    pbUp.Visible = false;
                else pbUp.Visible = true;
            }
        }
        private double Rejting { get; set; }
        private bool isVoted { get; set; }
        private List<PictureBox> lista { get; set; }

        public OnsiteNarackaPodatoci(OracleConnection conn, Naracka nar, Restoran res)
        {
            InitializeComponent();
            Naracka = nar;
            Restoran = res;
            Rejting = Restoran.Rejting;
            lista = new List<PictureBox>();
            lista.Add(pbZvezda1);
            lista.Add(pbZvezda2);
            lista.Add(pbZvezda3);
            lista.Add(pbZvezda4);
            lista.Add(pbZvezda5);
            postaviRejting(Rejting);
            Conn = conn;
            BrMasa = 1;
            Opacity = 0;
        }

        private void postaviRejting(double rejting)
        {
            int i = 0;
            foreach (PictureBox pb in lista)
            {
                i += 2;
                if (i <= rejting || System.Math.Abs((i - rejting)) <= 0.5)
                    pb.Image = Resources.Polna_zvezda;
                else if (System.Math.Abs((i - rejting)) <= 1.5)
                    pb.Image = Resources.Pola_zvezda;
                else pb.Image = Resources.Prazna_zvezda;

            }

        }

        private void pbZvezda1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            if (lista[0].Equals(sender))
                postaviRejting(2);
            else if (lista[1].Equals(sender))
                postaviRejting(4);
            else if (lista[2].Equals(sender))
                postaviRejting(6);
            else if (lista[3].Equals(sender))
                postaviRejting(8);
            else if (lista[4].Equals(sender))
                postaviRejting(10);
        }

        private void pbZvezda1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            postaviRejting(Rejting);
        }

        private void pbZvezda1_Click(object sender, EventArgs e)
        {
            if (lista[0].Equals(sender))
                Rejting = 2;
            else if (lista[1].Equals(sender))
                Rejting = 4;
            else if (lista[2].Equals(sender))
                Rejting = 6;
            else if (lista[3].Equals(sender))
                Rejting = 8;
            else if (lista[4].Equals(sender))
                Rejting = 10;
            isVoted = true;
        }

        private void buttonFASAP1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void buttonFASAP2_Click(object sender, EventArgs e)
        {
            if (isVoted)
            {
                string updateOnsite = @"UPDATE RESTORAN
                                        SET REJTING = ((REJTING * PRIMEROK) + :Rejting) / (PRIMEROK + 1), PRIMEROK = PRIMEROK + 1
                                        WHERE RESTORAN_ID = :ResID";

                OracleCommand cmd = new OracleCommand(updateOnsite, Conn);

                OracleParameter prm = new OracleParameter("Rejting", OracleDbType.Double);
                prm.Value = Rejting;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("ResID", OracleDbType.Int64);
                prm.Value = Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                int br;
                try
                {
                    br = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    
                }
            }
            Onsite online = new Onsite(Naracka.NarackaID, Naracka.VkupnaCena, DateTime.Now, BrMasa);
            online.Stavki = Naracka.Stavki;
            online.SqlInsert(Conn, Restoran.RestoranID);
            UspesnostNaNaracka up = new UspesnostNaNaracka();
            up.ShowDialog();
            DialogResult = DialogResult.Yes;
        }

        private void pbUp_Click(object sender, EventArgs e)
        {
            BrMasa++;
        }

        private void pbDown_Click(object sender, EventArgs e)
        {
            BrMasa--;
        }

        private void pbUp_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            pbUp.Image = Resources.LightArrowUp;
        }

        private void pbUp_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            pbUp.Image = Resources.DarkArrowUp;
        }

        private void pbDown_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            pbDown.Image = Resources.LightArrowDown;
        }

        private void pbDown_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            pbDown.Image = Resources.DarkArrowDown;
        }

        private void buttonFASAP2_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            Label lb = sender as Label;
            lb.Image = Resources.LightButton___Copy;
            lb.ForeColor = Color.SaddleBrown;
        }

        private void buttonFASAP2_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            Label lb = sender as Label;
            lb.Image = Resources.DarkButton___Copy;
            lb.ForeColor = Color.Khaki;
        }
    }
}
