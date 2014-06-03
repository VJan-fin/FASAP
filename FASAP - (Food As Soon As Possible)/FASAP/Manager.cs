using System;
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
    public partial class Manager : Form
    {
        private OracleConnection Conn { get; set; }
        public int VrabotenID { get; set; }
        public int RestoranID { get; set; }

        public Manager(OracleConnection conn, int vrabID, int resID)
        {
            InitializeComponent();
            Conn = conn;
            VrabotenID = vrabID;
            RestoranID = resID;
            this.dataGridView1.Rows.Add("4", "Бојан Бојоски", 2, 0, 2);
            this.dataGridView1.Rows.Add("2", "Радослав Стрезоски", 0, 1, 1);
            this.dataGridView1.Rows.Add("3", "Предраг Ќирески", 0, 1, 1);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT IME_VRABOTEN, PREZIME_VRABOTEN FROM VRABOTEN WHERE VRABOTEN_ID = :VRAB_ID";
            OracleCommand cmd = new OracleCommand(sql, Conn);
            OracleParameter prm = new OracleParameter("VRAB_ID", OracleDbType.Varchar2);
            prm.Value = VrabotenID;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            string imeIPrezime = "";
            if (dr.Read())
            {
                imeIPrezime = dr.GetString(0) + " " + dr.GetString(1);
            }
            this.tbImePrezime.Text = imeIPrezime;

            sql = @"SELECT IME_RESTORAN FROM RESTORAN WHERE RESTORAN_ID= :RES_ID";
            cmd = new OracleCommand(sql, Conn);
            prm = new OracleParameter("VRAB_ID", OracleDbType.Varchar2);
            prm.Value = RestoranID;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label1.Text = dr.GetString(0);
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
