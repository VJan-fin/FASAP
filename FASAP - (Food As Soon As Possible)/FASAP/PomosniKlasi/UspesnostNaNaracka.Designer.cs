namespace SmetkaZaNaracka
{
    partial class UspesnostNaNaracka
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UspesnostNaNaracka));
            this.dbLayoutPanel1 = new SmetkaZaNaracka.DBLayoutPanel(this.components);
            this.labelFASAP2 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dbLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbLayoutPanel1
            // 
            this.dbLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.dbLayoutPanel1.ColumnCount = 1;
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel1.Controls.Add(this.labelFASAP2, 0, 1);
            this.dbLayoutPanel1.Controls.Add(this.labelFASAP1, 0, 0);
            this.dbLayoutPanel1.Location = new System.Drawing.Point(2, 253);
            this.dbLayoutPanel1.Name = "dbLayoutPanel1";
            this.dbLayoutPanel1.RowCount = 2;
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel1.Size = new System.Drawing.Size(1365, 162);
            this.dbLayoutPanel1.TabIndex = 4;
            // 
            // labelFASAP2
            // 
            this.labelFASAP2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFASAP2.AutoSize = true;
            this.labelFASAP2.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP2.Font = new System.Drawing.Font("Trebuchet MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP2.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP2.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP2.Image")));
            this.labelFASAP2.LblObject = null;
            this.labelFASAP2.Location = new System.Drawing.Point(539, 98);
            this.labelFASAP2.Name = "labelFASAP2";
            this.labelFASAP2.Size = new System.Drawing.Size(286, 46);
            this.labelFASAP2.TabIndex = 1;
            this.labelFASAP2.Text = "Добар апетит  ";
            // 
            // labelFASAP1
            // 
            this.labelFASAP1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFASAP1.AutoSize = true;
            this.labelFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFASAP1.ForeColor = System.Drawing.Color.GreenYellow;
            this.labelFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP1.Image")));
            this.labelFASAP1.LblObject = null;
            this.labelFASAP1.Location = new System.Drawing.Point(343, 17);
            this.labelFASAP1.Name = "labelFASAP1";
            this.labelFASAP1.Size = new System.Drawing.Size(678, 46);
            this.labelFASAP1.TabIndex = 0;
            this.labelFASAP1.Text = "Нарачката е успешно реализирана   ";
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UspesnostNaNaracka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SmetkaZaNaracka.Properties.Resources.FasapBackground12;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.dbLayoutPanel1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UspesnostNaNaracka";
            this.Opacity = 1D;
            this.Text = "UspesnostNaNaracka";
            this.Controls.SetChildIndex(this.dbLayoutPanel1, 0);
            this.dbLayoutPanel1.ResumeLayout(false);
            this.dbLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DBLayoutPanel dbLayoutPanel1;
        private LabelFASAP labelFASAP2;
        private LabelFASAP labelFASAP1;
        private System.Windows.Forms.Timer timer1;
    }
}