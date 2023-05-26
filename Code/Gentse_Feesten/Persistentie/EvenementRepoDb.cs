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
    public class EvenementRepoDb : IEvenementenRepo
    {
        private string _connectionString;
        public EvenementRepoDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddEvenement(Evenement evenement)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Evenementen (Id, Titel, Eindtijd, Starttijd, Prijs, Beschrijving) VALUES (@Id, @Titel, @Eindtijd, @Starttijd, @Prijs, @Beschrijving)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", SqlDbType.VarChar);
                    command.Parameters["@Id"].Value = evenement.Id;

                    command.Parameters.AddWithValue("@Titel", SqlDbType.VarChar);
                    command.Parameters["@Titel"].Value = evenement.Titel;

                    command.Parameters.AddWithValue("@Eindtijd", SqlDbType.DateTime);
                    command.Parameters["@Eindtijd"].Value = evenement.Eindtijd;

                    command.Parameters.AddWithValue("@Starttijd", SqlDbType.DateTime);
                    command.Parameters["@Starttijd"].Value = evenement.Starttijd;

                    command.Parameters.AddWithValue("@Prijs", SqlDbType.Decimal);
                    command.Parameters["@Prijs"].Value = evenement.Prijs;

                    command.Parameters.AddWithValue("@Beschrijving", SqlDbType.VarChar);
                    command.Parameters["@Beschrijving"].Value = evenement.Beschrijving;


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public List<Evenement> GeefEvenementen()
        {
            List<Evenement> evenementen = new List<Evenement>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Evenementen";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = (string)reader["Id"];
                            string titel = (string)reader["Titel"];
                            DateTime eindtijd = (DateTime)reader["Eindtijd"];
                            DateTime starttijd = (DateTime)reader["Starttijd"];
                            decimal prijs = (decimal)reader["Prijs"];
                            string beschrijving = (string)reader["Beschrijving"];

                            Evenement evenement = new Evenement(id, titel, eindtijd, starttijd, prijs, beschrijving);
                            evenementen.Add(evenement);
                        }
                    }
                }
            }
            return evenementen;
        }
    }
}
