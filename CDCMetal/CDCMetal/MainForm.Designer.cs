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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUserLoggato = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.conformitàToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creaCertificatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.conformitàToolStripMenuItem});
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
            this.creaCartelleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.creaCartelleToolStripMenuItem.Text = "Crea cartelle ...";
            this.creaCartelleToolStripMenuItem.Click += new System.EventHandler(this.creaCartelleToolStripMenuItem_Click);
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
            this.creaCertificatoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.creaCertificatoToolStripMenuItem.Text = "Crea certificato ...";
            this.creaCertificatoToolStripMenuItem.Click += new System.EventHandler(this.creaCertificatoToolStripMenuItem_Click);
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
    }
}

