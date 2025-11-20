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
            this.components = new System.ComponentModel.Container();
            this.dgvPorposeMixture = new System.Windows.Forms.DataGridView();
            this.idpurposeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namepurposeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tPurposeMixtureBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mixRubberDataSet = new MixRubber2.MixRubberDataSet();
            this.txtPurposeMixtureName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorposeMixture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tPurposeMixtureBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mixRubberDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPorposeMixture
            // 
            this.dgvPorposeMixture.AllowUserToAddRows = false;
            this.dgvPorposeMixture.AllowUserToDeleteRows = false;
            this.dgvPorposeMixture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPorposeMixture.AutoGenerateColumns = false;
            this.dgvPorposeMixture.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPorposeMixture.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idpurposeDataGridViewTextBoxColumn,
            this.namepurposeDataGridViewTextBoxColumn});
            this.dgvPorposeMixture.DataSource = this.tPurposeMixtureBindingSource;
            this.dgvPorposeMixture.Location = new System.Drawing.Point(0, 0);
            this.dgvPorposeMixture.MultiSelect = false;
            this.dgvPorposeMixture.Name = "dgvPorposeMixture";
            this.dgvPorposeMixture.ReadOnly = true;
            this.dgvPorposeMixture.Size = new System.Drawing.Size(299, 393);
            this.dgvPorposeMixture.TabIndex = 0;
            // 
            // idpurposeDataGridViewTextBoxColumn
            // 
            this.idpurposeDataGridViewTextBoxColumn.DataPropertyName = "id_purpose";
            this.idpurposeDataGridViewTextBoxColumn.HeaderText = "id_purpose";
            this.idpurposeDataGridViewTextBoxColumn.Name = "idpurposeDataGridViewTextBoxColumn";
            this.idpurposeDataGridViewTextBoxColumn.ReadOnly = true;
            this.idpurposeDataGridViewTextBoxColumn.Visible = false;
            // 
            // namepurposeDataGridViewTextBoxColumn
            // 
            this.namepurposeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.namepurposeDataGridViewTextBoxColumn.DataPropertyName = "name_purpose";
            this.namepurposeDataGridViewTextBoxColumn.HeaderText = "Назначение смеси";
            this.namepurposeDataGridViewTextBoxColumn.Name = "namepurposeDataGridViewTextBoxColumn";
            // 
            // tPurposeMixtureBindingSource
            // 
            this.tPurposeMixtureBindingSource.DataMember = "tPurposeMixture";
            this.tPurposeMixtureBindingSource.DataSource = this.mixRubberDataSet;
            // 
            // mixRubberDataSet
            // 
            this.mixRubberDataSet.DataSetName = "MixRubberDataSet";
            this.mixRubberDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtPurposeMixtureName
            // 
            this.txtPurposeMixtureName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurposeMixtureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtPurposeMixtureName.Location = new System.Drawing.Point(2, 399);
            this.txtPurposeMixtureName.Name = "txtPurposeMixtureName";
            this.txtPurposeMixtureName.Size = new System.Drawing.Size(295, 26);
            this.txtPurposeMixtureName.TabIndex = 1;
            this.txtPurposeMixtureName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPurposeMixtureName_KeyPress);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(2, 426);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(295, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(2, 449);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(295, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // PurposeMixture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 476);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtPurposeMixtureName);
            this.Controls.Add(this.dgvPorposeMixture);
            this.Name = "PurposeMixture";
            this.Text = "Назначение смеси";
            this.Load += new System.EventHandler(this.PurposeMixture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorposeMixture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tPurposeMixtureBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mixRubberDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPorposeMixture;
        private MixRubberDataSet mixRubberDataSet;
        private System.Windows.Forms.BindingSource tPurposeMixtureBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idpurposeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namepurposeDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtPurposeMixtureName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
    }
}