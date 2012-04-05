using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    /// <summary>
    /// geeft actie aan die speler uitvoert.
    /// </summary>
    public abstract class Actie
    {
        private Speler _speler;
 
        public Speler Speler
        {
            get { return _speler; }
        }
	
        public Actie(Speler speler)
        {
            _speler = speler;
        }

    }
}
