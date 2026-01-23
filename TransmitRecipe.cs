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
    public partial class TransmitRecipe : Form
    {
        public TransmitRecipe()
        {
            InitializeComponent();
        }

        private void TransmitRecipe_Load(object sender, EventArgs e)
        {
            cbPurposeMixture.DisplayMember = "name_purpose";
            cbPurposeMixture.ValueMember = "id_purpose";
            cbPurposeMixture.SelectedValueChanged += CbPurposeMixture_SelectedValueChanged;
            cbPurposeMixture.DataSource = DBHelper.GetData("SELECT [id_purpose],[name_purpose] FROM [dbo].[tPurposeMixture]");

            cbMixtureRecipe.DisplayMember = "number";
            cbMixtureRecipe.ValueMember = "id_mixture_recipe";
        }

        private void CbPurposeMixture_SelectedValueChanged(object sender, EventArgs e)
        {
            //DataTable dtMixtureRecipe = DBHelper.GetData("SELECT [id_mixture_recipe],[number] FROM [dbo].[tMixtureRecipes] WHERE [id_purpose] ='" + cbPurposeMixture.SelectedValue + "' AND [id_mode] = '" + cbMixMode.SelectedValue + "'");
            //cbMixtureRecipe.DataSource = dtMixtureRecipe;
        }
    }
}
