using Domein;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DomeinController _dc;
        public MainWindow(DomeinController dc)
        {
            InitializeComponent();

            _dc = dc;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Begin_Window newWindow = new Begin_Window(_dc);
            newWindow.Show();
            this.Close();
        }
    }
}
