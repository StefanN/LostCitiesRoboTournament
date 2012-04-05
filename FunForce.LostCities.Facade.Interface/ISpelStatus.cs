using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{
    public interface ISpelStatus
    {
        /// <summary>
        /// score van de speler
        /// </summary>
        int ScoreTegenstander { get; } 
        
        /// <summary>
        /// score van de tegenstander
        /// </summary>
        int Score{ get; } 

        /// <summary>
        /// de hand met kaarten van de speler
        /// </summary>
        /// <returns></returns>
        IStapel Hand { get; }

        /// <summary>
        /// het aantal kaarten dat nog op de trekstapel aanwezig is
        /// </summary>
        /// <returns></returns>
        int AantalKaartenOpTrekstapel { get; }

        /// <summary>
        /// het bord waarop beide spelers kaarten afleggen
        /// </summary>
        /// <returns></returns>
        IAflegBord AflegBord { get; }

        /// <summary>
        /// het expeditiebord van de speler
        /// </summary>
        /// <returns></returns>
        IExpeditieBord Bord { get; }

        /// <summary>
        /// het expeditiebord van de tegenstander
        /// </summary>
        /// <returns></returns>
        IExpeditieBord BordTegenstander { get;}
    }
}
