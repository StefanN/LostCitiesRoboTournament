using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{
    public interface ILaatsteBeurt
    {
        IKaart LegKaart { get; set; }
        
        IStapel LegStapel { get; set; }
        
        IKaart PakKaart { get; set; }

        IStapel PakStapel { get; set; }

    }
}
