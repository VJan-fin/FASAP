namespace SmetkaZaNaracka
{
    partial class FasapPocetenEkran
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FasapPocetenEkran));
            this.lbNajbarani = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbKategorija = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbGrad = new System.Windows.Forms.ListBox();
            this.lbRestorani = new System.Windows.Forms.ListBox();
            this.lblRestorani = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new SmetkaZaNaracka.SearchButton();
            this.logo = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbNajbarani
            // 
            this.lbNajbarani.BackColor = System.Drawing.SystemColors.Menu;
            this.lbNajbarani.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNajbarani.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lbNajbarani.FormattingEnabled = true;
            this.lbNajbarani.ItemHeight = 18;
            this.lbNajbarani.Location = new System.Drawing.Point(12, 215);
            this.lbNajbarani.Name = "lbNajbarani";
            this.lbNajbarani.Size = new System.Drawing.Size(184, 94);
            this.lbNajbarani.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label1.Location = new System.Drawing.Point(12, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "НАЈБАРАНИ РЕСТОРАНИ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(202, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 297);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbKategorija);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Категорија";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbKategorija
            // 
            this.lbKategorija.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lbKategorija.FormattingEnabled = true;
            this.lbKategorija.ItemHeight = 18;
            this.lbKategorija.Location = new System.Drawing.Point(6, 6);
            this.lbKategorija.Name = "lbKategorija";
            this.lbKategorija.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbKategorija.Size = new System.Drawing.Size(180, 256);
            this.lbKategorija.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbGrad);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 266);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Град";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbGrad
            // 
            this.lbGrad.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lbGrad.FormattingEnabled = true;
            this.lbGrad.ItemHeight = 18;
            this.lbGrad.Location = new System.Drawing.Point(6, 6);
            this.lbGrad.Name = "lbGrad";
            this.lbGrad.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbGrad.Size = new System.Drawing.Size(180, 256);
            this.lbGrad.TabIndex = 0;
            // 
            // lbRestorani
            // 
            this.lbRestorani.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lbRestorani.FormattingEnabled = true;
            this.lbRestorani.Location = new System.Drawing.Point(404, 32);
            this.lbRestorani.Name = "lbRestorani";
            this.lbRestorani.Size = new System.Drawing.Size(296, 277);
            this.lbRestorani.TabIndex = 4;
            this.lbRestorani.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbRestorani_MouseDoubleClick);
            // 
            // lblRestorani
            // 
            this.lblRestorani.AutoSize = true;
            this.lblRestorani.BackColor = System.Drawing.Color.Transparent;
            this.lblRestorani.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestorani.Location = new System.Drawing.Point(401, 12);
            this.lblRestorani.Name = "lblRestorani";
            this.lblRestorani.Size = new System.Drawing.Size(115, 18);
            this.lblRestorani.TabIndex = 5;
            this.lblRestorani.Text = " РЕСТОРАНИ";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "ИНФО";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(667, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 27);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.Color.Transparent;
            this.logo.Image = global::SmetkaZaNaracka.Properties.Resources.FASAP_LOGO;
            this.logo.Location = new System.Drawing.Point(12, 12);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(184, 177);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // FasapPocetenEkran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(707, 328);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblRestorani);
            this.Controls.Add(this.lbRestorani);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbNajbarani);
            this.Controls.Add(this.logo);
            this.ForeColor = System.Drawing.Color.SaddleBrown;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FasapPocetenEkran";
            this.Text = "Food As Soon As Possible";
            this.Load += new System.EventHandler(this.FasapPocetenEkran_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.ListBox lbNajbarani;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lbKategorija;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox lbGrad;
        private System.Windows.Forms.ListBox lbRestorani;
        private System.Windows.Forms.Label lblRestorani;
        private SearchButton button1;
        private System.Windows.Forms.Button button2;

    }
}