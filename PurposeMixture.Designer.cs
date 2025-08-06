namespace MixRubber2
{
    partial class PurposeMixture
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
            this.dgvPorposeMixture = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorposeMixture)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPorposeMixture
            // 
            this.dgvPorposeMixture.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPorposeMixture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPorposeMixture.Location = new System.Drawing.Point(0, 0);
            this.dgvPorposeMixture.Name = "dgvPorposeMixture";
            this.dgvPorposeMixture.Size = new System.Drawing.Size(299, 450);
            this.dgvPorposeMixture.TabIndex = 0;
            // 
            // PurposeMixture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 450);
            this.Controls.Add(this.dgvPorposeMixture);
            this.Name = "PurposeMixture";
            this.Text = "Назначение смеси";
            this.Load += new System.EventHandler(this.PurposeMixture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorposeMixture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPorposeMixture;
    }
}