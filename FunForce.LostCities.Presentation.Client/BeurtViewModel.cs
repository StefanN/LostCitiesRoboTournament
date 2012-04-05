using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunForce.LostCities.Presentation.Client
{
    /// <summary>
    /// Beurt ViewModel
    /// </summary>
    public class BeurtViewModel : BaseViewModel
    {
        KaartViewModel _laatstGepakteKaart;
        KaartViewModel _laatstGelegdeKaart;
        string _laatstOpgelegdAan;
        string _laatstGepaktVan;
        string _melding;

        /// <summary>
        /// Constructor
        /// </summary>
        public BeurtViewModel()
            : base()
        {
        }

        /// <summary>
        /// Clear SpelerBeurt
        /// </summary>
        public void Clear()
        {
            this.LaatstGelegdeKaart = null;
            this.LaatstGepakteKaart = null;
            this.LaatstGepaktVan = null;
            this.LaatstOpgelegdAan = null;
            this.Melding = null;
        }

        /// <summary>
        /// Property: Laatst gepakte kaart
        /// </summary>
        public KaartViewModel LaatstGepakteKaart
        {
            get { return _laatstGepakteKaart; }
            set { _laatstGepakteKaart = value; OnPropertyChanged("LaatstGepakteKaart"); }
        }

        /// <summary>
        /// Property: Laatst gelegde kaart
        /// </summary>
        public KaartViewModel LaatstGelegdeKaart
        {
            get { return _laatstGelegdeKaart; }
            set { _laatstGelegdeKaart = value; OnPropertyChanged("LaatstGelegdeKaart"); }
        }

        /// <summary>
        /// Property: Tekst bevat waaraan de laatste kaart is gelegd
        /// </summary>
        public string LaatstOpgelegdAan
        {
            get { return _laatstOpgelegdAan; }
            set { _laatstOpgelegdAan = value; OnPropertyChanged("LaatstOpgelegdAan"); }
        }

        /// <summary>
        /// Property: Tekst bevat waarvan de laatste is gepakt
        /// </summary>
        public string LaatstGepaktVan
        {
            get { return _laatstGepaktVan; }
            set { _laatstGepaktVan = value; OnPropertyChanged("LaatstGepaktVan"); }
        }

        /// <summary>
        /// Property: Tekst bevat exception
        /// </summary>
        public string Melding
        {
            get { return _melding; }
            set { _melding = value; OnPropertyChanged("Melding"); }
        }



    }
}
