namespace SmetkaZaNaracka
{
    partial class InfoForma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForma));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbRegister = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lbLogin = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP2 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP3 = new SmetkaZaNaracka.LabelFASAP(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::SmetkaZaNaracka.Properties.Resources.FASAP_LOGO;
            this.pictureBox3.Location = new System.Drawing.Point(67, 22);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(369, 276);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // lbRegister
            // 
            this.lbRegister.BackColor = System.Drawing.Color.Transparent;
            this.lbRegister.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRegister.ForeColor = System.Drawing.Color.Khaki;
            this.lbRegister.Image = ((System.Drawing.Image)(resources.GetObject("lbRegister.Image")));
            this.lbRegister.LblObject = null;
            this.lbRegister.Location = new System.Drawing.Point(914, 179);
            this.lbRegister.Name = "lbRegister";
            this.lbRegister.Size = new System.Drawing.Size(346, 61);
            this.lbRegister.TabIndex = 9;
            this.lbRegister.Text = " Регистрирај нов ресторан  ";
            this.lbRegister.Click += new System.EventHandler(this.lbRegister_Click);
            // 
            // lbLogin
            // 
            this.lbLogin.BackColor = System.Drawing.Color.Transparent;
            this.lbLogin.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.ForeColor = System.Drawing.Color.Khaki;
            this.lbLogin.Image = ((System.Drawing.Image)(resources.GetObject("lbLogin.Image")));
            this.lbLogin.LblObject = null;
            this.lbLogin.Location = new System.Drawing.Point(914, 91);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(238, 51);
            this.lbLogin.TabIndex = 8;
            this.lbLogin.Text = " Логирај се     ";
            this.lbLogin.Click += new System.EventHandler(this.lbLogin_Click);
            // 
            // labelFASAP1
            // 
            this.labelFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP1.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP1.Image")));
            this.labelFASAP1.LblObject = null;
            this.labelFASAP1.Location = new System.Drawing.Point(35, 340);
            this.labelFASAP1.Name = "labelFASAP1";
            this.labelFASAP1.Size = new System.Drawing.Size(766, 123);
            this.labelFASAP1.TabIndex = 10;
            this.labelFASAP1.Text = resources.GetString("labelFASAP1.Text");
            // 
            // labelFASAP2
            // 
            this.labelFASAP2.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP2.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP2.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP2.Image")));
            this.labelFASAP2.LblObject = null;
            this.labelFASAP2.Location = new System.Drawing.Point(35, 463);
            this.labelFASAP2.Name = "labelFASAP2";
            this.labelFASAP2.Size = new System.Drawing.Size(766, 149);
            this.labelFASAP2.TabIndex = 11;
            this.labelFASAP2.Text = resources.GetString("labelFASAP2.Text");
            // 
            // labelFASAP3
            // 
            this.labelFASAP3.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP3.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP3.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP3.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP3.Image")));
            this.labelFASAP3.LblObject = null;
            this.labelFASAP3.Location = new System.Drawing.Point(39, 612);
            this.labelFASAP3.Name = "labelFASAP3";
            this.labelFASAP3.Size = new System.Drawing.Size(762, 110);
            this.labelFASAP3.TabIndex = 12;
            this.labelFASAP3.Text = resources.GetString("labelFASAP3.Text");
            // 
            // InfoForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.labelFASAP3);
            this.Controls.Add(this.labelFASAP2);
            this.Controls.Add(this.labelFASAP1);
            this.Controls.Add(this.lbRegister);
            this.Controls.Add(this.lbLogin);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "InfoForma";
            this.Opacity = 1D;
            this.Text = "InfoForma";
            this.Load += new System.EventHandler(this.InfoForma_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pictureBox3, 0);
            this.Controls.SetChildIndex(this.lbLogin, 0);
            this.Controls.SetChildIndex(this.lbRegister, 0);
            this.Controls.SetChildIndex(this.labelFASAP1, 0);
            this.Controls.SetChildIndex(this.labelFASAP2, 0);
            this.Controls.SetChildIndex(this.labelFASAP3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private LabelFASAP lbLogin;
        private LabelFASAP lbRegister;
        private LabelFASAP labelFASAP1;
        private LabelFASAP labelFASAP2;
        private LabelFASAP labelFASAP3;
    }
}