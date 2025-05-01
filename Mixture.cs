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
            dataGridView2.Rows.Add("sdf1");
            dataGridView2.Rows.Add("sdf2");
            dataGridView2.Rows.Add("sdf3");
            dataGridView2.Rows.Add("sdf4");
            dataGridView2.Rows.Add("sdf5");
            dataGridView2.Rows.Add("sdf6");
            dataGridView2.Rows.Add("sdf7");
            dataGridView2.Rows.Add("sdf8");
            dataGridView2.Rows.Add("sdf9");
        }
    }
}
