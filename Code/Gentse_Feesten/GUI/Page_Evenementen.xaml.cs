using Domein;
using Domein.Models;
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
    /// Interaction logic for Page_Evenementen.xaml
    /// </summary>
    public partial class Page_Evenementen : Page
    {
        private DomeinController _dc;
        public Page_Evenementen(DomeinController dc)
        {
            InitializeComponent();

            _dc = dc;

            RefreshEvenementOvervieuw();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvEvenement.ItemsSource);
            view.Filter = TitelFilter;
        }

        private void RefreshEvenementOvervieuw()
        {
            lvEvenement.ItemsSource = _dc.GeefEvenementen();
        }

        private bool TitelFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilterTitel.Text))
                return true;
            else
                return ((item as Evenement).Titel.IndexOf(txtFilterTitel.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilterTitel_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvEvenement.ItemsSource).Refresh();
        }
    }
}
