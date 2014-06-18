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
    public partial class Login : BackgroundForm
    {
        private OracleConnection Conn { get; set; }
        private Vraboten vraboten;
        private String username;
        private String password;
        private bool tocenUser;
        private bool tocenPass;
        private int VrabotenId;
        private String ime;
        private String prezime;
        private String pozicija="";
        private int RestoranId;
        private int status;
        public Login(OracleConnection conn)
        {
            InitializeComponent();
            Conn = conn;
            username = "";
            password = "";
            ime = "";
            prezime = "";
            status = 0;
            tocenUser = false;
            tocenPass = false;
        }

        private void button1Log_Click(object sender, EventArgs e)
        {
            
        }

        private void logiranje()
        {
            {

                username = tbUserName.Text;
                password = tbPassword.Text;
                if (username == "")
                {
                    MessageBoxForm mbf = new MessageBoxForm("Внесeте корисничко име!", false);
                    mbf.ShowDialog();
                    tbUserName.Select();
                    
                }
                else if (password == "")
                {
                    MessageBoxForm mbf = new MessageBoxForm("Внесeте лозинка!", false);
                    mbf.ShowDialog();
                    tbPassword.Select();
                }
                else
                {

                    string sql = @"SELECT LOZINKA FROM KORISNIK WHERE KORISNICHKO_IME = :KOR_IME"; // C#
                    OracleCommand cmd = new OracleCommand(sql, Conn);
                    try
                    {
                        OracleParameter prm = new OracleParameter("KOR_IME", OracleDbType.Varchar2);
                        prm.Value = username;
                        cmd.Parameters.Add(prm);
                        cmd.CommandType = CommandType.Text;
                        OracleDataReader dr = cmd.ExecuteReader();
                        String realPass = ""; // tocniot password od bazata

                        if (dr.Read()) // ako uspee da procita znaci postoi toa korisnicko ime
                        {
                            realPass = dr.GetString(0);
                            tocenUser = true;
                        }
                        else // ne postoi toa korisnicko ime
                        {
                            tocenUser = false;
                            MessageBoxForm mbf = new MessageBoxForm("Не постои тоа корисничко име. Обидете се повторно.", false);
                            mbf.ShowDialog();
                            tbPassword.Clear();
                            tbUserName.Clear();
                            tbUserName.Select();

                        }

                        // ako postoi korisnickoto ime proveri dali vneseniot password (password) se sovpagja so vistinskiot (realPass)
                        if (tocenUser)
                        {
                            if (password == realPass)
                            {
                                //MessageBox.Show("tocen pasvord");
                                tocenPass = true;
                            }
                            else
                            {
                                MessageBoxForm mbf = new MessageBoxForm("Внесовте погрешна лозинка. Обидете се повторно.", false);
                                mbf.ShowDialog();
                                tocenPass = false;
                                tbPassword.Clear();
                            }
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

                    //ako e vnesen tocniot password vcitaj gi podatocite za vraboteniot
                    if (tocenPass)
                    {
                        sql = @"Select v.vraboten_id, v.ime_vraboten, v.prezime_vraboten,i.Pozicija, i.status,r.restoran_id From Korisnik k join Vraboten v on k.Vraboten_ID=v.Vraboten_ID Join Izvrshuva i on i.Vraboten_ID=v.Vraboten_ID Join Restoran r on r.Restoran_ID=i.Restoran_ID where korisnichko_ime = :KOR_IME";
                        cmd = new OracleCommand(sql, Conn);
                        try
                        {
                            OracleParameter prm = new OracleParameter("KOR_IME", OracleDbType.Varchar2);
                            prm.Value = username;
                            cmd.Parameters.Add(prm);
                            cmd.CommandType = CommandType.Text;
                            OracleDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                VrabotenId = (int)dr.GetValue(0);
                                ime = dr.GetString(1);
                                prezime = dr.GetString(2);
                                pozicija = dr.GetString(3);
                                int st;
                                if (int.TryParse(dr.GetString(4), out st))
                                    status = st;
                                RestoranId = (int)dr.GetValue(5);
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


                        if (status == 0) //ako vraboteniot e neaktiven, nema privilegii za pristap
                        {
                            MessageBoxForm mbf = new MessageBoxForm("Немате привилегии за пристап!", false);
                            if (mbf.ShowDialog() == DialogResult.Yes)
                                this.Close();
                            else
                                this.Close();
                        }
                        else
                        {
                            if (pozicija.ToLower() == "доставувач")
                                vraboten = new Dostavuvac(VrabotenId, RestoranId, ime, prezime, username, password);
                            else if (pozicija.ToLower() == "келнер")
                                vraboten = new Kelner(VrabotenId, RestoranId, ime, prezime, username, password);

                            if (pozicija.ToLower() == "менаџер")
                            {
                                ManagerC manager = new ManagerC(VrabotenId, RestoranId, ime, prezime, username, password);
                                ManagerForma managerForma = new ManagerForma(Conn, manager);
                                managerForma.Show();
                                tbPassword.Clear();
                            }
                            else
                            {
                                VrabotenForma vf = new VrabotenForma(Conn, vraboten);
                                vf.Show();
                                tbPassword.Clear();
                            }
                        }
                        //MessageBox.Show(String.Format("Vraboten: {0}, Pozicija: {1}, RestoranID: {2}",VrabotenId,pozicija,RestoranId));
                    }
                }
            }
        }
       

       
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            logiranje();
            
        }

        private void btnLogIn_MouseEnter(object sender, EventArgs e)
        {
            this.btnLogIn.Image = Resources.LightButton___Copy;
            this.btnLogIn.ForeColor = Color.Sienna;
            this.Cursor = Cursors.Hand;
        }

        private void btnLogIn_MouseLeave(object sender, EventArgs e)
        {
            this.btnLogIn.Image = Resources.DarkButton___Copy;
            this.btnLogIn.ForeColor = Color.Khaki;
            this.Cursor = Cursors.Default;
        }

       
    }
}

