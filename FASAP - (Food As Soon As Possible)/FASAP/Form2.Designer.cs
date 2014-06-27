using System.Windows.Forms;
namespace SmetkaZaNaracka
{
    partial class Loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.labelFASAP1 = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.lblZab = new SmetkaZaNaracka.LabelFASAP(this.components);
            this.labelFASAP2 = new SmetkaZaNaracka.LabelFASAP(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SmetkaZaNaracka.Properties.Resources.FASAP_LOGO;
            this.pictureBox1.Location = new System.Drawing.Point(466, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(445, 443);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer2
            // 
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelFASAP1
            // 
            this.labelFASAP1.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP1.Font = new System.Drawing.Font("Trebuchet MS", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFASAP1.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP1.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP1.Image")));
            this.labelFASAP1.LblObject = null;
            this.labelFASAP1.Location = new System.Drawing.Point(293, 531);
            this.labelFASAP1.Name = "labelFASAP1";
            this.labelFASAP1.Size = new System.Drawing.Size(791, 118);
            this.labelFASAP1.TabIndex = 11;
            this.labelFASAP1.Text = "Food As Soon As Possible";
            // 
            // lblZab
            // 
            this.lblZab.AutoSize = true;
            this.lblZab.BackColor = System.Drawing.Color.Transparent;
            this.lblZab.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblZab.ForeColor = System.Drawing.Color.Khaki;
            this.lblZab.Image = ((System.Drawing.Image)(resources.GetObject("lblZab.Image")));
            this.lblZab.LblObject = null;
            this.lblZab.Location = new System.Drawing.Point(30, 23);
            this.lblZab.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblZab.Name = "lblZab";
            this.lblZab.Size = new System.Drawing.Size(47, 22);
            this.lblZab.TabIndex = 72;
            this.lblZab.Text = "v1.0 ";
            // 
            // labelFASAP2
            // 
            this.labelFASAP2.AutoSize = true;
            this.labelFASAP2.BackColor = System.Drawing.Color.Transparent;
            this.labelFASAP2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFASAP2.ForeColor = System.Drawing.Color.Khaki;
            this.labelFASAP2.Image = ((System.Drawing.Image)(resources.GetObject("labelFASAP2.Image")));
            this.labelFASAP2.LblObject = null;
            this.labelFASAP2.Location = new System.Drawing.Point(30, 56);
            this.labelFASAP2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFASAP2.Name = "labelFASAP2";
            this.labelFASAP2.Size = new System.Drawing.Size(214, 66);
            this.labelFASAP2.TabIndex = 73;
            this.labelFASAP2.Text = "©2014 Виктор Јаневски, \r\nАлександар Кузманоски, \r\nАнгела Јосифовска  ";
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SmetkaZaNaracka.Properties.Resources.FasapBackground;
            this.ClientSize = new System.Drawing.Size(1376, 700);
            this.Controls.Add(this.labelFASAP2);
            this.Controls.Add(this.lblZab);
            this.Controls.Add(this.labelFASAP1);
            this.Controls.Add(this.pictureBox1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Loading";
            this.Opacity = 1D;
            this.Text = "Form2";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.labelFASAP1, 0);
            this.Controls.SetChildIndex(this.lblZab, 0);
            this.Controls.SetChildIndex(this.labelFASAP2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Timer timer2;
        private LabelFASAP labelFASAP1;
        private LabelFASAP lblZab;
        private LabelFASAP labelFASAP2;
    }
}