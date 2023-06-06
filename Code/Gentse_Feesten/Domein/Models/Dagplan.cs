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
        public int GebruikerId { get; set; }
        public DateTime Datum { get; set; }
        //public decimal BeschikbaarBudget { get; set; }
        public List<DagplanEvenementen> Evenementen { get; set; }

        public Dagplan(int id, int gebruikerId, DateTime datum)
        {
            Id = id;
            GebruikerId = gebruikerId;
            Datum = datum;
            //BeschikbaarBudget = beschikbaarBudget;
            Evenementen = new List<DagplanEvenementen>();
        }
    }
}
