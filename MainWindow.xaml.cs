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

namespace MixRubber2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //OPCDA.AllTags.Where(x => x.Name == "C_CurrentWeight").First().ValueChanged += Scada_C_CurrentWeight_ValueChanged;
            //OPCDA.ConnectionOPC();
        }

        private void Scada_C_CurrentWeight_ValueChanged(object obj)
        {
            //Int16 val = Convert.ToInt16(obj);
            //txtValue.Text = val.ToString();
        }
    }
}
