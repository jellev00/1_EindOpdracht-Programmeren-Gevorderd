using Domein.DTOs;
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
    public class DagplanEvenmentenRepoDb : IDagplanEvenementenRepo
    {
        private string _connectionString;
        public DagplanEvenmentenRepoDb(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddEvenement(DagplanEvenementen dagplanEvenementen)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO DagplanEvenementen (DagplanId, Id, Titel, Eindtijd, Starttijd, Prijs, Beschrijving) VALUES (@DagplanId, @Id, @Titel, @Eindtijd, @Starttijd, @Prijs, @Beschrijving)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DagplanId", SqlDbType.Int);
                    command.Parameters["@DagplanId"].Value = dagplanEvenementen.DagplanID;

                    command.Parameters.AddWithValue("@Id", SqlDbType.VarChar);
                    command.Parameters["@Id"].Value = dagplanEvenementen.Id;

                    command.Parameters.AddWithValue("@Titel", SqlDbType.VarChar);
                    command.Parameters["@Titel"].Value = dagplanEvenementen.Titel;

                    command.Parameters.AddWithValue("@Eindtijd", SqlDbType.DateTime);
                    command.Parameters["@Eindtijd"].Value = dagplanEvenementen.Eindtijd;

                    command.Parameters.AddWithValue("@Starttijd", SqlDbType.DateTime);
                    command.Parameters["@Starttijd"].Value = dagplanEvenementen.Starttijd;

                    command.Parameters.AddWithValue("@Prijs", SqlDbType.Decimal);
                    command.Parameters["@Prijs"].Value = dagplanEvenementen.Prijs;

                    command.Parameters.AddWithValue("@Beschrijving", SqlDbType.VarChar);
                    command.Parameters["@Beschrijving"].Value = dagplanEvenementen.Beschrijving;


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public List<DagplanEvenementenDTO> GeefEvenementenVanDagplan(int DagplanID)
        {
            List<DagplanEvenementenDTO> dagplanEvenementen = new List<DagplanEvenementenDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string selectSql = "SELECT * FROM DagplanEvenementen WHERE DagplanId = @DagplanID;";
                    SqlCommand selectCommand = new SqlCommand(selectSql, connection);
                    selectCommand.Parameters.AddWithValue("@DagplanID", DagplanID);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = (string)reader["Id"];
                            string titel = (string)reader["Titel"];
                            DateTime eindtijd = (DateTime)reader["Eindtijd"];
                            DateTime starttijd = (DateTime)reader["Starttijd"];
                            decimal prijs = (decimal)reader["Prijs"];
                            string beschrijving = (string)reader["Beschrijving"];

                            DagplanEvenementenDTO dagplanevenementen = new DagplanEvenementenDTO(DagplanID, id, titel, eindtijd, starttijd, prijs, beschrijving);
                            dagplanEvenementen.Add(dagplanevenementen);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return dagplanEvenementen;
        }

        public void DeleteEvenementenOfDagplan(int dagplanId)
        {
            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();

                    string deleteSQL = $"DELETE from DagplanEvenementen WHERE DagplanId = @DagplanId;";

                    SqlCommand deleteCommand = new(deleteSQL, connection);
                    deleteCommand.Parameters.AddWithValue("@DagplanId", dagplanId);

                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEvenementVanDagplan(string EvenementId)
        {
            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();

                    string deleteSQL = $"DELETE from DagplanEvenementen WHERE Id = @Id;";

                    SqlCommand deleteCommand = new(deleteSQL, connection);
                    deleteCommand.Parameters.AddWithValue("@Id", EvenementId);

                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
