using Domein.Models;
using Domein.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domein.DTOs;

namespace Persistentie
{
    public class DagplanRepoDb : IDagplanRepo
    {
        private string _connectionString;
        public DagplanRepoDb(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddDagplan(Dagplan dagplan)
        {
            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();

                    string insertSql = $"INSERT INTO Dagplannen (Id, GebruikerId, Datum) VALUES (@Id, @GebruikerId, @Datum);";
                    SqlCommand insertCommand = new(insertSql, connection);

                    insertCommand.Parameters.Add("@Id", SqlDbType.Int);
                    insertCommand.Parameters["@Id"].Value = dagplan.Id;

                    insertCommand.Parameters.Add("@GebruikerId", SqlDbType.Int);
                    insertCommand.Parameters["@GebruikerId"].Value = dagplan.GebruikerId;

                    insertCommand.Parameters.Add("@Datum", SqlDbType.DateTime);
                    insertCommand.Parameters["@Datum"].Value = dagplan.Datum;

                    insertCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool BestaatDagplan(int gebruikerId, DateTime datum)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string selectSql = "SELECT COUNT(*) FROM Dagplannen WHERE GebruikerId = @GebruikerId AND Datum = @Datum;";
                    SqlCommand selectCommand = new SqlCommand(selectSql, connection);

                    selectCommand.Parameters.AddWithValue("@GebruikerId", gebruikerId);
                    selectCommand.Parameters.AddWithValue("@Datum", datum);

                    int count = (int)selectCommand.ExecuteScalar();
                    connection.Close();

                    return count > 0;
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteDagplan(int dagplanId)
        {
            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();

                    string deleteSQL = $"DELETE from Dagplannen WHERE Id = @Id;";

                    SqlCommand deleteCommand = new(deleteSQL, connection);
                    deleteCommand.Parameters.AddWithValue("@Id", dagplanId);

                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public Dagplan GeefDagplan(int dagplanId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string selectSql = "SELECT * FROM Dagplannen WHERE Id = @Id;";
                    SqlCommand selectCommand = new SqlCommand(selectSql, connection);
                    selectCommand.Parameters.AddWithValue("@Id", dagplanId);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dagplan dagplan = new Dagplan(dagplanId, (int)reader["GebruikerID"], (DateTime)reader["Datum"])
                            {
                                //Id = (int)reader["Id"],
                                //Gebruiker = (Gebruiker)reader["GebruikerId"],
                                //Datum = (DateTime)reader["Datum"],
                                //TotaleKostprijs = (decimal)reader["Prijs"]
                            };

                            return dagplan;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return null; // Als het dagplan niet gevonden wordt, geef null terug
        }

        public List<DagplanDTO> GeefDagplanVanGebruiker(int gebruikerId)
        {
            List<DagplanDTO> dagplannen = new List<DagplanDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string selectSql = "SELECT * FROM Dagplannen WHERE GebruikerId = @GebruikerId;";
                    SqlCommand selectCommand = new SqlCommand(selectSql, connection);
                    selectCommand.Parameters.AddWithValue("@GebruikerId", gebruikerId);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = (int)reader["Id"];
                            DateTime datum = (DateTime)reader["Datum"];
                            string evenement1 = (string)reader["Evenement1"];
                            string evenement2 = (string)reader["Evenement2"];

                            DagplanDTO dagplan = new DagplanDTO(id, gebruikerId, datum, evenement1, evenement2);
                            dagplannen.Add(dagplan);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return dagplannen;
        }

        public void UpdateDagplan(Dagplan dagplan, string evenement1, string evenement2)
        {
            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();

                    string updateSQL = $"UPDATE Dagplannen SET Evenement1 = @Evenement1, Evenement2 = @Evenement2 where Id = @Id;";
                    SqlCommand command = new(updateSQL, connection);

                    command.Parameters.AddWithValue("@Id", dagplan.Id);
                    command.Parameters.AddWithValue("@Evenement1", evenement1);
                    command.Parameters.AddWithValue("@Evenement2", evenement2);

                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
