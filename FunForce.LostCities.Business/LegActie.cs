using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class LegActie: Actie
    {
        private int indexInHand;
        public LegActie(int indexInHand, Speler speler)
            : base(speler)
        {
            if (indexInHand < 0 || indexInHand > 7)
            {
                throw new LostCitiesException("geen geldige index");
            }
            this.indexInHand = indexInHand;
        }

        /// <summary>
        /// geeft de index van de kaart in de hand
        /// die de speler wil wegleggen
        /// </summary>
        public int IndexInHand
        {
            get { return indexInHand; }
        }
    }
}
