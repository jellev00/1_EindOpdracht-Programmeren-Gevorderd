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
    public class EvenementRepoBestand : IEvenementenRepo
    {
        private string _connectionString;
        private List<Evenement> _evenementen;
        public EvenementRepoBestand(string bestand, string connectionString)
        {
            _connectionString = connectionString;
            _evenementen = new List<Evenement>();

            try
            {
                using (var reader = new StreamReader(bestand))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(';');

                        // Type omzetten
                        Evenement evenement = new Evenement(
                            values[0],
                            values[1].Trim('\"'),
                            DateTime.Parse(values[2]), // .Trim('\'')
                            DateTime.Parse(values[3]), // .Trim('\'')
                            decimal.Parse(values[4]),
                            values[5].Trim('\"')
                            );
                        _evenementen.Add(evenement);
                        AddEvenement(evenement);
                    }
                }

            }
            catch
            {
                throw;
            }

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
            throw new NotImplementedException();
        }
    }
}
