using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Domein;
using Domein.Interfaces;
using Persistentie;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IGebruikersRepo gebruikerRepo = new GebruikerRepoDb(@"Data Source =.\SQLEXPRESS;Initial Catalog = GentseFeestenDB; Integrated Security = True; TrustServerCertificate=True");
            IEvenementenRepo evenementRepo = new EvenementRepoDb(@"Data Source =.\SQLEXPRESS;Initial Catalog = GentseFeestenDB; Integrated Security = True; TrustServerCertificate=True");
            IDagplanRepo dagplanRepo = new DagplanRepoDb(@"Data Source =.\SQLEXPRESS;Initial Catalog = GentseFeestenDB; Integrated Security = True; TrustServerCertificate=True");

            DomeinController dc = new DomeinController(evenementRepo, gebruikerRepo, dagplanRepo);

            // Create the startup window
            MainWindow wnd = new MainWindow(dc);
            // Do stuff here, e.g. to the window
            wnd.Title = "Gentse Feesten";
            // Show the window
            wnd.Show();
        }
    }
}
