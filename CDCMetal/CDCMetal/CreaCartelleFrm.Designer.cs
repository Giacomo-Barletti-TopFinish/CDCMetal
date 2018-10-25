namespace CDCMetal
{
    partial class CreaCartelleFrm
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
            this.lblMessaggio = new System.Windows.Forms.Label();
            this.btnCreaCartelle = new System.Windows.Forms.Button();
            this.btnLeggiDati = new System.Windows.Forms.Button();
            this.ddlDataCollaudo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDettaglio = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMessaggio);
            this.groupBox1.Controls.Add(this.btnCreaCartelle);
            this.groupBox1.Controls.Add(this.btnLeggiDati);
            this.groupBox1.Controls.Add(this.ddlDataCollaudo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1360, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblMessaggio
            // 
            this.lblMessaggio.AutoSize = true;
            this.lblMessaggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessaggio.ForeColor = System.Drawing.Color.Red;
            this.lblMessaggio.Location = new System.Drawing.Point(6, 60);
            this.lblMessaggio.Name = "lblMessaggio";
            this.lblMessaggio.Size = new System.Drawing.Size(0, 18);
            this.lblMessaggio.TabIndex = 3;
            // 
            // btnCreaCartelle
            // 
            this.btnCreaCartelle.Enabled = false;
            this.btnCreaCartelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreaCartelle.Location = new System.Drawing.Point(483, 9);
            this.btnCreaCartelle.Name = "btnCreaCartelle";
            this.btnCreaCartelle.Size = new System.Drawing.Size(110, 33);
            this.btnCreaCartelle.TabIndex = 2;
            this.btnCreaCartelle.Text = "Crea cartelle";
            this.btnCreaCartelle.UseVisualStyleBackColor = true;
            this.btnCreaCartelle.Click += new System.EventHandler(this.btnCreaCartelle_Click);
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
            // dgvDettaglio
            // 
            this.dgvDettaglio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDettaglio.Location = new System.Drawing.Point(12, 127);
            this.dgvDettaglio.Name = "dgvDettaglio";
            this.dgvDettaglio.Size = new System.Drawing.Size(1360, 546);
            this.dgvDettaglio.TabIndex = 1;
            // 
            // CreaCartelleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 685);
            this.Controls.Add(this.dgvDettaglio);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreaCartelleFrm";
            this.Text = "CreaCartelleFrm";
            this.Load += new System.EventHandler(this.CreaCartelleFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlDataCollaudo;
        private System.Windows.Forms.Button btnLeggiDati;
        private System.Windows.Forms.Button btnCreaCartelle;
        private System.Windows.Forms.Label lblMessaggio;
        private System.Windows.Forms.DataGridView dgvDettaglio;
    }
}