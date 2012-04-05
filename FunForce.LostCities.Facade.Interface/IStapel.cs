using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{
    public interface IStapel
    {
        int AantalKaarten { get;}
        IKaart this[int index] { get;}
    }
}
