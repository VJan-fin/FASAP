using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmetkaZaNaracka.Properties;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka
{
    public partial class Loading : BackgroundForm
    {
        private OracleConnection Conn { get; set; }

        public Loading()
        {
            InitializeComponent();
            Opacity = 0;
            DoubleBuffered = true;
            timer2.Start();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer2.Stop();
            this.OpenConnection();
            FasapPoceten fp = new FasapPoceten(this.Conn);
            fp.ShowDialog();
            Close();
        }

        private void OpenConnection()
        {
            string oradb = "Data Source=(DESCRIPTION="
          + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1620))"
          + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
          + "User Id=DBA_20132014L_GRP_020;Password=7734924;";
            try
            {
                Conn = new OracleConnection();
                Conn.ConnectionString = oradb;
                Conn.Open();
            }
            catch (Exception ex)
            {
                MessageBoxForm mbf = new MessageBoxForm("Настана грешка при поврзувањето со базата! Проверете ја конекцијата", false);
                if (mbf.ShowDialog() == DialogResult.Yes)
                    this.Close();
                else
                    this.Close();
            }
        }
    }
}
