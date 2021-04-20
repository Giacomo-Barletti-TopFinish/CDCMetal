namespace CDCMetal
{
    partial class CreaDimensionaleFrm
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
            this.dgvDettaglio = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlBrand = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMessaggio = new System.Windows.Forms.Label();
            this.btnLeggiDati = new System.Windows.Forms.Button();
            this.ddlDataCollaudo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreaPDF = new System.Windows.Forms.Button();
            this.dgvMisure = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkCopiaSchedaTecnica = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisure)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDettaglio
            // 
            this.dgvDettaglio.AllowUserToAddRows = false;
            this.dgvDettaglio.AllowUserToDeleteRows = false;
            this.dgvDettaglio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDettaglio.Location = new System.Drawing.Point(0, 84);
            this.dgvDettaglio.MultiSelect = false;
            this.dgvDettaglio.Name = "dgvDettaglio";
            this.dgvDettaglio.ReadOnly = true;
            this.dgvDettaglio.Size = new System.Drawing.Size(1384, 298);
            this.dgvDettaglio.TabIndex = 5;
            this.dgvDettaglio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDettaglio_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlBrand);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblMessaggio);
            this.groupBox1.Controls.Add(this.btnLeggiDati);
            this.groupBox1.Controls.Add(this.ddlDataCollaudo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1360, 54);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // ddlBrand
            // 
            this.ddlBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlBrand.FormattingEnabled = true;
            this.ddlBrand.Location = new System.Drawing.Point(395, 13);
            this.ddlBrand.Name = "ddlBrand";
            this.ddlBrand.Size = new System.Drawing.Size(157, 26);
            this.ddlBrand.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(327, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Brand";
            // 
            // lblMessaggio
            // 
            this.lblMessaggio.AutoSize = true;
            this.lblMessaggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessaggio.ForeColor = System.Drawing.Color.Red;
            this.lblMessaggio.Location = new System.Drawing.Point(589, 21);
            this.lblMessaggio.Name = "lblMessaggio";
            this.lblMessaggio.Size = new System.Drawing.Size(0, 18);
            this.lblMessaggio.TabIndex = 3;
            // 
            // btnLeggiDati
            // 
            this.btnLeggiDati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeggiDati.Location = new System.Drawing.Point(851, 9);
            this.btnLeggiDati.Name = "btnLeggiDati";
            this.btnLeggiDati.Size = new System.Drawing.Size(110, 33);
            this.btnLeggiDati.TabIndex = 2;
            this.btnLeggiDati.Text = "Leggi dati";
            this.btnLeggiDati.UseVisualStyleBackColor = true;
            this.btnLeggiDati.Click += new System.EventHandler(this.btnLeggiDati_Click);
            // 
            // ddlDataCollaudo
            // 
            this.ddlDataCollaudo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDataCollaudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDataCollaudo.FormattingEnabled = true;
            this.ddlDataCollaudo.Location = new System.Drawing.Point(123, 13);
            this.ddlDataCollaudo.Name = "ddlDataCollaudo";
            this.ddlDataCollaudo.Size = new System.Drawing.Size(157, 26);
            this.ddlDataCollaudo.TabIndex = 1;
            this.ddlDataCollaudo.SelectedIndexChanged += new System.EventHandler(this.ddlDataCollaudo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data collaudo";
            // 
            // btnCreaPDF
            // 
            this.btnCreaPDF.Enabled = false;
            this.btnCreaPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreaPDF.Location = new System.Drawing.Point(206, 405);
            this.btnCreaPDF.Name = "btnCreaPDF";
            this.btnCreaPDF.Size = new System.Drawing.Size(110, 33);
            this.btnCreaPDF.TabIndex = 2;
            this.btnCreaPDF.Text = "Crea PDF";
            this.btnCreaPDF.UseVisualStyleBackColor = true;
            this.btnCreaPDF.Click += new System.EventHandler(this.btnCreaPDF_Click);
            // 
            // dgvMisure
            // 
            this.dgvMisure.AllowUserToAddRows = false;
            this.dgvMisure.AllowUserToDeleteRows = false;
            this.dgvMisure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMisure.Location = new System.Drawing.Point(0, 444);
            this.dgvMisure.Name = "dgvMisure";
            this.dgvMisure.Size = new System.Drawing.Size(1384, 243);
            this.dgvMisure.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Schede Collaudo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 423);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Misure";
            // 
            // chkCopiaSchedaTecnica
            // 
            this.chkCopiaSchedaTecnica.AutoSize = true;
            this.chkCopiaSchedaTecnica.Checked = true;
            this.chkCopiaSchedaTecnica.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopiaSchedaTecnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCopiaSchedaTecnica.Location = new System.Drawing.Point(341, 411);
            this.chkCopiaSchedaTecnica.Name = "chkCopiaSchedaTecnica";
            this.chkCopiaSchedaTecnica.Size = new System.Drawing.Size(169, 22);
            this.chkCopiaSchedaTecnica.TabIndex = 9;
            this.chkCopiaSchedaTecnica.Text = "Copia scheda tecnica";
            this.chkCopiaSchedaTecnica.UseVisualStyleBackColor = true;
            // 
            // CreaDimensionaleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 685);
            this.Controls.Add(this.chkCopiaSchedaTecnica);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCreaPDF);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvMisure);
            this.Controls.Add(this.dgvDettaglio);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreaDimensionaleFrm";
            this.Text = "Crea Certificato Dimensionale";
            this.Load += new System.EventHandler(this.CreaDimensionaleFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._DS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDettaglio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMessaggio;
        private System.Windows.Forms.Button btnCreaPDF;
        private System.Windows.Forms.Button btnLeggiDati;
        private System.Windows.Forms.ComboBox ddlDataCollaudo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMisure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkCopiaSchedaTecnica;
        private System.Windows.Forms.ComboBox ddlBrand;
        private System.Windows.Forms.Label label4;
    }
}