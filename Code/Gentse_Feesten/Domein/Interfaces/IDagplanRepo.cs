using Domein.DTOs;
using Domein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Interfaces
{
    public interface IDagplanRepo
    {
        bool BestaatDagplan(int gebruikerId, DateTime datum);
        Dagplan GeefDagplan(int dagplanId);
        List<DagplanDTO> GeefDagplanVanGebruiker(int gebruikerId);
        void AddDagplan(Dagplan dagplan);
        void DeleteDagplan(int dagplanId);
        void UpdateDagplan(Dagplan dagplan, string evenement1, string evenement2);
    }
}
