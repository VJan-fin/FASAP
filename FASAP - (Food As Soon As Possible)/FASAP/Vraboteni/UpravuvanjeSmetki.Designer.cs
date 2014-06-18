namespace SmetkaZaNaracka
{
    partial class UpravuvanjeSmetki
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpravuvanjeSmetki));
            this.labelFASAP2 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.lblVrabotenID = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP3 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP4 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.tbRetypePassword = new System.Windows.Forms.TextBox();
            this.lblZad2 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lblZad1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lblZab = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.buttonOtkazi = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.buttonIzmeni = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.SuspendLayout();
            // 
            // labelFASAP2
            // 
            this.labelFASAP2.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP2.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP2.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP2.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP2.Image")));
            this.labelFASAP2.LblObject = null;
            this.labelFASAP2.Location = new System.Drawing.Point(450, 375);
            this.labelFASAP2.Name = "labelFASAP2";
            this.labelFASAP2.Size = new System.Drawing.Size(135, 45);
            this.labelFASAP2.TabIndex = 10;
            this.labelFASAP2.Text = "Лозинка:";
            // 
            // labelFASAP1
            // 
            this.labelFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP1.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP1.Image")));
            this.labelFASAP1.LblObject = null;
            this.labelFASAP1.Location = new System.Drawing.Point(366, 262);
            this.labelFASAP1.Name = "labelFASAP1";
            this.labelFASAP1.Size = new System.Drawing.Size(219, 45);
            this.labelFASAP1.TabIndex = 9;
            this.labelFASAP1.Text = "Корисничко име:";
            // 
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.Color.Sienna;
            this.tbPassword.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPassword.ForeColor = System.Drawing.Color.Khaki;
            this.tbPassword.Location = new System.Drawing.Point(633, 381);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(367, 32);
            this.tbPassword.TabIndex = 8;
            this.tbPassword.TabStop = false;
            this.tbPassword.Click += new System.EventHandler(this.tbUserName_Click);
            this.tbPassword.Validating += new System.ComponentModel.CancelEventHandler(this.tbUserName_Validating);
            // 
            // tbUserName
            // 
            this.tbUserName.BackColor = System.Drawing.Color.Sienna;
            this.tbUserName.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbUserName.ForeColor = System.Drawing.Color.Khaki;
            this.tbUserName.Location = new System.Drawing.Point(633, 268);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(367, 32);
            this.tbUserName.TabIndex = 7;
            this.tbUserName.TabStop = false;
            this.tbUserName.Click += new System.EventHandler(this.tbUserName_Click);
            this.tbUserName.Validating += new System.ComponentModel.CancelEventHandler(this.tbUserName_Validating);
            // 
            // lblVrabotenID
            // 
            this.lblVrabotenID.AutoSize = true;
            this.lblVrabotenID.BackColor = System.Drawing.Color.Transparent;
            this.lblVrabotenID.Font = new System.Drawing.Font("Trebuchet MS", 45.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVrabotenID.ForeColor = System.Drawing.Color.Khaki;
            this.lblVrabotenID.Image = ((System.Drawing.Image)(resources.GetObject("lblVrabotenID.Image")));
            this.lblVrabotenID.LblObject = null;
            this.lblVrabotenID.Location = new System.Drawing.Point(375, 48);
            this.lblVrabotenID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVrabotenID.Name = "lblVrabotenID";
            this.lblVrabotenID.Size = new System.Drawing.Size(125, 76);
            this.lblVrabotenID.TabIndex = 49;
            this.lblVrabotenID.Text = "ID  ";
            // 
            // labelFASAP3
            // 
            this.labelFASAP3.AutoSize = true;
            this.labelFASAP3.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP3.Font = new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFASAP3.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP3.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP3.Image")));
            this.labelFASAP3.LblObject = null;
            this.labelFASAP3.Location = new System.Drawing.Point(44, 75);
            this.labelFASAP3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFASAP3.Name = "labelFASAP3";
            this.labelFASAP3.Size = new System.Drawing.Size(309, 40);
            this.labelFASAP3.TabIndex = 48;
            this.labelFASAP3.Text = "Бр. на вработен:   ";
            // 
            // labelFASAP4
            // 
            this.labelFASAP4.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP4.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP4.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP4.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP4.Image")));
            this.labelFASAP4.LblObject = null;
            this.labelFASAP4.Location = new System.Drawing.Point(295, 495);
            this.labelFASAP4.Name = "labelFASAP4";
            this.labelFASAP4.Size = new System.Drawing.Size(290, 45);
            this.labelFASAP4.TabIndex = 51;
            this.labelFASAP4.Text = "Потврдете ја лозинката:";
            // 
            // tbRetypePassword
            // 
            this.tbRetypePassword.BackColor = System.Drawing.Color.Sienna;
            this.tbRetypePassword.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRetypePassword.ForeColor = System.Drawing.Color.Khaki;
            this.tbRetypePassword.Location = new System.Drawing.Point(633, 501);
            this.tbRetypePassword.Name = "tbRetypePassword";
            this.tbRetypePassword.Size = new System.Drawing.Size(367, 32);
            this.tbRetypePassword.TabIndex = 50;
            this.tbRetypePassword.TabStop = false;
            this.tbRetypePassword.Click += new System.EventHandler(this.tbUserName_Click);
            // 
            // lblZad2
            // 
            this.lblZad2.AutoSize = true;
            this.lblZad2.BackColor = System.Drawing.Color.Transparent;
            this.lblZad2.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblZad2.ForeColor = System.Drawing.Color.Crimson;
            this.lblZad2.Image = ((System.Drawing.Image)(resources.GetObject("lblZad2.Image")));
            this.lblZad2.LblObject = null;
            this.lblZad2.Location = new System.Drawing.Point(1005, 384);
            this.lblZad2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblZad2.Name = "lblZad2";
            this.lblZad2.Size = new System.Drawing.Size(21, 27);
            this.lblZad2.TabIndex = 69;
            this.lblZad2.Text = "*";
            // 
            // lblZad1
            // 
            this.lblZad1.AutoSize = true;
            this.lblZad1.BackColor = System.Drawing.Color.Transparent;
            this.lblZad1.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblZad1.ForeColor = System.Drawing.Color.Crimson;
            this.lblZad1.Image = ((System.Drawing.Image)(resources.GetObject("lblZad1.Image")));
            this.lblZad1.LblObject = null;
            this.lblZad1.Location = new System.Drawing.Point(1005, 271);
            this.lblZad1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblZad1.Name = "lblZad1";
            this.lblZad1.Size = new System.Drawing.Size(21, 27);
            this.lblZad1.TabIndex = 68;
            this.lblZad1.Text = "*";
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
            this.lblZab.TabIndex = 72;
            this.lblZab.Text = "Забелешка: Полињата означени со * се задолжителни    ";
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
            this.buttonOtkazi.TabIndex = 92;
            this.buttonOtkazi.Text = "Откажи";
            this.buttonOtkazi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonOtkazi.Click += new System.EventHandler(this.buttonOtkazi_Click);
            this.buttonOtkazi.MouseEnter += new System.EventHandler(this.buttonOtkazi_MouseEnter);
            this.buttonOtkazi.MouseLeave += new System.EventHandler(this.buttonOtkazi_MouseLeave);
            // 
            // buttonIzmeni
            // 
            this.buttonIzmeni.BackColor = System.Drawing.Color.Transparent;
            this.buttonIzmeni.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.buttonIzmeni.ForeColor = System.Drawing.Color.Khaki;
            this.buttonIzmeni.Image = ((System.Drawing.Image)(resources.GetObject("buttonIzmeni.Image")));
            this.buttonIzmeni.Location = new System.Drawing.Point(950, 683);
            this.buttonIzmeni.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.buttonIzmeni.Name = "buttonIzmeni";
            this.buttonIzmeni.Size = new System.Drawing.Size(176, 54);
            this.buttonIzmeni.TabIndex = 91;
            this.buttonIzmeni.Text = "Измени";
            this.buttonIzmeni.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonIzmeni.Click += new System.EventHandler(this.buttonIzmeni_Click);
            this.buttonIzmeni.MouseEnter += new System.EventHandler(this.buttonIzmeni_MouseEnter);
            this.buttonIzmeni.MouseLeave += new System.EventHandler(this.buttonIzmeni_MouseLeave);
            // 
            // UpravuvanjeSmetki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.buttonOtkazi);
            this.Controls.Add(this.buttonIzmeni);
            this.Controls.Add(this.lblZab);
            this.Controls.Add(this.lblZad2);
            this.Controls.Add(this.lblZad1);
            this.Controls.Add(this.labelFASAP4);
            this.Controls.Add(this.tbRetypePassword);
            this.Controls.Add(this.lblVrabotenID);
            this.Controls.Add(this.labelFASAP3);
            this.Controls.Add(this.labelFASAP2);
            this.Controls.Add(this.labelFASAP1);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UpravuvanjeSmetki";
            this.Opacity = 1D;
            this.Text = "UpravuvanjeSmetki";
            this.Controls.SetChildIndex(this.tbUserName, 0);
            this.Controls.SetChildIndex(this.tbPassword, 0);
            this.Controls.SetChildIndex(this.labelFASAP1, 0);
            this.Controls.SetChildIndex(this.labelFASAP2, 0);
            this.Controls.SetChildIndex(this.labelFASAP3, 0);
            this.Controls.SetChildIndex(this.lblVrabotenID, 0);
            this.Controls.SetChildIndex(this.tbRetypePassword, 0);
            this.Controls.SetChildIndex(this.labelFASAP4, 0);
            this.Controls.SetChildIndex(this.lblZad1, 0);
            this.Controls.SetChildIndex(this.lblZad2, 0);
            this.Controls.SetChildIndex(this.lblZab, 0);
            this.Controls.SetChildIndex(this.buttonIzmeni, 0);
            this.Controls.SetChildIndex(this.buttonOtkazi, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelFASAP labelFASAP2;
        private LabelFASAP labelFASAP1;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUserName;
        private LabelFASAP lblVrabotenID;
        private LabelFASAP labelFASAP3;
        private LabelFASAP labelFASAP4;
        private System.Windows.Forms.TextBox tbRetypePassword;
        private LabelFASAP lblZad2;
        private LabelFASAP lblZad1;
        private LabelFASAP lblZab;
        private ButtonFASAP buttonOtkazi;
        private ButtonFASAP buttonIzmeni;
    }
}