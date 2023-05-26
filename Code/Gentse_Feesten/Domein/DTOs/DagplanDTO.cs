using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domein.DTOs
{
    public class DagplanDTO
    {
        public DagplanDTO(int id, int gebruikerId, DateTime datum, string evenement1, string evenement2)
        {
            Id = id;
            GebruikerId = gebruikerId;
            Datum = datum;
            Evenement1 = evenement1;
            Evenement2 = evenement2;
        }

        public int Id { get; set; }
        public int GebruikerId { get; set; }
        public DateTime Datum { get; set; }
        public string Evenement1 { get; set; }
        public string Evenement2 { get; set; }
    }
}
