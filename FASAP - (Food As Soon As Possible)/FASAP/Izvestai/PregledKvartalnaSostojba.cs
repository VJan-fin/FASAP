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
    public partial class PregledKvartalnaSostojba : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Restoran Restoran { get; set; }

        private int tekovnaGodina { get; set; }
        /// <summary>
        /// Pomosni listi koi gi sodrzat poziciite na tabelata
        /// </summary>
        private List<LabelFASAP> VkPromet { get; set; }
        private List<LabelFASAP> VkPlata { get; set; }
        private List<LabelFASAP> VkDodatoci { get; set; }
        private List<LabelFASAP> VkTrosoci { get; set; }
        private List<LabelFASAP> Sostojba { get; set; }

        public PregledKvartalnaSostojba(OracleConnection conn, Restoran rest)
        {
            InitializeComponent();

            this.Conn = conn;
            this.Restoran = rest;
            Init();
        }
        
        // samo za primer
        public PregledKvartalnaSostojba()
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

            Init();
        }
        
        public void Init()
        {
            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.tekovnaGodina = DateTime.Now.Year;
            this.lblGodina.Text = this.tekovnaGodina.ToString();
            this.lblImeRestoran.Text = this.Restoran.Ime + " ";

            this.VkPromet = new List<LabelFASAP>();
            this.VkPromet.Add(this.lblPromet1);
            this.VkPromet.Add(this.lblPromet2);
            this.VkPromet.Add(this.lblPromet3);
            this.VkPromet.Add(this.lblPromet4);

            this.VkPlata = new List<LabelFASAP>();
            this.VkPlata.Add(this.lblPlata1);
            this.VkPlata.Add(this.lblPlata2);
            this.VkPlata.Add(this.lblPlata3);
            this.VkPlata.Add(this.lblPlata4);

            this.VkDodatoci = new List<LabelFASAP>();
            this.VkDodatoci.Add(this.lblDodatok1);
            this.VkDodatoci.Add(this.lblDodatok2);
            this.VkDodatoci.Add(this.lblDodatok3);
            this.VkDodatoci.Add(this.lblDodatok4);

            this.VkTrosoci = new List<LabelFASAP>();
            this.VkTrosoci.Add(this.lblTrosoci1);
            this.VkTrosoci.Add(this.lblTrosoci2);
            this.VkTrosoci.Add(this.lblTrosoci3);
            this.VkTrosoci.Add(this.lblTrosoci4);

            this.Sostojba = new List<LabelFASAP>();
            this.Sostojba.Add(this.lblSostojba1);
            this.Sostojba.Add(this.lblSostojba2);
            this.Sostojba.Add(this.lblSostojba3);
            this.Sostojba.Add(this.lblSostojba4);

            this.ObnoviEkran();
        }

        /// <summary>
        /// Inicijalizacija na labelite vo tabelata
        /// </summary>
        private void ClearLabels()
        {
            foreach (var item in this.VkPromet)
                item.Text = " - ";
            foreach (var item in this.VkPlata)
                item.Text = " - ";
            foreach (var item in this.VkPromet)
                item.Text = " - ";
            foreach (var item in this.VkDodatoci)
                item.Text = " - ";
            foreach (var item in this.VkTrosoci)
                item.Text = " - ";
            foreach (var item in this.Sostojba)
                item.Text = " - ";
        }

        /// <summary>
        /// Inicijalizacija na labelite i povtorno vcituvanje
        /// na podatocite od bazata
        /// </summary>
        private void ObnoviEkran()
        {
            this.lblGodina.Text = this.tekovnaGodina.ToString();
            this.ClearLabels();
            this.VcitajPodatoci();
        }

        /// <summary>
        /// Popolnuvanje na tabelata so soodvetnite
        /// podatoci koi vleguvaat vo izvestajot
        /// </summary>
        private void VcitajPodatoci()
        {
            String sqlTab = this.GetReportSQL();
            OracleCommand cmd = new OracleCommand(sqlTab, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID1", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID2", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID3", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID4", OracleDbType.Int64);
                prm.Value = this.Restoran.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("GOD", OracleDbType.Int64);
                prm.Value = this.tekovnaGodina;
                cmd.Parameters.Add(prm);

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                int ind = 0;
                while (dr.Read())
                {
                    this.VkPromet[ind].Text = dr.GetInt32(2).ToString() + " ден. ";
                    this.VkPlata[ind].Text = dr.GetInt32(3).ToString() + " ден. ";
                    this.VkDodatoci[ind].Text = dr.GetInt32(4).ToString() + " ден. ";
                    this.VkTrosoci[ind].Text = dr.GetInt32(5).ToString() + " ден. ";
                    this.Sostojba[ind].Text = dr.GetInt32(6).ToString() + " ден. ";
                    ind++;
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

        /// <summary>
        /// Metod koj go vraka SQL izrazot koj treba da se evaluira
        /// za da se dobijat potrebnite podatoci
        /// </summary>
        /// <returns></returns>
        private string GetReportSQL()
        {
            string sqlReport = @"
                with q as (select distinct case
		                 when mesec_promet between 1 and 3 then 'Прв квартал'
		                 when mesec_promet between 4 and 6 then 'Втор квартал'
		                 when mesec_promet between 7 and 9 then 'Трет квартал'
		                 else 'Четврт квартал' end as kvartali
		                 from promet
		                )
                select vk_promet.godina as Година, vk_promet.kvartal as Квартал, vk_promet.vkupen_promet as Вкупен_промет, vk_plata.iznos_plata as Вкупно_плата, 
	                vk_dodatoci.iznos_dodatok as Вкупно_додатоци, (vk_dodatoci.iznos_dodatok + vk_plata.iznos_plata) as Вкупни_трошоци, (vk_promet.vkupen_promet - (vk_dodatoci.iznos_dodatok + vk_plata.iznos_plata)) as Состојба
                from 
	                (select t.godina_promet as Godina, t.kvartali as Kvartal, sum(t.iznos_promet) as Vkupen_promet
	                 from
		                (select distinct *
		                 from q, promet p
		                 where (p.mesec_promet between 1 and 3) and q.kvartali = 'Прв квартал'
			                  or (p.mesec_promet between 4 and 6) and q.kvartali = 'Втор квартал'
			                  or (p.mesec_promet between 7 and 9) and q.kvartali = 'Трет квартал'
			                  or (p.mesec_promet between 10 and 12) and q.kvartali = 'Четврт квартал'
			                  and restoran_id = :REST_ID1
		                 order by p.godina_promet, q.kvartali
		                ) t
	                 group by t.godina_promet, t.kvartali
	                ) vk_promet,
	                (select godina_dodatok as Godina, kvartali as Kvartal, sum(Dodatok) as Iznos_dodatok
	                 from q,
		                (select d.godina_dodatok, d.mesec_dodatok, sum(iznos_dodatok) as Dodatok
		                 from restoran r join dodatok d on d.restoran_id = r.restoran_id
		                 where r.restoran_id = :REST_ID2
		                 group by d.godina_dodatok, d.mesec_dodatok
		                ) t
	                 where (t.mesec_dodatok between 1 and 3) and q.kvartali = 'Прв квартал'
		                  or (t.mesec_dodatok between 4 and 6) and q.kvartali = 'Втор квартал'
		                  or (t.mesec_dodatok between 7 and 9) and q.kvartali = 'Трет квартал'
		                  or (t.mesec_dodatok between 10 and 12) and q.kvartali = 'Четврт квартал'
	                 group by godina_dodatok, kvartali
	                ) vk_dodatoci,
	                (select godina_dodatok as godina, kvartali as kvartal, sum(plata) as iznos_plata
	                 from q,
		                (select vraboten_id, datum_na_vrabotuvanje, mesec_dodatok, godina_dodatok, plata, status
		                 from izvrshuva i,
			                (select distinct mesec_dodatok, godina_dodatok
			                 from dodatok
			                 where restoran_id = :REST_ID3
			                ) dat
		                 where to_char(i.datum_na_vrabotuvanje, 'yyyy') < godina_dodatok 
			                or (to_char(i.datum_na_vrabotuvanje, 'yyyy') = godina_dodatok and to_char(i.datum_na_vrabotuvanje, 'mm') < mesec_dodatok) 
			                and i.restoran_id = :REST_ID4 and i.status like '1'
		                 order by i.vraboten_id, godina_dodatok, mesec_dodatok
		                ) plati
	                 where (plati.mesec_dodatok between 1 and 3) and q.kvartali = 'Прв квартал'
		                or (plati.mesec_dodatok between 4 and 6) and q.kvartali = 'Втор квартал'
		                or (plati.mesec_dodatok between 7 and 9) and q.kvartali = 'Трет квартал'
		                or (plati.mesec_dodatok between 10 and 12) and q.kvartali = 'Четврт квартал'
	                 group by godina_dodatok, kvartali
	                ) vk_plata
                where vk_promet.godina = vk_dodatoci.godina and vk_promet.kvartal = vk_dodatoci.kvartal and vk_promet.godina = vk_plata.godina
	                and vk_promet.kvartal = vk_plata.kvartal and vk_promet.godina = :GOD
                order by Година, 
                    case 
                       when Квартал = 'Прв квартал' then 1 
                       when Квартал = 'Втор квартал' then 2
                       when Квартал = 'Трет квартал' then 3
                       else 4
                    end";

            return sqlReport;
        }

        private void btnTekoven_Click(object sender, EventArgs e)
        {
            this.tekovnaGodina = DateTime.Now.Year;
            this.ObnoviEkran();
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

        private void btnTekoven_MouseEnter(object sender, EventArgs e)
        {
            this.btnTekoven.Image = Resources.LightButton___Copy;
            this.btnTekoven.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnTekoven_MouseLeave(object sender, EventArgs e)
        {
            this.btnTekoven.Image = Resources.DarkButton___Copy;
            this.btnTekoven.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void pbLeftG_Click(object sender, EventArgs e)
        {
            if (this.tekovnaGodina > 1950)
            {
                this.tekovnaGodina--;
                this.ObnoviEkran();
            }
            else
            {
                MessageBoxForm mbf = new MessageBoxForm("Извештаи постари од 1950 не може да бидат прикажани", false);
                mbf.ShowDialog();
            }
        }

        private void pbRightG_Click(object sender, EventArgs e)
        {
            if (this.tekovnaGodina < DateTime.Now.Year)
            {
                this.tekovnaGodina++;
                this.ObnoviEkran();
            }
            else
            {
                MessageBoxForm mbf = new MessageBoxForm("Прикажана е тековната година", false);
                mbf.ShowDialog();
            }
        }
    }
}
