namespace CDCMetal
{
    partial class AnalisiPiomboFrm
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
            this.ddlElemento = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLunghezza = new System.Windows.Forms.Label();
            this.nLunghezza = new System.Windows.Forms.NumericUpDown();
            this.lblLarghezza = new System.Windows.Forms.Label();
            this.nLarghezza = new System.Windows.Forms.NumericUpDown();
            this.lblSpessore = new System.Windows.Forms.Label();
            this.nSpessore = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodice = new System.Windows.Forms.TextBox();
            this.ddlMateriale = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtDataCertificato = new System.Windows.Forms.DateTimePicker();
            this.nConcentrazione = new System.Windows.Forms.NumericUpDown();
            this.nMatracciolo = new System.Windows.Forms.NumericUpDown();
            this.nPesoCampione = new System.Windows.Forms.NumericUpDown();
            this.nPd = new System.Windows.Forms.NumericUpDown();
            this.nCd = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEsito = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMetodo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLotto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreaPDF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nLunghezza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nLarghezza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSpessore)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nConcentrazione)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMatracciolo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPesoCampione)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCd)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlElemento
            // 
            this.ddlElemento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlElemento.FormattingEnabled = true;
            this.ddlElemento.Location = new System.Drawing.Point(18, 40);
            this.ddlElemento.Margin = new System.Windows.Forms.Padding(4);
            this.ddlElemento.Name = "ddlElemento";
            this.ddlElemento.Size = new System.Drawing.Size(220, 26);
            this.ddlElemento.TabIndex = 0;
            this.ddlElemento.SelectedIndexChanged += new System.EventHandler(this.ddlElemento_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Elemento da analizzare";
            // 
            // lblLunghezza
            // 
            this.lblLunghezza.AutoSize = true;
            this.lblLunghezza.Location = new System.Drawing.Point(276, 18);
            this.lblLunghezza.Name = "lblLunghezza";
            this.lblLunghezza.Size = new System.Drawing.Size(120, 18);
            this.lblLunghezza.TabIndex = 2;
            this.lblLunghezza.Text = "Lunghezza (mm)";
            // 
            // nLunghezza
            // 
            this.nLunghezza.Location = new System.Drawing.Point(276, 40);
            this.nLunghezza.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nLunghezza.Name = "nLunghezza";
            this.nLunghezza.Size = new System.Drawing.Size(112, 24);
            this.nLunghezza.TabIndex = 5;
            this.nLunghezza.ValueChanged += new System.EventHandler(this.nLunghezza_ValueChanged);
            // 
            // lblLarghezza
            // 
            this.lblLarghezza.AutoSize = true;
            this.lblLarghezza.Location = new System.Drawing.Point(276, 72);
            this.lblLarghezza.Name = "lblLarghezza";
            this.lblLarghezza.Size = new System.Drawing.Size(117, 18);
            this.lblLarghezza.TabIndex = 2;
            this.lblLarghezza.Text = "Larghezza (mm)";
            // 
            // nLarghezza
            // 
            this.nLarghezza.Location = new System.Drawing.Point(276, 95);
            this.nLarghezza.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nLarghezza.Name = "nLarghezza";
            this.nLarghezza.Size = new System.Drawing.Size(112, 24);
            this.nLarghezza.TabIndex = 6;
            this.nLarghezza.ValueChanged += new System.EventHandler(this.nLunghezza_ValueChanged);
            // 
            // lblSpessore
            // 
            this.lblSpessore.AutoSize = true;
            this.lblSpessore.Location = new System.Drawing.Point(276, 131);
            this.lblSpessore.Name = "lblSpessore";
            this.lblSpessore.Size = new System.Drawing.Size(112, 18);
            this.lblSpessore.TabIndex = 2;
            this.lblSpessore.Text = "Spessore (mm)";
            // 
            // nSpessore
            // 
            this.nSpessore.Location = new System.Drawing.Point(276, 153);
            this.nSpessore.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nSpessore.Name = "nSpessore";
            this.nSpessore.Size = new System.Drawing.Size(112, 24);
            this.nSpessore.TabIndex = 7;
            this.nSpessore.ValueChanged += new System.EventHandler(this.nLunghezza_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 18);
            this.label5.TabIndex = 1;
            this.label5.Text = "Codice campione";
            // 
            // txtCodice
            // 
            this.txtCodice.Location = new System.Drawing.Point(18, 95);
            this.txtCodice.MaxLength = 20;
            this.txtCodice.Name = "txtCodice";
            this.txtCodice.Size = new System.Drawing.Size(220, 24);
            this.txtCodice.TabIndex = 1;
            // 
            // ddlMateriale
            // 
            this.ddlMateriale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMateriale.FormattingEnabled = true;
            this.ddlMateriale.Location = new System.Drawing.Point(18, 153);
            this.ddlMateriale.Margin = new System.Windows.Forms.Padding(4);
            this.ddlMateriale.Name = "ddlMateriale";
            this.ddlMateriale.Size = new System.Drawing.Size(220, 26);
            this.ddlMateriale.TabIndex = 3;
            this.ddlMateriale.SelectedIndexChanged += new System.EventHandler(this.ddlMateriale_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 131);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "Materiale";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(454, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 18);
            this.label7.TabIndex = 2;
            this.label7.Text = "Volume (cm3)";
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(457, 42);
            this.txtVolume.MaxLength = 20;
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.ReadOnly = true;
            this.txtVolume.Size = new System.Drawing.Size(180, 24);
            this.txtVolume.TabIndex = 4;
            this.txtVolume.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(454, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 18);
            this.label8.TabIndex = 2;
            this.label8.Text = "Peso (gr)";
            // 
            // txtPeso
            // 
            this.txtPeso.Location = new System.Drawing.Point(457, 94);
            this.txtPeso.MaxLength = 20;
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.ReadOnly = true;
            this.txtPeso.Size = new System.Drawing.Size(180, 24);
            this.txtPeso.TabIndex = 4;
            this.txtPeso.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtDataCertificato);
            this.groupBox1.Controls.Add(this.nConcentrazione);
            this.groupBox1.Controls.Add(this.nMatracciolo);
            this.groupBox1.Controls.Add(this.nPesoCampione);
            this.groupBox1.Controls.Add(this.nPd);
            this.groupBox1.Controls.Add(this.nCd);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtEsito);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMetodo);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(18, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(949, 241);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Analisi";
            // 
            // dtDataCertificato
            // 
            this.dtDataCertificato.Location = new System.Drawing.Point(545, 21);
            this.dtDataCertificato.Name = "dtDataCertificato";
            this.dtDataCertificato.Size = new System.Drawing.Size(200, 24);
            this.dtDataCertificato.TabIndex = 12;
            this.dtDataCertificato.TabStop = false;
            // 
            // nConcentrazione
            // 
            this.nConcentrazione.DecimalPlaces = 3;
            this.nConcentrazione.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nConcentrazione.Location = new System.Drawing.Point(814, 64);
            this.nConcentrazione.Name = "nConcentrazione";
            this.nConcentrazione.Size = new System.Drawing.Size(112, 24);
            this.nConcentrazione.TabIndex = 10;
            this.nConcentrazione.ValueChanged += new System.EventHandler(this.nM_ValueChanged);
            // 
            // nMatracciolo
            // 
            this.nMatracciolo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nMatracciolo.Location = new System.Drawing.Point(472, 64);
            this.nMatracciolo.Name = "nMatracciolo";
            this.nMatracciolo.Size = new System.Drawing.Size(112, 24);
            this.nMatracciolo.TabIndex = 9;
            this.nMatracciolo.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nMatracciolo.ValueChanged += new System.EventHandler(this.nM_ValueChanged);
            // 
            // nPesoCampione
            // 
            this.nPesoCampione.DecimalPlaces = 4;
            this.nPesoCampione.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nPesoCampione.Location = new System.Drawing.Point(162, 64);
            this.nPesoCampione.Name = "nPesoCampione";
            this.nPesoCampione.Size = new System.Drawing.Size(112, 24);
            this.nPesoCampione.TabIndex = 8;
            this.nPesoCampione.ValueChanged += new System.EventHandler(this.nM_ValueChanged);
            // 
            // nPd
            // 
            this.nPd.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nPd.Location = new System.Drawing.Point(205, 121);
            this.nPd.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nPd.Name = "nPd";
            this.nPd.ReadOnly = true;
            this.nPd.Size = new System.Drawing.Size(112, 24);
            this.nPd.TabIndex = 5;
            // 
            // nCd
            // 
            this.nCd.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nCd.Location = new System.Drawing.Point(472, 120);
            this.nCd.Name = "nCd";
            this.nCd.Size = new System.Drawing.Size(112, 24);
            this.nCd.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(591, 66);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(216, 18);
            this.label13.TabIndex = 1;
            this.label13.Text = "Concentrazione Pd XRF (mg/L)";
            // 
            // txtEsito
            // 
            this.txtEsito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEsito.Location = new System.Drawing.Point(375, 181);
            this.txtEsito.MaxLength = 20;
            this.txtEsito.Name = "txtEsito";
            this.txtEsito.ReadOnly = true;
            this.txtEsito.Size = new System.Drawing.Size(131, 24);
            this.txtEsito.TabIndex = 4;
            this.txtEsito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(295, 66);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(170, 18);
            this.label12.TabIndex = 1;
            this.label12.Text = "Volume matracciolo (ml)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 66);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 18);
            this.label11.TabIndex = 1;
            this.label11.Text = "Peso campione (g)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(288, 184);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 18);
            this.label10.TabIndex = 1;
            this.label10.Text = "Esito";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(384, 123);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 18);
            this.label9.TabIndex = 1;
            this.label9.Text = "Cd (PPM)";
            // 
            // txtMetodo
            // 
            this.txtMetodo.Location = new System.Drawing.Point(93, 24);
            this.txtMetodo.MaxLength = 20;
            this.txtMetodo.Name = "txtMetodo";
            this.txtMetodo.Size = new System.Drawing.Size(220, 24);
            this.txtMetodo.TabIndex = 4;
            this.txtMetodo.TabStop = false;
            this.txtMetodo.Text = "XRF";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(418, 24);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 18);
            this.label14.TabIndex = 1;
            this.label14.Text = "Data certificato";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 123);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pb (PPM)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Metodo";
            // 
            // txtLotto
            // 
            this.txtLotto.Location = new System.Drawing.Point(18, 210);
            this.txtLotto.MaxLength = 20;
            this.txtLotto.Name = "txtLotto";
            this.txtLotto.Size = new System.Drawing.Size(220, 24);
            this.txtLotto.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 189);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Lotto/DDT";
            // 
            // btnCreaPDF
            // 
            this.btnCreaPDF.Location = new System.Drawing.Point(376, 525);
            this.btnCreaPDF.Name = "btnCreaPDF";
            this.btnCreaPDF.Size = new System.Drawing.Size(100, 30);
            this.btnCreaPDF.TabIndex = 12;
            this.btnCreaPDF.Text = "Crea PDF";
            this.btnCreaPDF.UseVisualStyleBackColor = true;
            this.btnCreaPDF.Click += new System.EventHandler(this.btnCreaPDF_Click);
            // 
            // AnalisiPiomboFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 609);
            this.Controls.Add(this.btnCreaPDF);
            this.Controls.Add(this.txtLotto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPeso);
            this.Controls.Add(this.txtVolume);
            this.Controls.Add(this.txtCodice);
            this.Controls.Add(this.nSpessore);
            this.Controls.Add(this.lblSpessore);
            this.Controls.Add(this.nLarghezza);
            this.Controls.Add(this.lblLarghezza);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nLunghezza);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblLunghezza);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlMateriale);
            this.Controls.Add(this.ddlElemento);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AnalisiPiomboFrm";
            this.Text = "Analisi piombo e cadmio";
            this.Load += new System.EventHandler(this.AnalisiPiomboFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._DS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nLunghezza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nLarghezza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSpessore)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nConcentrazione)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMatracciolo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPesoCampione)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlElemento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLunghezza;
        private System.Windows.Forms.NumericUpDown nLunghezza;
        private System.Windows.Forms.Label lblLarghezza;
        private System.Windows.Forms.NumericUpDown nLarghezza;
        private System.Windows.Forms.Label lblSpessore;
        private System.Windows.Forms.NumericUpDown nSpessore;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCodice;
        private System.Windows.Forms.ComboBox ddlMateriale;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEsito;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMetodo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nCd;
        private System.Windows.Forms.NumericUpDown nConcentrazione;
        private System.Windows.Forms.NumericUpDown nMatracciolo;
        private System.Windows.Forms.NumericUpDown nPesoCampione;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nPd;
        private System.Windows.Forms.TextBox txtLotto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreaPDF;
        private System.Windows.Forms.DateTimePicker dtDataCertificato;
        private System.Windows.Forms.Label label14;
    }
}