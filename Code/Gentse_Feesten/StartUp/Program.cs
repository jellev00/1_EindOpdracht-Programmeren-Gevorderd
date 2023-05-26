using Domein.Interfaces;
using Persistentie;

namespace StartUp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = GentseFeestenDB; Integrated Security = True; TrustServerCertificate=True";

            // CSV-bestand voor Gebruikers
            // string gebruikersCsvPath = "D:\\Hogent\\Semester_2\\PG\\EindOpdracht\\Code Tests\\test_eindopdracht\\Persistentie\\Data\\Gebruiker.csv" /*@".\Data\Gebruikers_Gentse_Feesten.csv"*/;
            // IGebruikersRepo GebruikerDb = new GebruikerRepoDb(connectionString);
            // IGebruikersRepo GebruikerBestand = new GebruikerRepoBestand(gebruikersCsvPath, connectionString);
            // CSV-bestand voor Evenementen
            // string evenementenCsvPath = "D:\\Hogent\\Semester_2\\PG\\EindOpdracht\\Code Tests\\test_eindopdracht\\Persistentie\\Data\\gentse-feesten-evenementen-202223.csv" /*@".\Data\gentse-feesten-evenementen-202223.csv"*/;
            // IEvenementenRepo connectionDbEvenement = new EvenementRepoDb(connectionString);
            // IEvenementenRepo EvenementBestand = new EvenementRepoBestand(evenementenCsvPath, connectionString); ;

            Console.WriteLine("Data initialization completed");
        }
    }
}