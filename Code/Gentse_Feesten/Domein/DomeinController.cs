using Domein.DTOs;
using Domein.Interfaces;
using Domein.Models;
using Domein.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace Domein
{
    public class DomeinController
    {
        private IGebruikersRepo _repoG;
        private IEvenementenRepo _repoE;
        private IDagplanRepo _repoD;
        private IDagplanEvenementenRepo _repoDE;

        public DomeinController(IEvenementenRepo repoE, IGebruikersRepo repoG, IDagplanRepo repoD, IDagplanEvenementenRepo repoDE)
        {
            _repoE = repoE;
            _repoG = repoG;
            _repoD = repoD;
            _repoDE = repoDE;
        }

        public void MaakDagplan(int id, int gebruikerId, DateTime datum)
        {
            // Controleer of de gebruiker al een dagplan heeft voor dezelfde dag
            if (_repoD.BestaatDagplan(gebruikerId, datum))
            {
                throw new DagplanException("Er bestaat al een dagplan voor deze dag.");
            }

            // Maak het nieuwe dagplan aan
            Dagplan dagplan = new Dagplan(id, gebruikerId, datum);
            _repoD.AddDagplan(dagplan);
        }

        public void VoegEvenementToeAanDagplan(int dagplanId, Evenement evenement)
        {
            // Haal het dagplan en evenement op
            Dagplan dagplan = _repoD.GeefDagplan(dagplanId);
            Gebruiker gebruiker = _repoG.GeefGebruiker(dagplan.GebruikerId);
            //Evenement evenement = _repoE.GeefEvenementen().FirstOrDefault(e => e.Id == evenementId);

            if (dagplan == null || evenement == null)
            {
                throw new DagplanException("Ongeldig dagplan of evenement.");
            }

            // Controleer of het evenement al aanwezig is in het dagplan
            foreach (DagplanEvenementenDTO evenementen in GeefEvenementenVanDagplan(dagplanId))
            {
                if (evenementen.Id == evenement.Id)
                {
                    throw new DagplanException("Dit evenement is al toegevoegd aan het dagplan.");
                }
            }

            //// Controleer of het evenement overlapt met andere evenementen in het dagplan
            foreach (DagplanEvenementenDTO evenementen in GeefEvenementenVanDagplan(dagplanId))
            {
                if (OverlappingEvenementen(evenementen, evenement))
                {
                    throw new DagplanException("Dit evenement overlapt met een ander evenement in het dagplan.");

                }
            }

            // Controleer of het evenement op dezelfde datum plaatsvindt als het dagplan
            if (evenement.Starttijd.Date != dagplan.Datum.Date)
            {
                throw new DagplanException("Dit evenement vindt niet plaats op dezelfde datum als het dagplan.");
            }

            // Controleer of er minstens 30 minuten tussen evenementen zitten
            if (!IsErMinstens30MinutenVerschil(dagplanId, evenement))
            {
                throw new DagplanException("Er moet minstens 30 minuten tussen evenementen zitten.");
            }

            // Controleer of de totale kosten van de evenementen het beschikbare budget overschrijden
            decimal totaleKosten = dagplan.Evenementen.Sum(e => e.Prijs) + evenement.Prijs;
            if (totaleKosten > gebruiker.Prijs)
            {
                throw new DagplanException("De totale kosten van de evenementen overschrijden het beschikbare budget van het dagplan.");
            }

            // Voeg het evenement toe aan het dagplan
            DagplanEvenementen dagplanEvenement = new DagplanEvenementen(dagplanId, evenement.Id, evenement.Titel, evenement.Eindtijd, evenement.Starttijd, evenement.Prijs, evenement.Beschrijving);
            _repoDE.AddEvenement(dagplanEvenement);
        }

        private bool OverlappingEvenementen(DagplanEvenementenDTO evenement1, Evenement evenement2)
        {
            return OverlappingEvenementen(evenement1, evenement2.Starttijd, evenement2.Eindtijd);
        }

        private bool OverlappingEvenementen(DagplanEvenementenDTO evenement, DateTime starttijd, DateTime eindtijd)
        {
            return (starttijd >= evenement.Starttijd && starttijd < evenement.Eindtijd) ||
                   (eindtijd > evenement.Starttijd && eindtijd <= evenement.Eindtijd) ||
                   (evenement.Starttijd >= starttijd && evenement.Starttijd < eindtijd) ||
                   (evenement.Eindtijd > starttijd && evenement.Eindtijd <= eindtijd);
        }

        private bool IsErMinstens30MinutenVerschil(int dagplanId, Evenement nieuwEvenement)
        {
            List<DagplanEvenementenDTO> evenementen = GeefEvenementenVanDagplan(dagplanId);

            foreach (DagplanEvenementenDTO evenement in evenementen)
            {
                var minuten1 = nieuwEvenement.Starttijd - evenement.Eindtijd;
                var minuten2 = evenement.Starttijd - nieuwEvenement.Eindtijd;

                if (evenement.Eindtijd < nieuwEvenement.Starttijd)
                {
                    if (minuten1.TotalMinutes < 30)
                    {
                        return false;
                    }
                } else if (nieuwEvenement.Eindtijd < evenement.Starttijd)
                {
                    if (minuten2.TotalMinutes < 30)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Verwijderen van een dagplan
        public void VerwijderDagplan(int dagplanId)
        {
            _repoD.DeleteDagplan(dagplanId);
        }

        // Verwijderen Evenementen van Dagplan
        public void VerwijderEvenemetenVanDagplan(int dagplanId)
        {
            _repoDE.DeleteEvenementenOfDagplan(dagplanId);
        }

        public void VerwijderEvenementVanDagplan(int dagplanId, string evenementId)
        {
            _repoDE.DeleteEvenementVanDagplan(dagplanId, evenementId);
        }

        // De dagplannen van een gebruiker opvragen
        public List<DagplanDTO> GeefDagplannenVoorGebruiker(int gebruikerId)
        {
            return _repoD.GeefDagplanVanGebruiker(gebruikerId)
                .Select(Dagplan => new DagplanDTO(Dagplan.Id, Dagplan.GebruikerId, Dagplan.Datum))
                .ToList();
        }

        public Gebruiker GeefGebruiker(int gebruikerId)
        {
            Gebruiker gebruiker = _repoG.GeefGebruiker(gebruikerId);
            return gebruiker;
        }

        public Dagplan GeefDagplan(int Id)
        {
            Dagplan dagplan = _repoD.GeefDagplan(Id);
            return dagplan;
        }

        public List<GebruikerDTO> GeefGebruikers()
        {
            return _repoG.GeefGebruikers()
                .Select(gebruiker => new GebruikerDTO(gebruiker.Id, gebruiker.Naam, gebruiker.Voornaam, gebruiker.Prijs))
                .ToList();
        }

        public List<Evenement> GeefEvenementen()
        {
            return _repoE.GeefEvenementen()
                .Select(evenement => new Evenement(evenement.Id, evenement.Titel, evenement.Eindtijd, evenement.Starttijd, evenement.Prijs, evenement.Beschrijving))
                .ToList();
        }

        public List<DagplanEvenementenDTO> GeefEvenementenVanDagplan(int dagplanId)
        {
            return _repoDE.GeefEvenementenVanDagplan(dagplanId)
                .Select(evenement => new DagplanEvenementenDTO(dagplanId, evenement.Id, evenement.Titel, evenement.Eindtijd, evenement.Starttijd, evenement.Prijs, evenement.Beschrijving))
                .ToList();
        }
    }
}
