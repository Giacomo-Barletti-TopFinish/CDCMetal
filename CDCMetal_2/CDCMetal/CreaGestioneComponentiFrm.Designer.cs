namespace CDCMetal
{
    partial class CreaGestioneComponentiFrm
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
            this.ddlBrand = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMessaggio = new System.Windows.Forms.Label();
            this.btnLeggiDati = new System.Windows.Forms.Button();
            this.ddlDataCollaudo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDettaglio = new System.Windows.Forms.DataGridView();
            this.dgvArticoli = new System.Windows.Forms.DataGridView();
            this.buSalva = new System.Windows.Forms.Button();
            this.dgvDimensioni = new System.Windows.Forms.DataGridView();
            this.dgvSpessori = new System.Windows.Forms.DataGridView();
            this.buSalvaDimensioniSpessori = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._DS)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticoli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDimensioni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpessori)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlBrand);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblMessaggio);
            this.groupBox1.Controls.Add(this.btnLeggiDati);
            this.groupBox1.Controls.Add(this.ddlDataCollaudo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1360, 59);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // ddlBrand
            // 
            this.ddlBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlBrand.FormattingEnabled = true;
            this.ddlBrand.Location = new System.Drawing.Point(358, 13);
            this.ddlBrand.Name = "ddlBrand";
            this.ddlBrand.Size = new System.Drawing.Size(157, 26);
            this.ddlBrand.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(305, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Brand";
            // 
            // lblMessaggio
            // 
            this.lblMessaggio.AutoSize = true;
            this.lblMessaggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessaggio.ForeColor = System.Drawing.Color.Red;
            this.lblMessaggio.Location = new System.Drawing.Point(552, 21);
            this.lblMessaggio.Name = "lblMessaggio";
            this.lblMessaggio.Size = new System.Drawing.Size(0, 18);
            this.lblMessaggio.TabIndex = 3;
            // 
            // btnLeggiDati
            // 
            this.btnLeggiDati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeggiDati.Location = new System.Drawing.Point(804, 9);
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
            // dgvDettaglio
            // 
            this.dgvDettaglio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDettaglio.Location = new System.Drawing.Point(12, 77);
            this.dgvDettaglio.Name = "dgvDettaglio";
            this.dgvDettaglio.Size = new System.Drawing.Size(352, 166);
            this.dgvDettaglio.TabIndex = 4;
            this.dgvDettaglio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDettaglio_CellClick);
            // 
            // dgvArticoli
            // 
            this.dgvArticoli.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArticoli.Location = new System.Drawing.Point(370, 77);
            this.dgvArticoli.Name = "dgvArticoli";
            this.dgvArticoli.Size = new System.Drawing.Size(878, 166);
            this.dgvArticoli.TabIndex = 5;
            this.dgvArticoli.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArticoli_CellClick);
            this.dgvArticoli.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvArticoli_UserDeletingRow);
            // 
            // buSalva
            // 
            this.buSalva.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buSalva.Location = new System.Drawing.Point(1138, 245);
            this.buSalva.Name = "buSalva";
            this.buSalva.Size = new System.Drawing.Size(110, 33);
            this.buSalva.TabIndex = 6;
            this.buSalva.Text = "Salva Articoli";
            this.buSalva.UseVisualStyleBackColor = true;
            this.buSalva.Click += new System.EventHandler(this.buSalva_Click);
            // 
            // dgvDimensioni
            // 
            this.dgvDimensioni.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDimensioni.Location = new System.Drawing.Point(12, 281);
            this.dgvDimensioni.Name = "dgvDimensioni";
            this.dgvDimensioni.Size = new System.Drawing.Size(1108, 147);
            this.dgvDimensioni.TabIndex = 7;
            this.dgvDimensioni.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvDimensioni_UserDeletingRow);
            // 
            // dgvSpessori
            // 
            this.dgvSpessori.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpessori.Location = new System.Drawing.Point(12, 449);
            this.dgvSpessori.Name = "dgvSpessori";
            this.dgvSpessori.Size = new System.Drawing.Size(1108, 155);
            this.dgvSpessori.TabIndex = 8;
            this.dgvSpessori.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvSpessori_UserDeletingRow);
            // 
            // buSalvaDimensioniSpessori
            // 
            this.buSalvaDimensioniSpessori.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buSalvaDimensioniSpessori.Location = new System.Drawing.Point(1140, 396);
            this.buSalvaDimensioniSpessori.Name = "buSalvaDimensioniSpessori";
            this.buSalvaDimensioniSpessori.Size = new System.Drawing.Size(108, 69);
            this.buSalvaDimensioniSpessori.TabIndex = 9;
            this.buSalvaDimensioniSpessori.Text = "Salva Dimensioni e Spessori";
            this.buSalvaDimensioniSpessori.UseVisualStyleBackColor = true;
            this.buSalvaDimensioniSpessori.Click += new System.EventHandler(this.buSalvaDimensioniSpessori_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Dimensioni";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 431);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Spessori";
            // 
            // CreaGestioneComponentiFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 634);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buSalvaDimensioniSpessori);
            this.Controls.Add(this.dgvSpessori);
            this.Controls.Add(this.dgvDimensioni);
            this.Controls.Add(this.buSalva);
            this.Controls.Add(this.dgvArticoli);
            this.Controls.Add(this.dgvDettaglio);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreaGestioneComponentiFrm";
            this.Text = "CreaGestioneComponentiFrm";
            this.Load += new System.EventHandler(this.CreaGestioneComponentiFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._DS)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticoli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDimensioni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpessori)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ddlBrand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMessaggio;
        private System.Windows.Forms.Button btnLeggiDati;
        private System.Windows.Forms.ComboBox ddlDataCollaudo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDettaglio;
        private System.Windows.Forms.DataGridView dgvArticoli;
        private System.Windows.Forms.Button buSalva;
        private System.Windows.Forms.DataGridView dgvDimensioni;
        private System.Windows.Forms.DataGridView dgvSpessori;
        private System.Windows.Forms.Button buSalvaDimensioniSpessori;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}