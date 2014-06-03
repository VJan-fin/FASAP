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
    public partial class VrabotenForma : Form
    {
        private OracleConnection Conn { get; set; }
        public int VrabotenID { get; set; }
        public int RestoranID { get; set; }

        public VrabotenForma(OracleConnection conn, int vrabID, int resID)
        {
            InitializeComponent();
            Conn = conn;
            VrabotenID = vrabID;
            RestoranID = resID;
        }

        private void VrabotenForma_Load(object sender, EventArgs e)
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
            this.label2.Text = imeIPrezime;
        }
    }
}
