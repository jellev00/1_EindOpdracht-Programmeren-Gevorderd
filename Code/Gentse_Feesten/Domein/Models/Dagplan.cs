using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Models
{
    public class Dagplan
    {
        public int Id { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public int GebruikerId { get; set; }
        public DateTime Datum { get; set; }
        public string Evenement1 { get; set; }
        public string Evenement2 { get; set; }
        public List<Evenement> Evenementen { get; set; }
        public decimal TotaleKostprijs
        {
            get { return TotaleKostprijs; }
            set
            {
                Evenementen.Sum(e => e.Prijs);
            }
        }

        public Dagplan(int id, int gebruiker, DateTime datum)
        {
            Evenementen = new List<Evenement>();

            Id = id;
            GebruikerId = gebruiker;
            Datum = datum;
        }
    }
}
