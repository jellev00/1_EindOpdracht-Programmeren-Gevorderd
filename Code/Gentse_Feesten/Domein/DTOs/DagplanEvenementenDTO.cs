﻿using Domein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.DTOs
{
    public class DagplanEvenementenDTO
    {
        public DagplanEvenementenDTO(int dagplanId, string id, string titel, DateTime eindtijd, DateTime starttijd, decimal prijs, string beschrijving)
        {
            DagplanID = dagplanId;
            Id = id;
            Titel = titel;
            Eindtijd = eindtijd;
            Starttijd = starttijd;
            Prijs = prijs;
            Beschrijving = beschrijving;
        }

        public int DagplanID { get; set; }
        public string Id { get; set; }
        public string Titel { get; set; }
        public DateTime Eindtijd { get; set; }
        public DateTime Starttijd { get; set; }
        public decimal Prijs { get; set; }
        public string Beschrijving { get; set; }
        public int rndId { get; set; }
    }
}
