using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Models
{
    public class Gebruiker
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public decimal Prijs { get; set; }

        public Gebruiker(int id, string naam, string voornaam, decimal prijs)
        {
            Id = id;
            Naam = naam;
            Voornaam = voornaam;
            Prijs = prijs;
        }
    }
}
