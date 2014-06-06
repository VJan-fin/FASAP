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
    public partial class VrabotenForma : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Vraboten Vraboten { get; set; }

        public VrabotenForma(OracleConnection conn, Vraboten vraboten)
        {
            InitializeComponent();
            this.Conn = conn;
            this.Vraboten = vraboten;
        }

        private void VrabotenForma_Load(object sender, EventArgs e)
        {
            /*string sql = @"SELECT IME_VRABOTEN, PREZIME_VRABOTEN FROM VRABOTEN WHERE VRABOTEN_ID = :VRAB_ID";
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
             * */
            string podatoci = Vraboten.Ime + " " + Vraboten.Prezime + " " + Vraboten.Pozicija + " " + Vraboten.RestoranID + " ";
            this.label2.Text = podatoci;
        }
    }
}
