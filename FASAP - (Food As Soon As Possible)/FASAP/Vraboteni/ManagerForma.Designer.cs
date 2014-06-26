namespace SmetkaZaNaracka
{
    partial class ManagerForma
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerForma));
            this.lblrest = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.buttonFASAP1 = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.labelFASAP2 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lblManager = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.btnVraboteni = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.labelFASAP7 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.dbLayoutPanel1 = new SmetkaZaNaracka.DBLayoutPanel(this.components);
            this.btnPregledProdazba = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.btnPregledRegioni = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.labelFASAP6 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.btnPregledPromet = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.btnPridonesPromet = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.btnKvartalnaSostojba = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.labelFASAP3 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP8 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP4 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP5 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.btnPonuda = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.btnInformacii = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.tbURL = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.dbLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblrest
            // 
            this.lblrest.BackColor = System.Drawing.Color.Transparent;
            this.lblrest.Font = new System.Drawing.Font("Trebuchet MS", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblrest.ForeColor = System.Drawing.Color.Khaki;
            this.lblrest.Image = ((System.Drawing.Image)(resources.GetObject("lblrest.Image")));
            this.lblrest.LblObject = null;
            this.lblrest.Location = new System.Drawing.Point(48, 26);
            this.lblrest.Name = "lblrest";
            this.lblrest.Size = new System.Drawing.Size(427, 100);
            this.lblrest.TabIndex = 4;
            this.lblrest.Text = "labelFASAP1";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(57, 261);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(162, 135);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // buttonFASAP1
            // 
            this.buttonFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.buttonFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFASAP1.ForeColor = System.Drawing.Color.Khaki;
            this.buttonFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("buttonFASAP1.Image")));
            this.buttonFASAP1.Location = new System.Drawing.Point(242, 349);
            this.buttonFASAP1.Name = "buttonFASAP1";
            this.buttonFASAP1.Size = new System.Drawing.Size(164, 47);
            this.buttonFASAP1.TabIndex = 7;
            this.buttonFASAP1.Text = "Промени слика";
            this.buttonFASAP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonFASAP1.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.buttonFASAP1.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // labelFASAP2
            // 
            this.labelFASAP2.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP2.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP2.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP2.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP2.Image")));
            this.labelFASAP2.LblObject = null;
            this.labelFASAP2.Location = new System.Drawing.Point(242, 263);
            this.labelFASAP2.Name = "labelFASAP2";
            this.labelFASAP2.Size = new System.Drawing.Size(164, 27);
            this.labelFASAP2.TabIndex = 8;
            this.labelFASAP2.Text = "URL на сликата";
            // 
            // lblManager
            // 
            this.lblManager.BackColor = System.Drawing.Color.Transparent;
            this.lblManager.Font = new System.Drawing.Font("Trebuchet MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManager.ForeColor = System.Drawing.Color.Khaki;
            this.lblManager.Image = ((System.Drawing.Image)(resources.GetObject("lblManager.Image")));
            this.lblManager.LblObject = null;
            this.lblManager.Location = new System.Drawing.Point(204, 150);
            this.lblManager.Name = "lblManager";
            this.lblManager.Size = new System.Drawing.Size(434, 49);
            this.lblManager.TabIndex = 9;
            this.lblManager.Text = "labelFASAP3";
            // 
            // labelFASAP1
            // 
            this.labelFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP1.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP1.Image")));
            this.labelFASAP1.LblObject = null;
            this.labelFASAP1.Location = new System.Drawing.Point(52, 165);
            this.labelFASAP1.Name = "labelFASAP1";
            this.labelFASAP1.Size = new System.Drawing.Size(133, 34);
            this.labelFASAP1.TabIndex = 20;
            this.labelFASAP1.Text = "Менаџер:";
            // 
            // btnVraboteni
            // 
            this.btnVraboteni.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVraboteni.BackColor = System.Drawing.Color.Transparent;
            this.btnVraboteni.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnVraboteni.ForeColor = System.Drawing.Color.Khaki;
            this.btnVraboteni.Image = ((System.Drawing.Image)(resources.GetObject("btnVraboteni.Image")));
            this.btnVraboteni.Location = new System.Drawing.Point(296, 622);
            this.btnVraboteni.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnVraboteni.Name = "btnVraboteni";
            this.btnVraboteni.Size = new System.Drawing.Size(207, 73);
            this.btnVraboteni.TabIndex = 53;
            this.btnVraboteni.Text = "Управување со вработени";
            this.btnVraboteni.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnVraboteni.Click += new System.EventHandler(this.btnVraboteni_Click);
            this.btnVraboteni.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.btnVraboteni.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // labelFASAP7
            // 
            this.labelFASAP7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFASAP7.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP7.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP7.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP7.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP7.Image")));
            this.labelFASAP7.LblObject = null;
            this.labelFASAP7.Location = new System.Drawing.Point(22, 71);
            this.labelFASAP7.Name = "labelFASAP7";
            this.labelFASAP7.Size = new System.Drawing.Size(320, 75);
            this.labelFASAP7.TabIndex = 17;
            this.labelFASAP7.Text = "Промет по месеци за избраната година\r\n";
            // 
            // dbLayoutPanel1
            // 
            this.dbLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.dbLayoutPanel1.ColumnCount = 2;
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.6072F));
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.3928F));
            this.dbLayoutPanel1.Controls.Add(this.btnPregledProdazba, 1, 5);
            this.dbLayoutPanel1.Controls.Add(this.btnPregledRegioni, 1, 4);
            this.dbLayoutPanel1.Controls.Add(this.labelFASAP6, 0, 5);
            this.dbLayoutPanel1.Controls.Add(this.btnPregledPromet, 1, 1);
            this.dbLayoutPanel1.Controls.Add(this.btnPridonesPromet, 1, 2);
            this.dbLayoutPanel1.Controls.Add(this.btnKvartalnaSostojba, 1, 3);
            this.dbLayoutPanel1.Controls.Add(this.labelFASAP7, 0, 1);
            this.dbLayoutPanel1.Controls.Add(this.labelFASAP3, 1, 0);
            this.dbLayoutPanel1.Controls.Add(this.labelFASAP8, 0, 2);
            this.dbLayoutPanel1.Controls.Add(this.labelFASAP4, 0, 3);
            this.dbLayoutPanel1.Controls.Add(this.labelFASAP5, 0, 4);
            this.dbLayoutPanel1.Location = new System.Drawing.Point(738, 123);
            this.dbLayoutPanel1.Name = "dbLayoutPanel1";
            this.dbLayoutPanel1.RowCount = 6;
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.420289F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.11594F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.11594F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.11594F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.11594F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.11594F));
            this.dbLayoutPanel1.Size = new System.Drawing.Size(583, 594);
            this.dbLayoutPanel1.TabIndex = 54;
            // 
            // btnPregledProdazba
            // 
            this.btnPregledProdazba.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPregledProdazba.BackColor = System.Drawing.Color.Transparent;
            this.btnPregledProdazba.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnPregledProdazba.ForeColor = System.Drawing.Color.Khaki;
            this.btnPregledProdazba.Image = ((System.Drawing.Image)(resources.GetObject("btnPregledProdazba.Image")));
            this.btnPregledProdazba.Location = new System.Drawing.Point(370, 502);
            this.btnPregledProdazba.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnPregledProdazba.Name = "btnPregledProdazba";
            this.btnPregledProdazba.Size = new System.Drawing.Size(207, 73);
            this.btnPregledProdazba.TabIndex = 58;
            this.btnPregledProdazba.Text = "Преглед на продажбата";
            this.btnPregledProdazba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPregledProdazba.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.btnPregledProdazba.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // btnPregledRegioni
            // 
            this.btnPregledRegioni.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPregledRegioni.BackColor = System.Drawing.Color.Transparent;
            this.btnPregledRegioni.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnPregledRegioni.ForeColor = System.Drawing.Color.Khaki;
            this.btnPregledRegioni.Image = ((System.Drawing.Image)(resources.GetObject("btnPregledRegioni.Image")));
            this.btnPregledRegioni.Location = new System.Drawing.Point(370, 393);
            this.btnPregledRegioni.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnPregledRegioni.Name = "btnPregledRegioni";
            this.btnPregledRegioni.Size = new System.Drawing.Size(207, 73);
            this.btnPregledRegioni.TabIndex = 58;
            this.btnPregledRegioni.Text = "Преглед по региони";
            this.btnPregledRegioni.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPregledRegioni.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.btnPregledRegioni.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // labelFASAP6
            // 
            this.labelFASAP6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFASAP6.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP6.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP6.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP6.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP6.Image")));
            this.labelFASAP6.LblObject = null;
            this.labelFASAP6.Location = new System.Drawing.Point(22, 491);
            this.labelFASAP6.Name = "labelFASAP6";
            this.labelFASAP6.Size = new System.Drawing.Size(320, 95);
            this.labelFASAP6.TabIndex = 62;
            this.labelFASAP6.Text = "Споредба на продажбата на производите чија цена била променета";
            // 
            // btnPregledPromet
            // 
            this.btnPregledPromet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPregledPromet.BackColor = System.Drawing.Color.Transparent;
            this.btnPregledPromet.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnPregledPromet.ForeColor = System.Drawing.Color.Khaki;
            this.btnPregledPromet.Image = ((System.Drawing.Image)(resources.GetObject("btnPregledPromet.Image")));
            this.btnPregledPromet.Location = new System.Drawing.Point(370, 72);
            this.btnPregledPromet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnPregledPromet.Name = "btnPregledPromet";
            this.btnPregledPromet.Size = new System.Drawing.Size(207, 73);
            this.btnPregledPromet.TabIndex = 56;
            this.btnPregledPromet.Text = "Преглед на прометот";
            this.btnPregledPromet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPregledPromet.Click += new System.EventHandler(this.btnPregledPromet_Click);
            this.btnPregledPromet.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.btnPregledPromet.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // btnPridonesPromet
            // 
            this.btnPridonesPromet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPridonesPromet.BackColor = System.Drawing.Color.Transparent;
            this.btnPridonesPromet.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnPridonesPromet.ForeColor = System.Drawing.Color.Khaki;
            this.btnPridonesPromet.Image = ((System.Drawing.Image)(resources.GetObject("btnPridonesPromet.Image")));
            this.btnPridonesPromet.Location = new System.Drawing.Point(370, 179);
            this.btnPridonesPromet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnPridonesPromet.Name = "btnPridonesPromet";
            this.btnPridonesPromet.Size = new System.Drawing.Size(207, 73);
            this.btnPridonesPromet.TabIndex = 55;
            this.btnPridonesPromet.Text = "Придонес во прометот";
            this.btnPridonesPromet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPridonesPromet.Click += new System.EventHandler(this.btnPridonesPromet_Click);
            this.btnPridonesPromet.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.btnPridonesPromet.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // btnKvartalnaSostojba
            // 
            this.btnKvartalnaSostojba.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKvartalnaSostojba.BackColor = System.Drawing.Color.Transparent;
            this.btnKvartalnaSostojba.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnKvartalnaSostojba.ForeColor = System.Drawing.Color.Khaki;
            this.btnKvartalnaSostojba.Image = ((System.Drawing.Image)(resources.GetObject("btnKvartalnaSostojba.Image")));
            this.btnKvartalnaSostojba.Location = new System.Drawing.Point(370, 286);
            this.btnKvartalnaSostojba.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnKvartalnaSostojba.Name = "btnKvartalnaSostojba";
            this.btnKvartalnaSostojba.Size = new System.Drawing.Size(207, 73);
            this.btnKvartalnaSostojba.TabIndex = 57;
            this.btnKvartalnaSostojba.Text = "Квартална состојба";
            this.btnKvartalnaSostojba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnKvartalnaSostojba.Click += new System.EventHandler(this.btnKvartalnaSostojba_Click);
            this.btnKvartalnaSostojba.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.btnKvartalnaSostojba.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // labelFASAP3
            // 
            this.labelFASAP3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFASAP3.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP3.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP3.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP3.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP3.Image")));
            this.labelFASAP3.LblObject = null;
            this.labelFASAP3.Location = new System.Drawing.Point(386, 7);
            this.labelFASAP3.Name = "labelFASAP3";
            this.labelFASAP3.Size = new System.Drawing.Size(175, 41);
            this.labelFASAP3.TabIndex = 58;
            this.labelFASAP3.Text = "Извештаи";
            // 
            // labelFASAP8
            // 
            this.labelFASAP8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFASAP8.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP8.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP8.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP8.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP8.Image")));
            this.labelFASAP8.LblObject = null;
            this.labelFASAP8.Location = new System.Drawing.Point(22, 168);
            this.labelFASAP8.Name = "labelFASAP8";
            this.labelFASAP8.Size = new System.Drawing.Size(320, 95);
            this.labelFASAP8.TabIndex = 59;
            this.labelFASAP8.Text = "Процентуално учество на вработените во вкупниот промет";
            // 
            // labelFASAP4
            // 
            this.labelFASAP4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFASAP4.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP4.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP4.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP4.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP4.Image")));
            this.labelFASAP4.LblObject = null;
            this.labelFASAP4.Location = new System.Drawing.Point(22, 285);
            this.labelFASAP4.Name = "labelFASAP4";
            this.labelFASAP4.Size = new System.Drawing.Size(320, 75);
            this.labelFASAP4.TabIndex = 60;
            this.labelFASAP4.Text = "Целокупна финансиска состојба по квартали";
            // 
            // labelFASAP5
            // 
            this.labelFASAP5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFASAP5.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP5.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP5.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP5.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP5.Image")));
            this.labelFASAP5.LblObject = null;
            this.labelFASAP5.Location = new System.Drawing.Point(22, 382);
            this.labelFASAP5.Name = "labelFASAP5";
            this.labelFASAP5.Size = new System.Drawing.Size(320, 95);
            this.labelFASAP5.TabIndex = 61;
            this.labelFASAP5.Text = "Број и процентуална застапеност на нарачките по региони";
            // 
            // btnPonuda
            // 
            this.btnPonuda.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPonuda.BackColor = System.Drawing.Color.Transparent;
            this.btnPonuda.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnPonuda.ForeColor = System.Drawing.Color.Khaki;
            this.btnPonuda.Image = ((System.Drawing.Image)(resources.GetObject("btnPonuda.Image")));
            this.btnPonuda.Location = new System.Drawing.Point(52, 622);
            this.btnPonuda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnPonuda.Name = "btnPonuda";
            this.btnPonuda.Size = new System.Drawing.Size(207, 73);
            this.btnPonuda.TabIndex = 54;
            this.btnPonuda.Text = "Управување со понудата";
            this.btnPonuda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPonuda.Click += new System.EventHandler(this.btnPonuda_Click);
            this.btnPonuda.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.btnPonuda.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // btnInformacii
            // 
            this.btnInformacii.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnInformacii.BackColor = System.Drawing.Color.Transparent;
            this.btnInformacii.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnInformacii.ForeColor = System.Drawing.Color.Khaki;
            this.btnInformacii.Image = ((System.Drawing.Image)(resources.GetObject("btnInformacii.Image")));
            this.btnInformacii.Location = new System.Drawing.Point(52, 519);
            this.btnInformacii.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnInformacii.Name = "btnInformacii";
            this.btnInformacii.Size = new System.Drawing.Size(207, 73);
            this.btnInformacii.TabIndex = 55;
            this.btnInformacii.Text = "Информации за ресторанот";
            this.btnInformacii.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInformacii.Click += new System.EventHandler(this.btnInfo_Click);
            this.btnInformacii.MouseEnter += new System.EventHandler(this.btnPregledPromet_MouseEnter);
            this.btnInformacii.MouseLeave += new System.EventHandler(this.btnPregledPromet_MouseLeave);
            // 
            // tbURL
            // 
            this.tbURL.BackColor = System.Drawing.Color.Sienna;
            this.tbURL.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbURL.ForeColor = System.Drawing.Color.Khaki;
            this.tbURL.Location = new System.Drawing.Point(246, 302);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(358, 30);
            this.tbURL.TabIndex = 56;
            // 
            // ManagerForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.btnInformacii);
            this.Controls.Add(this.btnPonuda);
            this.Controls.Add(this.btnVraboteni);
            this.Controls.Add(this.dbLayoutPanel1);
            this.Controls.Add(this.labelFASAP1);
            this.Controls.Add(this.lblManager);
            this.Controls.Add(this.labelFASAP2);
            this.Controls.Add(this.buttonFASAP1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.lblrest);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ManagerForma";
            this.Opacity = 1D;
            this.Text = "ManagerForma";
            this.Controls.SetChildIndex(this.lblrest, 0);
            this.Controls.SetChildIndex(this.pictureBox3, 0);
            this.Controls.SetChildIndex(this.buttonFASAP1, 0);
            this.Controls.SetChildIndex(this.labelFASAP2, 0);
            this.Controls.SetChildIndex(this.lblManager, 0);
            this.Controls.SetChildIndex(this.labelFASAP1, 0);
            this.Controls.SetChildIndex(this.dbLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.btnVraboteni, 0);
            this.Controls.SetChildIndex(this.btnPonuda, 0);
            this.Controls.SetChildIndex(this.btnInformacii, 0);
            this.Controls.SetChildIndex(this.tbURL, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.dbLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelFASAP lblrest;
        private System.Windows.Forms.PictureBox pictureBox3;
        private ButtonFASAP buttonFASAP1;
        private LabelFASAP labelFASAP2;
        private LabelFASAP lblManager;
        private LabelFASAP labelFASAP1;
        private ButtonFASAP btnVraboteni;
        private LabelFASAP labelFASAP7;
        private DBLayoutPanel dbLayoutPanel1;
        private ButtonFASAP btnPonuda;
        private ButtonFASAP btnPregledPromet;
        private ButtonFASAP btnPridonesPromet;
        private ButtonFASAP btnKvartalnaSostojba;
        private LabelFASAP labelFASAP3;
        private LabelFASAP labelFASAP8;
        private LabelFASAP labelFASAP4;
        private ButtonFASAP btnPregledProdazba;
        private ButtonFASAP btnPregledRegioni;
        private LabelFASAP labelFASAP6;
        private LabelFASAP labelFASAP5;
        private ButtonFASAP btnInformacii;
        private System.Windows.Forms.TextBox tbURL;
    }
}