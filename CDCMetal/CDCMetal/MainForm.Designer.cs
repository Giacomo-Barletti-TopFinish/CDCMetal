namespace CDCMetal
{
    partial class MainForm
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
            this.cdcMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caricaNuovoDocumentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cartelleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creaCartelleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conformitàToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creaCertificatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dimensioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creaCertificatoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.laboratorioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.antiallergicoNichelFreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorimetricoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spessoriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verniciCoprentiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenutaAcidoNitricoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.analisiPiomboToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eTICHETTEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creaEtichetteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUserLoggato = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.singoloPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.daExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cdcMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cdcMenu
            // 
            this.cdcMenu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdcMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.excelToolStripMenuItem,
            this.cartelleToolStripMenuItem,
            this.conformitàToolStripMenuItem,
            this.dimensioniToolStripMenuItem,
            this.laboratorioToolStripMenuItem,
            this.pDFToolStripMenuItem,
            this.eTICHETTEToolStripMenuItem});
            this.cdcMenu.Location = new System.Drawing.Point(0, 0);
            this.cdcMenu.Name = "cdcMenu";
            this.cdcMenu.Size = new System.Drawing.Size(1558, 25);
            this.cdcMenu.TabIndex = 1;
            this.cdcMenu.Text = "cdcMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.loginToolStripMenuItem.Text = "Login ...";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.caricaNuovoDocumentoToolStripMenuItem});
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(49, 21);
            this.excelToolStripMenuItem.Text = "Excel";
            // 
            // caricaNuovoDocumentoToolStripMenuItem
            // 
            this.caricaNuovoDocumentoToolStripMenuItem.Name = "caricaNuovoDocumentoToolStripMenuItem";
            this.caricaNuovoDocumentoToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.caricaNuovoDocumentoToolStripMenuItem.Text = "Carica nuovo documento...";
            this.caricaNuovoDocumentoToolStripMenuItem.Click += new System.EventHandler(this.caricaNuovoDocumentoToolStripMenuItem_Click);
            // 
            // cartelleToolStripMenuItem
            // 
            this.cartelleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creaCartelleToolStripMenuItem});
            this.cartelleToolStripMenuItem.Name = "cartelleToolStripMenuItem";
            this.cartelleToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.cartelleToolStripMenuItem.Text = "Cartelle";
            // 
            // creaCartelleToolStripMenuItem
            // 
            this.creaCartelleToolStripMenuItem.Name = "creaCartelleToolStripMenuItem";
            this.creaCartelleToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.creaCartelleToolStripMenuItem.Text = "Crea cartelle ...";
            this.creaCartelleToolStripMenuItem.Click += new System.EventHandler(this.creaCartelleToolStripMenuItem_Click);
            // 
            // conformitàToolStripMenuItem
            // 
            this.conformitàToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creaCertificatoToolStripMenuItem});
            this.conformitàToolStripMenuItem.Name = "conformitàToolStripMenuItem";
            this.conformitàToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.conformitàToolStripMenuItem.Text = "Conformità";
            // 
            // creaCertificatoToolStripMenuItem
            // 
            this.creaCertificatoToolStripMenuItem.Name = "creaCertificatoToolStripMenuItem";
            this.creaCertificatoToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.creaCertificatoToolStripMenuItem.Text = "Crea certificato ...";
            this.creaCertificatoToolStripMenuItem.Click += new System.EventHandler(this.creaCertificatoToolStripMenuItem_Click);
            // 
            // dimensioniToolStripMenuItem
            // 
            this.dimensioniToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creaCertificatoToolStripMenuItem1});
            this.dimensioniToolStripMenuItem.Name = "dimensioniToolStripMenuItem";
            this.dimensioniToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.dimensioniToolStripMenuItem.Text = "Dimensioni";
            // 
            // creaCertificatoToolStripMenuItem1
            // 
            this.creaCertificatoToolStripMenuItem1.Name = "creaCertificatoToolStripMenuItem1";
            this.creaCertificatoToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.creaCertificatoToolStripMenuItem1.Text = "Crea Certificato ...";
            this.creaCertificatoToolStripMenuItem1.Click += new System.EventHandler(this.creaCertificatoToolStripMenuItem1_Click);
            // 
            // laboratorioToolStripMenuItem
            // 
            this.laboratorioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.antiallergicoNichelFreeToolStripMenuItem,
            this.colorimetricoToolStripMenuItem,
            this.spessoriToolStripMenuItem,
            this.verniciCoprentiToolStripMenuItem,
            this.tenutaAcidoNitricoToolStripMenuItem,
            this.toolStripSeparator1,
            this.analisiPiomboToolStripMenuItem});
            this.laboratorioToolStripMenuItem.Name = "laboratorioToolStripMenuItem";
            this.laboratorioToolStripMenuItem.Size = new System.Drawing.Size(89, 21);
            this.laboratorioToolStripMenuItem.Text = "Laboratorio";
            // 
            // antiallergicoNichelFreeToolStripMenuItem
            // 
            this.antiallergicoNichelFreeToolStripMenuItem.Name = "antiallergicoNichelFreeToolStripMenuItem";
            this.antiallergicoNichelFreeToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.antiallergicoNichelFreeToolStripMenuItem.Text = "Antiallergico / Nichel Free";
            this.antiallergicoNichelFreeToolStripMenuItem.Click += new System.EventHandler(this.antiallergicoNichelFreeToolStripMenuItem_Click);
            // 
            // colorimetricoToolStripMenuItem
            // 
            this.colorimetricoToolStripMenuItem.Name = "colorimetricoToolStripMenuItem";
            this.colorimetricoToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.colorimetricoToolStripMenuItem.Text = "Colorimetrico";
            this.colorimetricoToolStripMenuItem.Click += new System.EventHandler(this.colorimetricoToolStripMenuItem_Click);
            // 
            // spessoriToolStripMenuItem
            // 
            this.spessoriToolStripMenuItem.Name = "spessoriToolStripMenuItem";
            this.spessoriToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.spessoriToolStripMenuItem.Text = "Spessori";
            this.spessoriToolStripMenuItem.Click += new System.EventHandler(this.spessoriToolStripMenuItem_Click);
            // 
            // verniciCoprentiToolStripMenuItem
            // 
            this.verniciCoprentiToolStripMenuItem.Name = "verniciCoprentiToolStripMenuItem";
            this.verniciCoprentiToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.verniciCoprentiToolStripMenuItem.Text = "Vernici coprenti";
            this.verniciCoprentiToolStripMenuItem.Click += new System.EventHandler(this.verniciCoprentiToolStripMenuItem_Click);
            // 
            // tenutaAcidoNitricoToolStripMenuItem
            // 
            this.tenutaAcidoNitricoToolStripMenuItem.Name = "tenutaAcidoNitricoToolStripMenuItem";
            this.tenutaAcidoNitricoToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.tenutaAcidoNitricoToolStripMenuItem.Text = "Tenuta acido nitrico";
            this.tenutaAcidoNitricoToolStripMenuItem.Click += new System.EventHandler(this.tenutaAcidoNitricoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // analisiPiomboToolStripMenuItem
            // 
            this.analisiPiomboToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singoloPDFToolStripMenuItem,
            this.daExcelToolStripMenuItem});
            this.analisiPiomboToolStripMenuItem.Name = "analisiPiomboToolStripMenuItem";
            this.analisiPiomboToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.analisiPiomboToolStripMenuItem.Text = "Analisi piombo";
            // 
            // pDFToolStripMenuItem
            // 
            this.pDFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listaToolStripMenuItem});
            this.pDFToolStripMenuItem.Name = "pDFToolStripMenuItem";
            this.pDFToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
            this.pDFToolStripMenuItem.Text = "PDF";
            // 
            // listaToolStripMenuItem
            // 
            this.listaToolStripMenuItem.Name = "listaToolStripMenuItem";
            this.listaToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.listaToolStripMenuItem.Text = "Lista...";
            this.listaToolStripMenuItem.Click += new System.EventHandler(this.listaToolStripMenuItem_Click);
            // 
            // eTICHETTEToolStripMenuItem
            // 
            this.eTICHETTEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creaEtichetteToolStripMenuItem});
            this.eTICHETTEToolStripMenuItem.Name = "eTICHETTEToolStripMenuItem";
            this.eTICHETTEToolStripMenuItem.Size = new System.Drawing.Size(69, 21);
            this.eTICHETTEToolStripMenuItem.Text = "Etichette";
            // 
            // creaEtichetteToolStripMenuItem
            // 
            this.creaEtichetteToolStripMenuItem.Name = "creaEtichetteToolStripMenuItem";
            this.creaEtichetteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.creaEtichetteToolStripMenuItem.Text = "Crea etichette...";
            this.creaEtichetteToolStripMenuItem.Click += new System.EventHandler(this.creaEtichetteToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUserLoggato,
            this.prgProgressBar,
            this.lblStatusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 763);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1558, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "cdcStatus";
            // 
            // lblUserLoggato
            // 
            this.lblUserLoggato.Name = "lblUserLoggato";
            this.lblUserLoggato.Size = new System.Drawing.Size(0, 17);
            // 
            // prgProgressBar
            // 
            this.prgProgressBar.Name = "prgProgressBar";
            this.prgProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // singoloPDFToolStripMenuItem
            // 
            this.singoloPDFToolStripMenuItem.Name = "singoloPDFToolStripMenuItem";
            this.singoloPDFToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.singoloPDFToolStripMenuItem.Text = "Singolo PDF";
            this.singoloPDFToolStripMenuItem.Click += new System.EventHandler(this.singoloPDFToolStripMenuItem_Click);
            // 
            // daExcelToolStripMenuItem
            // 
            this.daExcelToolStripMenuItem.Name = "daExcelToolStripMenuItem";
            this.daExcelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.daExcelToolStripMenuItem.Text = "Da Excel";
            this.daExcelToolStripMenuItem.Click += new System.EventHandler(this.daExcelToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1558, 785);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cdcMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.cdcMenu;
            this.Name = "MainForm";
            this.Text = "CDC Metal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.cdcMenu.ResumeLayout(false);
            this.cdcMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip cdcMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar prgProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caricaNuovoDocumentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblUserLoggato;
        private System.Windows.Forms.ToolStripMenuItem cartelleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creaCartelleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conformitàToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creaCertificatoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dimensioniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creaCertificatoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem laboratorioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem antiallergicoNichelFreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorimetricoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spessoriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eTICHETTEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creaEtichetteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verniciCoprentiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenutaAcidoNitricoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem analisiPiomboToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singoloPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem daExcelToolStripMenuItem;
    }
}

