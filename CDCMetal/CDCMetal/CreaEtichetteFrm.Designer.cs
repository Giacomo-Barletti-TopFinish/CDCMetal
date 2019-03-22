namespace CDCMetal
{
    partial class CreaEtichetteFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ddlDataCollaudo = new System.Windows.Forms.ComboBox();
            this.btnLeggiDati = new System.Windows.Forms.Button();
            this.lblMessaggio = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlStampanti = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStampaEtichette = new System.Windows.Forms.Button();
            this.btnVerificaEtichette = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDettaglio
            // 
            this.dgvDettaglio.AllowUserToAddRows = false;
            this.dgvDettaglio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDettaglio.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDettaglio.Location = new System.Drawing.Point(0, 139);
            this.dgvDettaglio.Name = "dgvDettaglio";
            this.dgvDettaglio.Size = new System.Drawing.Size(1484, 546);
            this.dgvDettaglio.TabIndex = 5;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlStampanti);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnStampaEtichette);
            this.groupBox1.Controls.Add(this.btnVerificaEtichette);
            this.groupBox1.Controls.Add(this.lblMessaggio);
            this.groupBox1.Controls.Add(this.btnLeggiDati);
            this.groupBox1.Controls.Add(this.ddlDataCollaudo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1460, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // ddlStampanti
            // 
            this.ddlStampanti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStampanti.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStampanti.FormattingEnabled = true;
            this.ddlStampanti.Location = new System.Drawing.Point(925, 13);
            this.ddlStampanti.Margin = new System.Windows.Forms.Padding(4);
            this.ddlStampanti.Name = "ddlStampanti";
            this.ddlStampanti.Size = new System.Drawing.Size(528, 26);
            this.ddlStampanti.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(840, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Stampanti";
            // 
            // btnStampaEtichette
            // 
            this.btnStampaEtichette.Enabled = false;
            this.btnStampaEtichette.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnStampaEtichette.Location = new System.Drawing.Point(701, 9);
            this.btnStampaEtichette.Name = "btnStampaEtichette";
            this.btnStampaEtichette.Size = new System.Drawing.Size(110, 33);
            this.btnStampaEtichette.TabIndex = 4;
            this.btnStampaEtichette.Text = "Stampa";
            this.btnStampaEtichette.UseVisualStyleBackColor = true;
            this.btnStampaEtichette.Click += new System.EventHandler(this.btnStampaEtichette_Click);
            // 
            // btnVerificaEtichette
            // 
            this.btnVerificaEtichette.Enabled = false;
            this.btnVerificaEtichette.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnVerificaEtichette.Location = new System.Drawing.Point(573, 9);
            this.btnVerificaEtichette.Name = "btnVerificaEtichette";
            this.btnVerificaEtichette.Size = new System.Drawing.Size(110, 33);
            this.btnVerificaEtichette.TabIndex = 4;
            this.btnVerificaEtichette.Text = "Verifica";
            this.btnVerificaEtichette.UseVisualStyleBackColor = true;
            this.btnVerificaEtichette.Click += new System.EventHandler(this.btnVerificaEtichette_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(827, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(405, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "NUMORE ETICHETTE deve essere nel formato NSCXQTA;";
            // 
            // CreaEtichetteFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 685);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvDettaglio);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreaEtichetteFrm";
            this.Text = "CreaEtichetteFrm";
            this.Load += new System.EventHandler(this.CreaEtichetteFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettaglio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDettaglio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlDataCollaudo;
        private System.Windows.Forms.Button btnLeggiDati;
        private System.Windows.Forms.Label lblMessaggio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStampaEtichette;
        private System.Windows.Forms.Button btnVerificaEtichette;
        private System.Windows.Forms.ComboBox ddlStampanti;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}