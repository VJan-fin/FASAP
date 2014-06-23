namespace SmetkaZaNaracka
{
    partial class Vraboteni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vraboteni));
            this.labelFASAP1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.btnIzbrisi = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.btnDodadi = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.btnPregled = new SmetkaZaNaracka.ButtonFASAP(this.components);
            this.SuspendLayout();
            // 
            // labelFASAP1
            // 
            this.labelFASAP1.AutoSize = true;
            this.labelFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold);
            this.labelFASAP1.ForeColor = System.Drawing.Color.White;
            this.labelFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP1.Image")));
            this.labelFASAP1.LblObject = null;
            this.labelFASAP1.Location = new System.Drawing.Point(70, 272);
            this.labelFASAP1.Name = "labelFASAP1";
            this.labelFASAP1.Size = new System.Drawing.Size(377, 27);
            this.labelFASAP1.TabIndex = 4;
            this.labelFASAP1.Text = "Треба да има листа на вработени";
            // 
            // btnIzbrisi
            // 
            this.btnIzbrisi.BackColor = System.Drawing.Color.Transparent;
            this.btnIzbrisi.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnIzbrisi.ForeColor = System.Drawing.Color.Khaki;
            this.btnIzbrisi.Image = ((System.Drawing.Image)(resources.GetObject("btnIzbrisi.Image")));
            this.btnIzbrisi.Location = new System.Drawing.Point(533, 454);
            this.btnIzbrisi.Name = "btnIzbrisi";
            this.btnIzbrisi.Size = new System.Drawing.Size(277, 54);
            this.btnIzbrisi.TabIndex = 5;
            this.btnIzbrisi.Text = "Избриши вработен";
            this.btnIzbrisi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDodadi
            // 
            this.btnDodadi.BackColor = System.Drawing.Color.Transparent;
            this.btnDodadi.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnDodadi.ForeColor = System.Drawing.Color.Khaki;
            this.btnDodadi.Image = ((System.Drawing.Image)(resources.GetObject("btnDodadi.Image")));
            this.btnDodadi.Location = new System.Drawing.Point(535, 371);
            this.btnDodadi.Name = "btnDodadi";
            this.btnDodadi.Size = new System.Drawing.Size(275, 62);
            this.btnDodadi.TabIndex = 6;
            this.btnDodadi.Text = "Додади нов вработен";
            this.btnDodadi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDodadi.Click += new System.EventHandler(this.btnDodadi_Click);
            this.btnDodadi.MouseEnter += new System.EventHandler(this.btnDodadi_MouseEnter);
            this.btnDodadi.MouseLeave += new System.EventHandler(this.btnDodadi_MouseLeave);
            // 
            // btnPregled
            // 
            this.btnPregled.BackColor = System.Drawing.Color.Transparent;
            this.btnPregled.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.btnPregled.ForeColor = System.Drawing.Color.Khaki;
            this.btnPregled.Image = ((System.Drawing.Image)(resources.GetObject("btnPregled.Image")));
            this.btnPregled.Location = new System.Drawing.Point(535, 528);
            this.btnPregled.Name = "btnPregled";
            this.btnPregled.Size = new System.Drawing.Size(282, 59);
            this.btnPregled.TabIndex = 7;
            this.btnPregled.Text = "Преглед на вработен";
            this.btnPregled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Vraboteni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 690);
            this.Controls.Add(this.btnPregled);
            this.Controls.Add(this.btnDodadi);
            this.Controls.Add(this.btnIzbrisi);
            this.Controls.Add(this.labelFASAP1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Vraboteni";
            this.Opacity = 1D;
            this.Text = "Vraboteni";
            this.Controls.SetChildIndex(this.labelFASAP1, 0);
            this.Controls.SetChildIndex(this.btnIzbrisi, 0);
            this.Controls.SetChildIndex(this.btnDodadi, 0);
            this.Controls.SetChildIndex(this.btnPregled, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelFASAP labelFASAP1;
        private ButtonFASAP btnIzbrisi;
        private ButtonFASAP btnDodadi;
        private ButtonFASAP btnPregled;
    }
}