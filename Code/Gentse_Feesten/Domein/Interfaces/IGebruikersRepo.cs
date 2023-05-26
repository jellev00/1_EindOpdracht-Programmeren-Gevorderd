using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domein.Models;

namespace Domein.Interfaces
{
    public interface IGebruikersRepo
    {
        Gebruiker GeefGebruiker(int gebruikerId);
        List<Gebruiker> GeefGebruikers();
        void AddGebruiker(Gebruiker gebruiker);
    }
}
