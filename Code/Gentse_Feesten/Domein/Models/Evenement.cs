﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Models
{
    public class Evenement
    {
        public string Id { get; set; }
        public string Titel { get; set; }
        public DateTime Eindtijd { get; set; }
        public DateTime Starttijd { get; set; }
        public decimal Prijs { get; set; }
        public string Beschrijving { get; set; }

        public Evenement(string id, string titel, DateTime eindtijd, DateTime starttijd, decimal prijs, string beschrijving)
        {
            Id = id;
            Titel = titel;
            Eindtijd = eindtijd;
            Starttijd = starttijd;
            Prijs = prijs;
            Beschrijving = beschrijving;
        }
    }
}
