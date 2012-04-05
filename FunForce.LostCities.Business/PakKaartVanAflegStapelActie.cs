using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class PakKaartVanAflegStapelActie : PakActie
    {
        private Kleur kleur;
        public PakKaartVanAflegStapelActie(Speler speler, Kleur kleur)
            : base(speler)
        {
            this.kleur = kleur;
        }

        /// <summary>
        /// geeft de kleur van de kaart die van de trekstapel gepakt moet worden
        /// </summary>
        public int KaartKleur
        {
            get { return (int)kleur; }
        }

        #region IPakKaartVanAflegstapelActie Members

        public Kleur Kleur
        {
            get { return kleur; }
        }

        #endregion
    }
}

