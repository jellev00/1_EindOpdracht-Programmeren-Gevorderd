using Domein;
using Domein.DTOs;
using Domein.Exceptions;
using Domein.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Page_DagplanGebruiker.xaml
    /// </summary>
    public partial class Page_DagplanGebruiker : Page
    {
        private DomeinController _dc;
        public Page_DagplanGebruiker(DomeinController dc)
        {
            InitializeComponent();

            _dc = dc;

            RefreshGebruikerOvervieuw();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvGebruiker.ItemsSource);
            view.Filter = UserFilter;
        }

        private void RefreshGebruikerOvervieuw()
        {
            lvGebruiker.ItemsSource = _dc.GeefGebruikers();
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilterNaam.Text))
                return true;
            else
                return ((item as GebruikerDTO).Naam.IndexOf(txtFilterNaam.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilterNaam_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvGebruiker.ItemsSource).Refresh();
        }

        private void lvGebruiker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GebruikerDTO selectedGebruikerDTO = (lvGebruiker.SelectedItem as GebruikerDTO);

            if (selectedGebruikerDTO != null)
            {
                RefreshGebruikerDagplanOvervieuw();
            }
        }

        private void RefreshGebruikerDagplanOvervieuw()
        {
            GebruikerDTO selectedGebruikerDTO = (lvGebruiker.SelectedItem as GebruikerDTO);

            if (selectedGebruikerDTO != null)
            {
                lvDagplan.ItemsSource = _dc.GeefDagplannenVoorGebruiker(selectedGebruikerDTO.Id);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DagplanDTO selectedDagplanDTO = lvDagplan.SelectedItem as DagplanDTO;

            if (selectedDagplanDTO != null)
            {
                _dc.VerwijderDagplan(selectedDagplanDTO.Id);
                _dc.VerwijderEvenemetenVanDagplan(selectedDagplanDTO.Id);
                RefreshGebruikerDagplanOvervieuw();
            }
        }

        private static int _SelectedDagplanID;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DagplanDTO selectedDagplanDTO = lvDagplan.SelectedItem as DagplanDTO;
            _SelectedDagplanID = selectedDagplanDTO.Id;

            // Create the startup window
            ToonDagplanGebruiker_Window ToonDagplanGebruikerW = new ToonDagplanGebruiker_Window(_dc);
            // Do stuff here, e.g. to the window
            ToonDagplanGebruikerW.Title = "Dagplan Gebruiker Info";
            // Show the window
            ToonDagplanGebruikerW.ShowDialog();
        }

        int dagplanId = _SelectedDagplanID;
        public int selectedDagplan()
        {
            int Id = dagplanId;
            return Id;
        }
    }
}
