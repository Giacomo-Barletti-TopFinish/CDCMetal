﻿namespace CDCMetal
{
    partial class CreaSpessoreFrm
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
            this.chkCopiaReferto = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreaPDF = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvMisure = new System.Windows.Forms.DataGridView();
            this.dgvDettaglio = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMessaggio = new System.Windows.Forms.Label();
            this.btnLeggiDati = new System.Windows.Forms.Button();
            this.ddlDataCollaudo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApplicazione = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nMisurePerCampione = new System.Windows.Forms.NumericUpDown();
            this.btnCreaCampioni = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumeroCampioni = new System.Windows.Forms.TextBox();
            this.dgvAggregati = new System.Windows.Forms.DataGridView();
            this.chkApriPDF = new System.Windows.Forms.CheckBox();
            this.dgvSpessore = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSpessoreRichiesto = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMisurePerCampione)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAggregati)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpessore)).BeginInit();
            this.SuspendLayout();
            // 
            // chkCopiaReferto
            // 
            this.chkCopiaReferto.AutoSize = true;
            this.chkCopiaReferto.Checked = true;
            this.chkCopiaReferto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopiaReferto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCopiaReferto.Location = new System.Drawing.Point(894, 646);
            this.chkCopiaReferto.Name = "chkCopiaReferto";
            this.chkCopiaReferto.Size = new System.Drawing.Size(260, 22);
            this.chkCopiaReferto.TabIndex = 16;
            this.chkCopiaReferto.TabStop = false;
            this.chkCopiaReferto.Text = "Copia PDF nella cartella Referti Lab";
            this.chkCopiaReferto.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 419);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "Misure  [μm]";
            // 
            // btnCreaPDF
            // 
            this.btnCreaPDF.Enabled = false;
            this.btnCreaPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreaPDF.Location = new System.Drawing.Point(778, 640);
            this.btnCreaPDF.Name = "btnCreaPDF";
            this.btnCreaPDF.Size = new System.Drawing.Size(110, 33);
            this.btnCreaPDF.TabIndex = 8;
            this.btnCreaPDF.Text = "Crea PDF";
            this.btnCreaPDF.UseVisualStyleBackColor = true;
            this.btnCreaPDF.Click += new System.EventHandler(this.btnCreaPDF_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Schede Collaudo";
            // 
            // dgvMisure
            // 
            this.dgvMisure.AllowUserToAddRows = false;
            this.dgvMisure.AllowUserToDeleteRows = false;
            this.dgvMisure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMisure.Location = new System.Drawing.Point(0, 441);
            this.dgvMisure.Name = "dgvMisure";
            this.dgvMisure.Size = new System.Drawing.Size(718, 243);
            this.dgvMisure.TabIndex = 13;
            this.dgvMisure.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMisure_CellValueChanged);
            // 
            // dgvDettaglio
            // 
            this.dgvDettaglio.AllowUserToAddRows = false;
            this.dgvDettaglio.AllowUserToDeleteRows = false;
            this.dgvDettaglio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDettaglio.Location = new System.Drawing.Point(0, 80);
            this.dgvDettaglio.MultiSelect = false;
            this.dgvDettaglio.Name = "dgvDettaglio";
            this.dgvDettaglio.ReadOnly = true;
            this.dgvDettaglio.Size = new System.Drawing.Size(1372, 183);
            this.dgvDettaglio.TabIndex = 3;
            this.dgvDettaglio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDettaglio_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMessaggio);
            this.groupBox1.Controls.Add(this.btnLeggiDati);
            this.groupBox1.Controls.Add(this.ddlDataCollaudo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1360, 52);
            this.groupBox1.TabIndex = 11;
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
            this.btnLeggiDati.Location = new System.Drawing.Point(339, 9);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(978, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "Applicazione";
            // 
            // txtApplicazione
            // 
            this.txtApplicazione.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApplicazione.Location = new System.Drawing.Point(1075, 276);
            this.txtApplicazione.MaxLength = 50;
            this.txtApplicazione.Name = "txtApplicazione";
            this.txtApplicazione.Size = new System.Drawing.Size(288, 24);
            this.txtApplicazione.TabIndex = 17;
            this.txtApplicazione.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(978, 354);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "Misure per campione";
            // 
            // nMisurePerCampione
            // 
            this.nMisurePerCampione.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nMisurePerCampione.Location = new System.Drawing.Point(1135, 352);
            this.nMisurePerCampione.Name = "nMisurePerCampione";
            this.nMisurePerCampione.Size = new System.Drawing.Size(74, 24);
            this.nMisurePerCampione.TabIndex = 6;
            this.nMisurePerCampione.ValueChanged += new System.EventHandler(this.nMisurePerCampione_ValueChanged);
            // 
            // btnCreaCampioni
            // 
            this.btnCreaCampioni.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreaCampioni.Location = new System.Drawing.Point(1215, 373);
            this.btnCreaCampioni.Name = "btnCreaCampioni";
            this.btnCreaCampioni.Size = new System.Drawing.Size(143, 33);
            this.btnCreaCampioni.TabIndex = 7;
            this.btnCreaCampioni.Text = "Crea campioni";
            this.btnCreaCampioni.UseVisualStyleBackColor = true;
            this.btnCreaCampioni.Click += new System.EventHandler(this.btnCreaCampioni_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(984, 385);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "Numero di campioni";
            // 
            // txtNumeroCampioni
            // 
            this.txtNumeroCampioni.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroCampioni.Location = new System.Drawing.Point(1135, 382);
            this.txtNumeroCampioni.MaxLength = 5;
            this.txtNumeroCampioni.Name = "txtNumeroCampioni";
            this.txtNumeroCampioni.ReadOnly = true;
            this.txtNumeroCampioni.Size = new System.Drawing.Size(74, 24);
            this.txtNumeroCampioni.TabIndex = 17;
            // 
            // dgvAggregati
            // 
            this.dgvAggregati.AllowUserToAddRows = false;
            this.dgvAggregati.AllowUserToDeleteRows = false;
            this.dgvAggregati.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAggregati.Location = new System.Drawing.Point(778, 441);
            this.dgvAggregati.Name = "dgvAggregati";
            this.dgvAggregati.Size = new System.Drawing.Size(594, 173);
            this.dgvAggregati.TabIndex = 21;
            // 
            // chkApriPDF
            // 
            this.chkApriPDF.AutoSize = true;
            this.chkApriPDF.Checked = true;
            this.chkApriPDF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkApriPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkApriPDF.Location = new System.Drawing.Point(1171, 646);
            this.chkApriPDF.Name = "chkApriPDF";
            this.chkApriPDF.Size = new System.Drawing.Size(86, 22);
            this.chkApriPDF.TabIndex = 16;
            this.chkApriPDF.TabStop = false;
            this.chkApriPDF.Text = "Apri PDF";
            this.chkApriPDF.UseVisualStyleBackColor = true;
            // 
            // dgvSpessore
            // 
            this.dgvSpessore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpessore.Location = new System.Drawing.Point(0, 279);
            this.dgvSpessore.Name = "dgvSpessore";
            this.dgvSpessore.Size = new System.Drawing.Size(960, 136);
            this.dgvSpessore.TabIndex = 22;
            this.dgvSpessore.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvSpessore_CellValidating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(978, 319);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Spessore richiesto";
            // 
            // txtSpessoreRichiesto
            // 
            this.txtSpessoreRichiesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpessoreRichiesto.Location = new System.Drawing.Point(1126, 316);
            this.txtSpessoreRichiesto.MaxLength = 10;
            this.txtSpessoreRichiesto.Name = "txtSpessoreRichiesto";
            this.txtSpessoreRichiesto.Size = new System.Drawing.Size(237, 24);
            this.txtSpessoreRichiesto.TabIndex = 17;
            this.txtSpessoreRichiesto.TabStop = false;
            // 
            // CreaSpessoreFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 685);
            this.Controls.Add(this.dgvSpessore);
            this.Controls.Add(this.dgvAggregati);
            this.Controls.Add(this.btnCreaCampioni);
            this.Controls.Add(this.nMisurePerCampione);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNumeroCampioni);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSpessoreRichiesto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtApplicazione);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkApriPDF);
            this.Controls.Add(this.chkCopiaReferto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCreaPDF);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvMisure);
            this.Controls.Add(this.dgvDettaglio);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreaSpessoreFrm";
            this.Text = "Crea Certificato Spessore";
            this.Load += new System.EventHandler(this.CreaSpessoreFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMisurePerCampione)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAggregati)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpessore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCopiaReferto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreaPDF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvMisure;
        private System.Windows.Forms.DataGridView dgvDettaglio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMessaggio;
        private System.Windows.Forms.Button btnLeggiDati;
        private System.Windows.Forms.ComboBox ddlDataCollaudo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApplicazione;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nMisurePerCampione;
        private System.Windows.Forms.Button btnCreaCampioni;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumeroCampioni;
        private System.Windows.Forms.DataGridView dgvAggregati;
        private System.Windows.Forms.CheckBox chkApriPDF;
        private System.Windows.Forms.DataGridView dgvSpessore;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSpessoreRichiesto;
    }
}