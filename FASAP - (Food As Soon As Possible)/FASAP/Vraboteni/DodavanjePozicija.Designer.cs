namespace SmetkaZaNaracka
{
    partial class DodavanjePozicija
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DodavanjePozicija));
            this.lblZab = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lblZad1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lblIme = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.tbImePozicija = new System.Windows.Forms.TextBox();
            this.buttonOtkazi = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.btnDodadi = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.dbLayoutPanel2 = new SmetkaZaNaracka.DBLayoutPanel(this.components);
            this.lbl6 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lbl5 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lbl4 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lbl3 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lbl2 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lbl1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.pbListUp = new System.Windows.Forms.PictureBox();
            this.pbListDown = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnDodadiPoz = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.btnOtstraniPoz = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.dbLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbListUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbListDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblZab
            // 
            this.lblZab.AutoSize = true;
            this.lblZab.BackColor = System.Drawing.Color.Transparent;
            this.lblZab.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblZab.ForeColor = System.Drawing.Color.Khaki;
            this.lblZab.Image = ((System.Drawing.Image)(resources.GetObject("lblZab.Image")));
            this.lblZab.LblObject = null;
            this.lblZab.Location = new System.Drawing.Point(883, 113);
            this.lblZab.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblZab.Name = "lblZab";
            this.lblZab.Size = new System.Drawing.Size(464, 22);
            this.lblZab.TabIndex = 76;
            this.lblZab.Text = "Забелешка: Полињата означени со * се задолжителни    ";
            // 
            // lblZad1
            // 
            this.lblZad1.AutoSize = true;
            this.lblZad1.BackColor = System.Drawing.Color.Transparent;
            this.lblZad1.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblZad1.ForeColor = System.Drawing.Color.Crimson;
            this.lblZad1.Image = ((System.Drawing.Image)(resources.GetObject("lblZad1.Image")));
            this.lblZad1.LblObject = null;
            this.lblZad1.Location = new System.Drawing.Point(1019, 371);
            this.lblZad1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblZad1.Name = "lblZad1";
            this.lblZad1.Size = new System.Drawing.Size(21, 27);
            this.lblZad1.TabIndex = 75;
            this.lblZad1.Text = "*";
            this.lblZad1.Visible = false;
            // 
            // lblIme
            // 
            this.lblIme.BackColor = System.Drawing.Color.Transparent;
            this.lblIme.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.lblIme.ForeColor = System.Drawing.Color.Khaki;
            this.lblIme.Image = ((System.Drawing.Image)(resources.GetObject("lblIme.Image")));
            this.lblIme.LblObject = null;
            this.lblIme.Location = new System.Drawing.Point(450, 362);
            this.lblIme.Name = "lblIme";
            this.lblIme.Size = new System.Drawing.Size(219, 45);
            this.lblIme.TabIndex = 74;
            this.lblIme.Text = "Име на позицијата:";
            this.lblIme.Visible = false;
            // 
            // tbImePozicija
            // 
            this.tbImePozicija.BackColor = System.Drawing.Color.Sienna;
            this.tbImePozicija.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbImePozicija.ForeColor = System.Drawing.Color.Khaki;
            this.tbImePozicija.Location = new System.Drawing.Point(706, 368);
            this.tbImePozicija.Name = "tbImePozicija";
            this.tbImePozicija.Size = new System.Drawing.Size(308, 32);
            this.tbImePozicija.TabIndex = 73;
            this.tbImePozicija.TabStop = false;
            this.tbImePozicija.Visible = false;
            this.tbImePozicija.Validating += new System.ComponentModel.CancelEventHandler(this.tbImePozicija_Validating);
            // 
            // buttonOtkazi
            // 
            this.buttonOtkazi.BackColor = System.Drawing.Color.Transparent;
            this.buttonOtkazi.CausesValidation = false;
            this.buttonOtkazi.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.buttonOtkazi.ForeColor = System.Drawing.Color.Khaki;
            this.buttonOtkazi.Image = ((System.Drawing.Image)(resources.GetObject("buttonOtkazi.Image")));
            this.buttonOtkazi.Location = new System.Drawing.Point(1156, 683);
            this.buttonOtkazi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.buttonOtkazi.Name = "buttonOtkazi";
            this.buttonOtkazi.Size = new System.Drawing.Size(176, 54);
            this.buttonOtkazi.TabIndex = 94;
            this.buttonOtkazi.Text = "Откажи";
            this.buttonOtkazi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonOtkazi.Click += new System.EventHandler(this.buttonOtkazi_Click);
            this.buttonOtkazi.MouseEnter += new System.EventHandler(this.buttonOtkazi_MouseEnter);
            this.buttonOtkazi.MouseLeave += new System.EventHandler(this.buttonOtkazi_MouseLeave);
            // 
            // btnDodadi
            // 
            this.btnDodadi.BackColor = System.Drawing.Color.Transparent;
            this.btnDodadi.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnDodadi.ForeColor = System.Drawing.Color.Khaki;
            this.btnDodadi.Image = ((System.Drawing.Image)(resources.GetObject("btnDodadi.Image")));
            this.btnDodadi.Location = new System.Drawing.Point(950, 683);
            this.btnDodadi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnDodadi.Name = "btnDodadi";
            this.btnDodadi.Size = new System.Drawing.Size(176, 54);
            this.btnDodadi.TabIndex = 93;
            this.btnDodadi.Text = "Додади";
            this.btnDodadi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDodadi.Visible = false;
            this.btnDodadi.Click += new System.EventHandler(this.btnDodadi_Click);
            this.btnDodadi.MouseEnter += new System.EventHandler(this.buttonOtkazi_MouseEnter);
            this.btnDodadi.MouseLeave += new System.EventHandler(this.buttonOtkazi_MouseLeave);
            // 
            // dbLayoutPanel2
            // 
            this.dbLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dbLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.dbLayoutPanel2.ColumnCount = 1;
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel2.Controls.Add(this.lbl6, 0, 6);
            this.dbLayoutPanel2.Controls.Add(this.lbl5, 0, 5);
            this.dbLayoutPanel2.Controls.Add(this.lbl4, 0, 4);
            this.dbLayoutPanel2.Controls.Add(this.lbl3, 0, 3);
            this.dbLayoutPanel2.Controls.Add(this.lbl2, 0, 2);
            this.dbLayoutPanel2.Controls.Add(this.lbl1, 0, 1);
            this.dbLayoutPanel2.Controls.Add(this.pbListUp, 0, 0);
            this.dbLayoutPanel2.Controls.Add(this.pbListDown, 0, 7);
            this.dbLayoutPanel2.Location = new System.Drawing.Point(487, 184);
            this.dbLayoutPanel2.Name = "dbLayoutPanel2";
            this.dbLayoutPanel2.RowCount = 8;
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dbLayoutPanel2.Size = new System.Drawing.Size(393, 464);
            this.dbLayoutPanel2.TabIndex = 95;
            // 
            // lbl6
            // 
            this.lbl6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl6.AutoSize = true;
            this.lbl6.BackColor = System.Drawing.Color.Transparent;
            this.lbl6.Font = new System.Drawing.Font("Trebuchet MS", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl6.ForeColor = System.Drawing.Color.Gold;
            this.lbl6.Image = ((System.Drawing.Image)(resources.GetObject("lbl6.Image")));
            this.lbl6.LblObject = null;
            this.lbl6.Location = new System.Drawing.Point(174, 362);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(45, 29);
            this.lbl6.TabIndex = 100;
            this.lbl6.Text = ": : ";
            this.lbl6.Click += new System.EventHandler(this.lbl1_Click);
            this.lbl6.MouseEnter += new System.EventHandler(this.lblMeni1_MouseEnter);
            this.lbl6.MouseLeave += new System.EventHandler(this.lblMeni1_MouseLeave);
            // 
            // lbl5
            // 
            this.lbl5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl5.AutoSize = true;
            this.lbl5.BackColor = System.Drawing.Color.Transparent;
            this.lbl5.Font = new System.Drawing.Font("Trebuchet MS", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl5.ForeColor = System.Drawing.Color.Gold;
            this.lbl5.Image = ((System.Drawing.Image)(resources.GetObject("lbl5.Image")));
            this.lbl5.LblObject = null;
            this.lbl5.Location = new System.Drawing.Point(174, 304);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(45, 29);
            this.lbl5.TabIndex = 100;
            this.lbl5.Text = ": : ";
            this.lbl5.Click += new System.EventHandler(this.lbl1_Click);
            this.lbl5.MouseEnter += new System.EventHandler(this.lblMeni1_MouseEnter);
            this.lbl5.MouseLeave += new System.EventHandler(this.lblMeni1_MouseLeave);
            // 
            // lbl4
            // 
            this.lbl4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl4.AutoSize = true;
            this.lbl4.BackColor = System.Drawing.Color.Transparent;
            this.lbl4.Font = new System.Drawing.Font("Trebuchet MS", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl4.ForeColor = System.Drawing.Color.Gold;
            this.lbl4.Image = ((System.Drawing.Image)(resources.GetObject("lbl4.Image")));
            this.lbl4.LblObject = null;
            this.lbl4.Location = new System.Drawing.Point(174, 246);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(45, 29);
            this.lbl4.TabIndex = 24;
            this.lbl4.Text = ": : ";
            this.lbl4.Click += new System.EventHandler(this.lbl1_Click);
            this.lbl4.MouseEnter += new System.EventHandler(this.lblMeni1_MouseEnter);
            this.lbl4.MouseLeave += new System.EventHandler(this.lblMeni1_MouseLeave);
            // 
            // lbl3
            // 
            this.lbl3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl3.AutoSize = true;
            this.lbl3.BackColor = System.Drawing.Color.Transparent;
            this.lbl3.Font = new System.Drawing.Font("Trebuchet MS", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl3.ForeColor = System.Drawing.Color.Gold;
            this.lbl3.Image = ((System.Drawing.Image)(resources.GetObject("lbl3.Image")));
            this.lbl3.LblObject = null;
            this.lbl3.Location = new System.Drawing.Point(174, 188);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(45, 29);
            this.lbl3.TabIndex = 21;
            this.lbl3.Text = ": : ";
            this.lbl3.Click += new System.EventHandler(this.lbl1_Click);
            this.lbl3.MouseEnter += new System.EventHandler(this.lblMeni1_MouseEnter);
            this.lbl3.MouseLeave += new System.EventHandler(this.lblMeni1_MouseLeave);
            // 
            // lbl2
            // 
            this.lbl2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl2.AutoSize = true;
            this.lbl2.BackColor = System.Drawing.Color.Transparent;
            this.lbl2.Font = new System.Drawing.Font("Trebuchet MS", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl2.ForeColor = System.Drawing.Color.Gold;
            this.lbl2.Image = ((System.Drawing.Image)(resources.GetObject("lbl2.Image")));
            this.lbl2.LblObject = null;
            this.lbl2.Location = new System.Drawing.Point(174, 130);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(45, 29);
            this.lbl2.TabIndex = 20;
            this.lbl2.Text = ": : ";
            this.lbl2.Click += new System.EventHandler(this.lbl1_Click);
            this.lbl2.MouseEnter += new System.EventHandler(this.lblMeni1_MouseEnter);
            this.lbl2.MouseLeave += new System.EventHandler(this.lblMeni1_MouseLeave);
            // 
            // lbl1
            // 
            this.lbl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.Color.Transparent;
            this.lbl1.Font = new System.Drawing.Font("Trebuchet MS", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl1.ForeColor = System.Drawing.Color.Gold;
            this.lbl1.Image = ((System.Drawing.Image)(resources.GetObject("lbl1.Image")));
            this.lbl1.LblObject = null;
            this.lbl1.Location = new System.Drawing.Point(174, 72);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(45, 29);
            this.lbl1.TabIndex = 18;
            this.lbl1.Text = ": : ";
            this.lbl1.Click += new System.EventHandler(this.lbl1_Click);
            this.lbl1.MouseEnter += new System.EventHandler(this.lblMeni1_MouseEnter);
            this.lbl1.MouseLeave += new System.EventHandler(this.lblMeni1_MouseLeave);
            // 
            // pbListUp
            // 
            this.pbListUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbListUp.Image = global::SmetkaZaNaracka.Properties.Resources.DarkArrowUp;
            this.pbListUp.Location = new System.Drawing.Point(146, 9);
            this.pbListUp.Name = "pbListUp";
            this.pbListUp.Size = new System.Drawing.Size(100, 39);
            this.pbListUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbListUp.TabIndex = 0;
            this.pbListUp.TabStop = false;
            this.pbListUp.Click += new System.EventHandler(this.pbListUp_Click);
            this.pbListUp.MouseEnter += new System.EventHandler(this.pictureBoxUp_MouseEnter);
            this.pbListUp.MouseLeave += new System.EventHandler(this.pictureBoxUp_MouseLeave);
            // 
            // pbListDown
            // 
            this.pbListDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbListDown.Image = global::SmetkaZaNaracka.Properties.Resources.DarkArrowDown;
            this.pbListDown.Location = new System.Drawing.Point(146, 414);
            this.pbListDown.Name = "pbListDown";
            this.pbListDown.Size = new System.Drawing.Size(100, 41);
            this.pbListDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbListDown.TabIndex = 41;
            this.pbListDown.TabStop = false;
            this.pbListDown.Click += new System.EventHandler(this.pbListDown_Click);
            this.pbListDown.MouseEnter += new System.EventHandler(this.pictureBoxDown_MouseEnter);
            this.pbListDown.MouseLeave += new System.EventHandler(this.pictureBoxDown_MouseLeave);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::SmetkaZaNaracka.Properties.Resources.FASAP_LOGO;
            this.pictureBox3.Location = new System.Drawing.Point(60, 40);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(282, 276);
            this.pictureBox3.TabIndex = 96;
            this.pictureBox3.TabStop = false;
            // 
            // btnDodadiPoz
            // 
            this.btnDodadiPoz.BackColor = System.Drawing.Color.Transparent;
            this.btnDodadiPoz.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnDodadiPoz.ForeColor = System.Drawing.Color.Khaki;
            this.btnDodadiPoz.Image = ((System.Drawing.Image)(resources.GetObject("btnDodadiPoz.Image")));
            this.btnDodadiPoz.Location = new System.Drawing.Point(1125, 203);
            this.btnDodadiPoz.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnDodadiPoz.Name = "btnDodadiPoz";
            this.btnDodadiPoz.Size = new System.Drawing.Size(207, 73);
            this.btnDodadiPoz.TabIndex = 98;
            this.btnDodadiPoz.Text = "Додавање нова позиција";
            this.btnDodadiPoz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDodadiPoz.Click += new System.EventHandler(this.buttonDodadiVrab_Click);
            this.btnDodadiPoz.MouseEnter += new System.EventHandler(this.buttonOtkazi_MouseEnter);
            this.btnDodadiPoz.MouseLeave += new System.EventHandler(this.buttonOtkazi_MouseLeave);
            // 
            // btnOtstraniPoz
            // 
            this.btnOtstraniPoz.BackColor = System.Drawing.Color.Transparent;
            this.btnOtstraniPoz.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnOtstraniPoz.ForeColor = System.Drawing.Color.Khaki;
            this.btnOtstraniPoz.Image = ((System.Drawing.Image)(resources.GetObject("btnOtstraniPoz.Image")));
            this.btnOtstraniPoz.Location = new System.Drawing.Point(1125, 316);
            this.btnOtstraniPoz.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnOtstraniPoz.Name = "btnOtstraniPoz";
            this.btnOtstraniPoz.Size = new System.Drawing.Size(207, 73);
            this.btnOtstraniPoz.TabIndex = 99;
            this.btnOtstraniPoz.Text = "Отстрани позиција";
            this.btnOtstraniPoz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOtstraniPoz.Click += new System.EventHandler(this.btnOtstraniPoz_Click);
            this.btnOtstraniPoz.MouseEnter += new System.EventHandler(this.buttonOtkazi_MouseEnter);
            this.btnOtstraniPoz.MouseLeave += new System.EventHandler(this.buttonOtkazi_MouseLeave);
            // 
            // DodavanjePozicija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.btnOtstraniPoz);
            this.Controls.Add(this.btnDodadiPoz);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.dbLayoutPanel2);
            this.Controls.Add(this.buttonOtkazi);
            this.Controls.Add(this.btnDodadi);
            this.Controls.Add(this.lblZab);
            this.Controls.Add(this.lblZad1);
            this.Controls.Add(this.lblIme);
            this.Controls.Add(this.tbImePozicija);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DodavanjePozicija";
            this.Opacity = 1D;
            this.Text = "DodavanjePozicija";
            this.Controls.SetChildIndex(this.tbImePozicija, 0);
            this.Controls.SetChildIndex(this.lblIme, 0);
            this.Controls.SetChildIndex(this.lblZad1, 0);
            this.Controls.SetChildIndex(this.lblZab, 0);
            this.Controls.SetChildIndex(this.btnDodadi, 0);
            this.Controls.SetChildIndex(this.buttonOtkazi, 0);
            this.Controls.SetChildIndex(this.dbLayoutPanel2, 0);
            this.Controls.SetChildIndex(this.pictureBox3, 0);
            this.Controls.SetChildIndex(this.btnDodadiPoz, 0);
            this.Controls.SetChildIndex(this.btnOtstraniPoz, 0);
            this.dbLayoutPanel2.ResumeLayout(false);
            this.dbLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbListUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbListDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelFASAP lblZab;
        private LabelFASAP lblZad1;
        private LabelFASAP lblIme;
        private System.Windows.Forms.TextBox tbImePozicija;
        private ButtonFASAP buttonOtkazi;
        private ButtonFASAP btnDodadi;
        private DBLayoutPanel dbLayoutPanel2;
        private System.Windows.Forms.PictureBox pbListDown;
        private LabelFASAP lbl4;
        private LabelFASAP lbl3;
        private LabelFASAP lbl2;
        private LabelFASAP lbl1;
        private System.Windows.Forms.PictureBox pbListUp;
        private System.Windows.Forms.PictureBox pictureBox3;
        private ButtonFASAP btnDodadiPoz;
        private ButtonFASAP btnOtstraniPoz;
        private LabelFASAP lbl6;
        private LabelFASAP lbl5;
    }
}