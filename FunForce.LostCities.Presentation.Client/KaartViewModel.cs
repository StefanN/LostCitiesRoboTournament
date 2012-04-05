using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Presentation.Client
{
    /// <summary>
    /// ViewModel van een kaart
    /// </summary>
    public class KaartViewModel : BaseViewModel
    {
 
        string _waarde;
        string _offset = "0";
        ColorSelector selector = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kaart">De kaart</param>
        public KaartViewModel(IKaart kaart)
        {
            SetKleur(kaart);
            SetWaarde(kaart);
        }


        /// <summary>
        /// Vergelijk kaarten op waarde en de donkere kleur. Dit betekent dat de donkere kleur uniek moet zijn
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is KaartViewModel)
            {
                return (this.Waarde == ((KaartViewModel)obj).Waarde) && (this.KleurDonker == ((KaartViewModel)obj).KleurDonker);
            }
            return false;
        }

        /// <summary>
        /// Moet overriden worden als de Equals method wordt overriden
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Offset wordt gebruik om kaarten te laten overlappen
        /// </summary>
        public string Offset
        {
            get { return _offset; }
            set { _offset = value; OnPropertyChanged("Offset"); }
        }

        /// <summary>
        /// Donkere kleur van de kaart
        /// </summary>
        public string KleurDonker
        {
            get { return selector.KleurDonker; }
            set { selector.KleurDonker = value; OnPropertyChanged("KleurDonker"); }
        }

        /// <summary>
        /// Lichte kleur van de kaart
        /// </summary>
        public string KleurLicht
        {
            get { return selector.KleurLicht; }
            set { selector.KleurLicht = value; OnPropertyChanged("KleurLicht"); }
        }

        /// <summary>
        /// De waarde van de kaart
        /// </summary>
        public string Waarde
        {
            get { return _waarde; }
            set { _waarde = value; OnPropertyChanged("Waarde"); }
        }

        /// <summary>
        /// Set de kleur van de kaart.
        /// Kaart : null is geen kaart
        /// </summary>
        /// <param name="kaart">kaart</param>
        private void SetKleur(IKaart kaart)
        {
            string kleur;

            if (kaart == null)
            {
                kleur = "Transparant";
            }
            else
            {
                kleur = kaart.Kleur.ToString();
            }

            selector = new ColorSelector(kleur);

        }

        /// <summary>
        /// Set de waarde van de kaart
        /// Kaart : null is geen kaart
        /// </summary>
        /// <param name="kaart">kaart</param>
        private void SetWaarde(IKaart kaart)
        {
            if (kaart == null)
            {
                _waarde = "";
            }

            if (kaart is IExpeditieKaart)
            {
                _waarde = ((IExpeditieKaart)kaart).Waarde.ToString();
            }

            if (kaart is IWeddenschapsKaart)
            {
                _waarde = "W";
            }
        }

    }



}
