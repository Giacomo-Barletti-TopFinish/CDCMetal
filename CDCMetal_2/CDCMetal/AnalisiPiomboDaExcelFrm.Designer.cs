namespace CDCMetal
{
    partial class AnalisiPiomboDaExcelFrm
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
            this.dgvExcelCaricato = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblNumeroRigheExcel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreaPdf = new System.Windows.Forms.Button();
            this.btnApriFile = new System.Windows.Forms.Button();
            this.btnCercaFile = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelCaricato)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvExcelCaricato
            // 
            this.dgvExcelCaricato.AllowUserToAddRows = false;
            this.dgvExcelCaricato.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExcelCaricato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcelCaricato.Location = new System.Drawing.Point(12, 132);
            this.dgvExcelCaricato.Name = "dgvExcelCaricato";
            this.dgvExcelCaricato.ReadOnly = true;
            this.dgvExcelCaricato.Size = new System.Drawing.Size(1360, 541);
            this.dgvExcelCaricato.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.lblNumeroRigheExcel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnCreaPdf);
            this.groupBox1.Controls.Add(this.btnApriFile);
            this.groupBox1.Controls.Add(this.btnCercaFile);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1360, 114);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selezionare il file";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(18, 53);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 16);
            this.lblMessage.TabIndex = 5;
            // 
            // lblNumeroRigheExcel
            // 
            this.lblNumeroRigheExcel.AutoSize = true;
            this.lblNumeroRigheExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroRigheExcel.ForeColor = System.Drawing.Color.Red;
            this.lblNumeroRigheExcel.Location = new System.Drawing.Point(473, 85);
            this.lblNumeroRigheExcel.Name = "lblNumeroRigheExcel";
            this.lblNumeroRigheExcel.Size = new System.Drawing.Size(0, 16);
            this.lblNumeroRigheExcel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(361, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Numero righe";
            // 
            // btnCreaPdf
            // 
            this.btnCreaPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreaPdf.Location = new System.Drawing.Point(631, 72);
            this.btnCreaPdf.Name = "btnCreaPdf";
            this.btnCreaPdf.Size = new System.Drawing.Size(110, 33);
            this.btnCreaPdf.TabIndex = 5;
            this.btnCreaPdf.Text = "Crea PDF";
            this.btnCreaPdf.UseVisualStyleBackColor = true;
            this.btnCreaPdf.Click += new System.EventHandler(this.btnCreaPdf_Click);
            // 
            // btnApriFile
            // 
            this.btnApriFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApriFile.Location = new System.Drawing.Point(1034, 24);
            this.btnApriFile.Name = "btnApriFile";
            this.btnApriFile.Size = new System.Drawing.Size(110, 33);
            this.btnApriFile.TabIndex = 4;
            this.btnApriFile.Text = "Leggi file";
            this.btnApriFile.UseVisualStyleBackColor = true;
            this.btnApriFile.Click += new System.EventHandler(this.btnApriFile_Click);
            // 
            // btnCercaFile
            // 
            this.btnCercaFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCercaFile.Location = new System.Drawing.Point(889, 24);
            this.btnCercaFile.Name = "btnCercaFile";
            this.btnCercaFile.Size = new System.Drawing.Size(110, 33);
            this.btnCercaFile.TabIndex = 2;
            this.btnCercaFile.Text = "Cerca ...";
            this.btnCercaFile.UseVisualStyleBackColor = true;
            this.btnCercaFile.Click += new System.EventHandler(this.btnCercaFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(6, 28);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(877, 24);
            this.txtFilePath.TabIndex = 1;
            // 
            // AnalisiPiomboDaExcelFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 685);
            this.Controls.Add(this.dgvExcelCaricato);
            this.Controls.Add(this.groupBox1);
            this.Name = "AnalisiPiomboDaExcelFrm";
            this.Text = "AnalisiPiomboDaExcelFrm";
            ((System.ComponentModel.ISupportInitialize)(this._DS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelCaricato)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExcelCaricato;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblNumeroRigheExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreaPdf;
        private System.Windows.Forms.Button btnApriFile;
        private System.Windows.Forms.Button btnCercaFile;
        private System.Windows.Forms.TextBox txtFilePath;
    }
}