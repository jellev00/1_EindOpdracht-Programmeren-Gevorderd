using Domein.Interfaces;
using Domein.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistentie
{
    public class GebruikerRepoBestand : IGebruikersRepo
    {
        private string _connectionString;
        private List<Gebruiker> _gebruiker;
        public GebruikerRepoBestand(string bestand, string connectionString)
        {
            _connectionString = connectionString;
            _gebruiker = new List<Gebruiker>();

            try
            {
                using (var reader = new StreamReader(bestand))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        // Type omzetten
                        Gebruiker gebruiker = new Gebruiker(
                            int.Parse(values[0]),
                            values[1],
                            values[2],
                            decimal.Parse(values[3])
                            );
                        _gebruiker.Add(gebruiker);
                        AddGebruiker(gebruiker);
                    }
                }

            }
            catch
            {
                throw;
            }
        }

        public void AddGebruiker(Gebruiker gebruiker)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Gebruikers (Id, Naam, Voornaam, Prijs) VALUES (@Id, @Naam, @Voornaam, @Prijs)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int);
                    command.Parameters["@Id"].Value = gebruiker.Id;

                    command.Parameters.Add("@Naam", SqlDbType.VarChar);
                    command.Parameters["@Naam"].Value = gebruiker.Naam;

                    command.Parameters.Add("@Voornaam", SqlDbType.VarChar);
                    command.Parameters["@Voornaam"].Value = gebruiker.Voornaam;

                    command.Parameters.AddWithValue("@Prijs", SqlDbType.Decimal);
                    command.Parameters["@Prijs"].Value = gebruiker.Prijs;

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public Gebruiker GeefGebruiker(int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public List<Gebruiker> GeefGebruikers()
        {
            throw new NotImplementedException();
        }
    }
}
