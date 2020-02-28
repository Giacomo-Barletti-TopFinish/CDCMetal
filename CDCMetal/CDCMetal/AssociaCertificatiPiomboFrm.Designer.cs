namespace CDCMetal
{
    partial class AssociaCertificatiPiomboFrm
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
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDettaglio = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMessaggio = new System.Windows.Forms.Label();
            this.btnLeggiDati = new System.Windows.Forms.Button();
            this.ddlDataCollaudo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nPesoArticolo = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nPesoRiga = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAssocia = new System.Windows.Forms.Button();
            this.nPesoAssociazione = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.lstCertificatiDaAssociare = new System.Windows.Forms.ListBox();
            this.lstCertificatiAssociati = new System.Windows.Forms.ListBox();
            this.btnRimuovi = new System.Windows.Forms.Button();
            this.btnCopiaCertificati = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nPesoArticolo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPesoRiga)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPesoAssociazione)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Schede Collaudo";
            // 
            // dgvDettaglio
            // 
            this.dgvDettaglio.AllowUserToAddRows = false;
            this.dgvDettaglio.AllowUserToDeleteRows = false;
            this.dgvDettaglio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDettaglio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDettaglio.Location = new System.Drawing.Point(2, 90);
            this.dgvDettaglio.MultiSelect = false;
            this.dgvDettaglio.Name = "dgvDettaglio";
            this.dgvDettaglio.ReadOnly = true;
            this.dgvDettaglio.Size = new System.Drawing.Size(1372, 183);
            this.dgvDettaglio.TabIndex = 15;
            this.dgvDettaglio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDettaglio_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMessaggio);
            this.groupBox1.Controls.Add(this.btnLeggiDati);
            this.groupBox1.Controls.Add(this.ddlDataCollaudo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1360, 52);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // lblMessaggio
            // 
            this.lblMessaggio.AutoSize = true;
            this.lblMessaggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessaggio.ForeColor = System.Drawing.Color.Red;
            this.lblMessaggio.Location = new System.Drawing.Point(467, 16);
            this.lblMessaggio.Name = "lblMessaggio";
            this.lblMessaggio.Size = new System.Drawing.Size(0, 18);
            this.lblMessaggio.TabIndex = 3;
            // 
            // btnLeggiDati
            // 
            this.btnLeggiDati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeggiDati.Location = new System.Drawing.Point(359, 9);
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
            this.ddlDataCollaudo.Size = new System.Drawing.Size(206, 26);
            this.ddlDataCollaudo.TabIndex = 1;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "Peso articolo (g)";
            // 
            // nPesoArticolo
            // 
            this.nPesoArticolo.DecimalPlaces = 1;
            this.nPesoArticolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nPesoArticolo.Location = new System.Drawing.Point(147, 297);
            this.nPesoArticolo.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nPesoArticolo.Name = "nPesoArticolo";
            this.nPesoArticolo.Size = new System.Drawing.Size(85, 24);
            this.nPesoArticolo.TabIndex = 19;
            this.nPesoArticolo.ValueChanged += new System.EventHandler(this.nPesoArticolo_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(273, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 18);
            this.label4.TabIndex = 18;
            this.label4.Text = "Peso riga prenotazione (Kg)";
            // 
            // nPesoRiga
            // 
            this.nPesoRiga.DecimalPlaces = 3;
            this.nPesoRiga.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nPesoRiga.Location = new System.Drawing.Point(471, 297);
            this.nPesoRiga.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nPesoRiga.Name = "nPesoRiga";
            this.nPesoRiga.ReadOnly = true;
            this.nPesoRiga.Size = new System.Drawing.Size(85, 24);
            this.nPesoRiga.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(262, 341);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = "Certificati piombo";
            // 
            // btnAssocia
            // 
            this.btnAssocia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAssocia.Location = new System.Drawing.Point(599, 373);
            this.btnAssocia.Name = "btnAssocia";
            this.btnAssocia.Size = new System.Drawing.Size(110, 33);
            this.btnAssocia.TabIndex = 2;
            this.btnAssocia.Text = "Associa -->";
            this.btnAssocia.UseVisualStyleBackColor = true;
            this.btnAssocia.Click += new System.EventHandler(this.btnAssocia_Click);
            // 
            // nPesoAssociazione
            // 
            this.nPesoAssociazione.DecimalPlaces = 3;
            this.nPesoAssociazione.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nPesoAssociazione.Location = new System.Drawing.Point(617, 298);
            this.nPesoAssociazione.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nPesoAssociazione.Name = "nPesoAssociazione";
            this.nPesoAssociazione.ReadOnly = true;
            this.nPesoAssociazione.Size = new System.Drawing.Size(85, 24);
            this.nPesoAssociazione.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(729, 303);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 18);
            this.label6.TabIndex = 18;
            this.label6.Text = "Peso associazione (Kg)";
            // 
            // lstCertificatiDaAssociare
            // 
            this.lstCertificatiDaAssociare.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCertificatiDaAssociare.FormattingEnabled = true;
            this.lstCertificatiDaAssociare.ItemHeight = 18;
            this.lstCertificatiDaAssociare.Location = new System.Drawing.Point(265, 373);
            this.lstCertificatiDaAssociare.Name = "lstCertificatiDaAssociare";
            this.lstCertificatiDaAssociare.Size = new System.Drawing.Size(313, 112);
            this.lstCertificatiDaAssociare.TabIndex = 22;
            // 
            // lstCertificatiAssociati
            // 
            this.lstCertificatiAssociati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCertificatiAssociati.FormattingEnabled = true;
            this.lstCertificatiAssociati.ItemHeight = 18;
            this.lstCertificatiAssociati.Location = new System.Drawing.Point(732, 370);
            this.lstCertificatiAssociati.Name = "lstCertificatiAssociati";
            this.lstCertificatiAssociati.Size = new System.Drawing.Size(313, 112);
            this.lstCertificatiAssociati.TabIndex = 22;
            this.lstCertificatiAssociati.SelectedIndexChanged += new System.EventHandler(this.lstCertificatiAssociati_SelectedIndexChanged);
            // 
            // btnRimuovi
            // 
            this.btnRimuovi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRimuovi.Location = new System.Drawing.Point(599, 430);
            this.btnRimuovi.Name = "btnRimuovi";
            this.btnRimuovi.Size = new System.Drawing.Size(110, 33);
            this.btnRimuovi.TabIndex = 2;
            this.btnRimuovi.Text = "Rimuovi <--";
            this.btnRimuovi.UseVisualStyleBackColor = true;
            this.btnRimuovi.Click += new System.EventHandler(this.btnRimuovi_Click);
            // 
            // btnCopiaCertificati
            // 
            this.btnCopiaCertificati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopiaCertificati.Location = new System.Drawing.Point(572, 543);
            this.btnCopiaCertificati.Name = "btnCopiaCertificati";
            this.btnCopiaCertificati.Size = new System.Drawing.Size(159, 33);
            this.btnCopiaCertificati.TabIndex = 2;
            this.btnCopiaCertificati.Text = "Copia certificati";
            this.btnCopiaCertificati.UseVisualStyleBackColor = true;
            this.btnCopiaCertificati.Click += new System.EventHandler(this.btnCopiaCertificati_Click);
            // 
            // AssociaCertificatiPiomboFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 685);
            this.Controls.Add(this.lstCertificatiAssociati);
            this.Controls.Add(this.lstCertificatiDaAssociare);
            this.Controls.Add(this.btnCopiaCertificati);
            this.Controls.Add(this.btnRimuovi);
            this.Controls.Add(this.btnAssocia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nPesoAssociazione);
            this.Controls.Add(this.nPesoRiga);
            this.Controls.Add(this.nPesoArticolo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvDettaglio);
            this.Controls.Add(this.groupBox1);
            this.Name = "AssociaCertificatiPiomboFrm";
            this.Text = "AssociaCertificatiPiomboFrm";
            this.Load += new System.EventHandler(this.AssociaCertificatiPiomboFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._DS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nPesoArticolo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPesoRiga)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPesoAssociazione)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDettaglio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMessaggio;
        private System.Windows.Forms.Button btnLeggiDati;
        private System.Windows.Forms.ComboBox ddlDataCollaudo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nPesoArticolo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nPesoRiga;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAssocia;
        private System.Windows.Forms.NumericUpDown nPesoAssociazione;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstCertificatiDaAssociare;
        private System.Windows.Forms.ListBox lstCertificatiAssociati;
        private System.Windows.Forms.Button btnRimuovi;
        private System.Windows.Forms.Button btnCopiaCertificati;
    }
}