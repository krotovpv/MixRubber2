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
        

        bool scalesC_IsClosed = false;
        bool scalesC_Weighing = false;

        bool scalesJ_IsClosed = false;
        bool scalesJ_Weighing = false;

        bool scalesD_IsClosed = false;
        bool scalesD_Weighing = false;

        bool scalesE_IsClosed = false;
        bool scalesE_Weighing = false;

        bool scalesSh_IsClosed = false;
        bool scalesSh_Weighing = false;

        bool scalesJu_IsClosed = false;
        bool scalesJu_Weighing = false;

        bool scalesY_IsClosed = false;
        bool scalesY_Weighing = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

            OPCDA.AllTags.Where(x => x.Name == "ScalesC_KRP").First().ValueChanged += ScalesC_KRP_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_Initial").First().ValueChanged += ScalesC_Initial_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_IsBusy").First().ValueChanged += ScalesC_IsBusy_ValueChanged;
            OPCDA.AllTags.Where(x => x.Name == "ScalesC_WeightNotNormal").First().ValueChanged += ScalesC_WeightNotNotmal_ValueChanged;

            OPCDA.AllTags.Where(x => x.Name == "ContainerC_IsClosed").First().ValueChanged += ContainerC_IsClosedChanged;
            OPCDA.AllTags.Where(x => x.Name == "ContainerC_IsOpened").First().ValueChanged += ContainerC_IsOpenedChanged;
            
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

            //OPCDA.ConnectionOPC();
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
            if ((bool)obj)
            {
                if (scalesC_IsClosed)
                    imgScalesCSilo.Source = BI_SiloClosedGreen;
            }    
            else
            {
                if (scalesC_IsClosed)
                    imgScalesCSilo.Source = BI_SiloClosed;
            }
            scalesC_Weighing = (bool)obj;
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
                if (scalesC_Weighing)
                    imgScalesCSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesCSilo.Source = BI_SiloClosed;
            }
            scalesC_IsClosed = (bool)obj;
        }

        private void ScalesC_Initial_ValueChanged(object obj)
        {
            if ((bool)obj)
                imgScalesCInitial.Visibility = Visibility.Visible;
            else
                imgScalesCInitial.Visibility = Visibility.Hidden;
        }

        private void ScalesC_KRP_ValueChanged(object obj)
        {
            if ((bool)obj)
            {
                btnKRP.Content = "Выкл КРП";
                btnKRP.Background = new SolidColorBrush(Colors.Red);
                btnKRP.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                btnKRP.Content = "Вкл КРП";
                btnKRP.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD));
                btnKRP.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void ScalesC_FullReal_ValueChanged(object obj)
        {
            txtScalesCFullReal.Text = ((float)obj).ToString();
        }

        private void ScalesC_CurReal_ValueChanged(object obj)
        {
            txtScalesCCurReal.Text = ((float)obj).ToString();
        }

        private void ScalesC_FullNeed_ValueChanged(object obj)
        {
            txtScalesCFullNeed.Text = ((float)obj).ToString();
        }

        private void ScalesC_CurNeed_ValueChanged(object obj)
        {
            txtScalesCCurNeed.Text = ((float)obj).ToString();
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
                imgConteinerC.Source = BI_ConteinerClose;
        }

        private void ContainerC_IsOpenedChanged(object obj)
        {
            if ((bool)obj)
                imgConteinerC.Source = BI_ConteinerOpen;
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
            txtScalesJFullReal.Text = ((float)obj).ToString();
        }

        private void ScalesJ_CurReal_ValueChanged(object obj)
        {
            txtScalesJCurReal.Text = ((float)obj).ToString();
        }

        private void ScalesJ_FullNeed_ValueChanged(object obj)
        {
            txtScalesJFullNeed.Text = ((float)obj).ToString();
        }

        private void ScalesJ_CurNeed_ValueChanged(object obj)
        {
            txtScalesJCurNeed.Text = ((float)obj).ToString();
        }

        private void ScalesJ_WeighingChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesJ_IsClosed)
                    imgScalesJSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (scalesJ_IsClosed)
                    imgScalesJSilo.Source = BI_SiloClosed;
            }
            scalesJ_Weighing = (bool)obj;
        }

        private void ScalesJ_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesJ_Weighing)
                    imgScalesJSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesJSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesJSilo.Source = BI_SiloOpend;
            }
            scalesJ_IsClosed = (bool)obj;
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
            txtScalesDFullReal.Text = ((float)obj).ToString();
        }

        private void ScalesD_CurReal_ValueChanged(object obj)
        {
            txtScalesDCurReal.Text = ((float)obj).ToString();
        }

        private void ScalesD_FullNeed_ValueChanged(object obj)
        {
            txtScalesDFullNeed.Text = ((float)obj).ToString();
        }

        private void ScalesD_CurNeed_ValueChanged(object obj)
        {
            txtScalesDCurNeed.Text = ((float)obj).ToString();
        }

        private void ScalesD_WeighingChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesD_IsClosed)
                    imgScalesDSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (scalesD_IsClosed)
                    imgScalesDSilo.Source = BI_SiloClosed;
            }
            scalesD_Weighing = (bool)obj;
        }

        private void ScalesD_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesD_Weighing)
                    imgScalesDSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesDSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesDSilo.Source = BI_SiloOpend;
            }
            scalesD_IsClosed = (bool)obj;
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
            txtScalesEFullReal.Text = ((float)obj).ToString();
        }

        private void ScalesE_CurReal_ValueChanged(object obj)
        {
            txtScalesECurReal.Text = ((float)obj).ToString();
        }

        private void ScalesE_FullNeed_ValueChanged(object obj)
        {
            txtScalesEFullNeed.Text = ((float)obj).ToString();
        }

        private void ScalesE_CurNeed_ValueChanged(object obj)
        {
            txtScalesECurNeed.Text = ((float)obj).ToString();
        }

        private void ScalesE_WeighingChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesE_IsClosed)
                    imgScalesESilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (scalesE_IsClosed)
                    imgScalesESilo.Source = BI_SiloClosed;
            }
            scalesE_Weighing = (bool)obj;
        }

        private void ScalesE_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesE_Weighing)
                    imgScalesESilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesESilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesESilo.Source = BI_SiloOpend;
            }
            scalesE_IsClosed = (bool)obj;
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
            txtScalesShFullReal.Text = ((float)obj).ToString();
        }

        private void ScalesSh_CurReal_ValueChanged(object obj)
        {
            txtScalesShCurReal.Text = ((float)obj).ToString();
        }

        private void ScalesSh_FullNeed_ValueChanged(object obj)
        {
            txtScalesShFullNeed.Text = ((float)obj).ToString();
        }

        private void ScalesSh_CurNeed_ValueChanged(object obj)
        {
            txtScalesShCurNeed.Text = ((float)obj).ToString();
        }

        private void ScalesSh_WeighingChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesSh_IsClosed)
                    imgScalesShSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (scalesSh_IsClosed)
                    imgScalesShSilo.Source = BI_SiloClosed;
            }
            scalesSh_Weighing = (bool)obj;
        }

        private void ScalesSh_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesSh_Weighing)
                    imgScalesShSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesShSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesShSilo.Source = BI_SiloOpend;
            }
            scalesSh_IsClosed = (bool)obj;
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
            txtScalesJuFullReal.Text = ((float)obj).ToString();
        }

        private void ScalesJu_CurReal_ValueChanged(object obj)
        {
            txtScalesJuCurReal.Text = ((float)obj).ToString();
        }

        private void ScalesJu_FullNeed_ValueChanged(object obj)
        {
            txtScalesJuFullNeed.Text = ((float)obj).ToString();
        }

        private void ScalesJu_CurNeed_ValueChanged(object obj)
        {
            txtScalesJuCurNeed.Text = ((float)obj).ToString();
        }

        private void ScalesJu_WeighingChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesJu_IsClosed)
                    imgScalesJuSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (scalesJu_IsClosed)
                    imgScalesJuSilo.Source = BI_SiloClosed;
            }
            scalesJu_Weighing = (bool)obj;
        }

        private void ScalesJu_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesJu_Weighing)
                    imgScalesJuSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesJuSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesJuSilo.Source = BI_SiloOpend;
            }
            scalesJu_IsClosed = (bool)obj;
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
            txtScalesYFullReal.Text = ((float)obj).ToString();
        }

        private void ScalesY_CurReal_ValueChanged(object obj)
        {
            txtScalesYCurReal.Text = ((float)obj).ToString();
        }

        private void ScalesY_FullNeed_ValueChanged(object obj)
        {
            txtScalesYFullNeed.Text = ((float)obj).ToString();
        }

        private void ScalesY_CurNeed_ValueChanged(object obj)
        {
            txtScalesYCurNeed.Text = ((float)obj).ToString();
        }

        private void ScalesY_WeighingChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesY_IsClosed)
                    imgScalesYSilo.Source = BI_SiloClosedGreen;
            }
            else
            {
                if (scalesY_IsClosed)
                    imgScalesYSilo.Source = BI_SiloClosed;
            }
            scalesY_Weighing = (bool)obj;
        }

        private void ScalesY_IsClosedChanged(object obj)
        {
            if ((bool)obj)
            {
                if (scalesY_Weighing)
                    imgScalesYSilo.Source = BI_SiloClosedGreen;
                else
                    imgScalesYSilo.Source = BI_SiloClosed;
            }
            else
            {
                imgScalesYSilo.Source = BI_SiloOpend;
            }
            scalesY_IsClosed = (bool)obj;
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

        private void btnFullScreen_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
