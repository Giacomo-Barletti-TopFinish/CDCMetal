namespace CDCMetal
{
    partial class ExcelCaricaNuovoDocumentoFrm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnCercaFile = new System.Windows.Forms.Button();
            this.btnApriFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnApriFile);
            this.groupBox1.Controls.Add(this.btnCercaFile);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1360, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selezionare il file";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(6, 19);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(974, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnCercaFile
            // 
            this.btnCercaFile.Location = new System.Drawing.Point(1002, 17);
            this.btnCercaFile.Name = "btnCercaFile";
            this.btnCercaFile.Size = new System.Drawing.Size(75, 23);
            this.btnCercaFile.TabIndex = 1;
            this.btnCercaFile.Text = "Cerca ...";
            this.btnCercaFile.UseVisualStyleBackColor = true;
            this.btnCercaFile.Click += new System.EventHandler(this.btnCercaFile_Click);
            // 
            // btnApriFile
            // 
            this.btnApriFile.Location = new System.Drawing.Point(1108, 17);
            this.btnApriFile.Name = "btnApriFile";
            this.btnApriFile.Size = new System.Drawing.Size(75, 23);
            this.btnApriFile.TabIndex = 1;
            this.btnApriFile.Text = "Leggi file";
            this.btnApriFile.UseVisualStyleBackColor = true;
            // 
            // ExcelCaricaNuovoDocumentoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 685);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExcelCaricaNuovoDocumentoFrm";
            this.Text = "Carica nuovo excel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApriFile;
        private System.Windows.Forms.Button btnCercaFile;
        private System.Windows.Forms.TextBox txtFilePath;
    }
}