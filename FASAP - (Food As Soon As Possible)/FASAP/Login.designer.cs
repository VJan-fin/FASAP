namespace SmetkaZaNaracka
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.labelFASAP1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP2 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnLogIn = new SmetkaZaNaracka.ButtonFASAP(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tbUserName
            // 
            this.tbUserName.BackColor = System.Drawing.Color.Sienna;
            this.tbUserName.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUserName.ForeColor = System.Drawing.Color.Khaki;
            this.tbUserName.Location = new System.Drawing.Point(636, 348);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(265, 30);
            this.tbUserName.TabIndex = 0;
            // 
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.Color.Sienna;
            this.tbPassword.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.ForeColor = System.Drawing.Color.Khaki;
            this.tbPassword.Location = new System.Drawing.Point(636, 458);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(265, 30);
            this.tbPassword.TabIndex = 1;
            // 
            // labelFASAP1
            // 
            this.labelFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP1.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP1.Image")));
            this.labelFASAP1.LblObject = null;
            this.labelFASAP1.Location = new System.Drawing.Point(363, 341);
            this.labelFASAP1.Name = "labelFASAP1";
            this.labelFASAP1.Size = new System.Drawing.Size(219, 44);
            this.labelFASAP1.TabIndex = 5;
            this.labelFASAP1.Text = "Корисничко име:";
            // 
            // labelFASAP2
            // 
            this.labelFASAP2.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP2.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP2.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP2.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP2.Image")));
            this.labelFASAP2.LblObject = null;
            this.labelFASAP2.Location = new System.Drawing.Point(447, 451);
            this.labelFASAP2.Name = "labelFASAP2";
            this.labelFASAP2.Size = new System.Drawing.Size(135, 45);
            this.labelFASAP2.TabIndex = 6;
            this.labelFASAP2.Text = "Лозинка:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::SmetkaZaNaracka.Properties.Resources.FASAP_LOGO;
            this.pictureBox3.Location = new System.Drawing.Point(67, 22);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(369, 276);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.Transparent;
            this.btnLogIn.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnLogIn.ForeColor = System.Drawing.Color.Khaki;
            this.btnLogIn.Image = global::SmetkaZaNaracka.Properties.Resources.DarkButton___Copy;
            this.btnLogIn.Location = new System.Drawing.Point(736, 582);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(165, 55);
            this.btnLogIn.TabIndex = 9;
            this.btnLogIn.Text = "Логирај се";
            this.btnLogIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            this.btnLogIn.MouseEnter += new System.EventHandler(this.btnLogIn_MouseEnter);
            this.btnLogIn.MouseLeave += new System.EventHandler(this.btnLogIn_MouseLeave);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.labelFASAP2);
            this.Controls.Add(this.labelFASAP1);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Login";
            this.Opacity = 1D;
            this.Text = "Login";
            this.Controls.SetChildIndex(this.tbUserName, 0);
            this.Controls.SetChildIndex(this.tbPassword, 0);
            this.Controls.SetChildIndex(this.labelFASAP1, 0);
            this.Controls.SetChildIndex(this.labelFASAP2, 0);
            this.Controls.SetChildIndex(this.pictureBox3, 0);
            this.Controls.SetChildIndex(this.btnLogIn, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private LabelFASAP labelFASAP1;
        private LabelFASAP labelFASAP2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private ButtonFASAP btnLogIn;
    }
}