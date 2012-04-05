using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Presentation.Client
{
    /// <summary>
    /// Trekstapel ViewModel. 
    /// </summary>
    public class TrekStapelViewModel : BaseViewModel
    {
        int _aantalKaarten;
        KaartViewModel _bovensteKaart;

        /// <summary>
        /// Defaultonstructor
        /// </summary>
        public TrekStapelViewModel() : base()
        {
            this.BovensteKaart = null;
        }

        /// <summary>
        /// Verwerk het nieuwe aantal kaarten. Set aantal en toon al dan de achterkant van een kaart 
        /// </summary>
        /// <param name="aantalKaarten">Aantal kaarten van de trekstapel</param>
        public void VerwerkBeurt(int aantalKaarten)
        {

            this.AantalKaarten = aantalKaarten;

            if (aantalKaarten == 0)
            {
                this.BovensteKaart = null;
            }
            else
            {
                this.BovensteKaart = new KaartViewModel(null);
            }
        }

        /// <summary>
        /// Property: Aantal kaarten van de trekstapel
        /// </summary>
        public int AantalKaarten
        {
            get { return _aantalKaarten; }
            set { _aantalKaarten = value; OnPropertyChanged("AantalKaarten"); }
        }

        /// <summary>
        /// Property: De achterkant van de bovenste kaart
        /// </summary>
        public KaartViewModel BovensteKaart
        {
            get { return _bovensteKaart; }
            set { _bovensteKaart = value; OnPropertyChanged("BovensteKaart"); }
        }

    }
}
