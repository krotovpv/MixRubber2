using System;
using System.Data;
using System.Windows.Forms;

namespace MixRubber2
{
    public partial class MixMode2 : Form
    {
        public MixMode2()
        {
            InitializeComponent();
        }

        private void MixMode2_Load(object sender, EventArgs e)
        {
            FillingData();

        }

        private void FillingData()
        {
            DataTable dtOperations = DBHelper.GetData("SELECT [kod_operation],[name_operation] FROM [dbo].[tOperations]");
            foreach (var item in tabPageRG.Controls)
            {
                if (item is ComboBox cb)
                {
                    cb.DisplayMember = "name_operation";
                    cb.ValueMember = "kod_operation";
                    cb.DataSource = dtOperations.Copy();
                }
            }
            foreach (var item in tabPageRJ.Controls)
            {
                if (item is ComboBox cb)
                {
                    cb.DisplayMember = "name_operation";
                    cb.ValueMember = "kod_operation";
                    cb.DataSource = dtOperations.Copy();
                }
            }

            DataTable dtMixMode = DBHelper.GetData("SELECT [id_mode],[mode_name] FROM [dbo].[tMixModes]");
            dgvMixMode.Rows.Clear();

            dgvMixMode.SelectionChanged += DgvMixMode_SelectionChanged;
            for (int i = 0; i < dtMixMode.Rows.Count; i++)
                dgvMixMode.Rows.Add(dtMixMode.Rows[i].ItemArray[0], dtMixMode.Rows[i].ItemArray[1]);
        }

        private void DgvMixMode_SelectionChanged(object sender, EventArgs e)
        {
            ClearUI();
            int rowIndex = dgvMixMode.SelectedCells[0].RowIndex;
            int idMode = Convert.ToInt32(dgvMixMode.Rows[rowIndex].Cells["ColumnIdMixMode"].Value);
            DataTable dtMixMode = DBHelper.GetData("SELECT [mode_name],[temp_unload],[temp_critical],[temp_tolerance],[parametr1],[parametr2],[parametr3],[parametr4],[parametr5] FROM [dbo].[tMixModes] WHERE [id_mode] = '" + idMode + "'");
            txtName.Text = dtMixMode.Rows[0].ItemArray[0].ToString();
            txtUnloadTemperature.Text = dtMixMode.Rows[0].ItemArray[1].ToString();
            txtCriticalTemperature.Text = dtMixMode.Rows[0].ItemArray[2].ToString();
            txtToleranceTemperature.Text = dtMixMode.Rows[0].ItemArray[3].ToString();
            txtParam1.Text = dtMixMode.Rows[0].ItemArray[4].ToString();
            txtParam2.Text = dtMixMode.Rows[0].ItemArray[5].ToString();
            txtParam3.Text = dtMixMode.Rows[0].ItemArray[6].ToString();
            txtParam4.Text = dtMixMode.Rows[0].ItemArray[7].ToString();
            txtParam5.Text = dtMixMode.Rows[0].ItemArray[8].ToString();

            DataTable dtModeOperationsRG = DBHelper.GetData("SELECT [id_operation],[position],[time_start],[time_stop] FROM [dbo].[tMode_Operation] WHERE [id_mode] = '" + idMode + "' AND [short_name_type] = 'РГ'");
            for (int i = 0; i < dtModeOperationsRG.Rows.Count; i++)
            {
                int position = 0;
                if (Int32.TryParse(dtModeOperationsRG.Rows[i].ItemArray[1].ToString(), out position))
                {
                    if (position == 1)
                    {
                        cbOperationRG1.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart1.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop1.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 2)
                    {
                        cbOperationRG2.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart2.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop2.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 3)
                    {
                        cbOperationRG3.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart3.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop3.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 4)
                    {
                        cbOperationRG4.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart4.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop4.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 5)
                    {
                        cbOperationRG5.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart5.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop5.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 6)
                    {
                        cbOperationRG6.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart6.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop6.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 7)
                    {
                        cbOperationRG7.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart7.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop7.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 8)
                    {
                        cbOperationRG8.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart8.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop8.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 9)
                    {
                        cbOperationRG9.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart9.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop9.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 10)
                    {
                        cbOperationRG10.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart10.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop10.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 11)
                    {
                        cbOperationRG11.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart11.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop11.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 12)
                    {
                        cbOperationRG12.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart12.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop12.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 13)
                    {
                        cbOperationRG13.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart13.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop13.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 14)
                    {
                        cbOperationRG14.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart14.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop14.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 15)
                    { 
                        cbOperationRG15.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart15.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop15.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 16)
                    {
                        cbOperationRG16.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart16.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop16.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 17)
                    {
                        cbOperationRG17.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart17.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop17.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 18)
                    {
                        cbOperationRG18.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart18.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop18.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 19)
                    {
                        cbOperationRG19.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart19.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop19.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 20)
                    {
                        cbOperationRG20.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart20.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop20.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 21)
                    {
                        cbOperationRG21.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart21.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop21.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 22)
                    {
                        cbOperationRG22.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart22.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop22.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 23)
                    {
                        cbOperationRG23.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart23.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop23.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 24)
                    {
                        cbOperationRG24.SelectedValue = dtModeOperationsRG.Rows[i].ItemArray[0].ToString();
                        txtOperationRGStart24.Text = dtModeOperationsRG.Rows[i].ItemArray[2].ToString();
                        txtOperationRGStop24.Text = dtModeOperationsRG.Rows[i].ItemArray[3].ToString();
                    }
                }
            }

            DataTable dtModeOperationsRJ = DBHelper.GetData("SELECT [id_operation],[position],[time_start],[time_stop] FROM [dbo].[tMode_Operation] WHERE [id_mode] = '" + idMode + "' AND [short_name_type] = 'РЖ'");
            for (int i = 0; i < dtModeOperationsRJ.Rows.Count; i++)
            {
                int position = 0;
                if (Int32.TryParse(dtModeOperationsRJ.Rows[i].ItemArray[1].ToString(), out position))
                {
                    if (position == 1)
                    {
                        cbOperationRJ1.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart1.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop1.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 2)
                    {
                        cbOperationRJ2.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart2.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop2.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 3)
                    {
                        cbOperationRJ3.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart3.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop3.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 4)
                    {
                        cbOperationRJ4.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart4.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop4.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 5)
                    {
                        cbOperationRJ5.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart5.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop5.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 6)
                    {
                        cbOperationRJ6.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart6.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop6.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 7)
                    {
                        cbOperationRJ7.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart7.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop7.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 8)
                    {
                        cbOperationRJ8.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart8.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop8.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 9)
                    {
                        cbOperationRJ9.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart9.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop9.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 10)
                    {
                        cbOperationRJ10.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart10.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop10.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 11)
                    {
                        cbOperationRJ11.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart11.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop11.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 12)
                    {
                        cbOperationRJ12.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart12.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop12.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 13)
                    {
                        cbOperationRJ13.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart13.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop13.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 14)
                    {
                        cbOperationRJ14.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart14.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop14.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 15)
                    {
                        cbOperationRJ15.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart15.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop15.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 16)
                    {
                        cbOperationRJ16.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart16.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop16.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 17)
                    {
                        cbOperationRJ17.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart17.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop17.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 18)
                    {
                        cbOperationRJ18.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart18.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop18.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 19)
                    {
                        cbOperationRJ19.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart19.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop19.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 20)
                    {
                        cbOperationRJ20.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart20.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop20.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 21)
                    {
                        cbOperationRJ21.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart21.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop21.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 22)
                    {
                        cbOperationRJ22.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart22.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop22.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 23)
                    {
                        cbOperationRJ23.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart23.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop23.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 24)
                    {
                        cbOperationRJ24.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart24.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop24.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 25)
                    {
                        cbOperationRJ25.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart25.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop25.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 26)
                    {
                        cbOperationRJ26.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart26.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop26.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 27)
                    {
                        cbOperationRJ27.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart27.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop27.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 28)
                    {
                        cbOperationRJ28.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart28.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop28.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 29)
                    {
                        cbOperationRJ29.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart29.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop29.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 30)
                    {
                        cbOperationRJ30.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart30.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop30.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 31)
                    {
                        cbOperationRJ31.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart31.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop31.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                    else if (position == 32)
                    {
                        cbOperationRJ32.SelectedValue = dtModeOperationsRJ.Rows[i].ItemArray[0].ToString();
                        txtOperationRJStart32.Text = dtModeOperationsRJ.Rows[i].ItemArray[2].ToString();
                        txtOperationRJStop32.Text = dtModeOperationsRJ.Rows[i].ItemArray[3].ToString();
                    }
                }
            }



        }

        private void ClearUI()
        {
            txtName.Text = "";
            txtUnloadTemperature.Text = "";
            txtCriticalTemperature.Text = "";
            txtToleranceTemperature.Text = "";
            txtParam1.Text = "";
            txtParam2.Text = "";
            txtParam3.Text = "";
            txtParam4.Text = "";
            txtParam5.Text = "";
            foreach (var item in tabPageRG.Controls)
            {
                if (item is ComboBox cb)
                {
                    cb.SelectedValue = 0;
                }
                else if (item is TextBox txt)
                {
                    txt.Text = "";
                }
            }
            foreach (var item in tabPageRJ.Controls)
            {
                if (item is ComboBox cb)
                {
                    cb.SelectedValue = 0;
                }
                else if (item is TextBox txt)
                {
                    txt.Text = "";
                }
            }
        }

        private void btnCreateMixMode_Click(object sender, EventArgs e)
        {
            ClearUI();
        }
    }
}
