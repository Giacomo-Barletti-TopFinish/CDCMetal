namespace CDCMetal
{
    partial class CreaGestioneColoriFrm
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
            this.ddlColore = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvColori = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.lblMessaggio = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColori)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlColore
            // 
            this.ddlColore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlColore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlColore.FormattingEnabled = true;
            this.ddlColore.Location = new System.Drawing.Point(116, 25);
            this.ddlColore.Name = "ddlColore";
            this.ddlColore.Size = new System.Drawing.Size(157, 26);
            this.ddlColore.TabIndex = 2;
            this.ddlColore.SelectedIndexChanged += new System.EventHandler(this.ddlColore_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Colore";
            // 
            // dgvColori
            // 
            this.dgvColori.AllowUserToDeleteRows = false;
            this.dgvColori.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColori.Location = new System.Drawing.Point(38, 67);
            this.dgvColori.Name = "dgvColori";
            this.dgvColori.ReadOnly = true;
            this.dgvColori.Size = new System.Drawing.Size(939, 217);
            this.dgvColori.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button1.Location = new System.Drawing.Point(867, 307);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 33);
            this.button1.TabIndex = 7;
            this.button1.Text = "Salva";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblMessaggio
            // 
            this.lblMessaggio.AutoSize = true;
            this.lblMessaggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessaggio.ForeColor = System.Drawing.Color.Red;
            this.lblMessaggio.Location = new System.Drawing.Point(35, 322);
            this.lblMessaggio.Name = "lblMessaggio";
            this.lblMessaggio.Size = new System.Drawing.Size(0, 18);
            this.lblMessaggio.TabIndex = 8;
            // 
            // CreaGestioneColoriFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 361);
            this.Controls.Add(this.lblMessaggio);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvColori);
            this.Controls.Add(this.ddlColore);
            this.Controls.Add(this.label1);
            this.Name = "CreaGestioneColoriFrm";
            this.Text = "CreaGestioneColoriFrm";
            ((System.ComponentModel.ISupportInitialize)(this._DS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColori)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlColore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvColori;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblMessaggio;
    }
}