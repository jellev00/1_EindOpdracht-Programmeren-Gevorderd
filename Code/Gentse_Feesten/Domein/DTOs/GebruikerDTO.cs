using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.DTOs
{
    public class GebruikerDTO
    {
        public GebruikerDTO(int id, string naam, string voornaam, decimal prijs)
        {
            Id = id;
            Naam = naam;
            Voornaam = voornaam;
            Prijs = prijs;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public decimal Prijs { get; set; }
    }
}
