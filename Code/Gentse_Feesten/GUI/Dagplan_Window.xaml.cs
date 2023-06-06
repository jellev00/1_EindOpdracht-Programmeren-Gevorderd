using Domein;
using Domein.DTOs;
using Domein.Exceptions;
using Domein.Models;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for Dagplan_Window.xaml
    /// </summary>
    public partial class Dagplan_Window : Window
    {
        private DomeinController _dc;
        public Dagplan_Window(DomeinController dc)
        {
            InitializeComponent();

            _dc = dc;

            RefreshEvenementOvervieuw();

            ApplyFilters();
        }

        private void RefreshEvenementOvervieuw()
        {
            lvEvenement.ItemsSource = _dc.GeefEvenementen();
        }

        private void txtFilterTitel_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            Page_Gebruiker page_Gebruiker = new Page_Gebruiker(_dc);
            int ID = page_Gebruiker.DagplanId();
            var dagplan = _dc.GeefDagplan(ID);

            string filterText = txtFilterTitel.Text;
            string dateFilterText = dagplan.Datum.Date.ToString(); //txtFilterStartDatum.Text

            // Converteer de tekst naar een datumwaarde (hieronder is een voorbeeld van de datumfilterlogica)
            DateTime? filterDate = null;
            if (DateTime.TryParse(dateFilterText, out DateTime parsedDate))
            {
                filterDate = parsedDate;
            }

            // Filter de gegevens op basis van de ingevoerde waarden
            var filteredItems = _dc.GeefEvenementen().Where(item =>
                item.Titel.Contains(filterText) &&
                (filterDate == null || item.Starttijd.Date == filterDate.Value.Date));

            // Update de ListView met de gefilterde gegevens
            lvEvenement.ItemsSource = filteredItems;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page_Gebruiker page_Gebruiker = new Page_Gebruiker(_dc);
            int ID = page_Gebruiker.DagplanId();
            int IDGebruiker = page_Gebruiker.GebruikersId();
            var dagplan = _dc.GeefDagplan(ID);

            foreach (Evenement evenement in lvEvenement.SelectedItems)
            {
                try
                {
                    _dc.VoegEvenementToeAanDagplan(ID, evenement);
                }
                catch (DagplanException ex)
                {
                    MessageBox.Show($"Fout bij het toevoegen van een Evenement: {ex.Message}", "ERROR Evenement", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            //int count = 0;
            //Evenement evenement1 = null;
            //Evenement evenement2 = null;
            //foreach (Evenement evenement in lvEvenement.SelectedItems)
            //{
            //    count++;
            //    if (count == 1)
            //    {
            //        evenement1 = evenement;
            //    }
            //    else if (count == 2)
            //    {
            //        evenement2 = evenement;
            //    }
            //    else if (count >= 3)
            //    {
            //        MessageBox.Show("Er mogen maar 2 evenementen geselecteerd zijn", "ERROR Evenement", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }
            //}

            //if (evenement1 != null && evenement2 != null)
            //{
            //    _dc.VoegEvenementToeAanDagplan(ID, IDGebruiker, dagplan, evenement1, evenement2);
            //}
            //else
            //{
            //    MessageBox.Show("Selecteer 2 evenementen", "ERROR Evenement", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
