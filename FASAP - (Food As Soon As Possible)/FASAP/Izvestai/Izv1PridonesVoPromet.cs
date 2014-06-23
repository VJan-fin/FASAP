using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka
{
    public enum SortArg
    {
        Ime,
        Prezime,
        Promet,
        Procent
    }

    public partial class Izv1PridonesVoPromet : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private int RestoranID { get; set; }

        private int tekovenMesec { get; set; }
        private int tekovnaGodina { get; set; }
        private int indexStart { get; set; }
        private int PozInd { get; set; }
        private List<LabelFASAP> iminja { get; set; }
        private List<LabelFASAP> promet { get; set; }
        private List<LabelFASAP> prosek { get; set; }
        private List<PictureBox> OrderPics { get; set; }
        private List<String> Pozicii { get; set; }
        private List<VrabPrometProcent> vraboteni { get; set; }
        private List<VrabPrometProcent> showVraboteni { get; set; }

        private SortArg SortParam { get; set; }
        private SortingOrder SortStat { get; set; }

        public Izv1PridonesVoPromet() //probno
        {
            InitializeComponent();
            this.RestoranID = 1;

            string oradb = "Data Source=(DESCRIPTION="
          + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
          + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
          + "User Id=DBA_20132014L_GRP_020;Password=7734924;";

            Conn = new OracleConnection();
            Conn.ConnectionString = oradb;
            Conn.Open();

            Init();
        }
        public Izv1PridonesVoPromet(OracleConnection conn, int restID)
        {
            InitializeComponent();
            this.Conn = conn;
            this.RestoranID = restID;
            Init();
        }
        public void Init()
        {
            vraboteni = new List<VrabPrometProcent>();
            
            this.DoubleBuffered = true;
            this.Opacity = 0;
            indexStart = 0;

            iminja = new List<LabelFASAP>();
            promet = new List<LabelFASAP>();
            prosek = new List<LabelFASAP>();

            iminja.Add(labelFASAP1);
            iminja.Add(labelFASAP2);
            iminja.Add(labelFASAP3);
            iminja.Add(labelFASAP4);
            iminja.Add(labelFASAP5);
            iminja.Add(labelFASAP6);
            iminja.Add(labelFASAP7);
            iminja.Add(labelFASAP8);

            promet.Add(labelFASAP11);
            promet.Add(labelFASAP12);
            promet.Add(labelFASAP13);
            promet.Add(labelFASAP14);
            promet.Add(labelFASAP15);
            promet.Add(labelFASAP16);
            promet.Add(labelFASAP17);
            promet.Add(labelFASAP18);

            prosek.Add(labelFASAP21);
            prosek.Add(labelFASAP22);
            prosek.Add(labelFASAP23);
            prosek.Add(labelFASAP24);
            prosek.Add(labelFASAP25);
            prosek.Add(labelFASAP26);
            prosek.Add(labelFASAP27);
            prosek.Add(labelFASAP28);

            OrderPics = new List<PictureBox>();
            OrderPics.Add(pbIme);
            OrderPics.Add(pbPrezime);
            OrderPics.Add(pbPromet);
            OrderPics.Add(pbProcent);

            clearLabels();

            VcitajPozicii();

            this.SortParam = SortArg.Procent;
            this.SortStat = SortingOrder.Desc;

            foreach (LabelFASAP l in iminja)
            {
                l.Anchor=System.Windows.Forms.AnchorStyles.None;
                l.ForeColor = Color.Khaki;
            }
            foreach (LabelFASAP l in promet)
            {
                l.Anchor = System.Windows.Forms.AnchorStyles.None;
                l.ForeColor = Color.Khaki;
            }
            foreach (LabelFASAP l in prosek)
            {
                l.Anchor = System.Windows.Forms.AnchorStyles.None;
                l.ForeColor = Color.Khaki;
            }
        
        }
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
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }
            Pozicii.Remove("Менаџер");
            this.UpdatePozicii();
        }
        private void FiltrirajVraboteni()
        {
            this.showVraboteni = new List<VrabPrometProcent>();
            foreach (var item in this.vraboteni)
                this.showVraboteni.Add(item);

            if (this.lblPozicija.Text != "Сите")
                this.showVraboteni.RemoveAll(w => w.Funkcija != this.lblPozicija.Text);

        }

        private void UpdatePozicii()
        {
            if (this.Pozicii.Count != 0)
                this.lblPozicija.Text = this.Pozicii[this.PozInd];
            else
                this.lblPozicija.Text = " ";

          
            indexStart = 0;
            FiltrirajVraboteni();
            PodrediVrab();
            popolniLabeli();
        }

        public void clearLabels()
        {
            foreach (LabelFASAP l in iminja)
            {
                l.Text = ": :";
            }
            foreach (LabelFASAP l in promet)
            {
                l.Text = ": :";
            }
            foreach (LabelFASAP l in prosek)
            {
                l.Text = ": :";
            }
            lblPromet.Text = ": :";
        }
        //samo probni redici za da gi testiram kopcinjata nagore nadole :D
        public void probno()
        {
            VrabPrometProcent v = new VrabPrometProcent(1, "Angela", "Josifovska", 3423, 55); v.Funkcija = "Келнер"; vraboteni.Add(v);
            v = new VrabPrometProcent(2, "Aleksandar", "Kuzmanoski", 2514, 46); v.Funkcija = "Доставувач"; vraboteni.Add(v);
            v = new VrabPrometProcent(3, "Viktor", "Janevski", 3423, 55); v.Funkcija = "Келнер"; vraboteni.Add(v);
            v = new VrabPrometProcent(2, "vrab4", "Kuzmanoski", 2514, 46); v.Funkcija = "Доставувач"; vraboteni.Add(v);
            v = new VrabPrometProcent(2, "vrab5", "Kuzmanoski", 2514, 46); v.Funkcija = "Келнер"; vraboteni.Add(v);
            v = new VrabPrometProcent(2, "vrab6", "shrtuyuooyio", 2514, 46);  v.Funkcija = "Доставувач";vraboteni.Add(v);
            v = new VrabPrometProcent(2, "vrab7", "Kuzmanoski", 2514, 46); v.Funkcija = "Келнер"; vraboteni.Add(v);
            v = new VrabPrometProcent(3, "vrab8", "Janevski", 3423, 55);  v.Funkcija = "Доставувач";vraboteni.Add(v);
            v = new VrabPrometProcent(3, "vrab9", "Janevski", 3423, 55); v.Funkcija = "Келнер"; vraboteni.Add(v);
            v = new VrabPrometProcent(3, "vrab10", "zdtrtutyi", 3423, 55); v.Funkcija = "Келнер"; vraboteni.Add(v);
            v = new VrabPrometProcent(3, "vrab11", "Janevski", 3423, 55); v.Funkcija = "Доставувач"; vraboteni.Add(v);
            v = new VrabPrometProcent(1, "vrab12", "Josifovska", 3423, 55); v.Funkcija = "Келнер"; vraboteni.Add(v);
            v = new VrabPrometProcent(1, "vrab13", "Josisdjfhjh", 3423, 55); v.Funkcija = "Келнер"; vraboteni.Add(v);

            showVraboteni = new List<VrabPrometProcent>(vraboteni);
        }
        public void popolniLabeli()
        {
            clearLabels();
            for (int i = indexStart; i < indexStart + 8; i++)
            {
                if (i >= showVraboteni.Count) break;
                iminja[i - indexStart].Text = String.Format(": {0} :",showVraboteni[i].getImePrezime());
                promet[i - indexStart].Text =  String.Format(": {0} :",showVraboteni[i].getPromet());
                prosek[i - indexStart].Text = String.Format(": {0} :", showVraboteni[i].getProcent());
            }
        }
        private void ChangeOrder()
        {
            if (this.SortStat == SortingOrder.Asc)
                this.SortStat = SortingOrder.Desc;
            else
                this.SortStat = SortingOrder.Asc;
        }
        private void UpdateImages(PictureBox pb)
        {
            //PictureBox pb = sender as PictureBox;
            foreach (var item in this.OrderPics)
            {
                if (pb == item && this.SortStat == SortingOrder.Asc)
                    item.Image = Resources.LightArrowUp;
                else if (pb == item && this.SortStat == SortingOrder.Desc)
                    item.Image = Resources.LightArrowDown;
                else if (pb != item)
                    item.Image = Resources.DarkArrowUp;
            }
           
        }
        public void PodrediVrab()
        {

            if (this.SortParam == SortArg.Ime)
                OrderEmpStats.OrderByName(this.showVraboteni, this.SortStat == SortingOrder.Asc);
            else if (this.SortParam == SortArg.Prezime)
                OrderEmpStats.OrderBySurname(this.showVraboteni, this.SortStat == SortingOrder.Asc);
            else if (this.SortParam == SortArg.Promet)
                OrderEmpStats.OrderByPromet(this.showVraboteni, this.SortStat == SortingOrder.Asc);
            else if (this.SortParam == SortArg.Procent)
                OrderEmpStats.OrderByPercent(this.showVraboteni, this.SortStat == SortingOrder.Asc);
        }
        public void vcitajTabela()
        {
            String sql = getSqlVer2();
            OracleCommand cmd = new OracleCommand(sql, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID1", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("MESEC1", OracleDbType.NChar);
                prm.Value = lblMesec.Text;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("GODINA1", OracleDbType.NChar);
                prm.Value = lblGodina.Text;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("DATUM1", OracleDbType.Varchar2);
                prm.Value = lblMesec.Text+"/"+lblGodina.Text;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID2", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID3", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("MESEC2", OracleDbType.NChar);
                prm.Value = lblMesec.Text;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("GODINA2", OracleDbType.NChar);
                prm.Value = lblGodina.Text;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("DATUM2", OracleDbType.Varchar2);
                prm.Value = lblMesec.Text + "/" + lblGodina.Text;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("REST_ID4", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string ime = dr.GetString(1);
                    string prezime = dr.GetString(2);
                    int promet = dr.GetInt32(3);
                  //  lblPromet.Text = String.Format("{0}", dr.GetInt32(4));
                    decimal procent = dr.GetDecimal(5);
                    VrabPrometProcent v = new VrabPrometProcent(id, ime, prezime, promet, procent);
                    vraboteni.Add(v);
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
        public void vcitajFunkcii()
        {
            foreach(VrabPrometProcent v in vraboteni)
            {
                vcitajFunkcija(v);
            }
        }
        private void vcitajFunkcija(VrabPrometProcent v)
        {
            String sql = "select pozicija from izvrshuva where restoran_id= :REST_ID and VRABOTEN_ID= :VRAB_ID";
            OracleCommand cmd = new OracleCommand(sql, Conn);

            try
            {
                OracleParameter prm = new OracleParameter("REST_ID", OracleDbType.Int64);
                prm.Value = this.RestoranID;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("VRAB_ID", OracleDbType.Int64);
                prm.Value = v.VrabID;
                cmd.Parameters.Add(prm);

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    v.Funkcija = dr.GetString(0);
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
        public void vcitajPromet(){
            String sql="select iznos_promet from promet where mesec_promet = :MESEC and godina_promet= :GODINA";
            OracleCommand cmd = new OracleCommand(sql, Conn);

           try
            {
                OracleParameter prm = new OracleParameter("MESEC", OracleDbType.Int64);
                prm.Value = lblMesec.Text;
                cmd.Parameters.Add(prm);

                prm = new OracleParameter("GODINA", OracleDbType.Int64);
                prm.Value = lblGodina.Text;
                cmd.Parameters.Add(prm);

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                
                if(dr.Read())
                {
                    lblPromet.Text=String.Format(": {0} ден. :",dr.GetInt32(0));
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
        public void postaviTekovenDatum()
        {
            this.tekovenMesec = DateTime.Now.Month;
            this.tekovnaGodina = DateTime.Now.Year;
            azurirajMesecGodina();
        }
        public void azurirajMesecGodina()
        {
          
            if (tekovenMesec >= 1 && tekovenMesec <= 9)
            {
                lblMesec.Text = String.Format("0{0}", tekovenMesec);
            }
            else
            {
                lblMesec.Text = String.Format("{0}", tekovenMesec);
            }
            lblGodina.Text = String.Format("{0}", tekovnaGodina);
        }
        // da se smeni probnoto so komentarite
        private void Izv1PridonesVoPromet_Load(object sender, EventArgs e)
        {
            postaviTekovenDatum();
            // vcitajPromet();
            // vcitajTabela();
            //vcitajFunkcii();
            //FiltrirajVraboteni();
            probno();
            popolniLabeli();
        
        }
        private void btnVnesi_Click(object sender, EventArgs e)
        {
            indexStart = 0;
            vraboteni.Clear();
            vcitajTabela();
            vcitajFunkcii();
            FiltrirajVraboteni();
            popolniLabeli();
            vcitajPromet();
        }
       

        

        private void pbleftM_Click(object sender, EventArgs e)
        {
            tekovenMesec--;
            if (tekovenMesec == 0) tekovenMesec = 12;
            azurirajMesecGodina();
        }

        private void pbrightM_Click(object sender, EventArgs e)
        {
            tekovenMesec++;
            if (tekovenMesec == 13) tekovenMesec = 1;
            azurirajMesecGodina();
        }

        private void pbLeftG_Click(object sender, EventArgs e)
        {

            if (tekovnaGodina > 1950)
            {
                tekovnaGodina--;
                azurirajMesecGodina();
            }
        }

        private void pbRightG_Click(object sender, EventArgs e)
        {
            if (tekovnaGodina < DateTime.Now.Year)
            {
                tekovnaGodina++;
                azurirajMesecGodina();
            }
           
        }

        private void pbleftM_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pbleftM_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb= sender as PictureBox;
            pb.Image = Resources.DarkArrowLeft;
        }
 
        private void pbrightM_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pbrightM_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowRight;
        }

        private void pbLeftG_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pbLeftG_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowLeft;
        }

        private void pbRightG_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pbRightG_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowRight;
        }

        private void btnTekoven_Click(object sender, EventArgs e)
        {
            postaviTekovenDatum();
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (showVraboteni.Count - indexStart -1 >= 8)
            {
                indexStart++;
                popolniLabeli();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (indexStart > 0)
            {
                indexStart--;
                popolniLabeli();
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowUp;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowUp;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowDown;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowDown;
        }

        private void btnVnesi_MouseEnter(object sender, EventArgs e)
        {
            this.btnVnesi.Image = Resources.LightButton___Copy;
            this.btnVnesi.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnVnesi_MouseLeave(object sender, EventArgs e)
        {
            this.btnVnesi.Image = Resources.DarkButton___Copy;
            this.btnVnesi.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnSortprezime_Click(object sender, EventArgs e)
        {
            if (this.SortParam == SortArg.Prezime)
                this.ChangeOrder();
            else
            {
                this.SortParam = SortArg.Prezime;
                this.SortStat = SortingOrder.Asc;
            }
            UpdateImages(pbPrezime);
            PodrediVrab();
            popolniLabeli();

        }

        private void btnSortIme_Click(object sender, EventArgs e)
        {
            if (this.SortParam == SortArg.Ime)
                this.ChangeOrder();
            else
            {
                this.SortParam = SortArg.Ime;
                this.SortStat = SortingOrder.Asc;
            }
            UpdateImages(pbIme);
            PodrediVrab();
            popolniLabeli();

        }

        private void btnPromet_Click(object sender, EventArgs e)
        {
            if (this.SortParam == SortArg.Promet)
                this.ChangeOrder();
            else
            {
                this.SortParam = SortArg.Promet;
                this.SortStat = SortingOrder.Asc;
            }
            UpdateImages(pbPromet);
            PodrediVrab();
            popolniLabeli();
        }

        private void btnProcent_Click(object sender, EventArgs e)
        {
            if (this.SortParam == SortArg.Procent)
                this.ChangeOrder();
            else
            {
                this.SortParam = SortArg.Procent;
                this.SortStat = SortingOrder.Asc;
            }
            UpdateImages(pbProcent);
            PodrediVrab();
            popolniLabeli();
        }

        public String getSqlVer2()
        {
            String sql = @"select id, ime, prezime, promet_vraboten, vk_promet_restoran, (round(promet_vraboten/vk_promet_restoran*100,2)) as procent  from
(

select A.id, A.ime, A.prezime, A.promet_vraboten, A.vk_promet_restoran from
(select vr.vraboten_id as id,vr.ime_vraboten as ime, vr.prezime_vraboten as prezime,sum( n.vkupna_cena) as promet_vraboten, p.iznos_promet as vk_promet_restoran
from vraboten vr join online_narachka ol on vr.vraboten_id=ol.vraboten_id
join narachka n on ol.restoran_id=n.restoran_id and ol.narachka_id=n.narachka_id and n.restoran_id= :REST_ID1
and n.realizirana=1 
join restoran r on n.restoran_id=r.restoran_id
join promet p on p.restoran_id=r.restoran_id
where p.mesec_promet= :MESEC1 and p.godina_promet= :GODINA1
and to_char(n.vreme,'MM/YYYY')= :DATUM1
and vr.vraboten_id in
(
select v.vraboten_id
from vraboten v join izvrshuva i on v.vraboten_id=i.vraboten_id
where i.pozicija='Доставувач' and i.restoran_id= :REST_ID2
)
group by (vr.vraboten_id, vr.ime_vraboten, vr.prezime_vraboten, p.iznos_promet)
)A


union


select B.id, B.ime, B.prezime, B.promet_vraboten, B.vk_promet_restoran from
(select vr.vraboten_id as id,vr.ime_vraboten as ime, vr.prezime_vraboten as prezime,sum( n.vkupna_cena) as promet_vraboten, p.iznos_promet as vk_promet_restoran
from vraboten vr join onsite_narachka os on vr.vraboten_id=os.vraboten_id
join narachka n on os.restoran_id=n.restoran_id and os.narachka_id=n.narachka_id and n.restoran_id= :REST_ID3 
and n.realizirana=1 
join restoran r on n.restoran_id=r.restoran_id
join promet p on p.restoran_id=r.restoran_id
where p.mesec_promet= :MESEC2 and p.godina_promet= :GODINA2
and to_char(n.vreme,'MM/YYYY')= :DATUM2
and vr.vraboten_id in
(
select v.vraboten_id
from vraboten v join izvrshuva i on v.vraboten_id=i.vraboten_id
where i.pozicija='Келнер' and i.restoran_id= :REST_ID4
)
group by (vr.vraboten_id, vr.ime_vraboten, vr.prezime_vraboten, p.iznos_promet)
)B

)
order by procent desc";
            return sql;
        }

        private void pbPozLeft_Click(object sender, EventArgs e)
        {
            if (this.Pozicii.Count > 1)
            {
                if (this.PozInd == 0)
                    this.PozInd = this.Pozicii.Count;
                this.PozInd--;
                this.UpdatePozicii();
            }
        }

        private void pbPozRight_Click(object sender, EventArgs e)
        {
            if (this.Pozicii.Count > 1)
            {
                this.PozInd = (this.PozInd + 1) % this.Pozicii.Count;
                this.UpdatePozicii();
            }
        }

        private void pbPozLeft_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowLeft;
        }

        private void pbPozLeft_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowLeft;
        }

        private void pbPozRight_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.LightArrowRight___Copy;
        }

        private void pbPozRight_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.Image = Resources.DarkArrowRight;
        }

        private void btnSortIme_MouseEnter(object sender, EventArgs e)
        {
            this.btnSortIme.Image = Resources.LightButton___Copy;
            this.btnSortIme.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnSortIme_MouseLeave(object sender, EventArgs e)
        {
            this.btnSortIme.Image = Resources.DarkButton___Copy;
            this.btnSortIme.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnSortprezime_MouseEnter(object sender, EventArgs e)
        {
            this.btnSortprezime.Image = Resources.LightButton___Copy;
            this.btnSortprezime.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnSortprezime_MouseLeave(object sender, EventArgs e)
        {
            this.btnSortprezime.Image = Resources.DarkButton___Copy;
            this.btnSortprezime.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnPromet_MouseEnter(object sender, EventArgs e)
        {
            this.btnPromet.Image = Resources.LightButton___Copy;
            this.btnPromet.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnPromet_MouseLeave(object sender, EventArgs e)
        {
            this.btnPromet.Image = Resources.DarkButton___Copy;
            this.btnPromet.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

        private void btnProcent_MouseEnter(object sender, EventArgs e)
        {
            this.btnProcent.Image = Resources.LightButton___Copy;
            this.btnProcent.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnProcent_MouseLeave(object sender, EventArgs e)
        {
            this.btnProcent.Image = Resources.DarkButton___Copy;
            this.btnProcent.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }
    }
}