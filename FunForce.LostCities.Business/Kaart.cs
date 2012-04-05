using System;
using System.Collections.Generic;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
	/// <summary>
	/// Description of Kaart.
	/// </summary>
	public abstract class Kaart : IKaart
	{
		Kleur kleur;
		
        /// <summary>
        /// constructor voor kaart
        /// </summary>
        /// <param name="kleur"></param>
        public Kaart(Kleur kleur)
		{
			this.kleur = kleur;	
		}
		
        /// <summary>
        /// kleur van de kaart
        /// </summary>
		public Kleur Kleur
		{
			get{return this.kleur;}
		}

        public bool IsGedekt
        {
            get { return false; }
        }
	}
}
