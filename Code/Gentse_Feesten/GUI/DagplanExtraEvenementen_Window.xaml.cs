using Domein;
using Domein.Exceptions;
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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for DagplanExtraEvenementen_Window.xaml
    /// </summary>
    public partial class DagplanExtraEvenementen_Window : Window
    {
        private DomeinController _dc;
        public DagplanExtraEvenementen_Window(DomeinController dc)
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
            ToonDagplanGebruiker_Window DagplanGebruiker = new ToonDagplanGebruiker_Window(_dc);
            int ID = DagplanGebruiker.DagplanEvenementId();
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
            ToonDagplanGebruiker_Window DagplanGebruiker = new ToonDagplanGebruiker_Window(_dc);
            int ID = DagplanGebruiker.DagplanEvenementId();
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
