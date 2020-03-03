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
            this.label5 = new System.Windows.Forms.Label();
            this.btnAssocia = new System.Windows.Forms.Button();
            this.lstCertificatiDaAssociare = new System.Windows.Forms.ListBox();
            this.lstCertificatiAssociati = new System.Windows.Forms.ListBox();
            this.btnRimuovi = new System.Windows.Forms.Button();
            this.btnCopiaCertificati = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMateriaPrima = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(262, 350);
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
            // lstCertificatiDaAssociare
            // 
            this.lstCertificatiDaAssociare.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCertificatiDaAssociare.FormattingEnabled = true;
            this.lstCertificatiDaAssociare.ItemHeight = 16;
            this.lstCertificatiDaAssociare.Location = new System.Drawing.Point(249, 373);
            this.lstCertificatiDaAssociare.Name = "lstCertificatiDaAssociare";
            this.lstCertificatiDaAssociare.Size = new System.Drawing.Size(313, 196);
            this.lstCertificatiDaAssociare.TabIndex = 22;
            this.lstCertificatiDaAssociare.DoubleClick += new System.EventHandler(this.lstCertificatiDaAssociare_DoubleClick);
            // 
            // lstCertificatiAssociati
            // 
            this.lstCertificatiAssociati.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCertificatiAssociati.FormattingEnabled = true;
            this.lstCertificatiAssociati.ItemHeight = 16;
            this.lstCertificatiAssociati.Location = new System.Drawing.Point(752, 373);
            this.lstCertificatiAssociati.Name = "lstCertificatiAssociati";
            this.lstCertificatiAssociati.Size = new System.Drawing.Size(313, 196);
            this.lstCertificatiAssociati.TabIndex = 22;
            this.lstCertificatiAssociati.DoubleClick += new System.EventHandler(this.lstCertificatiDaAssociare_DoubleClick);
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
            this.btnCopiaCertificati.Location = new System.Drawing.Point(577, 607);
            this.btnCopiaCertificati.Name = "btnCopiaCertificati";
            this.btnCopiaCertificati.Size = new System.Drawing.Size(159, 33);
            this.btnCopiaCertificati.TabIndex = 2;
            this.btnCopiaCertificati.Text = "Copia certificati";
            this.btnCopiaCertificati.UseVisualStyleBackColor = true;
            this.btnCopiaCertificati.Click += new System.EventHandler(this.btnCopiaCertificati_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(749, 350);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 18);
            this.label7.TabIndex = 20;
            this.label7.Text = "Certificati associati";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "Materia prima";
            // 
            // txtMateriaPrima
            // 
            this.txtMateriaPrima.Location = new System.Drawing.Point(170, 300);
            this.txtMateriaPrima.MaxLength = 100;
            this.txtMateriaPrima.Name = "txtMateriaPrima";
            this.txtMateriaPrima.Size = new System.Drawing.Size(576, 20);
            this.txtMateriaPrima.TabIndex = 23;
            // 
            // AssociaCertificatiPiomboFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 685);
            this.Controls.Add(this.txtMateriaPrima);
            this.Controls.Add(this.lstCertificatiAssociati);
            this.Controls.Add(this.lstCertificatiDaAssociare);
            this.Controls.Add(this.btnCopiaCertificati);
            this.Controls.Add(this.btnRimuovi);
            this.Controls.Add(this.btnAssocia);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAssocia;
        private System.Windows.Forms.ListBox lstCertificatiDaAssociare;
        private System.Windows.Forms.ListBox lstCertificatiAssociati;
        private System.Windows.Forms.Button btnRimuovi;
        private System.Windows.Forms.Button btnCopiaCertificati;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMateriaPrima;
    }
}