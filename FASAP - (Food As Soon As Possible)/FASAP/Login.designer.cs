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
            this.labelFASAP3 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(636, 348);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(265, 20);
            this.tbUserName.TabIndex = 0;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(636, 458);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(265, 20);
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
            // labelFASAP3
            // 
            this.labelFASAP3.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP3.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP3.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP3.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP3.Image")));
            this.labelFASAP3.LblObject = null;
            this.labelFASAP3.Location = new System.Drawing.Point(715, 560);
            this.labelFASAP3.Name = "labelFASAP3";
            this.labelFASAP3.Size = new System.Drawing.Size(186, 61);
            this.labelFASAP3.TabIndex = 7;
            this.labelFASAP3.Text = "Логирај се";
            this.labelFASAP3.Click += new System.EventHandler(this.labelFASAP3_Click);
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
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.labelFASAP3);
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
            this.Controls.SetChildIndex(this.labelFASAP3, 0);
            this.Controls.SetChildIndex(this.pictureBox3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private LabelFASAP labelFASAP1;
        private LabelFASAP labelFASAP2;
        private LabelFASAP labelFASAP3;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}