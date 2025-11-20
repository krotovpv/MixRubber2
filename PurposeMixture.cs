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

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить элемент?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            }
        }

        private void txtPurposeMixtureName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //this.tPurposeMixtureTableAdapter.Insert1(txtPurposeMixtureName.Text.Trim());
                //txtPurposeMixtureName.Clear();
                //this.tPurposeMixtureTableAdapter.Fill(this.mixRubberDataSet.tPurposeMixture);
                //e.Handled = true;
            }
        }
    }
}
