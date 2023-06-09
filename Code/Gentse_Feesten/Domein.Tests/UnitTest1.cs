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
                Evenement evenement = new Evenement("1", "Evenement 1", new DateTime(2022, 6, 6, 10, 0, 0), new DateTime(2022, 6, 6, 14, 0, 0), 10, "Evenement 1 beschrijving");
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
                return new Gebruiker(gebruikerId, "Jelle", "Vandriessche", 60);
            }

            public List<Gebruiker> GeefGebruikers()
            {
                throw new NotImplementedException();
            }
        }

        private class FakeDagplanRepo : IDagplanRepo
        {
            private List<Dagplan> dagplannen = new List<Dagplan>();
            public bool BestaatDagplan(int gebruikerId, DateTime datum)
            {
                return dagplannen.Any(d => d.GebruikerId == gebruikerId && d.Datum.Date == datum.Date);
            }

            public void AddDagplan(Dagplan dagplan)
            {
                dagplannen.Add(dagplan);
            }

            public void DeleteDagplan(int dagplanId)
            {

            }

            public Dagplan GeefDagplan(int id)
            {
                return new Dagplan(id, 2, new DateTime(2022, 06, 06));
            }

            public Dagplan[] GeefDagplanVanGebruiker(int gebruikerId)
            {
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

            private List<DagplanEvenementenDTO> evenementen = new List<DagplanEvenementenDTO>();
            public void AddEvenement(DagplanEvenementen dagplanEvenement)
            {
                var evenementDTO = new DagplanEvenementenDTO(
                    dagplanEvenement.DagplanID,
                    dagplanEvenement.Id,
                    dagplanEvenement.Titel,
                    dagplanEvenement.Eindtijd,
                    dagplanEvenement.Starttijd,
                    dagplanEvenement.Prijs,
                    dagplanEvenement.Beschrijving
                );

                evenementen.Add(evenementDTO);
            }

            public void DeleteEvenementenOfDagplan(int dagplanId)
            {

            }

            public List<DagplanEvenementenDTO> GeefEvenementenVanDagplan(int dagplanId)
            {
                //List<DagplanEvenementenDTO> evenement = new List<DagplanEvenementenDTO>();
                //evenement.Add(new DagplanEvenementenDTO(1 ,"evenement123", "Test Evenement", new DateTime(2022, 6, 6, 10, 0, 0), new DateTime(2022, 6, 6, 14, 0, 0), 10, "Dit is een test evenement."));
                return evenementen;
            }

            public void DeleteEvenementVanDagplan(int dagplanId, string EvenementId)
            {
                throw new NotImplementedException();
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
            int gebruikerId = 2;
            DateTime datum = new DateTime(2022, 06, 06);

            // Act
            domeinController.MaakDagplan(dagplanId, gebruikerId, datum);

            // Assert
            Assert.NotNull(domeinController.GeefDagplan(dagplanId));
        }

        [Fact]
        public void Bestaat_Dagplan()
        {
            // Arrange
            var repoE = new FakeEvenementenRepo();
            var repoG = new FakeGebruikersRepo();
            var repoD = new FakeDagplanRepo();
            var repoDE = new FakeDagplanEvenementenRepo();
            var domeinController = new DomeinController(repoE, repoG, repoD, repoDE);
            int dagplanId = 1;
            int gebruikerId = 2;
            DateTime datum = new DateTime(2022, 06, 06);

            // Act
            repoD.AddDagplan(new Dagplan(dagplanId, gebruikerId, datum));

            // Assert
            Assert.Throws<DagplanException>(() => domeinController.MaakDagplan(dagplanId, gebruikerId, datum));
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
            int dagplanId = 3;
            int gebruikerId = 2;

            // Haal het dagplan op uit de repository
            Dagplan dagplan = new Dagplan(dagplanId, gebruikerId, new DateTime(2022, 06, 06));


            Gebruiker gebruiker = new Gebruiker(gebruikerId, "Jelle", "Vandriessche", 60);
            EvenementDTO evenement = new EvenementDTO("evenement12", "Test Evenement", new DateTime(2022, 6, 6, 10, 0, 0), new DateTime(2022, 6, 6, 14, 0, 0), 10, "Dit is een test evenement.");

            // Act
            domeinController.VoegEvenementToeAanDagplan(dagplanId, evenement);

            // Assert
        }

        [Fact]
        public void EvenementIsAlToegevoegd()
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
            DagplanEvenementen evenement1 = new DagplanEvenementen(dagplanId, "evenement123", "Test Evenement", new DateTime(2022, 6, 6, 10, 0, 0), new DateTime(2022, 6, 6, 14, 0, 0), 10, "Dit is een test evenement.");
            EvenementDTO evenement = new EvenementDTO("evenement123", "Test Evenement", new DateTime(2022, 6, 6, 10, 0, 0), new DateTime(2022, 6, 6, 14, 0, 0), 10, "Dit is een test evenement.");

            // Act
            repoDE.AddEvenement(evenement1);

            // Assert
            Assert.Throws<DagplanException>(() => domeinController.VoegEvenementToeAanDagplan(dagplanId, evenement));
        }

        [Fact]
        public void EvenementToegevoegdAanDagplan_minimum_30min_tussen()
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
            DagplanEvenementen evenement1 = new DagplanEvenementen(dagplanId, "evenement123", "Test Evenement", new DateTime(2022, 6, 6, 14, 0, 0), new DateTime(2022, 6, 6, 10, 0, 0), 10, "Dit is een test evenement.");
            EvenementDTO evenement2 = new EvenementDTO("evenement1", "Test Evenement", new DateTime(2022, 6, 6, 16, 0, 0), new DateTime(2022, 6, 6, 14, 15, 0), 10, "Dit is een test evenement.");

            // Act
            repoDE.AddEvenement(evenement1);

            // Assert
            Assert.Throws<DagplanException>(() => domeinController.VoegEvenementToeAanDagplan(dagplanId, evenement2));
        }

        [Fact]
        public void EvenementToegevoegdAanDagplan_overlapping()
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
            EvenementDTO evenement1 = new EvenementDTO("evenement123", "Test Evenement", new DateTime(2022, 6, 6, 10, 0, 0), new DateTime(2022, 6, 6, 14, 0, 0), 10, "Dit is een test evenement.");
            EvenementDTO evenement2 = new EvenementDTO("evenement123", "Test Evenement", new DateTime(2022, 6, 6, 11, 30, 0), new DateTime(2022, 6, 6, 16, 0, 0), 10, "Dit is een test evenement.");

            // Act
            domeinController.VoegEvenementToeAanDagplan(dagplanId, evenement1);

            // Assert
            Assert.Throws<DagplanException>(() => domeinController.VoegEvenementToeAanDagplan(dagplanId, evenement2));
        }

        [Fact]
        public void EvenementToegevoegdAanDagplan_Dagbudget_overschreiden()
        {
            // Arrange
            var repoE = new FakeEvenementenRepo();
            var repoG = new FakeGebruikersRepo();
            var repoD = new FakeDagplanRepo();
            var repoDE = new FakeDagplanEvenementenRepo();
            var domeinController = new DomeinController(repoE, repoG, repoD, repoDE);
            int dagplanId = 3;
            int gebruikerId = 2;

            // Haal het dagplan op uit de repository
            Dagplan dagplan = new Dagplan(dagplanId, gebruikerId, new DateTime(2022, 06, 06));


            Gebruiker gebruiker = new Gebruiker(gebruikerId, "Jelle", "Vandriessche", 60);
            EvenementDTO evenement = new EvenementDTO("evenement12", "Test Evenement", new DateTime(2022, 6, 6, 10, 0, 0), new DateTime(2022, 6, 6, 14, 0, 0), 70, "Dit is een test evenement.");

            // Act

            // Assert
            Assert.Throws<DagplanException>(() => domeinController.VoegEvenementToeAanDagplan(dagplanId, evenement));
        }

        [Fact]
        public void EvenementToegevoegdAanDagplan_Evenement_niet_op_zelfde_datum_als_dagplan()
        {
            // Arrange
            var repoE = new FakeEvenementenRepo();
            var repoG = new FakeGebruikersRepo();
            var repoD = new FakeDagplanRepo();
            var repoDE = new FakeDagplanEvenementenRepo();
            var domeinController = new DomeinController(repoE, repoG, repoD, repoDE);
            int dagplanId = 3;
            int gebruikerId = 2;

            // Haal het dagplan op uit de repository
            Dagplan dagplan = new Dagplan(dagplanId, gebruikerId, new DateTime(2022, 06, 06));


            Gebruiker gebruiker = new Gebruiker(gebruikerId, "Jelle", "Vandriessche", 60);
            EvenementDTO evenement = new EvenementDTO("evenement12", "Test Evenement", new DateTime(2022, 6, 7, 10, 0, 0), new DateTime(2022, 6, 7, 14, 0, 0), 70, "Dit is een test evenement.");

            // Act

            // Assert
            Assert.Throws<DagplanException>(() => domeinController.VoegEvenementToeAanDagplan(dagplanId, evenement));
        }
    }
}