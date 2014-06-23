namespace SmetkaZaNaracka
{
    partial class OnsiteNarackaPodatoci
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnsiteNarackaPodatoci));
            this.buttonFASAP2 = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.buttonFASAP1 = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.labelFASAP1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lblBrojMasa = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.pbDown = new System.Windows.Forms.PictureBox();
            this.pbUp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUp)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFASAP2
            // 
            this.buttonFASAP2.BackColor = System.Drawing.Color.Transparent;
            this.buttonFASAP2.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.buttonFASAP2.ForeColor = System.Drawing.Color.Khaki;
            this.buttonFASAP2.Image = ((System.Drawing.Image)(resources.GetObject("buttonFASAP2.Image")));
            this.buttonFASAP2.Location = new System.Drawing.Point(668, 663);
            this.buttonFASAP2.Name = "buttonFASAP2";
            this.buttonFASAP2.Size = new System.Drawing.Size(191, 52);
            this.buttonFASAP2.TabIndex = 24;
            this.buttonFASAP2.Text = "Потврди";
            this.buttonFASAP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonFASAP2.Click += new System.EventHandler(this.buttonFASAP2_Click);
            this.buttonFASAP2.MouseEnter += new System.EventHandler(this.buttonFASAP2_MouseEnter);
            this.buttonFASAP2.MouseLeave += new System.EventHandler(this.buttonFASAP2_MouseLeave);
            // 
            // buttonFASAP1
            // 
            this.buttonFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.buttonFASAP1.CausesValidation = false;
            this.buttonFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.buttonFASAP1.ForeColor = System.Drawing.Color.Khaki;
            this.buttonFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("buttonFASAP1.Image")));
            this.buttonFASAP1.Location = new System.Drawing.Point(907, 663);
            this.buttonFASAP1.Name = "buttonFASAP1";
            this.buttonFASAP1.Size = new System.Drawing.Size(193, 52);
            this.buttonFASAP1.TabIndex = 23;
            this.buttonFASAP1.Text = "Откажи";
            this.buttonFASAP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonFASAP1.Click += new System.EventHandler(this.buttonFASAP1_Click);
            this.buttonFASAP1.MouseEnter += new System.EventHandler(this.pbUp_MouseEnter);
            this.buttonFASAP1.MouseLeave += new System.EventHandler(this.pbUp_MouseLeave);
            // 
            // labelFASAP1
            // 
            this.labelFASAP1.AutoSize = true;
            this.labelFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP1.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP1.Image")));
            this.labelFASAP1.LblObject = null;
            this.labelFASAP1.Location = new System.Drawing.Point(469, 294);
            this.labelFASAP1.Name = "labelFASAP1";
            this.labelFASAP1.Size = new System.Drawing.Size(226, 40);
            this.labelFASAP1.TabIndex = 25;
            this.labelFASAP1.Text = "Број на маса:";
            // 
            // lblBrojMasa
            // 
            this.lblBrojMasa.BackColor = System.Drawing.Color.Transparent;
            this.lblBrojMasa.Font = new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrojMasa.ForeColor = System.Drawing.Color.White;
            this.lblBrojMasa.Image = ((System.Drawing.Image)(resources.GetObject("lblBrojMasa.Image")));
            this.lblBrojMasa.LblObject = null;
            this.lblBrojMasa.Location = new System.Drawing.Point(466, 345);
            this.lblBrojMasa.Name = "lblBrojMasa";
            this.lblBrojMasa.Size = new System.Drawing.Size(229, 52);
            this.lblBrojMasa.TabIndex = 26;
            this.lblBrojMasa.Text = "1";
            // 
            // pbDown
            // 
            this.pbDown.BackColor = System.Drawing.Color.Transparent;
            this.pbDown.Image = global::SmetkaZaNaracka.Properties.Resources.DarkArrowDown;
            this.pbDown.Location = new System.Drawing.Point(715, 373);
            this.pbDown.Name = "pbDown";
            this.pbDown.Size = new System.Drawing.Size(43, 24);
            this.pbDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDown.TabIndex = 28;
            this.pbDown.TabStop = false;
            this.pbDown.Click += new System.EventHandler(this.pbDown_Click);
            this.pbDown.MouseEnter += new System.EventHandler(this.pbDown_MouseEnter);
            this.pbDown.MouseLeave += new System.EventHandler(this.pbDown_MouseLeave);
            // 
            // pbUp
            // 
            this.pbUp.BackColor = System.Drawing.Color.Transparent;
            this.pbUp.Image = global::SmetkaZaNaracka.Properties.Resources.DarkArrowUp;
            this.pbUp.Location = new System.Drawing.Point(715, 345);
            this.pbUp.Name = "pbUp";
            this.pbUp.Size = new System.Drawing.Size(43, 22);
            this.pbUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUp.TabIndex = 27;
            this.pbUp.TabStop = false;
            this.pbUp.Click += new System.EventHandler(this.pbUp_Click);
            this.pbUp.MouseEnter += new System.EventHandler(this.pbUp_MouseEnter);
            this.pbUp.MouseLeave += new System.EventHandler(this.pbUp_MouseLeave);
            // 
            // OnsiteNarackaPodatoci
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.pbDown);
            this.Controls.Add(this.pbUp);
            this.Controls.Add(this.lblBrojMasa);
            this.Controls.Add(this.labelFASAP1);
            this.Controls.Add(this.buttonFASAP2);
            this.Controls.Add(this.buttonFASAP1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "OnsiteNarackaPodatoci";
            this.Opacity = 1D;
            this.Text = "OnsiteNarackaPodatoci";
            this.Controls.SetChildIndex(this.buttonFASAP1, 0);
            this.Controls.SetChildIndex(this.buttonFASAP2, 0);
            this.Controls.SetChildIndex(this.labelFASAP1, 0);
            this.Controls.SetChildIndex(this.lblBrojMasa, 0);
            this.Controls.SetChildIndex(this.pbUp, 0);
            this.Controls.SetChildIndex(this.pbDown, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ButtonFASAP buttonFASAP2;
        private ButtonFASAP buttonFASAP1;
        private LabelFASAP labelFASAP1;
        private LabelFASAP lblBrojMasa;
        private System.Windows.Forms.PictureBox pbDown;
        private System.Windows.Forms.PictureBox pbUp;
    }
}