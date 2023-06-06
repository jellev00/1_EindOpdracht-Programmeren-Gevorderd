using Domein;
using Domein.DTOs;
using Domein.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for ToonDagplanGebruiker_Window.xaml
    /// </summary>
    public partial class ToonDagplanGebruiker_Window : Window
    {
        private DomeinController _dc;

        public ToonDagplanGebruiker_Window(DomeinController dc)
        {
            InitializeComponent();

            _dc = dc;

            RefreshDagplanVanGebruikerOvervieuw();
        }

        private static int _id;

        public void RefreshDagplanVanGebruikerOvervieuw()
        {
            Page_DagplanGebruiker page_DagplanGebruiker = new Page_DagplanGebruiker(_dc);
            var dagplanId = page_DagplanGebruiker.selectedDagplan();
            _id = dagplanId;
            var dagplan = _dc.GeefDagplan(dagplanId);
            var gebruiker = _dc.GeefGebruiker(dagplan.GebruikerId);
            DagplanId.Text = $"Dagplan Id: {dagplan.Id}";
            Gebruiker.Text = $"{gebruiker.Id}: {gebruiker.Naam} {gebruiker.Voornaam}";
            Datum.Text = $"{dagplan.Datum}";

            lvDagplanEvenementen.ItemsSource = _dc.GeefEvenementenVanDagplan(dagplan.Id);
        }

        int dagplanid = _id;
        public int DagplanEvenementId()
        {
            int Id = dagplanid;
            return Id;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Download

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
                    MessageBox.Show($"U kan de bestanden terug vinden op deze path {path}", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
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

            Page_DagplanGebruiker page_DagplanGebruiker = new Page_DagplanGebruiker(_dc);
            var dagplanId = page_DagplanGebruiker.selectedDagplan();

            var selectedDagplanDTO = _dc.GeefDagplan(dagplanId);

            var dagplanEvenement = _dc.GeefEvenementenVanDagplan(dagplanId);

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
            int evenementCounter = 1;
            foreach (var evenement in dagplanEvenement)
            {
                fileList.Add($"== *Evenement {evenementCounter}*");
                fileList.Add($"*Titel:* {evenement.Titel} +");
                fileList.Add($"*StartTijd:* {evenement.Starttijd} +");
                fileList.Add($"*Eindtijd:* {evenement.Eindtijd} +");
                fileList.Add($"*Prijs:* {evenement.Prijs} +");
                fileList.Add($"*Beschrijving:* {evenement.Beschrijving} +");
                fileList.Add(" ");
                evenementCounter++;
            }

            //fileList.Add("== *Evenement 1*");
            //fileList.Add($"{selectedDagplanDTO.Evenement1}");
            //fileList.Add(" ");
            //fileList.Add("== *Evenement 2*");
            //fileList.Add($"{selectedDagplanDTO.Evenement2}");

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Verwijderen

            DagplanEvenementenDTO selectedEvenement = lvDagplanEvenementen.SelectedItem as DagplanEvenementenDTO;

            if (selectedEvenement != null)
            {
                _dc.VerwijderEvenementVanDagplan(selectedEvenement.Id);
                RefreshDagplanVanGebruikerOvervieuw();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // open nieuw window voor evenementen toe te voegen

            // Create the startup window
            DagplanExtraEvenementen_Window DagplanExtraEvenementenW = new DagplanExtraEvenementen_Window(_dc);
            // Do stuff here, e.g. to the window
            DagplanExtraEvenementenW.Title = "Dagplan";
            // Show the window
            DagplanExtraEvenementenW.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // refresh ListVieuw
            RefreshDagplanVanGebruikerOvervieuw();
        }
    }
}
