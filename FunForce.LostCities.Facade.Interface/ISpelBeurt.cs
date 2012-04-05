using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{

    public enum LegBord
    {
        AflegBord,
        ExpeditieBord
    }

    public enum PakStapel
    {
        AflegStapel,
        TrekStapel
    }

    public interface ISpelBeurt
    {
                /// <summary>
        /// index van de kaart in de hand om weg te leggen
        /// </summary>
        int KaartIndexLegActie {get;}
        /// <summary>
        /// bord waarop kaart weggelegd moet worden
        /// </summary>
        LegBord LegBord { get;}

        /// <summary>
        /// stapel waarop kaart moet worden weggelegd
        /// </summary>
        PakStapel PakStapel { get;}

        /// <summary>
        /// kleur van stapel waarop kaart moet worden afgelegd
        /// </summary>
        Kleur PakKleurAflegStapel { get;}
    }
}
