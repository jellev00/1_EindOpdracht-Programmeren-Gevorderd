using System;
using Domein;
using Domein.DTOs;
using Domein.Exceptions;
using Domein.Interfaces;
using Domein.Models;
using Xunit;

namespace Domein.Tests
{
    public class DomeinControllerTests
    {
        private class FakeEvenementenRepo : IEvenementenRepo
        {
            public void AddEvenement(Evenement evenement)
            {
                throw new NotImplementedException();
            }

            List<Evenement> IEvenementenRepo.GeefEvenementen()
            {
                Evenement evenement = new Evenement("1", "Evenement 1", DateTime.Now, DateTime.Now.AddHours(1), 10, "Evenement 1 beschrijving");
                return new List<Evenement> { evenement };
            }
        }

        private class FakeGebruikersRepo : IGebruikersRepo
        {
            public void AddGebruiker(Gebruiker gebruiker)
            {
                throw new NotImplementedException();
            }

            public Gebruiker GeefGebruiker(int gebruikerId)
            {
                // Geef hier een nepgebruiker terug voor testdoeleinden
                return new Gebruiker(gebruikerId, "Jelle", "Vandriessche", 60);
            }

            public List<Gebruiker> GeefGebruikers()
            {
                throw new NotImplementedException();
            }
        }

        private class FakeDagplanRepo : IDagplanRepo
        {
            public bool BestaatDagplan(int gebruikerId, DateTime datum)
            {
                // Implementeer hier de logica om te controleren of een dagplan bestaat voor testdoeleinden
                // Voor deze test kunnen we altijd 'false' retourneren
                return false;
            }

            public void AddDagplan(Dagplan dagplan)
            {
                // Implementeer hier de logica om een dagplan toe te voegen voor testdoeleinden
            }

            public void DeleteDagplan(int dagplanId)
            {
                // Implementeer hier de logica om een dagplan te verwijderen voor testdoeleinden
            }

            public Dagplan GeefDagplan(int id)
            {
                // Implementeer hier de logica om een dagplan op te halen voor testdoeleinden
                // Voor deze test kunnen we altijd 'null' retourneren
                return new Dagplan(id, 2, new DateTime(2022, 06, 06));
            }

            public Dagplan[] GeefDagplanVanGebruiker(int gebruikerId)
            {
                // Implementeer hier de logica om de dagplannen van een gebruiker op te halen voor testdoeleinden
                // Voor deze test kunnen we een lege array retourneren
                return new Dagplan[0];
            }

            List<DagplanDTO> IDagplanRepo.GeefDagplanVanGebruiker(int gebruikerId)
            {
                throw new NotImplementedException();
            }

            public void UpdateDagplan(Dagplan dagplan)
            {
                throw new NotImplementedException();
            }
        }

        private class FakeDagplanEvenementenRepo : IDagplanEvenementenRepo
        {
            public void AddEvenement(DagplanEvenementen dagplanEvenement)
            {
                // Implementeer hier de logica om een evenement aan een dagplan toe te voegen voor testdoeleinden
            }

            public void DeleteEvenementVanDagplan(string evenementId)
            {
                // Implementeer hier de logica om een evenement van een dagplan te verwijderen voor testdoeleinden
            }

            public void DeleteEvenementenOfDagplan(int dagplanId)
            {
                // Implementeer hier de logica om alle evenementen van een dagplan te verwijderen voor testdoeleinden
            }

            public List<DagplanEvenementenDTO> GeefEvenementenVanDagplan(int dagplanId)
            {
                // Implementeer hier de logica om de evenementen van een dagplan op te halen voor testdoeleinden
                // Voor deze test kunnen we een lege lijst retourneren
                return new List<DagplanEvenementenDTO>();
            }
        }

        [Fact]
        public void MaakDagplan_DagplanToegevoegd()
        {
            // Arrange
            var repoE = new FakeEvenementenRepo();
            var repoG = new FakeGebruikersRepo();
            var repoD = new FakeDagplanRepo();
            var repoDE = new FakeDagplanEvenementenRepo();
            var domeinController = new DomeinController(repoE, repoG, repoD, repoDE);
            int dagplanId = 1;
            int gebruikerId = 1;
            DateTime datum = DateTime.Now;

            // Act
            domeinController.MaakDagplan(dagplanId, gebruikerId, datum);

            // Assert
            // Voeg hier je asserties toe om te controleren of het dagplan correct is toegevoegd
        }

        [Fact]
        public void EvenementToegevoegdAanDagplan()
        {
            // Arrange
            var repoE = new FakeEvenementenRepo();
            var repoG = new FakeGebruikersRepo();
            var repoD = new FakeDagplanRepo();
            var repoDE = new FakeDagplanEvenementenRepo();
            var domeinController = new DomeinController(repoE, repoG, repoD, repoDE);
            int dagplanId = 1;
            int gebruikerId = 2;

            // Haal het dagplan op uit de repository
            Dagplan dagplan = new Dagplan(dagplanId, gebruikerId, new DateTime(2022, 06, 06));


            Gebruiker gebruiker = new Gebruiker(gebruikerId, "Jelle", "Vandriessche", 60);
            Evenement evenement = new Evenement("evenement123", "Test Evenement", new DateTime(2022, 6, 6, 10, 0, 0), new DateTime(2022, 6, 6, 14, 0, 0), 10, "Dit is een test evenement.");

            // Act
            domeinController.VoegEvenementToeAanDagplan(dagplanId, evenement);

            // Assert
            // Voeg hier je asserties toe om te controleren of het evenement correct is toegevoegd aan het dagplan
        }

        // Voeg hier meer testmethoden toe om andere functionaliteiten van de DomeinController te testen
    }
}