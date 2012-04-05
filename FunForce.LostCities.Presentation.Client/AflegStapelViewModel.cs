using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Presentation.Client
{
    /// <summary>
    /// Aflegstapel ViewModel. 
    /// </summary>
    public class AflegStapelViewModel : BaseViewModel
    {

        int _aantalKaarten;
        KaartViewModel _bovensteKaart;
        Kleur _kleur;
        ColorSelector selector = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kleur">Kleur van de aflegstapel</param>
        public AflegStapelViewModel(Kleur kleur) : base()
        {
            _kleur = kleur;
            SetKleur(kleur);
            this.AantalKaarten = 0;
            this.BovensteKaart = null;
        }

        /// <summary>
        /// Verwerk beurt in de Aflegstapel
        /// </summary>
        /// <param name="sessie">de sessie waarin de aflegstapels zijn opgenomen</param>
        public void VerwerkBeurt(ISpelerSessie sessie)
        {
            this.AantalKaarten = sessie.GetAantalKaartenOpAflegstapel(_kleur);
            if (this.AantalKaarten == 0)
            {
                this.BovensteKaart = null;
            }
            else
            {
                this.BovensteKaart = new KaartViewModel(sessie.GetAflegBord().GetAflegStapel(_kleur).GetBovensteKaart());
            }



        }


        /// <summary>
        /// De ColorSelector bevat de kleuren van de de aflegstapel
        /// </summary>
        /// <param name="kleur"></param>
        private void SetKleur(Kleur kleur)
        {
            selector = new ColorSelector(kleur.ToString());
        }


        /// <summary>
        /// Propery: Aantal kaarten van de aflegstapel
        /// </summary>
        public int AantalKaarten
        {
            get { return _aantalKaarten; }
            set { _aantalKaarten = value; OnPropertyChanged("AantalKaarten"); }
        }

        /// <summary>
        /// De donkere kleur van de aflegstapel
        /// </summary>
        public string KleurDonker
        {
            get { return selector.KleurDonker; }
            set { selector.KleurDonker = value; OnPropertyChanged("KleurDonker"); }
        }

        /// <summary>
        /// De lichte kleur van de aflegstapel
        /// </summary>
        public string KleurLicht
        {
            get { return selector.KleurLicht; }
            set { selector.KleurLicht = value; OnPropertyChanged("KleurLicht"); }
        }

        /// <summary>
        /// De zichtbare bovenste kaart van de aflegstapel
        /// </summary>
        public KaartViewModel BovensteKaart
        {
            get { return _bovensteKaart; }
            set { _bovensteKaart = value; OnPropertyChanged("BovensteKaart"); }
        }


    }
}
