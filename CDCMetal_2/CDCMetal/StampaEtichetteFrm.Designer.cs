namespace CDCMetal
{
    partial class StampaEtichetteFrm
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
            this.btnAnnulla = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnAnnulla.Location = new System.Drawing.Point(523, 563);
            this.btnAnnulla.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnAnnulla.Name = "btnAnnulla";
            this.btnAnnulla.Size = new System.Drawing.Size(265, 75);
            this.btnAnnulla.TabIndex = 0;
            this.btnAnnulla.Text = "Annulla";
            this.btnAnnulla.UseVisualStyleBackColor = true;
            this.btnAnnulla.Click += new System.EventHandler(this.btnAnnulla_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 508);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1293, 35);
            this.progressBar1.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(20, 18);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(1291, 478);
            this.txtMessage.TabIndex = 2;
            // 
            // StampaEtichetteFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 692);
            this.ControlBox = false;
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnAnnulla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StampaEtichetteFrm";
            this.Text = "Stampa etichette";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnnulla;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtMessage;
    }
}