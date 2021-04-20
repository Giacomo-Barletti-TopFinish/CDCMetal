namespace CDCMetal
{
    partial class CreaGestioneBrandFrm
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
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvEnti = new System.Windows.Forms.DataGridView();
            this.dgvBrands = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBrands)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button1.Location = new System.Drawing.Point(660, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "Salva";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(12, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enti";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Brands";
            // 
            // dgvEnti
            // 
            this.dgvEnti.AllowUserToDeleteRows = false;
            this.dgvEnti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnti.Location = new System.Drawing.Point(12, 288);
            this.dgvEnti.Name = "dgvEnti";
            this.dgvEnti.ReadOnly = true;
            this.dgvEnti.Size = new System.Drawing.Size(642, 288);
            this.dgvEnti.TabIndex = 3;
            // 
            // dgvBrands
            // 
            this.dgvBrands.AllowUserToDeleteRows = false;
            this.dgvBrands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBrands.Location = new System.Drawing.Point(15, 37);
            this.dgvBrands.Name = "dgvBrands";
            this.dgvBrands.ReadOnly = true;
            this.dgvBrands.Size = new System.Drawing.Size(639, 217);
            this.dgvBrands.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button2.Location = new System.Drawing.Point(660, 543);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 33);
            this.button2.TabIndex = 7;
            this.button2.Text = "Salva";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CreaGestioneBrandFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 588);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvEnti);
            this.Controls.Add(this.dgvBrands);
            this.Name = "CreaGestioneBrandFrm";
            this.Text = "CreaGestioneBrandFrm";
            ((System.ComponentModel.ISupportInitialize)(this._DS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBrands)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBrands;
        private System.Windows.Forms.DataGridView dgvEnti;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}