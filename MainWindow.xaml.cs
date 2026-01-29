using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MixRubber2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage BI_SmallSilo = new BitmapImage(new Uri("/img/SmallSilo.png", UriKind.Relative));
        BitmapImage BI_SmallSiloGreen = new BitmapImage(new Uri("/img/SmallSiloGreen.png", UriKind.Relative));
        BitmapImage BI_SiloClosed = new BitmapImage(new Uri("/img/ScalesClose.png", UriKind.Relative));
        BitmapImage BI_SiloClosedGreen = new BitmapImage(new Uri("/img/ScalesCloseGreen.png", UriKind.Relative));
        BitmapImage BI_SiloOpend = new BitmapImage(new Uri("/img/ScalesOpen.png", UriKind.Relative));
        BitmapImage BI_ConteinerOpen = new BitmapImage(new Uri("/img/SiloOpen.png", UriKind.Relative));
        BitmapImage BI_ConteinerClose = new BitmapImage(new Uri("/img/SiloClose.png", UriKind.Relative));
        BitmapImage BI_ConteinerCloseGreen = new BitmapImage(new Uri("/img/SiloCloseGreen.png", UriKind.Relative));
        BitmapImage BI_Float = new BitmapImage(new Uri("/img/Float.png", UriKind.Relative));
        BitmapImage BI_FloatRed = new BitmapImage(new Uri("/img/FloatRed.png", UriKind.Relative));
        BitmapImage BI_Transporter = new BitmapImage(new Uri("/img/Transporter.png", UriKind.Relative));
        BitmapImage BI_TransporterGreen = new BitmapImage(new Uri("/img/TransporterGreen.png", UriKind.Relative));
        BitmapImage BI_Transporter2 = new BitmapImage(new Uri("/img/Transporter2.png", UriKind.Relative));
        BitmapImage BI_Transporter2Green = new BitmapImage(new Uri("/img/Transporter2Green.png", UriKind.Relative));
        BitmapImage BI_OilPump = new BitmapImage(new Uri("/img/Pump.png", UriKind.Relative));
        BitmapImage BI_OilPumpGreen = new BitmapImage(new Uri("/img/PumpGreen.png", UriKind.Relative));
        BitmapImage BI_Screw = new BitmapImage(new Uri("/img/Screw.png", UriKind.Relative));
        BitmapImage BI_ScrewGreen = new BitmapImage(new Uri("/img/ScrewGreen.png", UriKind.Relative));
        BitmapImage BI_Injector = new BitmapImage(new Uri("/img/SiloOil.png", UriKind.Relative));
        BitmapImage BI_InjectorFull = new BitmapImage(new Uri("/img/SiloOilFull.png", UriKind.Relative));

        Tag FirstSeriesTag = OPCDA.AllTags.Where(x => x.Name == "TUnloading_WithCorrect").First();
        Tag SecondSeriesTag = OPCDA.AllTags.Where(x => x.Name == "TDanger_WithCorrect").First();

        CancellationTokenSource cts = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
            InitializeMyComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            chart.ChartAreas.Add("Main");
            chart.ChartAreas["Main"].AxisY.Maximum = 200;
            chart.ChartAreas["Main"].AxisY.Minimum = 0;
            chart.ChartAreas["Main"].AxisY.IsLabelAutoFit = true;
            chart.ChartAreas["Main"].AxisX.LabelStyle.Enabled = false;
            chart.ChartAreas["Main"].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart.ChartAreas["Main"].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

            chart.Series.Add("FirstSeries");
            chart.Series["FirstSeries"].Color = System.Drawing.Color.Red;
            chart.Series["FirstSeries"].ChartType = SeriesChartType.FastLine;
            chart.Series["FirstSeries"].LegendText = "Температура";
            chart.Series["FirstSeries"].Points.Add(0);

            chart.Series.Add("SecondSeries");
            chart.Series["SecondSeries"].Color = System.Drawing.Color.Blue;
            chart.Series["SecondSeries"].ChartType = SeriesChartType.FastLine;
            chart.Series["SecondSeries"].LegendText = "Температура";
            chart.Series["SecondSeries"].Points.Add(0);

            //не получается обратится из другого потока
            //Timer chartTimer = new Timer(Chart_TimerCallback, null, 1000, 1000);

            #region Add Tag

            //ScalesC C
            OPCDA.AllTags.Where(x => x.Name == "BatcherC1").First().ValueChanged += ScalesC_Batcher1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherC2").First().ValueChanged += ScalesC_Batcher2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherC3").First().ValueChanged += ScalesC_Batcher3_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherC4").First().ValueChanged += ScalesC_Batcher4_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherC5").First().ValueChanged += ScalesC_Batcher5_ValueChanged;
            
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_IsClosed").First().ValueChanged += ScalesC_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_IsOpened").First().ValueChanged += ScalesC_IsOpenedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_Weighing").First().ValueChanged += ScalesC_WeighingChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesC_CurNeed").First().ValueChanged += ScalesC_CurNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_FullNeed").First().ValueChanged += ScalesC_FullNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_CurReal").First().ValueChanged += ScalesC_CurReal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_FullReal").First().ValueChanged += ScalesC_FullReal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesC_Initial").First().ValueChanged += ScalesC_Initial_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_IsBusy").First().ValueChanged += ScalesC_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_WeightNotNormal").First().ValueChanged += ScalesC_WeightNotNotmal_ValueChanged;

            //Conteiner C
            OPCDA.AllTags.Where(x => x.Name == "ContainerC_IsClosed").First().ValueChanged += ContainerC_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ContainerC_IsOpened").First().ValueChanged += ContainerC_IsOpenedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ContainerC_Weighing").First().ValueChanged += ContainerC_WeighingChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScrewTC_Working").First().ValueChanged += ScrewTC_WorkingChanged;
            
            //Scales J
            OPCDA.AllTags.Where(x => x.Name == "BatcherJ1").First().ValueChanged += ScalesJ_Batcher1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherJ2").First().ValueChanged += ScalesJ_Batcher2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherJ3").First().ValueChanged += ScalesJ_Batcher3_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherJ4").First().ValueChanged += ScalesJ_Batcher4_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_IsClosed").First().ValueChanged += ScalesJ_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_Weighing").First().ValueChanged += ScalesJ_WeighingChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_CurNeed").First().ValueChanged += ScalesJ_CurNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_FullNeed").First().ValueChanged += ScalesJ_FullNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_CurReal").First().ValueChanged += ScalesJ_CurReal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_FullReal").First().ValueChanged += ScalesJ_FullReal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_Initial").First().ValueChanged += ScalesJ_Initial_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_IsBusy").First().ValueChanged += ScalesJ_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJ_WeightNotNormal").First().ValueChanged += ScalesJ_WeightNotNotmal_ValueChanged;

            //Scales D
            OPCDA.AllTags.Where(x => x.Name == "BatcherD1").First().ValueChanged += ScalesD_Batcher1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherD2").First().ValueChanged += ScalesD_Batcher2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherD3").First().ValueChanged += ScalesD_Batcher3_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherD4").First().ValueChanged += ScalesD_Batcher4_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesD_IsClosed").First().ValueChanged += ScalesD_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesD_Weighing").First().ValueChanged += ScalesD_WeighingChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesD_CurNeed").First().ValueChanged += ScalesD_CurNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesD_FullNeed").First().ValueChanged += ScalesD_FullNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesD_CurReal").First().ValueChanged += ScalesD_CurReal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesD_FullReal").First().ValueChanged += ScalesD_FullReal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesD_Initial").First().ValueChanged += ScalesD_Initial_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesD_IsBusy").First().ValueChanged += ScalesD_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesD_WeightNotNormal").First().ValueChanged += ScalesD_WeightNotNotmal_ValueChanged;

            //Scales E
            OPCDA.AllTags.Where(x => x.Name == "BatcherE1").First().ValueChanged += ScalesE_Batcher1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherE2").First().ValueChanged += ScalesE_Batcher2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherE3").First().ValueChanged += ScalesE_Batcher3_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesE_IsClosed").First().ValueChanged += ScalesE_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesE_Weighing").First().ValueChanged += ScalesE_WeighingChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesE_CurNeed").First().ValueChanged += ScalesE_CurNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesE_FullNeed").First().ValueChanged += ScalesE_FullNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesE_CurReal").First().ValueChanged += ScalesE_CurReal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesE_FullReal").First().ValueChanged += ScalesE_FullReal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesE_Initial").First().ValueChanged += ScalesE_Initial_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesE_IsBusy").First().ValueChanged += ScalesE_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesE_WeightNotNormal").First().ValueChanged += ScalesE_WeightNotNotmal_ValueChanged;

            //Scales Sh
            OPCDA.AllTags.Where(x => x.Name == "BatcherSh1").First().ValueChanged += ScalesSh_Batcher1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherSh2").First().ValueChanged += ScalesSh_Batcher2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherSh3").First().ValueChanged += ScalesSh_Batcher3_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_IsClosed").First().ValueChanged += ScalesSh_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_Weighing").First().ValueChanged += ScalesSh_WeighingChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_CurNeed").First().ValueChanged += ScalesSh_CurNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_FullNeed").First().ValueChanged += ScalesSh_FullNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_CurReal").First().ValueChanged += ScalesSh_CurReal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_FullReal").First().ValueChanged += ScalesSh_FullReal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_Initial").First().ValueChanged += ScalesSh_Initial_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_IsBusy").First().ValueChanged += ScalesSh_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesSh_WeightNotNormal").First().ValueChanged += ScalesSh_WeightNotNotmal_ValueChanged;

            //Scales Ju
            OPCDA.AllTags.Where(x => x.Name == "BatcherJu1").First().ValueChanged += ScalesJu_Batcher1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherJu2").First().ValueChanged += ScalesJu_Batcher2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherJu3").First().ValueChanged += ScalesJu_Batcher3_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_IsClosed").First().ValueChanged += ScalesJu_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_Weighing").First().ValueChanged += ScalesJu_WeighingChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_CurNeed").First().ValueChanged += ScalesJu_CurNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_FullNeed").First().ValueChanged += ScalesJu_FullNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_CurReal").First().ValueChanged += ScalesJu_CurReal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_FullReal").First().ValueChanged += ScalesJu_FullReal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_Initial").First().ValueChanged += ScalesJu_Initial_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_IsBusy").First().ValueChanged += ScalesJu_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesJu_WeightNotNormal").First().ValueChanged += ScalesJu_WeightNotNotmal_ValueChanged;

            //Scales Y
            OPCDA.AllTags.Where(x => x.Name == "BatcherY1").First().ValueChanged += ScalesY_Batcher1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherY2").First().ValueChanged += ScalesY_Batcher2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BatcherY3").First().ValueChanged += ScalesY_Batcher3_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesY_IsClosed").First().ValueChanged += ScalesY_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesY_Weighing").First().ValueChanged += ScalesY_WeighingChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesY_CurNeed").First().ValueChanged += ScalesY_CurNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesY_FullNeed").First().ValueChanged += ScalesY_FullNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesY_CurReal").First().ValueChanged += ScalesY_CurReal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesY_FullReal").First().ValueChanged += ScalesY_FullReal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesY_Initial").First().ValueChanged += ScalesY_Initial_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesY_IsBusy").First().ValueChanged += ScalesY_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesY_WeightNotNormal").First().ValueChanged += ScalesY_WeightNotNotmal_ValueChanged;

            //Scales K
            OPCDA.AllTags.Where(x => x.Name == "ScalesK_FullNeed").First().ValueChanged += ScalesK_FullNeed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesK_FullReal").First().ValueChanged += ScalesK_FullReal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesK_CurReal").First().ValueChanged += ScalesK_CurReal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ScalesK_IsBusy").First().ValueChanged += ScalesK_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesK_WeightNotNormal").First().ValueChanged += ScalesK_WeightNotNotmal_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesK_Weighing").First().ValueChanged += ScalesK_Weighing_ValueChanged;

            //Mix Rubber
            OPCDA.AllTags.Where(x => x.Name == "MR_PressureOff").First().ValueChanged += MR_PressureOff_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_UpperPressUp").First().ValueChanged += MR_UpperPressUp_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_UpperPressDown").First().ValueChanged += MR_UpperPressDown_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_FlapValveClosed").First().ValueChanged += MR_FlapValveClosed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_PinClosed").First().ValueChanged += MR_Pin_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "HighTemperature").First().ValueChanged += MR_HighTemperature_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "CriticalTemperature").First().ValueChanged += MR_CriticalTemperature_ValueChanged;

            //Transports
            OPCDA.AllTags.Where(x => x.Name == "TK_Working").First().ValueChanged += TK_Working_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "TL1_Working").First().ValueChanged += TL1_Working_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "TL2_Working").First().ValueChanged += TL2_Working_ValueChanged;

            //Oil Pump
            OPCDA.AllTags.Where(x => x.Name == "OilPump_Working").First().ValueChanged += OilPump_Working_ValueChanged;

            //Injector
            OPCDA.AllTags.Where(x => x.Name == "Injector_Weighing").First().ValueChanged += Injector_WeighingChanged;

            OPCDA.AllTags.Where(x => x.Name == "Btn_StopProcess").First().ValueChanged += Btn_StopProcess_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ManualMode").First().ValueChanged += ManualMode_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "BagFilter_Blow").First().ValueChanged += BagFilter_Blow_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "DustCollectorOn").First().ValueChanged += DustCollectorOn_ValueChanged;
            //!!!не используется!!! OPCDA.AllTags.Where(x => x.Name == "ScrewPrepareOff").First().ValueChanged += ScrewPrepareOff_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "Not24V").First().ValueChanged += Not24V_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "RecipeNumber").First().ValueChanged += RecipeNumber_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "Resipe_IsNot").First().ValueChanged += Resipe_IsNot_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "EmergencyButton_Pushed").First().ValueChanged += EmergencyButton_Pushed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "StopButton_Pushed").First().ValueChanged += StopButton_Pushed_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "Batchers_Ready").First().ValueChanged += Batchers_Ready_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "Filling_OnStart").First().ValueChanged += Filling_OnStart_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "LastFilling_ToPlan").First().ValueChanged += LastFilling_ToPlan_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_Mix_IsFobidden").First().ValueChanged += MR_Mix_IsFobidden_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_Load_IsFobidden").First().ValueChanged += MR_Load_IsFobidden_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "TestMode_On").First().ValueChanged += TestMode_On_ValueChanged;

            //Temperature (пока нет необходимости)
            /*
            OPCDA.AllTags.Where(x => x.Name == "WaterT1").First().ValueChanged += WaterT1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "WaterT2").First().ValueChanged += WaterT2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "WaterT3").First().ValueChanged += WaterT3_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "WaterT4").First().ValueChanged += WaterT4_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "WaterT5").First().ValueChanged += WaterT5_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "WaterRotor1").First().ValueChanged += WaterRotor1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "WaterRotor2").First().ValueChanged += WaterRotor2_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "Batchers_Start1").First().ValueChanged += Batchers_Start1_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "Batchers_Start2").First().ValueChanged += Batchers_Start2_ValueChanged;
            */

            //String
            OPCDA.AllTags.Where(x => x.Name == "OperatorFIO").First().ValueChanged += OperatorFIO_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "RecipeName").First().ValueChanged += RecipeName_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ModeName").First().ValueChanged += ModeName_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ShiftName").First().ValueChanged += ShiftName_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "NumberLoad").First().ValueChanged += NumberLoad_ValueChanged;

            //Info
            OPCDA.AllTags.Where(x => x.Name == "ModeNumber").First().ValueChanged += ModeNumber_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "TUnloading").First().ValueChanged += TUnloading_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "TDanger").First().ValueChanged += TDanger_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "FullTimeMix").First().ValueChanged += FullTimeMix_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "TimeMixUnderPress").First().ValueChanged += TimeMixUnderPress_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "NumLoading_Shift").First().ValueChanged += NumLoading_Shift_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "NumLoading_Recept").First().ValueChanged += NumLoading_Recept_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "Plan_Loadings").First().ValueChanged += Plan_Loadings_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "TCorrect").First().ValueChanged += TCorrect_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "TUnloadSensor").First().ValueChanged += TUnloadSensor_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_TimerRG").First().ValueChanged += MR_TimerRG_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_TimerRJ").First().ValueChanged += MR_TimerRJ_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_Power").First().ValueChanged += MR_Power_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_TSensorUp").First().ValueChanged += MR_TSensorUp_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "MR_Mix_IsGoing").First().ValueChanged += MR_Mix_IsGoing_ValueChanged;

            #endregion

            OPCDA.ConnectionOPC();


            //Link
            Task task = new Task(async () =>
            {
                Ping ping = new Ping();
                while (!cts.Token.IsCancellationRequested)  // проверяем наличие сигнала отмены задачи
                {
                    var res = await ping.SendPingAsync(Link.ipScalesC, 1000).ConfigureAwait(false);
                    Link.ScalesC = res.Status;
                    Console.WriteLine($"Статус {res.Status}");
                    Thread.Sleep(200);
                }
                Console.WriteLine("Операция прервана");
                return;
            }, cts.Token);
            task.Start();

            //Link.ScalesCLinkChanged += Link_ScalesCLinkChanged;
        }
        /*
        private void Link_ScalesCLinkChanged(IPStatus obj)
        {
            if (lblScalesCLink.InvokeRequired)
            if (obj == IPStatus.Success)
                lblScalesCLink.Background = Brushes.LightGreen;
            else
                lblScalesCLink.Background = Brushes.Red;
        }*/

        private void Chart_TimerCallback(object state)
        {
            chart.Series["FirstSeries"].Points.Add(Convert.ToDouble(FirstSeriesTag.Value));
            chart.Series["SecondSeries"].Points.Add(Convert.ToDouble(SecondSeriesTag.Value));
        }

        private void InitializeMyComponent()
        {
            imgScalesCOverWeight.Visibility = Visibility.Hidden;
            imgScalesCBusy.Visibility = Visibility.Hidden;
            imgScalesCInitial.Visibility = Visibility.Hidden;

            imgScalesJOverWeight.Visibility = Visibility.Hidden;
            imgScalesJBusy.Visibility = Visibility.Hidden;
            imgScalesJInitial.Visibility = Visibility.Hidden;

            imgScalesDOverWeight.Visibility = Visibility.Hidden;
            imgScalesDBusy.Visibility = Visibility.Hidden;
            imgScalesDInitial.Visibility = Visibility.Hidden;

            imgScalesEOverWeight.Visibility = Visibility.Hidden;
            imgScalesEBusy.Visibility = Visibility.Hidden;
            imgScalesEInitial.Visibility = Visibility.Hidden;

            imgScalesShOverWeight.Visibility = Visibility.Hidden;
            imgScalesShBusy.Visibility = Visibility.Hidden;
            imgScalesShInitial.Visibility = Visibility.Hidden;

            imgScalesJuOverWeight.Visibility = Visibility.Hidden;
            imgScalesJuBusy.Visibility = Visibility.Hidden;
            imgScalesJuInitial.Visibility = Visibility.Hidden;

            imgScalesYOverWeight.Visibility = Visibility.Hidden;
            imgScalesYBusy.Visibility = Visibility.Hidden;
            imgScalesYInitial.Visibility = Visibility.Hidden;

            imgScalesKOverWeight.Visibility = Visibility.Hidden;
            imgScalesKBusy.Visibility = Visibility.Hidden;

            lblBatchersReady.Visibility = Visibility.Hidden;
            lblLoadReady.Visibility = Visibility.Hidden;
            lblLastLoad.Visibility = Visibility.Hidden;
            lblTestMode.Visibility = Visibility.Hidden;
            lblMixIsGoing.Visibility = Visibility.Hidden;
        }

        private void LblNameScalesJ_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("norm");
        }

        #region ScalesC

        private void ScalesC_WeightNotNotmal_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCOverWeight.Visibility = Visibility.Visible;
            else
                imgScalesCOverWeight.Visibility = Visibility.Hidden;
        }

        private void ScalesC_IsBusy_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCBusy.Visibility = Visibility.Visible;
            else
                imgScalesCBusy.Visibility = Visibility.Hidden;
        }

        private void ScalesC_WeighingChanged(object obj)
        {
            bool? isClose = (bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesC_IsClosed").First().Value;
            if ((bool)obj)
            {
                if (isClose == true)
                    imgScalesCSilo.Source = BI_SiloClosedGreen;
            }    
            else
            {
                if (isClose == true)
                    imgScalesCSilo.Source = BI_SiloClosed;
            }
        }

        private void ScalesC_IsOpenedChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCSilo.Source = BI_SiloOpend;
        }

        private void ScalesC_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if ((bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesC_Weighing").First().Value == true)
                    imgScalesCSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesCSilo.Source = BI_SiloClosed;
            }
        }

        private void ScalesC_Initial_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCInitial.Visibility = Visibility.Visible;
            else
                imgScalesCInitial.Visibility = Visibility.Hidden;
        }

        private void ScalesC_FullReal_ValueChanged(object obj)
        {
            txtScalesCFullReal.Text = obj.ToString();
            Tag fullNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesC_FullNeed").First();
            if (fullNeed.Value != null)
                txtScalesCFullDif.Text = Math.Round((float)fullNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesC_CurReal_ValueChanged(object obj)
        {
            txtScalesCCurReal.Text = obj.ToString();
            Tag curNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesC_CurNeed").First();
            if (curNeed.Value != null)
                txtScalesCCurDif.Text = Math.Round((float)curNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesC_FullNeed_ValueChanged(object obj)
        {
            txtScalesCFullNeed.Text = obj.ToString();
            Tag fullReal = OPCDA.AllTags.Where(x => x.Name == "ScalesC_FullReal").First();
            if (fullReal.Value != null)
                txtScalesCFullDif.Text = Math.Round((float)obj - (float)fullReal.Value, 1).ToString();
        }

        private void ScalesC_CurNeed_ValueChanged(object obj)
        {
            txtScalesCCurNeed.Text = obj.ToString();
            Tag curReal = OPCDA.AllTags.Where(x => x.Name == "ScalesC_CurReal").First();
            if (curReal.Value != null)
                txtScalesCCurDif.Text = Math.Round((float)obj - (float)curReal.Value, 1).ToString();
        }

        private void ScalesC_Batcher5_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCBatcher5.Source = BI_SmallSiloGreen;
            else
                imgScalesCBatcher5.Source = BI_SmallSilo;
        }

        private void ScalesC_Batcher4_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCBatcher4.Source = BI_SmallSiloGreen;
            else
                imgScalesCBatcher4.Source = BI_SmallSilo;
        }

        private void ScalesC_Batcher3_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCBatcher3.Source = BI_SmallSiloGreen;
            else
                imgScalesCBatcher3.Source = BI_SmallSilo;
        }

        private void ScalesC_Batcher2_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCBatcher2.Source = BI_SmallSiloGreen;
            else
                imgScalesCBatcher2.Source = BI_SmallSilo;
        }

        private void ScalesC_Batcher1_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCBatcher1.Source = BI_SmallSiloGreen;
            else
                imgScalesCBatcher1.Source = BI_SmallSilo;
        }

        #endregion

        #region ConteinerC
        private void ContainerC_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if ((bool?)OPCDA.AllTags.Where(x => x.Name == "ContainerC_Weighing").First().Value == true)
                    imgConteinerC.Source = BI_ConteinerCloseGreen;
                else
                    imgConteinerC.Source = BI_ConteinerClose;
            }
        }

        private void ContainerC_IsOpenedChanged(object obj)
        {
            if ((bool)obj)
                imgConteinerC.Source = BI_ConteinerOpen;
        }

        private void ContainerC_WeighingChanged(object obj)
        {
            bool? isClose = (bool?)OPCDA.AllTags.Where(x => x.Name == "ContainerC_IsClosed").First().Value;
            if ((bool)obj)
            {
                if (isClose == true)
                    imgConteinerC.Source = BI_ConteinerCloseGreen;
            }
            else
            {
                if (isClose == true)
                    imgConteinerC.Source = BI_ConteinerClose;
            }
        }

        private void ScrewTC_WorkingChanged(object obj)
        {
            if ((bool)obj)
                imgScrew.Source = BI_ScrewGreen;
            else
                imgScrew.Source = BI_Screw;
        }
        #endregion

        #region Scales J

        private void ScalesJ_WeightNotNotmal_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJOverWeight.Visibility = Visibility.Visible;
            else
                imgScalesJOverWeight.Visibility = Visibility.Hidden;
        }

        private void ScalesJ_IsBusy_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJBusy.Visibility = Visibility.Visible;
            else
                imgScalesJBusy.Visibility = Visibility.Hidden;
        }

        private void ScalesJ_Initial_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJInitial.Visibility = Visibility.Visible;
            else
                imgScalesJInitial.Visibility = Visibility.Hidden;
        }

        private void ScalesJ_FullReal_ValueChanged(object obj)
        {
            txtScalesJFullReal.Text = obj.ToString();
            Tag fullNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesJ_FullNeed").First();
            if (OPCDA.AllTags.Where(x => x.Name == "ScalesJ_FullNeed").First().Value != null)
                txtScalesJFullDif.Text = Math.Round((float)fullNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesJ_CurReal_ValueChanged(object obj)
        {
            txtScalesJCurReal.Text = obj.ToString();
            Tag curNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesJ_CurNeed").First();
            if (curNeed.Value != null)
                txtScalesJCurDif.Text = Math.Round((float)curNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesJ_FullNeed_ValueChanged(object obj)
        {
            txtScalesJFullNeed.Text = obj.ToString();
            Tag fullReal = OPCDA.AllTags.Where(x => x.Name == "ScalesJ_FullReal").First();
            if (fullReal.Value != null)
                txtScalesJFullDif.Text = Math.Round((float)obj - (float)fullReal.Value, 1).ToString();
        }

        private void ScalesJ_CurNeed_ValueChanged(object obj)
        {
            txtScalesJCurNeed.Text = obj.ToString();
            Tag curReal = OPCDA.AllTags.Where(x => x.Name == "ScalesJ_CurReal").First();
            if (curReal.Value != null)
                txtScalesJCurDif.Text = Math.Round((float)obj - (float)curReal.Value, 1).ToString();
        }

        private void ScalesJ_WeighingChanged(object obj)
        {
            bool? isClose = (bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesJ_IsClosed").First().Value;
            if ((bool)obj)
            {
                if (isClose == true)
                    imgScalesJSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (isClose == true)
                    imgScalesJSilo.Source = BI_SiloClosed;
            }
        }

        private void ScalesJ_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if ((bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesJ_Weighing").First().Value == true)
                    imgScalesJSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesJSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesJSilo.Source = BI_SiloOpend;
            }
        }

        private void ScalesJ_Batcher4_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJBatcher4.Source = BI_SmallSiloGreen;
            else
                imgScalesJBatcher4.Source = BI_SmallSilo;
        }

        private void ScalesJ_Batcher3_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJBatcher3.Source = BI_SmallSiloGreen;
            else
                imgScalesJBatcher3.Source = BI_SmallSilo;
        }

        private void ScalesJ_Batcher2_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJBatcher2.Source = BI_SmallSiloGreen;
            else
                imgScalesJBatcher2.Source = BI_SmallSilo;
        }

        private void ScalesJ_Batcher1_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJBatcher1.Source = BI_SmallSiloGreen;
            else
                imgScalesJBatcher1.Source = BI_SmallSilo;
        }

        #endregion

        #region Scales D

        private void ScalesD_WeightNotNotmal_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesDOverWeight.Visibility = Visibility.Visible;
            else
                imgScalesDOverWeight.Visibility = Visibility.Hidden;
        }

        private void ScalesD_IsBusy_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesDBusy.Visibility = Visibility.Visible;
            else
                imgScalesDBusy.Visibility = Visibility.Hidden;
        }

        private void ScalesD_Initial_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesDInitial.Visibility = Visibility.Visible;
            else
                imgScalesDInitial.Visibility = Visibility.Hidden;
        }

        private void ScalesD_FullReal_ValueChanged(object obj)
        {
            txtScalesDFullReal.Text = obj.ToString();
            Tag fullNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesD_FullNeed").First();
            if (fullNeed.Value != null)
                txtScalesDFullDif.Text = Math.Round((float)fullNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesD_CurReal_ValueChanged(object obj)
        {
            txtScalesDCurReal.Text = obj.ToString();
            Tag curNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesD_CurNeed").First();
            if (curNeed.Value != null)
                txtScalesDCurDif.Text = Math.Round((float)curNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesD_FullNeed_ValueChanged(object obj)
        {
            txtScalesDFullNeed.Text = obj.ToString();
            Tag fullReal = OPCDA.AllTags.Where(x => x.Name == "ScalesD_FullReal").First();
            if (fullReal.Value != null)
                txtScalesDFullDif.Text = Math.Round((float)obj - (float)fullReal.Value, 1).ToString();
        }

        private void ScalesD_CurNeed_ValueChanged(object obj)
        {
            txtScalesDCurNeed.Text = obj.ToString();
            Tag curReal = OPCDA.AllTags.Where(x => x.Name == "ScalesD_CurReal").First();
            if (curReal.Value != null)
                txtScalesDCurDif.Text = Math.Round((float)obj - (float)curReal.Value, 1).ToString();
        }

        private void ScalesD_WeighingChanged(object obj)
        {
            bool? isClosed = (bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesD_IsClosed").First().Value;
            if ((bool)obj)
            {
                if (isClosed == true)
                    imgScalesDSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (isClosed == true)
                    imgScalesDSilo.Source = BI_SiloClosed;
            }
        }

        private void ScalesD_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if ((bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesD_Weighing").First().Value == true)
                    imgScalesDSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesDSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesDSilo.Source = BI_SiloOpend;
            }
        }

        private void ScalesD_Batcher4_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesDBatcher4.Source = BI_SmallSiloGreen;
            else
                imgScalesDBatcher4.Source = BI_SmallSilo;
        }

        private void ScalesD_Batcher3_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesDBatcher3.Source = BI_SmallSiloGreen;
            else
                imgScalesDBatcher3.Source = BI_SmallSilo;
        }

        private void ScalesD_Batcher2_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesDBatcher2.Source = BI_SmallSiloGreen;
            else
                imgScalesDBatcher2.Source = BI_SmallSilo;
        }

        private void ScalesD_Batcher1_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesDBatcher1.Source = BI_SmallSiloGreen;
            else
                imgScalesDBatcher1.Source = BI_SmallSilo;
        }

        #endregion

        #region Scales E

        private void ScalesE_WeightNotNotmal_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesEOverWeight.Visibility = Visibility.Visible;
            else
                imgScalesEOverWeight.Visibility = Visibility.Hidden;
        }

        private void ScalesE_IsBusy_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesEBusy.Visibility = Visibility.Visible;
            else
                imgScalesEBusy.Visibility = Visibility.Hidden;
        }

        private void ScalesE_Initial_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesEInitial.Visibility = Visibility.Visible;
            else
                imgScalesEInitial.Visibility = Visibility.Hidden;
        }

        private void ScalesE_FullReal_ValueChanged(object obj)
        {
            txtScalesEFullReal.Text = obj.ToString();
            Tag fullNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesE_FullNeed").First();
            if (fullNeed.Value != null)
                txtScalesEFullDif.Text = Math.Round((float)fullNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesE_CurReal_ValueChanged(object obj)
        {
            txtScalesECurReal.Text = obj.ToString();
            Tag curNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesE_CurNeed").First();
            if (curNeed.Value != null)
                txtScalesECurDif.Text = Math.Round((float)curNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesE_FullNeed_ValueChanged(object obj)
        {
            txtScalesEFullNeed.Text = obj.ToString();
            Tag fullReal = OPCDA.AllTags.Where(x => x.Name == "ScalesE_FullReal").First();
            if (fullReal.Value != null)
                txtScalesEFullDif.Text = Math.Round((float)obj - (float)fullReal.Value, 1).ToString();
        }

        private void ScalesE_CurNeed_ValueChanged(object obj)
        {
            txtScalesECurNeed.Text = obj.ToString();
            Tag curReal = OPCDA.AllTags.Where(x => x.Name == "ScalesE_CurReal").First();
            if (curReal.Value != null)
                txtScalesECurDif.Text = Math.Round((float)obj - (float)curReal.Value, 1).ToString();
        }

        private void ScalesE_WeighingChanged(object obj)
        {
            bool? isClosed = (bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesE_IsClosed").First().Value;
            if ((bool)obj)
            {
                if (isClosed == true)
                    imgScalesESilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (isClosed == true)
                    imgScalesESilo.Source = BI_SiloClosed;
            }
        }

        private void ScalesE_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if ((bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesE_Weighing").First().Value == true)
                    imgScalesESilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesESilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesESilo.Source = BI_SiloOpend;
            }
        }

        private void ScalesE_Batcher3_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesEBatcher3.Source = BI_SmallSiloGreen;
            else
                imgScalesEBatcher3.Source = BI_SmallSilo;
        }

        private void ScalesE_Batcher2_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesEBatcher2.Source = BI_SmallSiloGreen;
            else
                imgScalesEBatcher2.Source = BI_SmallSilo;
        }

        private void ScalesE_Batcher1_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesEBatcher1.Source = BI_SmallSiloGreen;
            else
                imgScalesEBatcher1.Source = BI_SmallSilo;
        }

        #endregion

        #region Scales Sh

        private void ScalesSh_WeightNotNotmal_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesShOverWeight.Visibility = Visibility.Visible;
            else
                imgScalesShOverWeight.Visibility = Visibility.Hidden;
        }

        private void ScalesSh_IsBusy_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesShBusy.Visibility = Visibility.Visible;
            else
                imgScalesShBusy.Visibility = Visibility.Hidden;
        }

        private void ScalesSh_Initial_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesShInitial.Visibility = Visibility.Visible;
            else
                imgScalesShInitial.Visibility = Visibility.Hidden;
        }

        private void ScalesSh_FullReal_ValueChanged(object obj)
        {
            txtScalesShFullReal.Text = obj.ToString();
            Tag fullNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesSh_FullNeed").First();
            if (fullNeed.Value != null)
                txtScalesShFullDif.Text = Math.Round((float)fullNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesSh_CurReal_ValueChanged(object obj)
        {
            txtScalesShCurReal.Text = obj.ToString();
            Tag curNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesSh_CurNeed").First();
            if (curNeed.Value != null)
                txtScalesShCurDif.Text = Math.Round((float)curNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesSh_FullNeed_ValueChanged(object obj)
        {
            txtScalesShFullNeed.Text = obj.ToString();
            Tag fullReal = OPCDA.AllTags.Where(x => x.Name == "ScalesSh_FullReal").First();
            if (fullReal.Value != null)
                txtScalesShFullDif.Text = Math.Round((float)obj - (float)fullReal.Value, 1).ToString();
        }

        private void ScalesSh_CurNeed_ValueChanged(object obj)
        {
            txtScalesShCurNeed.Text = obj.ToString();
            Tag curReal = OPCDA.AllTags.Where(x => x.Name == "ScalesSh_CurReal").First();
            if (curReal.Value != null)
                txtScalesShCurDif.Text = Math.Round((float)obj - (float)curReal.Value, 1).ToString();
        }

        private void ScalesSh_WeighingChanged(object obj)
        {
            bool? isClosed = (bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesSh_IsClosed").First().Value;
            if ((bool)obj)
            {
                if (isClosed == true)
                    imgScalesShSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (isClosed == true)
                    imgScalesShSilo.Source = BI_SiloClosed;
            }
        }

        private void ScalesSh_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if ((bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesSh_Weighing").First().Value == true)
                    imgScalesShSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesShSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesShSilo.Source = BI_SiloOpend;
            }
        }

        private void ScalesSh_Batcher3_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesShBatcher3.Source = BI_SmallSiloGreen;
            else
                imgScalesShBatcher3.Source = BI_SmallSilo;
        }

        private void ScalesSh_Batcher2_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesShBatcher2.Source = BI_SmallSiloGreen;
            else
                imgScalesShBatcher2.Source = BI_SmallSilo;
        }

        private void ScalesSh_Batcher1_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesShBatcher1.Source = BI_SmallSiloGreen;
            else
                imgScalesShBatcher1.Source = BI_SmallSilo;
        }

        #endregion

        #region Scales Ju

        private void ScalesJu_WeightNotNotmal_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJuOverWeight.Visibility = Visibility.Visible;
            else
                imgScalesJuOverWeight.Visibility = Visibility.Hidden;
        }

        private void ScalesJu_IsBusy_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJuBusy.Visibility = Visibility.Visible;
            else
                imgScalesJuBusy.Visibility = Visibility.Hidden;
        }

        private void ScalesJu_Initial_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJuInitial.Visibility = Visibility.Visible;
            else
                imgScalesJuInitial.Visibility = Visibility.Hidden;
        }

        private void ScalesJu_FullReal_ValueChanged(object obj)
        {
            txtScalesJuFullReal.Text = obj.ToString();
            Tag fullNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesJu_FullNeed").First();
            if (fullNeed.Value != null)
                txtScalesJuFullDif.Text = Math.Round((float)fullNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesJu_CurReal_ValueChanged(object obj)
        {
            txtScalesJuCurReal.Text = obj.ToString();
            Tag curNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesJu_CurNeed").First();
            if (curNeed.Value != null)
                txtScalesJuCurDif.Text = Math.Round((float)curNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesJu_FullNeed_ValueChanged(object obj)
        {
            txtScalesJuFullNeed.Text = obj.ToString();
            Tag fullReal = OPCDA.AllTags.Where(x => x.Name == "ScalesJu_FullReal").First();
            if (fullReal.Value != null)
                txtScalesJuFullDif.Text = Math.Round((float)obj - (float)fullReal.Value, 1).ToString();
        }

        private void ScalesJu_CurNeed_ValueChanged(object obj)
        {
            txtScalesJuCurNeed.Text = obj.ToString();
            Tag curReal = OPCDA.AllTags.Where(x => x.Name == "ScalesJu_CurReal").First();
            if (curReal.Value != null)
                txtScalesJuCurDif.Text = Math.Round((float)obj - (float)curReal.Value, 1).ToString();
        }

        private void ScalesJu_WeighingChanged(object obj)
        {
            bool? isClosed = (bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesJu_IsClosed").First().Value;
            if ((bool)obj)
            {
                if (isClosed == true)
                    imgScalesJuSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (isClosed == true)
                    imgScalesJuSilo.Source = BI_SiloClosed;
            }
        }

        private void ScalesJu_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if ((bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesJu_Weighing").First().Value == true)
                    imgScalesJuSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesJuSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesJuSilo.Source = BI_SiloOpend;
            }
        }

        private void ScalesJu_Batcher3_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJuBatcher3.Source = BI_SmallSiloGreen;
            else
                imgScalesJuBatcher3.Source = BI_SmallSilo;
        }

        private void ScalesJu_Batcher2_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJuBatcher2.Source = BI_SmallSiloGreen;
            else
                imgScalesJuBatcher2.Source = BI_SmallSilo;
        }

        private void ScalesJu_Batcher1_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesJuBatcher1.Source = BI_SmallSiloGreen;
            else
                imgScalesJuBatcher1.Source = BI_SmallSilo;
        }

        #endregion

        #region Scales Y

        private void ScalesY_WeightNotNotmal_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesYOverWeight.Visibility = Visibility.Visible;
            else
                imgScalesYOverWeight.Visibility = Visibility.Hidden;
        }

        private void ScalesY_IsBusy_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesYBusy.Visibility = Visibility.Visible;
            else
                imgScalesYBusy.Visibility = Visibility.Hidden;
        }

        private void ScalesY_Initial_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesYInitial.Visibility = Visibility.Visible;
            else
                imgScalesYInitial.Visibility = Visibility.Hidden;
        }

        private void ScalesY_FullReal_ValueChanged(object obj)
        {
            txtScalesYFullReal.Text = obj.ToString();
            Tag fullNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesY_FullNeed").First();
            if (fullNeed.Value != null)
                txtScalesYFullDif.Text = Math.Round((float)fullNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesY_CurReal_ValueChanged(object obj)
        {
            txtScalesYCurReal.Text = obj.ToString();
            Tag curNeed = OPCDA.AllTags.Where(x => x.Name == "ScalesY_CurNeed").First();
            if (curNeed.Value != null)
                txtScalesYCurDif.Text = Math.Round((float)curNeed.Value - (float)obj, 1).ToString();
        }

        private void ScalesY_FullNeed_ValueChanged(object obj)
        {
            txtScalesYFullNeed.Text = obj.ToString();
            Tag fullReal = OPCDA.AllTags.Where(x => x.Name == "ScalesY_FullReal").First();
            if (fullReal.Value != null)
                txtScalesYFullDif.Text = Math.Round((float)obj - (float)fullReal.Value, 1).ToString();
        }

        private void ScalesY_CurNeed_ValueChanged(object obj)
        {
            txtScalesYCurNeed.Text = obj.ToString();
            Tag curReal = OPCDA.AllTags.Where(x => x.Name == "ScalesY_CurReal").First();
            if (curReal.Value != null)
                txtScalesYCurDif.Text = Math.Round((float)obj - (float)curReal.Value, 1).ToString();
        }

        private void ScalesY_WeighingChanged(object obj)
        {
            bool? isClosed = (bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesY_IsClosed").First().Value;
            if ((bool)obj)
            {
                if (isClosed == true)
                    imgScalesYSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (isClosed == true)
                    imgScalesYSilo.Source = BI_SiloClosed;
            }
        }

        private void ScalesY_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if ((bool?)OPCDA.AllTags.Where(x => x.Name == "ScalesY_Weighing").First().Value == true)
                    imgScalesYSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesYSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesYSilo.Source = BI_SiloOpend;
            }
        }

        private void ScalesY_Batcher3_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesYBatcher3.Source = BI_SmallSiloGreen;
            else
                imgScalesYBatcher3.Source = BI_SmallSilo;
        }

        private void ScalesY_Batcher2_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesYBatcher2.Source = BI_SmallSiloGreen;
            else
                imgScalesYBatcher2.Source = BI_SmallSilo;
        }

        private void ScalesY_Batcher1_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesYBatcher1.Source = BI_SmallSiloGreen;
            else
                imgScalesYBatcher1.Source = BI_SmallSilo;
        }

        #endregion

        #region ScalesK
        private void ScalesK_WeightNotNotmal_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesKOverWeight.Visibility = Visibility.Visible;
            else
                imgScalesKOverWeight.Visibility = Visibility.Hidden;
        }

        private void ScalesK_IsBusy_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesKBusy.Visibility = Visibility.Visible;
            else
                imgScalesKBusy.Visibility = Visibility.Hidden;
        }

        private void ScalesK_CurReal_ValueChanged(object obj)
        {
            txtScalesKCurReal.Text = obj.ToString();
        }

        private void ScalesK_FullReal_ValueChanged(object obj)
        {
            txtScalesKFullReal.Text = obj.ToString();
        }

        private void ScalesK_FullNeed_ValueChanged(object obj)
        {
            txtScalesKFullNeed.Text = obj.ToString();
        }
        private void ScalesK_Weighing_ValueChanged(object obj)
        {
            if ((bool)obj)
                rectangleScalesK.Fill = Brushes.LightGreen;
            else
                rectangleScalesK.Fill = Brushes.Gray;
        }
        #endregion

        #region MixRubber
        private void MR_PressureOff_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblPressureOff.Background = Brushes.Red;
            else
                lblPressureOff.Background = Brushes.Gray;
        }

        private void MR_UpperPressUp_ValueChanged(object obj)
        {
            bool up = (bool)obj;
            bool down = (bool)OPCDA.AllTags.Where(x => x.Name == "MR_UpperPressDown").First().Value;
            if (up && !down)
            {
                imgFloat.Source = BI_Float;
                Canvas.SetLeft(imgFloat, 0);
            }
            else if (up && down)
            {
                imgFloat.Source = BI_FloatRed;
            }
            else if (!up && !down)
            {
                imgFloat.Source = BI_Float;
                Canvas.SetLeft(imgFloat, 79);
            }
            else if (!up && down)
            {
                imgFloat.Source = BI_Float;
                Canvas.SetLeft(imgFloat, 158);
            }
        }

        private void MR_UpperPressDown_ValueChanged(object obj)
        {
            bool up = (bool)OPCDA.AllTags.Where(x => x.Name == "MR_UpperPressUp").First().Value;
            bool down = (bool)obj;
            if (!up && down)
            {
                imgFloat.Source = BI_Float;
                Canvas.SetLeft(imgFloat, 158);
            }
            else if (up && down)
            {
                imgFloat.Source = BI_FloatRed;
            }
            else if (!up && !down)
            {
                imgFloat.Source = BI_Float;
                Canvas.SetLeft(imgFloat, 79);
            }
            else if (up && !down)
            {
                imgFloat.Source = BI_Float;
                Canvas.SetLeft(imgFloat, 0);
            }
        }

        private void MR_FlapValveClosed_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgFlapValve.RenderTransform = new RotateTransform(90);
            else
                imgFlapValve.RenderTransform = new RotateTransform(0);
        }

        private void MR_Pin_ValueChanged(object obj)
        {
            if ((bool)obj)
                Canvas.SetLeft(imgPin, 146);
            else
                Canvas.SetLeft(imgPin, 161);
        }

        private void MR_HighTemperature_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblTemperatureHigh.Background = Brushes.Red;
            else
                lblTemperatureHigh.Background = Brushes.Gray;
        }

        private void MR_CriticalTemperature_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblTemperatureHigh.Background = Brushes.Red;
            else
                lblTemperatureHigh.Background = Brushes.Gray;
        }
        #endregion

        #region Transporters
        private void TK_Working_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgTransporterK.Source = BI_Transporter2Green;
            else
                imgTransporterK.Source = BI_Transporter2;
        }

        private void TL1_Working_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgTransporterL1.Source = BI_TransporterGreen;
            else
                imgTransporterL1.Source = BI_Transporter;
        }

        private void TL2_Working_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgTransporterL2.Source = BI_TransporterGreen;
            else
                imgTransporterL2.Source = BI_Transporter;
        }
        #endregion

        #region Injector

        private void Injector_WeighingChanged(object obj)
        {
            if ((bool)obj)
                imgInjector.Source = BI_InjectorFull;
            else
                imgInjector.Source= BI_Injector;
        }

        #endregion

        #region OilPump
        private void OilPump_Working_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgOilPump.Source = BI_OilPumpGreen;
            else
                imgOilPump.Source = BI_OilPump;
        }
        #endregion

        #region Info

        private void TestMode_On_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblTestMode.Visibility = Visibility.Visible;
            else
                lblTestMode.Visibility = Visibility.Hidden;
        }

        private void MR_Load_IsFobidden_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblRGLock.Background = Brushes.Red;
            else
                lblRGLock.Background = Brushes.Gray;
        }

        private void MR_Mix_IsFobidden_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblRJLock.Background = Brushes.Red;
            else
                lblRJLock.Background = Brushes.Gray;
        }

        private void Btn_StopProcess_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblKZTLock.Background = Brushes.Red;
            else
                lblKZTLock.Background = Brushes.Gray;
        }

        private void ManualMode_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblManualMode.Background = Brushes.Red;
            else
                lblManualMode.Background = Brushes.Gray;
        }

        private void DustCollectorOn_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblDustCollector.Background = Brushes.LimeGreen;
            else
                lblDustCollector.Background = Brushes.Gray;
        }

        private void BagFilter_Blow_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblBagFilter.Background = Brushes.LimeGreen;
            else
                lblBagFilter.Background = Brushes.Gray;
        }
        /* не используется
        private void ScrewPrepareOff_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblScrewPrepare.Background = Brushes.Gray;
            else
                lblScrewPrepare.Background = Brushes.Red;
        }
        */
        private void Not24V_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblNot24.Background = Brushes.Gray;
            else
                lblNot24.Background = Brushes.Red;
        }

        private void RecipeNumber_ValueChanged(object obj)
        {
            lblRecipeNumber.Content = obj.ToString();
        }

        private void Resipe_IsNot_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblResipeIsNot.Background = Brushes.Red;
            else
                lblResipeIsNot.Background = Brushes.Gray;
        }

        private void EmergencyButton_Pushed_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblEmergencyButtonPushed.Background = Brushes.Red;
            else
                lblEmergencyButtonPushed.Background = Brushes.Gray;
        }

        private void StopButton_Pushed_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblStopButtonPushed.Background = Brushes.Red;
            else
                lblStopButtonPushed.Background = Brushes.Gray;
        }

        private void LastFilling_ToPlan_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblLastLoad.Visibility = Visibility.Visible;
            else
                lblLastLoad.Visibility = Visibility.Hidden;
        }

        private void Filling_OnStart_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblLoadReady.Visibility = Visibility.Visible;
            else
                lblLoadReady.Visibility = Visibility.Hidden;
        }

        private void Batchers_Ready_ValueChanged(object obj)
        {
            if ((bool)obj)
                lblBatchersReady.Visibility = Visibility.Visible;
            else
                lblBatchersReady.Visibility = Visibility.Hidden;
        }

        private void Batchers_Start1_ValueChanged(object obj)
        {
            Tag batchersStart2 = OPCDA.AllTags.Where(x => x.Name == "Batchers_Start2").First();
            if ((bool)obj || (bool)batchersStart2.Value)
                lblBatchersStart.Background = Brushes.Red;
            else
                lblBatchersStart.Background = Brushes.Gray;
        }

        private void Batchers_Start2_ValueChanged(object obj)
        {
            Tag batchersStart1 = OPCDA.AllTags.Where(x => x.Name == "Batchers_Start1").First();
            if ((bool)obj || (bool)batchersStart1.Value)
                lblBatchersStart.Background = Brushes.Red;
            else
                lblBatchersStart.Background = Brushes.Gray;
        }
        #endregion

        #region Temperature water
        private void WaterT1_ValueChanged(object obj)
        {
            txtT1.Text = Math.Round(Convert.ToSingle(obj), 1).ToString();
        }
        private void WaterT2_ValueChanged(object obj)
        {
            txtT2.Text = Math.Round(Convert.ToSingle(obj), 1).ToString();
        }
        private void WaterT3_ValueChanged(object obj)
        {
            txtT3.Text = Math.Round(Convert.ToSingle(obj), 1).ToString();
        }
        private void WaterT4_ValueChanged(object obj)
        {
            txtT4.Text = Math.Round(Convert.ToSingle(obj), 1).ToString();
        }
        private void WaterT5_ValueChanged(object obj)
        {
            txtT5.Text = Math.Round(Convert.ToSingle(obj), 1).ToString();
        }
        private void WaterRotor1_ValueChanged(object obj)
        {
            txtTRotor1.Text = Math.Round(Convert.ToSingle(obj), 1).ToString();
        }
        private void WaterRotor2_ValueChanged(object obj)
        {
            txtTRotor2.Text = Math.Round(Convert.ToSingle(obj), 1).ToString();
        }
        #endregion

        #region String
        private void OperatorFIO_ValueChanged(object obj)
        {
            lblNameOperator.Content = obj.ToString();
        }
        private void RecipeName_ValueChanged(object obj)
        {
            lblRecipe.Content = obj.ToString();
        }
        private void ModeName_ValueChanged(object obj)
        {
            lblMode.Content = obj.ToString();
        }
        private void ShiftName_ValueChanged(object obj)
        {
            lblShift.Content = obj.ToString();
        }

        private void NumberLoad_ValueChanged(object obj)
        {
            lblNumLoad.Content = obj.ToString();
        }

        #endregion

        #region 505

        private void ModeNumber_ValueChanged(object obj)
        {
            lblModeNumber.Content = obj.ToString();
        }
        private void TUnloading_ValueChanged(object obj)
        {
            txtTUnloadFill.Text = obj.ToString();
        }
        private void TDanger_ValueChanged(Object obj)
        {
            txtTCritical.Text = obj.ToString();
        }
        private void FullTimeMix_ValueChanged(Object obj)
        {
            lblTimeMix.Content = obj.ToString() + " сек.";
        }
        private void TimeMixUnderPress_ValueChanged(Object obj)
        {
            lblTimeMixUnderPress.Content = obj.ToString() + " сек.";
        }
        private void NumLoading_Shift_ValueChanged(Object obj)
        {
            lblNumLoadingShift.Content = obj.ToString();
        }
        private void NumLoading_Recept_ValueChanged(Object obj)
        {
            lblNumLoadingRecept.Content = obj.ToString();
        }
        private void Plan_Loadings_ValueChanged(Object obj)
        {
            lblPlanLoadings.Content = obj.ToString();
        }
        private void TCorrect_ValueChanged(object obj)
        {
            txtTUnloadFillCorrect.Text = obj.ToString();
        }
        private void TUnloadSensor_ValueChanged(object obj)
        {
            txtTUnloadSensor.Text = obj.ToString();
        }
        private void MR_TimerRG_ValueChanged(Object obj)
        {
            lblTimeRG.Content = obj.ToString() + " сек.";
        }
        private void MR_TimerRJ_ValueChanged(Object obj)
        {
            lblTimeRJ.Content = obj.ToString() + " сек.";
        }
        private void MR_Power_ValueChanged(Object obj)
        {
            lblPower.Content = obj.ToString();
        }
        private void MR_TSensorUp_ValueChanged(Object obj)
        {
            lblTemperatureUp.Content = obj.ToString();
        }
        private void MR_Mix_IsGoing_ValueChanged(Object obj)
        {
            lblMixIsGoing.Content = obj.ToString();
            //if ((bool)obj)
            //    lblMixIsGoing.Visibility = Visibility.Visible;
            //else
            //    lblMixIsGoing.Visibility= Visibility.Hidden;
        }

        #endregion

        private void btnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            //new PurposeMixture().ShowDialog();
            

            //cts.Cancel();
            /*
            if (this.WindowStyle == WindowStyle.None)
            {
                // Exit fullscreen
                this.ResizeMode = ResizeMode.CanResize;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                //this.WindowState = WindowState.Normal;
            }
            else
            {
                // Enter fullscreen
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Normal;
                this.WindowState = WindowState.Maximized;
            }
            //new Mixture().Show();
            */
        }

        private void btnModes_Click(object sender, RoutedEventArgs e)
        {
            new MixMode2().ShowDialog();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnMix_Click(object sender, RoutedEventArgs e)
        {
            new Mixture2().ShowDialog();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            new TransmitRecipe().ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            OPCDA.DisconnectOPC();
            this.Close();
        }
    }
}
