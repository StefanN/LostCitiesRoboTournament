using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Business.Tests
{
    /// <summary>
    /// naar een idee van Robert Lelieveld. Wanneer twee
    /// domme spelers een spel spelen, spelen ze niet vals, 
    /// maar zal er niemand winnen
    /// </summary>
    public class DommeSpeler : Speler
    {
        public DommeSpeler()
        {
        }

        /// <summary>
        /// Bepaalt de actie die de speler wil uitvoeren
        /// </summary>
        /// <returns></returns>
        public new Beurt BepaalActies()
        {
            //voor nu, leg altijd eerste kaart weg....
            return new Beurt(new LegKaartWegActie(0, this), new PakKaartVanTrekStapelActie(this));
        }
    }
}
