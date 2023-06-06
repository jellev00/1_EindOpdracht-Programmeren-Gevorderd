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
using static GUI.Page_Evenementen;
using Domein;
using Domein.DTOs;
using Domein.Models;
using Domein.Exceptions;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Page_Gebruiker.xaml
    /// </summary>
    public partial class Page_Gebruiker : Page
    {
        private DomeinController _dc;
        public Page_Gebruiker(DomeinController dc)
        {
            InitializeComponent();

            _dc = dc;

            RefreshGebruikerOvervieuw();
            VulCmbDatumaan();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvGebruiker.ItemsSource);
            view.Filter = UserFilter;
        }

        private void VulCmbDatumaan()
        {
            List<DateTime> datum = new List<DateTime>();
            datum.Add(new DateTime(2022, 07, 15, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 16, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 17, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 18, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 19, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 20, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 21, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 22, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 23, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 24, 0, 0, 0));
            datum.Add(new DateTime(2022, 07, 25, 0, 0, 0));

            cmbDatum.ItemsSource = datum;
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

        private static int _id;
        private static int _idgebruiker;
        public int ID
        {
            get {
                GebruikerDTO selectedGebruikerDTO = (lvGebruiker.SelectedItem as GebruikerDTO);
                DateTime datum = DateTime.Parse(cmbDatum.Text);
                int dag = datum.Day;
                _idgebruiker = selectedGebruikerDTO.Id;
                string DagplanIDString = $"{dag}{selectedGebruikerDTO.Id}";
                int dagplanID = int.Parse(DagplanIDString);
                _id = dagplanID;
                //_id = 100 + selectedGebruikerDTO.Id;
                return _id;
            }
        }

        int dagplanid = _id;
        public int DagplanId()
        {
            int Id = dagplanid;
            return Id;
        }

        int gebruikerid = _idgebruiker;
        public int GebruikersId()
        {
            int Idgebruiker = gebruikerid;
            return Idgebruiker;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            GebruikerDTO selectedGebruikerDTO = (lvGebruiker.SelectedItem as GebruikerDTO);
            var paresDatum = DateTime.Parse(cmbDatum.Text);
            try
            {
                _dc.MaakDagplan(ID, selectedGebruikerDTO.Id, paresDatum);

                // Create the startup window
                Dagplan_Window DagplanW = new Dagplan_Window(_dc);
                // Do stuff here, e.g. to the window
                DagplanW.Title = "Dagplan";
                // Show the window
                DagplanW.ShowDialog();
            }
            catch (DagplanException ex)
            {
                MessageBox.Show($"Fout bij het maken van het dagplan: {ex.Message}", "ERROR Dagplan", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
