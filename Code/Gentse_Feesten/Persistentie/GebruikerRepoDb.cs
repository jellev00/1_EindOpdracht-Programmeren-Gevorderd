using Domein.Models;
using Domein.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Persistentie
{
    public class GebruikerRepoDb : IGebruikersRepo
    {
        private string _connectionString;
        public GebruikerRepoDb(string connectionString)
        {
            _connectionString = connectionString;
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

        public List<Gebruiker> GeefGebruikers()
        {
            List<Gebruiker> gebruikers = new List<Gebruiker>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Gebruikers";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = (int)reader["Id"];
                            string naam = (string)reader["Naam"];
                            string voornaam = (string)reader["Voornaam"];
                            decimal prijs = (decimal)reader["Prijs"];

                            Gebruiker gebruiker = new Gebruiker(id, naam, voornaam, prijs);
                            gebruikers.Add(gebruiker);
                        }
                    }
                }
            }
            return gebruikers;
        }

        public Gebruiker GeefGebruiker(int gebruikerId)
        {
            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new("SELECT * FROM Gebruikers WHERE Id = @GebruikerId;", connection);
                    command.Parameters.AddWithValue("@GebruikerId", gebruikerId);

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            string naam = (string)dataReader["naam"];
                            string voornaam = (string)dataReader["voornaam"];
                            decimal prijs = (decimal)dataReader["prijs"];

                            return new Gebruiker(gebruikerId, naam, voornaam, prijs);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
