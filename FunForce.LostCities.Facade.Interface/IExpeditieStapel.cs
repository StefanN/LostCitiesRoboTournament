using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{
    public interface IExpeditieStapel
    {
        int AantalKaarten{get;}
        IKaart GetKaart(int index);
        int GetScore();
    }
}
