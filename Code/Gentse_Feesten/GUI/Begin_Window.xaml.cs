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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Begin_Window.xaml
    /// </summary>
    public partial class Begin_Window : Window
    {
        private DomeinController _dc;
        public Begin_Window(DomeinController dc)
        {
            InitializeComponent();

            _dc = dc;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page_Gebruiker(_dc);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page_Evenementen(_dc);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page_DagplanGebruiker(_dc);
        }
    }
}
