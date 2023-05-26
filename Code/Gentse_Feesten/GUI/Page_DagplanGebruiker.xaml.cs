using Domein;
using Domein.DTOs;
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
                RefreshGebruikerDagplanOvervieuw();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // nieuwe folder aanmaken
            string newFolder = "Genste_Feesten_VIP_Dagplannen";

            string path = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    newFolder
                );

            // kijken of de folder al bestaat of niet en anders moet hij gemaakt worden
            if (!System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                    MessageBox.Show($"U kan de bestanden terug vinden op deze path {path}", "INFO" , MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException ie)
                {
                    MessageBox.Show("IO Error", "IO Error" + ie.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("General Error", "General Error" + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            DagplanDTO selectedDagplanDTO = (lvDagplan.SelectedItem as DagplanDTO);



            var dagplanId = selectedDagplanDTO.Id;

            var gebruiker = _dc.GeefGebruiker(selectedDagplanDTO.GebruikerId);

            string filePath = $@"{path}\{dagplanId}.adoc";

            List<string> fileList = new List<string>();
            fileList.Clear();

            fileList.Add("= *Dagplan*");
            fileList.Add(" ");
            fileList.Add($"=== Dagplan Id {selectedDagplanDTO.Id}");
            fileList.Add(" ");
            fileList.Add("== *Gebruiker*");
            fileList.Add($"{gebruiker.Id}: {gebruiker.Naam} {gebruiker.Voornaam}");
            fileList.Add(" ");
            fileList.Add("== *Datum*");
            fileList.Add($"{selectedDagplanDTO.Datum}");
            fileList.Add(" ");
            fileList.Add("== *Evenement 1*");
            fileList.Add($"{selectedDagplanDTO.Evenement1}");
            fileList.Add(" ");
            fileList.Add("== *Evenement 2*");
            fileList.Add($"{selectedDagplanDTO.Evenement2}");

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            foreach (string s in fileList)
            {
                sw.WriteLine(s);
            }
            sw.Flush();
            sw.Close();
        }
    }
}
