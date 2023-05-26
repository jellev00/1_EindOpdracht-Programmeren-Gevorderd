using Domein.DTOs;
using Domein.Interfaces;
using Domein.Models;
using Domein.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein
{
    public class DomeinController
    {
        private IGebruikersRepo _repoG;
        private IEvenementenRepo _repoE;
        private IDagplanRepo _repoD;

        public DomeinController(IEvenementenRepo repoE, IGebruikersRepo repoG, IDagplanRepo repoD)
        {
            _repoE = repoE;
            _repoG = repoG;
            _repoD = repoD;
        }

        public Dagplan MaakDagplan(int id, int gebruikerId, DateTime datum)
        {
            // Controleer of de gebruiker al een dagplan heeft voor de opgegeven datum
            if (_repoD.BestaatDagplan(gebruikerId, datum))
            {
                throw new DagplanException("Er bestaat al een dagplan voor de opgegeven gebruiker en datum.");
            }

            // Haal de gebruiker op uit de DB
            Gebruiker gebruiker = _repoG.GeefGebruiker(gebruikerId);
            if (gebruiker == null)
            {
                throw new DagplanException("Gebruiker niet gevonden.");
            }

            // Maak een nieuw dagplan aan
            Dagplan dagplan = new Dagplan(id, gebruikerId, datum)
            {
                Id = id,
                GebruikerId = gebruikerId,
                Datum = datum,
                Gebruiker = gebruiker
            };

            _repoD.AddDagplan(dagplan);
            return dagplan;
        }

        public void VoegEvenementToeAanDagplan(int ID, int IDGebruiker, Dagplan dagplan, Evenement evenement1, Evenement evenement2)
        {
            // Kijken of evenement 1 op de zelfde dag van het dagplan plaats vindt.

            var dagplanGebruiker = GeefDagplan(ID);

            if (evenement1.Starttijd.Date != dagplanGebruiker.Datum)
            {
                throw new DagplanException("Evenement 1 vindt niet plaats op de zelfde dag als het dagplan.");
            }

            // Kijken of evenement 2 op de zelfde dag van het dagplan plaats vindt.
            if (evenement2.Starttijd.Date != dagplan.Datum.Date)
            {
                throw new DagplanException("Evenement 2 vindt niet plaats op de zelfde dag als het dagplan.");
            }

            // Kijken voor overlappende evenementen
            if (OverlappingEvenementen(evenement1, evenement2))
            {
                throw new DagplanException("De evenementen overlappen elkaar.");
            }

            // kijken of alle evenementen niet over het dagbudget zijn
            decimal totaleKostPrijs = evenement1.Prijs + evenement2.Prijs;
            Gebruiker gebruiker = _repoG.GeefGebruiker(IDGebruiker);
            if (totaleKostPrijs > gebruiker.Prijs)
            {
                throw new DagplanException("de prijs van de evenementen is hoger dan het dagbudget van de gebruiker.");
            }

            string StringEvenement1 = $"{evenement1.Id}, {evenement1.Titel}, {evenement1.Starttijd}, {evenement1.Eindtijd}, {evenement1.Prijs}, {evenement1.Beschrijving}";
            string StringEvenement2 = $"{evenement2.Id}, {evenement2.Titel}, {evenement2.Starttijd}, {evenement2.Eindtijd}, {evenement2.Prijs}, {evenement2.Beschrijving}";

            _repoD.UpdateDagplan(dagplan, StringEvenement1, StringEvenement2);

            // evenement toevoegen aan dagplan
            dagplan.Evenementen.Add(evenement1);
            dagplan.Evenementen.Add(evenement2);
        }

        private bool OverlappingEvenementen(Evenement evenement1, Evenement evenement2)
        {
            return evenement1.Starttijd < evenement2.Eindtijd && evenement2.Starttijd < evenement1.Eindtijd;
        }

        // Verwijderen van een evenement van het dagplan
        public void VerwijderEvenementVanDagplan(Dagplan dagplan, Evenement evenement)
        {
            dagplan.Evenementen.Remove(evenement);
        }
        
         // Verwijderen van een dagplan
        public void VerwijderDagplan(int dagplanId)
        {
            _repoD.DeleteDagplan(dagplanId);
        }

        // De totale prijs berekenen
        public decimal BerekenTotaleKostPrijs(Dagplan dagplan)
        {
            decimal totaleKostPrijs = dagplan.Evenementen.Sum(e => e.Prijs);
            return totaleKostPrijs;
        }

        // De dagplannen van een gebruiker opvragen
        public List<DagplanDTO> GeefDagplannenVoorGebruiker(int gebruikerId)
        {
            return _repoD.GeefDagplanVanGebruiker(gebruikerId)
                .Select(Dagplan => new DagplanDTO(Dagplan.Id, Dagplan.GebruikerId, Dagplan.Datum, Dagplan.Evenement1, Dagplan.Evenement2))
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

        public decimal BerekenResterendDagbudget(Dagplan dagplan)
        {
            decimal resterendDagbudget = dagplan.Gebruiker.Prijs - BerekenTotaleKostPrijs(dagplan);
            return resterendDagbudget;
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
    }
}
