using System;
using Domein;
using Domein.DTOs;
using Domein.Exceptions;
using Domein.Interfaces;
using Domein.Models;
using Xunit;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Domein.Tests
{
    public class DomeinControllerTests
    {
        [Fact]
        public void MaakDagplan_GeldigeGebruiker_EnDatum_NieuwDagplanAangemaakt()
        {
            // Arrange
            int gebruikerId = 1;
            DateTime datum = DateTime.Now;
            var mockRepoE = new MockEvenementenRepo();
            var mockRepoG = new MockGebruikersRepo();
            var mockRepoD = new MockDagplanRepo();
            var domeinController = new DomeinController(mockRepoE, mockRepoG, mockRepoD);

            // Act
            var result = domeinController.MaakDagplan(1, gebruikerId, datum);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(gebruikerId, result.GebruikerId);
            Assert.Equal(datum, result.Datum);
            mockRepoD.AddDagplan(result);
        }

        [Fact]
        public void MaakDagplan_BestaandDagplanVoorGebruikerEnDatum_DagplanExceptionGegooid()
        {
            // Arrange
            int gebruikerId = 1;
            DateTime datum = DateTime.Now;
            var mockRepoE = new MockEvenementenRepo();
            var mockRepoG = new MockGebruikersRepo();
            var mockRepoD = new MockDagplanRepo();
            mockRepoD.BestaatDagplan(gebruikerId, datum);
            var domeinController = new DomeinController(mockRepoE, mockRepoG, mockRepoD);

            // Act & Assert
            Assert.Throws<DagplanException>(() => domeinController.MaakDagplan(1, gebruikerId, datum));
        }

        [Fact]
        public void MaakDagplan_OngeldigeGebruiker_GebruikerNietGevondenExceptionGegooid()
        {
            // Arrange
            int gebruikerId = 1;
            DateTime datum = DateTime.Now;
            var mockRepoE = new MockEvenementenRepo();
            var mockRepoG = new MockGebruikersRepo();
            var mockRepoD = new MockDagplanRepo();
            mockRepoG.GeefGebruiker(gebruikerId);
            var domeinController = new DomeinController(mockRepoE, mockRepoG, mockRepoD);

            // Act & Assert
            Assert.Throws<DagplanException>(() => domeinController.MaakDagplan(1, gebruikerId, datum));
        }

        // Voorbeeld van een mock repository voor IEvenementenRepo
        private class MockEvenementenRepo : IEvenementenRepo
        {
            // Implementeer de benodigde methoden en eigenschappen voor de mock

            public List<Evenement> GeefEvenementen()
            {
                throw new NotImplementedException();
            }

            public void AddEvenement(Evenement evenement)
            {
                throw new NotImplementedException();
            }
        }

        // Voorbeeld van een mock repository voor IGebruikersRepo
        private class MockGebruikersRepo : IGebruikersRepo
        {
            string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = GentseFeestenDB; Integrated Security = True; TrustServerCertificate=True";

            // Implementeer de benodigde methoden en eigenschappen voor de mock

            public List<Gebruiker> GeefGebruikers()
            {
                throw new NotImplementedException();
            }

            public Gebruiker GeefGebruiker(int gebruikerId)
            {
                try
                {
                    using (SqlConnection connection = new(connectionString))
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

            public void AddGebruiker(Gebruiker gebruiker)
            {
                throw new NotImplementedException();
            }
        }

        // Voorbeeld van een mock repository voor IDagplanRepo
        private class MockDagplanRepo : IDagplanRepo
        {
            string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = GentseFeestenDB; Integrated Security = True; TrustServerCertificate=True";


            // Implementeer de benodigde methoden en eigenschappen voor de mock

            public bool BestaatDagplan(int gebruikerId, DateTime datum)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
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

            public void AddDagplan(Dagplan dagplan)
            {
                try
                {
                    using (SqlConnection connection = new(connectionString))
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

            public void UpdateDagplan(Dagplan dagplan, string evenement1, string evenement2)
            {
                throw new NotImplementedException();
            }

            public Dagplan GeefDagplan(int dagplanId)
            {
                throw new NotImplementedException();
            }

            public List<DagplanDTO> GeefDagplanVanGebruiker(int gebruikerId)
            {
                throw new NotImplementedException();
            }

            public void DeleteDagplan(int dagplanId)
            {
                throw new NotImplementedException();
            }
        }

        public class DagplanException : Exception
        {
            public DagplanException(string message) : base(message)
            {
            }

            public DagplanException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}