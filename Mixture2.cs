using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixRubber2
{
    public partial class Mixture2 : Form
    {
        int materialStringCount = 0;
        public Mixture2()
        {
            InitializeComponent();
        }

        private void Mixture2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            cbPurpose.DisplayMember = "name";
            cbPurpose.ValueMember = "id";
            cbPurpose.DataSource = DBHelper.GetData("SELECT [id],[name] FROM [dbo].[tPurposeMixture]");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int thisPositionNumber = ++materialStringCount;
            Button btnAdd = (Button)sender;
            Point position = btnAdd.Location;
            btnAdd.Location = new Point(position.X, position.Y + 31);
            ComboBox cbMaterial = new ComboBox() { Location = position, Width = lblMaterial.Width, Font = cbMixMode.Font, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Name = "cbMaterial" + materialStringCount };
            TextBox txtWeight = new TextBox() { Location = new Point(lblWeight.Location.X, position.Y + 1), Width = lblWeight.Width, Font = cbMixMode.Font, BorderStyle = BorderStyle.FixedSingle, Name = "txtWeight" + materialStringCount };
            TextBox txt1percent = new TextBox() { Location = new Point(lbl1percent.Location.X, position.Y + 1), Width = lbl1percent.Width, Font = cbMixMode.Font, BorderStyle = BorderStyle.FixedSingle, Name = "txt1percent" + materialStringCount };
            TextBox txt2percent = new TextBox() { Location = new Point(lbl2percent.Location.X, position.Y + 1), Width = lbl2percent.Width, Font = cbMixMode.Font, BorderStyle = BorderStyle.FixedSingle, Name = "txt2percent" + materialStringCount };
            TextBox txtTolerance = new TextBox() { Location = new Point(lblTolerance.Location.X, position.Y + 1), Width = lblTolerance.Width, Font = cbMixMode.Font, BorderStyle = BorderStyle.FixedSingle, Name = "txtTolerance" + materialStringCount };
            Button btnDelRow = new Button() { Location = new Point(lblDel.Location.X, position.Y + 2), Width = lblDel.Width, FlatStyle = FlatStyle.Flat, BackgroundImage = global::MixRubber2.Properties.Resources.delete, BackgroundImageLayout = ImageLayout.Center, Name = materialStringCount.ToString() };
            btnDelRow.Click += (object obj, EventArgs ea) => { this.Controls.Remove(cbMaterial); this.Controls.Remove(txtWeight); this.Controls.Remove(txt1percent); this.Controls.Remove(txt2percent); this.Controls.Remove(txtTolerance); ShiftRows(btnDelRow); this.Controls.Remove(btnDelRow); };
            this.Controls.Add(cbMaterial);
            this.Controls.Add(txtWeight);
            this.Controls.Add(txt1percent);
            this.Controls.Add(txt2percent);
            this.Controls.Add(txtTolerance);
            this.Controls.Add(btnDelRow);
        }

        private void ShiftRows(Button btnDel)
        {
            for (int i = Convert.ToInt32(btnDel.Name); i < materialStringCount; i++)
            {
                Control ctrl = this.Controls["cbMaterial" + (i + 1)];
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 31);
                ctrl.Name = "cbMaterial" + i;
                ctrl = this.Controls["txtWeight" + (i + 1)];
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 31);
                ctrl.Name = "txtWeight" + i;
                ctrl = this.Controls["txt1percent" + (i + 1)];
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 31);
                ctrl.Name = "txt1percent" + i;
                ctrl = this.Controls["txt2percent" + (i + 1)];
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 31);
                ctrl.Name = "txt2percent" + i;
                ctrl = this.Controls["txtTolerance" + (i + 1)];
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 31);
                ctrl.Name = "txtTolerance" + i;
                ctrl = this.Controls[(i + 1).ToString()];
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 31);
                ctrl.Name = i.ToString();
            }
            btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y - 31);
            materialStringCount--;
        }

       
    }
}
