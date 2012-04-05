using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    /// <summary>
    /// Het spelbord waarop spelers hun kaarten 
    /// leggen.
    /// </summary>
    public abstract class Spelbord
    {
        protected Stapel[] stapels;

        public Spelbord()
        {
        }

        /// <summary>
        /// legt een kaart aan, aan de juiste stapel op het bord
        /// </summary>
        /// <param name="kaart">de kaart die wordt aangelegd</param>
        public virtual void LegKaartAan(Kaart kaart)
        {
            GetKleurStapel(kaart.Kleur).AddKaart(kaart);
        }

        public int AantalKaarten()
        {
            return AantalKaarten(Kleur.Wit) +
                    AantalKaarten(Kleur.Rood) +
                    AantalKaarten(Kleur.Geel) +
                    AantalKaarten(Kleur.Groen) +
                    AantalKaarten(Kleur.Blauw);
        }

        public int AantalKaarten(Kleur kleur)
        {
            return stapels[(int)kleur].AantalKaarten;
        }

        protected Stapel GetKleurStapel(Kleur kleur)
        {
            return stapels[(int)kleur];
        }

    }

}
