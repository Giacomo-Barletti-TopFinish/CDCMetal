namespace CDCMetal
{
    partial class ExceptionFrm
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
            this.txtErrore = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtErrore
            // 
            this.txtErrore.Location = new System.Drawing.Point(12, 229);
            this.txtErrore.Multiline = true;
            this.txtErrore.Name = "txtErrore";
            this.txtErrore.ReadOnly = true;
            this.txtErrore.Size = new System.Drawing.Size(499, 345);
            this.txtErrore.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CDCMetal.Properties.Resources.IT;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(501, 201);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2100;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ExceptionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 586);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtErrore);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionFrm";
            this.Text = "ERRORE";
            this.Load += new System.EventHandler(this.ExceptionFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtErrore;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
    }
}