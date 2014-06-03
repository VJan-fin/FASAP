namespace SmetkaZaNaracka
{
    partial class MessageBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblContents = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.buttonFASAPOtkazi = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.buttonFASAPPotvrdi = new SmetkaZaNaracka.ButtonFASAP(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SmetkaZaNaracka.Properties.Resources.FASAP_LOGO;
            this.pictureBox1.Location = new System.Drawing.Point(441, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblContents
            // 
            this.lblContents.BackColor = System.Drawing.Color.Transparent;
            this.lblContents.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.lblContents.ForeColor = System.Drawing.Color.White;
            this.lblContents.Image = ((System.Drawing.Image)(resources.GetObject("lblContents.Image")));
            this.lblContents.LblObject = null;
            this.lblContents.Location = new System.Drawing.Point(70, 54);
            this.lblContents.Name = "lblContents";
            this.lblContents.Size = new System.Drawing.Size(335, 201);
            this.lblContents.TabIndex = 3;
            this.lblContents.Text = "labelFASAP";
            // 
            // buttonFASAPOtkazi
            // 
            this.buttonFASAPOtkazi.BackColor = System.Drawing.Color.Transparent;
            this.buttonFASAPOtkazi.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.buttonFASAPOtkazi.ForeColor = System.Drawing.Color.Khaki;
            this.buttonFASAPOtkazi.Image = ((System.Drawing.Image)(resources.GetObject("buttonFASAPOtkazi.Image")));
            this.buttonFASAPOtkazi.Location = new System.Drawing.Point(312, 316);
            this.buttonFASAPOtkazi.Name = "buttonFASAPOtkazi";
            this.buttonFASAPOtkazi.Size = new System.Drawing.Size(177, 44);
            this.buttonFASAPOtkazi.TabIndex = 2;
            this.buttonFASAPOtkazi.Text = "Откажи";
            this.buttonFASAPOtkazi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonFASAPOtkazi.Click += new System.EventHandler(this.buttonFASAP2_Click);
            this.buttonFASAPOtkazi.MouseEnter += new System.EventHandler(this.buttonFASAP2_MouseEnter);
            this.buttonFASAPOtkazi.MouseLeave += new System.EventHandler(this.buttonFASAP2_MouseLeave);
            // 
            // buttonFASAPPotvrdi
            // 
            this.buttonFASAPPotvrdi.BackColor = System.Drawing.Color.Transparent;
            this.buttonFASAPPotvrdi.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.buttonFASAPPotvrdi.ForeColor = System.Drawing.Color.Khaki;
            this.buttonFASAPPotvrdi.Image = ((System.Drawing.Image)(resources.GetObject("buttonFASAPPotvrdi.Image")));
            this.buttonFASAPPotvrdi.Location = new System.Drawing.Point(97, 316);
            this.buttonFASAPPotvrdi.Name = "buttonFASAPPotvrdi";
            this.buttonFASAPPotvrdi.Size = new System.Drawing.Size(177, 44);
            this.buttonFASAPPotvrdi.TabIndex = 1;
            this.buttonFASAPPotvrdi.Text = "Потврди";
            this.buttonFASAPPotvrdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonFASAPPotvrdi.Click += new System.EventHandler(this.buttonFASAPPotvrdi_Click);
            this.buttonFASAPPotvrdi.MouseEnter += new System.EventHandler(this.buttonFASAPPotvrdi_MouseEnter);
            this.buttonFASAPPotvrdi.MouseLeave += new System.EventHandler(this.buttonFASAPPotvrdi_MouseLeave);
            // 
            // MessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SmetkaZaNaracka.Properties.Resources.Boards_Wooden_Surface_Background_Texture;
            this.ClientSize = new System.Drawing.Size(586, 393);
            this.Controls.Add(this.lblContents);
            this.Controls.Add(this.buttonFASAPOtkazi);
            this.Controls.Add(this.buttonFASAPPotvrdi);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBoxForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private ButtonFASAP buttonFASAPPotvrdi;
        private LabelFASAP lblContents;
        private ButtonFASAP buttonFASAPOtkazi;
    }
}