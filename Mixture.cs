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
    public partial class Mixture : Form
    {
        public Mixture()
        {
            InitializeComponent();
        }

        private void Mixture_Load(object sender, EventArgs e)
        {
            cbPurposeMixture.DisplayMember = "name_purpose";
            cbPurposeMixture.ValueMember = "id_purpose";
            cbPurposeMixture.DataSource = DBHelper.GetData("SELECT [id_purpose],[name_purpose] FROM [dbo].[tPurposeMixture]");

            cbMixMode.DisplayMember = "mode_name";
            cbMixMode.ValueMember = "id_mode";
            cbMixMode.DataSource = DBHelper.GetData("SELECT [id_mode],[mode_name] FROM [dbo].[tMixModes]");

            //listView2.Be
        }
    }
}
