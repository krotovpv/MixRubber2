using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
            DataTable dtOperations = DBHelper.GetData("SELECT [kod_operation],[name_operation],[color] FROM [dbo].[tOperations]");
            foreach (var item in tabPageRG.Controls)
            {
                if (item is ComboBox cb)
                {
                    cb.DisplayMember = "name_operation";
                    cb.ValueMember = "kod_operation";
                    cb.DataSource = dtOperations.Copy();
                    cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
                }
                else if (item is TextBox txt)
                {
                    txt.TextChanged += LblBackgroundRGChanged;
                }
            }
            foreach (var item in tabPageRJ.Controls)
            {
                if (item is ComboBox cb)
                {
                    cb.DisplayMember = "name_operation";
                    cb.ValueMember = "kod_operation";
                    cb.DataSource = dtOperations.Copy();
                    cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
                }
                else if (item is TextBox txt)
                {
                    txt.TextChanged += LblBackgroundRJChanged;
                }
            }

            SelectMixModes();
        }

        private void LblBackgroundRGChanged(object sender, EventArgs e)
        {
            List<int> startTime = new List<int>();
            List<int> stopTime = new List<int>();

            for (int i = 1; i < 25; i++)
            {
                if (((ComboBox)tabPageRG.Controls.Find("cbOperationRG" + i, false)[0]).SelectedIndex > 0)
                {
                    int currentStartTime;
                    int currentStopTime;
                    if (Int32.TryParse(tabPageRG.Controls.Find("txtOperationRGStart" + i, false)[0].Text, out currentStartTime))
                    {
                        startTime.Add(currentStartTime);
                        if (Int32.TryParse(tabPageRG.Controls.Find("txtOperationRGStop" + i, false)[0].Text, out currentStopTime))
                            stopTime.Add(currentStopTime);
                    }
                }
            }

            if (startTime.Count < 1 || stopTime.Count < 1) return;

            int startTimeMin = startTime.Min();
            int endTimeMax = stopTime.Max();

            int x = 291;
            int y = 34;
            int widthDefault = lblBackgroundRG.Width;
            int intervaleTime = endTimeMax - startTimeMin;
            double oneMinute = widthDefault / intervaleTime;

            for (int i = 1; i < 25; i++)
            {
                int currentStartTime;
                int currentStopTime;
                int currentX;
                int currentWidth;
                Label lblInterval = tabPageRG.Controls.Find("lblIntervalRG" + i, false)[0] as Label;
                TextBox txtStart = tabPageRG.Controls.Find("txtOperationRGStart" + i, false)[0] as TextBox;
                TextBox txtStop = tabPageRG.Controls.Find("txtOperationRGStop" + i, false)[0] as TextBox;

                if (Int32.TryParse(txtStart.Text, out currentStartTime))
                {
                    currentX = Convert.ToInt32((currentStartTime - startTimeMin) * oneMinute) + x;
                    lblInterval.Location = new Point(currentX, y * i);

                    if (Int32.TryParse(txtStop.Text, out currentStopTime))
                    {
                        currentWidth = Convert.ToInt32((currentStopTime - currentStartTime) * oneMinute);
                        lblInterval.Width = currentWidth;
                        lblInterval.Visible = true;
                    }
                    else
                    {
                        lblInterval.Visible = false;
                    }
                }
                else
                {
                    lblInterval.Visible = false;
                }
            }
        }

        private void LblBackgroundRJChanged(object sender, EventArgs e)
        {
            List<int> startTime = new List<int>();
            List<int> stopTime = new List<int>();

            for (int i = 1; i < 33; i++)
            {
                if (((ComboBox)tabPageRJ.Controls.Find("cbOperationRJ" + i, false)[0]).SelectedIndex > 0)
                {
                    int currentStartTime;
                    int currentStopTime;
                    if (Int32.TryParse(tabPageRJ.Controls.Find("txtOperationRJStart" + i, false)[0].Text, out currentStartTime))
                    {
                        startTime.Add(currentStartTime);
                        if (Int32.TryParse(tabPageRJ.Controls.Find("txtOperationRJStop" + i, false)[0].Text, out currentStopTime))
                            stopTime.Add(currentStopTime);
                    }
                }
            }

            if (startTime.Count < 1 || stopTime.Count < 1) return;

            int startTimeMin = startTime.Min();
            int endTimeMax = stopTime.Max();

            int x = 291;
            int y = 34;
            int widthDefault = lblBackgroundRJ.Width;
            int intervaleTime = endTimeMax - startTimeMin;
            double oneMinute = widthDefault / intervaleTime;

            for (int i = 1; i < 33; i++)
            {
                int currentStartTime;
                int currentStopTime;
                int currentX;
                int currentWidth;
                Label lblInterval = tabPageRJ.Controls.Find("lblIntervalRJ" + i, false)[0] as Label;
                TextBox txtStart = tabPageRJ.Controls.Find("txtOperationRJStart" + i, false)[0] as TextBox;
                TextBox txtStop = tabPageRJ.Controls.Find("txtOperationRJStop" + i, false)[0] as TextBox;

                if (Int32.TryParse(txtStart.Text, out currentStartTime))
                {
                    currentX = Convert.ToInt32((currentStartTime - startTimeMin) * oneMinute) + x;
                    lblInterval.Location = new Point(currentX, y * i);

                    if (Int32.TryParse(txtStop.Text, out currentStopTime))
                    {
                        currentWidth = Convert.ToInt32((currentStopTime - currentStartTime) * oneMinute);
                        lblInterval.Width = currentWidth;
                        lblInterval.Visible = true;
                    }
                    else
                    {
                        lblInterval.Visible = false;
                    }
                }
                else
                {
                    lblInterval.Visible = false;
                }
            }
        }

        private void Cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
                cb.BackColor = Color.White;
            else
                cb.BackColor = Color.PaleGreen;
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

        private void btnSaveMixMode_Click(object sender, EventArgs e)
        {
            DataTable dtIdMixMode = DBHelper.GetData("SELECT [id_mode] FROM [dbo].[tMixModes] WHERE [mode_name] = '" + txtName.Text.Trim() + "'");
            
            if (dtIdMixMode.Rows.Count > 0)
            {
                if (MessageBox.Show("Хотите перезаписать данный режим?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    string idMixMode = dtIdMixMode.Rows[0].ItemArray[0].ToString();
                    UpdateMixMode(idMixMode);
                    UpdateMixModeOperation(idMixMode);
                }
                else
                {
                    return;
                }
            }
            else
            {
                string idMixMode = InsertMixMode();
                InsertMixModeOperations(idMixMode);
            }

            SelectMixModes();
        }

        private void SelectMixModes()
        {
            dgvMixMode.SelectionChanged -= DgvMixMode_SelectionChanged;
            DataTable dtMixMode = DBHelper.GetData("SELECT [id_mode],[mode_name] FROM [dbo].[tMixModes]");
            dgvMixMode.Rows.Clear();

            dgvMixMode.SelectionChanged += DgvMixMode_SelectionChanged;
            for (int i = 0; i < dtMixMode.Rows.Count; i++)
                dgvMixMode.Rows.Add(dtMixMode.Rows[i].ItemArray[0], dtMixMode.Rows[i].ItemArray[1]);
        }

        private string InsertMixMode()
        {
            DataTable dt = DBHelper.GetData(
                    "INSERT INTO [dbo].[tMixModes] " +
                    "([mode_name]" +
                    ",[temp_unload]" +
                    ",[temp_critical]" +
                    ",[temp_tolerance]" +
                    ",[parametr1]" +
                    ",[parametr2]" +
                    ",[parametr3]" +
                    ",[parametr4]" +
                    ",[parametr5]) " +
                    "OUTPUT Inserted.id_mode VALUES " +
                    "('" + txtName.Text.Trim() + "'" +
                    ",'" + txtUnloadTemperature.Text.Trim() + "'" +
                    ",'" + txtCriticalTemperature.Text.Trim() + "'" +
                    ",'" + txtToleranceTemperature.Text.Trim() + "'" +
                    ",'" + txtParam1.Text.Trim() + "'" +
                    ",'" + txtParam2.Text.Trim() + "'" +
                    ",'" + txtParam3.Text.Trim() + "'" +
                    ",'" + txtParam4.Text.Trim() + "'" +
                    ",'" + txtParam5.Text.Trim() + "')");

            if (dt.Rows.Count > 0)
                return dt.Rows[0].ItemArray[0].ToString();
            else
                return "";
        }

        private void UpdateMixMode(string IdMixMode)
        {
            DBHelper.GetData(
                        "UPDATE [dbo].[tMixModes] " +
                        "SET " +
                        "[temp_unload] = '" + txtUnloadTemperature.Text.Trim() + "'" +
                        ",[temp_critical] = '" + txtCriticalTemperature.Text.Trim() + "'" +
                        ",[temp_tolerance] = '" + txtToleranceTemperature.Text.Trim() + "'" +
                        ",[parametr1] = '" + txtParam1.Text.Trim() + "'" +
                        ",[parametr2] = '" + txtParam2.Text.Trim() + "'" +
                        ",[parametr3] = '" + txtParam3.Text.Trim() + "'" +
                        ",[parametr4] = '" + txtParam4.Text.Trim() + "'" +
                        ",[parametr5] = '" + txtParam5.Text.Trim() + "'" +
                        "WHERE [id_mode] = '" + IdMixMode + "'");
        }

        private void DeleteMixMode(string IdMixMode)
        {
            DBHelper.GetData("DELETE FROM [dbo].[tMode_Operation] WHERE [id_mode] = '" + IdMixMode + "'");
            DBHelper.GetData("DELETE FROM [dbo].[tMixModes] WHERE [id_mode] = '" + IdMixMode + "'");
        }

        private void InsertMixModeOperations(string IdMixMode)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(
                    "INSERT INTO [dbo].[tMode_Operation] " +
                    "([id_mode]" +
                    ",[id_operation]" +
                    ",[position]" +
                    ",[time_start]" +
                    ",[time_stop]" +
                    ",[short_name_type]) VALUES ");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG1.SelectedValue + "', '1', '" + txtOperationRGStart1.Text.Trim() + "', '" + txtOperationRGStop1.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG2.SelectedValue + "', '2', '" + txtOperationRGStart2.Text.Trim() + "', '" + txtOperationRGStop2.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG3.SelectedValue + "', '3', '" + txtOperationRGStart3.Text.Trim() + "', '" + txtOperationRGStop3.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG4.SelectedValue + "', '4', '" + txtOperationRGStart4.Text.Trim() + "', '" + txtOperationRGStop4.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG5.SelectedValue + "', '5', '" + txtOperationRGStart5.Text.Trim() + "', '" + txtOperationRGStop5.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG6.SelectedValue + "', '6', '" + txtOperationRGStart6.Text.Trim() + "', '" + txtOperationRGStop6.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG7.SelectedValue + "', '7', '" + txtOperationRGStart7.Text.Trim() + "', '" + txtOperationRGStop7.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG8.SelectedValue + "', '8', '" + txtOperationRGStart8.Text.Trim() + "', '" + txtOperationRGStop8.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG9.SelectedValue + "', '9', '" + txtOperationRGStart9.Text.Trim() + "', '" + txtOperationRGStop9.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG10.SelectedValue + "', '10', '" + txtOperationRGStart10.Text.Trim() + "', '" + txtOperationRGStop10.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG11.SelectedValue + "', '11', '" + txtOperationRGStart11.Text.Trim() + "', '" + txtOperationRGStop11.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG12.SelectedValue + "', '12', '" + txtOperationRGStart12.Text.Trim() + "', '" + txtOperationRGStop12.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG13.SelectedValue + "', '13', '" + txtOperationRGStart13.Text.Trim() + "', '" + txtOperationRGStop13.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG14.SelectedValue + "', '14', '" + txtOperationRGStart14.Text.Trim() + "', '" + txtOperationRGStop14.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG15.SelectedValue + "', '15', '" + txtOperationRGStart15.Text.Trim() + "', '" + txtOperationRGStop15.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG16.SelectedValue + "', '16', '" + txtOperationRGStart16.Text.Trim() + "', '" + txtOperationRGStop16.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG17.SelectedValue + "', '17', '" + txtOperationRGStart17.Text.Trim() + "', '" + txtOperationRGStop17.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG18.SelectedValue + "', '18', '" + txtOperationRGStart18.Text.Trim() + "', '" + txtOperationRGStop18.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG19.SelectedValue + "', '19', '" + txtOperationRGStart19.Text.Trim() + "', '" + txtOperationRGStop19.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG20.SelectedValue + "', '20', '" + txtOperationRGStart20.Text.Trim() + "', '" + txtOperationRGStop20.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG21.SelectedValue + "', '21', '" + txtOperationRGStart21.Text.Trim() + "', '" + txtOperationRGStop21.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG22.SelectedValue + "', '22', '" + txtOperationRGStart22.Text.Trim() + "', '" + txtOperationRGStop22.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG23.SelectedValue + "', '23', '" + txtOperationRGStart23.Text.Trim() + "', '" + txtOperationRGStop23.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRG24.SelectedValue + "', '24', '" + txtOperationRGStart24.Text.Trim() + "', '" + txtOperationRGStop24.Text.Trim() + "', 'РГ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ1.SelectedValue + "', '1', '" + txtOperationRJStart1.Text.Trim() + "', '" + txtOperationRJStop1.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ2.SelectedValue + "', '2', '" + txtOperationRJStart2.Text.Trim() + "', '" + txtOperationRJStop2.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ3.SelectedValue + "', '3', '" + txtOperationRJStart3.Text.Trim() + "', '" + txtOperationRJStop3.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ4.SelectedValue + "', '4', '" + txtOperationRJStart4.Text.Trim() + "', '" + txtOperationRJStop4.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ5.SelectedValue + "', '5', '" + txtOperationRJStart5.Text.Trim() + "', '" + txtOperationRJStop5.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ6.SelectedValue + "', '6', '" + txtOperationRJStart6.Text.Trim() + "', '" + txtOperationRJStop6.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ7.SelectedValue + "', '7', '" + txtOperationRJStart7.Text.Trim() + "', '" + txtOperationRJStop7.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ8.SelectedValue + "', '8', '" + txtOperationRJStart8.Text.Trim() + "', '" + txtOperationRJStop8.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ9.SelectedValue + "', '9', '" + txtOperationRJStart9.Text.Trim() + "', '" + txtOperationRJStop9.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ10.SelectedValue + "', '10', '" + txtOperationRJStart10.Text.Trim() + "', '" + txtOperationRJStop10.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ11.SelectedValue + "', '11', '" + txtOperationRJStart11.Text.Trim() + "', '" + txtOperationRJStop11.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ12.SelectedValue + "', '12', '" + txtOperationRJStart12.Text.Trim() + "', '" + txtOperationRJStop12.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ13.SelectedValue + "', '13', '" + txtOperationRJStart13.Text.Trim() + "', '" + txtOperationRJStop13.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ14.SelectedValue + "', '14', '" + txtOperationRJStart14.Text.Trim() + "', '" + txtOperationRJStop14.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ15.SelectedValue + "', '15', '" + txtOperationRJStart15.Text.Trim() + "', '" + txtOperationRJStop15.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ16.SelectedValue + "', '16', '" + txtOperationRJStart16.Text.Trim() + "', '" + txtOperationRJStop16.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ17.SelectedValue + "', '17', '" + txtOperationRJStart17.Text.Trim() + "', '" + txtOperationRJStop17.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ18.SelectedValue + "', '18', '" + txtOperationRJStart18.Text.Trim() + "', '" + txtOperationRJStop18.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ19.SelectedValue + "', '19', '" + txtOperationRJStart19.Text.Trim() + "', '" + txtOperationRJStop19.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ20.SelectedValue + "', '20', '" + txtOperationRJStart20.Text.Trim() + "', '" + txtOperationRJStop20.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ21.SelectedValue + "', '21', '" + txtOperationRJStart21.Text.Trim() + "', '" + txtOperationRJStop21.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ22.SelectedValue + "', '22', '" + txtOperationRJStart22.Text.Trim() + "', '" + txtOperationRJStop22.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ23.SelectedValue + "', '23', '" + txtOperationRJStart23.Text.Trim() + "', '" + txtOperationRJStop23.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ24.SelectedValue + "', '24', '" + txtOperationRJStart24.Text.Trim() + "', '" + txtOperationRJStop24.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ25.SelectedValue + "', '25', '" + txtOperationRJStart25.Text.Trim() + "', '" + txtOperationRJStop25.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ26.SelectedValue + "', '26', '" + txtOperationRJStart26.Text.Trim() + "', '" + txtOperationRJStop26.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ27.SelectedValue + "', '27', '" + txtOperationRJStart27.Text.Trim() + "', '" + txtOperationRJStop27.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ28.SelectedValue + "', '28', '" + txtOperationRJStart28.Text.Trim() + "', '" + txtOperationRJStop28.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ29.SelectedValue + "', '29', '" + txtOperationRJStart29.Text.Trim() + "', '" + txtOperationRJStop29.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ30.SelectedValue + "', '30', '" + txtOperationRJStart30.Text.Trim() + "', '" + txtOperationRJStop30.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ31.SelectedValue + "', '31', '" + txtOperationRJStart31.Text.Trim() + "', '" + txtOperationRJStop31.Text.Trim() + "', 'РЖ'),");
            sqlQuery.Append("('" + IdMixMode + "', '" + cbOperationRJ32.SelectedValue + "', '32', '" + txtOperationRJStart32.Text.Trim() + "', '" + txtOperationRJStop32.Text.Trim() + "', 'РЖ')");

            DBHelper.GetData(sqlQuery.ToString());
        }

        private void UpdateMixModeOperation(string IdMixMode)
        {
            DBHelper.GetData("DELETE FROM [dbo].[tMode_Operation] WHERE [id_mode] = '" + IdMixMode + "'");
            InsertMixModeOperations(IdMixMode);
        }

        private void btnDeleteMixMode_Click(object sender, EventArgs e)
        {
            if (dgvMixMode.SelectedCells.Count < 1)
                return;

            if (MessageBox.Show("Вы уверены что хотите удалить режим смешения?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            int rowIndex = dgvMixMode.SelectedCells[0].RowIndex;
            string idMixMode = dgvMixMode.Rows[rowIndex].Cells["ColumnIdMixMode"].Value.ToString();

            DataTable dtRecipeMixtures = DBHelper.GetData("SELECT [id_mixture_recipe],[number],[id_purpose],[id_mode] FROM [dbo].[tMixtureRecipes] WHERE [id_mode] = '" + idMixMode + "'");
            if (dtRecipeMixtures.Rows.Count > 0)
            {
                MessageBox.Show("Удаление невозможно, так как данный режим изпользуется в рецепте!", "Удаление прервано", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DeleteMixMode(idMixMode);
            SelectMixModes();
        }

        private void txtOperation_TextChanged(object sender, EventArgs e)
        {
            TextBox thisTxt = sender as TextBox;
            int x;
            if (!Int32.TryParse(thisTxt.Text, out x))
            {
                thisTxt.Text = "";
            }
        }
    }
}
