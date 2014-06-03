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
    public partial class Login : Form
    {
        private OracleConnection Conn { get; set; }
        private String username;
        private String password;
        private bool tocenUser;
        private bool tocenPass;
        private int VrabotenId;
        private String pozicija="";
        private int RestoranId;
        public Login(OracleConnection conn)
        {
            InitializeComponent();
            Conn = conn;
            username = "";
            password = "";
            tocenUser = false;
            tocenPass = false;
        }

        private void button1Log_Click(object sender, EventArgs e)
        {
             username = tbUserName.Text;
             password = tbPassword.Text;

            string sql = @"SELECT LOZINKA FROM KORISNIK WHERE KORISNICHKO_IME = :KOR_IME"; // C#
            OracleCommand cmd = new OracleCommand(sql, Conn);
            OracleParameter prm = new OracleParameter("KOR_IME", OracleDbType.Varchar2);
            prm.Value = username;
            cmd.Parameters.Add(prm);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            String realPass = "";
           
            if (dr.Read())
            {
                realPass = dr.GetString(0);
                tocenUser = true;
            }
            else
            {
                tocenUser = false;
                MessageBox.Show("Не постои тоа корисничко име. Обидете се повторно.");
            }
            if (tocenUser)
            {
                if (password == realPass)
                {
                    //MessageBox.Show("tocen pasvord");
                    tocenPass = true;
                }
                else
                {
                    MessageBox.Show("Внесовте погрешна лозинка. Обидете се повторно.");
                    tocenPass = false;
                }
            }

            if (tocenPass)
            {
                sql = @"Select v.vraboten_id, i.Pozicija, r.restoran_id From Korisnik k join Vraboten v on k.Vraboten_ID=v.Vraboten_ID Join Izvrshuva i on i.Vraboten_ID=v.Vraboten_ID Join Restoran r on r.Restoran_ID=i.Restoran_ID where korisnichko_ime = :KOR_IME";
                 cmd = new OracleCommand(sql, Conn);
                 prm = new OracleParameter("KOR_IME", OracleDbType.Varchar2);
                prm.Value = username;
                cmd.Parameters.Add(prm);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    VrabotenId = (int)dr.GetValue(0);
                    pozicija = dr.GetString(1);
                    RestoranId = (int)dr.GetValue(2);
                }
                if (pozicija == "Менаџер")
                {
                    Manager manager = new Manager(Conn, VrabotenId, RestoranId);
                    manager.Show();
                }
                else
                {
                    VrabotenForma vf = new VrabotenForma(Conn, VrabotenId, RestoranId);
                    vf.Show();
                }
               
                //MessageBox.Show(String.Format("Vraboten: {0}, Pozicija: {1}, RestoranID: {2}",VrabotenId,pozicija,RestoranId));
            }
            
        }
    }
}

