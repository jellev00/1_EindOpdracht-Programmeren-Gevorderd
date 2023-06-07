using Domein.DTOs;
using Domein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Interfaces
{
    public interface IDagplanEvenementenRepo
    {
        List<DagplanEvenementenDTO> GeefEvenementenVanDagplan(int DagplanID);
        void AddEvenement(DagplanEvenementen dagplanEvenementen);
        void DeleteEvenementenOfDagplan(int dagplanId);
        void DeleteEvenementVanDagplan(int dagplanId, string EvenementId);
    }
}
