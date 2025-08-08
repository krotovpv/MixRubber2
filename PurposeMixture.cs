using MixRubber2.MixRubberDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixRubber2
{
    public partial class PurposeMixture : Form
    {

        public PurposeMixture()
        {
            InitializeComponent();
        }

        private void PurposeMixture_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mixRubberDataSet.tPurposeMixture". При необходимости она может быть перемещена или удалена.
            this.tPurposeMixtureTableAdapter.Fill(this.mixRubberDataSet.tPurposeMixture);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.tPurposeMixtureTableAdapter.Insert1(txtPurposeMixtureName.Text.Trim());
            txtPurposeMixtureName.Clear();
            this.tPurposeMixtureTableAdapter.Fill(this.mixRubberDataSet.tPurposeMixture);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить элемент?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.tPurposeMixtureTableAdapter.Delete1(Convert.ToInt32(dgvPorposeMixture.Rows[dgvPorposeMixture.SelectedCells[0].RowIndex].Cells[0].Value));
                this.tPurposeMixtureTableAdapter.Fill(this.mixRubberDataSet.tPurposeMixture);
            }
        }

        private void txtPurposeMixtureName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.tPurposeMixtureTableAdapter.Insert1(txtPurposeMixtureName.Text.Trim());
                txtPurposeMixtureName.Clear();
                this.tPurposeMixtureTableAdapter.Fill(this.mixRubberDataSet.tPurposeMixture);
                e.Handled = true;
            }
        }
    }
}
