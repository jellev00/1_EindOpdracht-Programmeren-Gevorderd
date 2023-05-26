using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domein.Models;

namespace Domein.Interfaces
{
    public interface IEvenementenRepo
    {
        List<Evenement> GeefEvenementen();
        void AddEvenement(Evenement evenement);
    }
}
