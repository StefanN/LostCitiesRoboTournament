using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunForce.LostCities.Facade.Interface;
using FunForce.LostCities.Facade;
using System.ComponentModel;

namespace FunForce.LostCities.Presentation.Client
{
    /// <summary>
    /// Viewmodel van het bord
    /// </summary>
    public class BordViewModel : BaseViewModel
    {
        SpelerViewModel _speler1 = null;
        SpelerViewModel _speler2 = null;
        AflegStapelViewModel _aflegstapelBlauw = null;
        AflegStapelViewModel _aflegstapelRood = null;
        AflegStapelViewModel _aflegstapelWit = null;
        AflegStapelViewModel _aflegstapelGroen = null;
        AflegStapelViewModel _aflegstapelGeel = null;
        TrekStapelViewModel _trekStapel = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public BordViewModel()
        {
            _speler1 = new SpelerViewModel();
            _speler2 = new SpelerViewModel();

            CreateAflegStapels();
            _trekStapel = new TrekStapelViewModel();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessie1">simulatiespeler 1</param>
        /// <param name="sessie2">simulatiespeler 2</param>
        public BordViewModel(ISpelerSessie sessie1, ISpelerSessie sessie2)
        {
            _speler1 = new SpelerViewModel(sessie1);
            _speler2 = new SpelerViewModel(sessie2);

            CreateAflegStapels();
            CreateTrekStapel(sessie1);
        }

        /// <summary>
        // Verwerk de beurt op het bord
        /// </summary>
        /// <param name="sessie1">simulatiespeler 1</param>
        /// <param name="sessie2">simulatiespeler 2</param>
        public void VerwerkBeurt(ISpelerSessie sessie1, ISpelerSessie sessie2)
        {

            ClearBeurt(_speler1);
            ClearBeurt(_speler2);

            //Om beurt te verwerken moet speler niet meer de actieve speler zijn
            if (!sessie1.IsActievepeler())
            {
                _speler1.VerwerkBeurt(sessie1);
            }
            else
            {
                _speler2.VerwerkBeurt(sessie2);
            }

            WijzigAflegStapels(sessie1);
            WijzigTrekStapel(sessie1);



            if (_trekStapel.AantalKaarten == 0)
            {

            }

        }
        /// <summary>
        /// Property: AflegStapel Geel
        /// </summary>
        public AflegStapelViewModel AflegstapelGeel
        {
            get { return _aflegstapelGeel; }
            set { _aflegstapelGeel = value; OnPropertyChanged("AflegstapelGeel"); }
        }

        /// <summary>
        /// Property: AflegStapel Blauw
        /// </summary>
        public AflegStapelViewModel AflegstapelBlauw
        {
            get { return _aflegstapelBlauw; }
            set { _aflegstapelBlauw = value; OnPropertyChanged("AflegstapelBlauw"); }
        }

        /// <summary>
        /// Property: AflegStapel Rood
        /// </summary>
        public AflegStapelViewModel AflegstapelRood
        {
            get { return _aflegstapelRood; }
            set { _aflegstapelRood = value; OnPropertyChanged("AflegstapelRood"); }
        }

        /// <summary>
        /// Property: AflegStapel Wit
        /// </summary>
        public AflegStapelViewModel AflegstapelWit
        {
            get { return _aflegstapelWit; }
            set { _aflegstapelWit = value; OnPropertyChanged("AflegstapelWit"); }
        }

        /// <summary>
        /// Property: AflegStapel Groen
        /// </summary>
        public AflegStapelViewModel AflegstapelGroen
        {
            get { return _aflegstapelGroen; }
            set { _aflegstapelGroen = value; OnPropertyChanged("AflegstapelGroen"); }
        }

        /// <summary>
        /// Property: Trekstapel 
        /// </summary>
        public TrekStapelViewModel TrekStapel
        {
            get { return _trekStapel; }
            set { _trekStapel = value; OnPropertyChanged("TrekStapel"); }
        }

        /// <summary>
        /// Property: Speler1
        /// </summary>
        public SpelerViewModel Speler1
        {
            get { return _speler1; }
            set { _speler1 = value; OnPropertyChanged("Speler1"); }
        }

        /// <summary>
        /// PropertySpeler2
        /// </summary>
        public SpelerViewModel Speler2
        {
            get { return _speler2; }
            set { _speler2 = value; OnPropertyChanged("Speler2"); }
        }

        /// <summary>
        /// Maak de trekstapel aan
        /// </summary>
        /// <param name="sessie"></param>
        private void CreateTrekStapel(ISpelerSessie sessie)
        {
            _trekStapel = new TrekStapelViewModel();
             WijzigTrekStapel(sessie);

        }

        /// <summary>
        /// Maak de aflegstapels aan
        /// </summary>
        private void CreateAflegStapels()
        {
            _aflegstapelBlauw = new AflegStapelViewModel(Kleur.Blauw);
            _aflegstapelRood = new AflegStapelViewModel(Kleur.Rood);
            _aflegstapelWit = new AflegStapelViewModel(Kleur.Wit);
            _aflegstapelGroen = new AflegStapelViewModel(Kleur.Groen);
            _aflegstapelGeel = new AflegStapelViewModel(Kleur.Geel);
        }

         private void ClearBeurt(SpelerViewModel speler)
        {
            speler.ClearBeurt();
        }
        
        /// <summary>
        /// Pas trekstapel aan
        /// </summary>
        /// <param name="sessie1"></param>
        public void WijzigTrekStapel(ISpelerSessie sessie1)
        {
            //Om beurt te verwerken moet speler niet meer de actieve speler zijn
            if (!sessie1.IsActievepeler())
            {
                this.TrekStapel.VerwerkBeurt(sessie1.GetAantalKaartenOpTrekstapel());
            }
            else
            {
                this.TrekStapel.VerwerkBeurt(sessie1.GetAantalKaartenOpTrekstapel());
            }
        }

        /// <summary>
        /// Pas aflegstapels aan
        /// </summary>
        /// <param name="sessie1"></param>
        private void WijzigAflegStapels(ISpelerSessie sessie1)
        {
            //Om beurt te verwerken moet speler niet meer de actieve speler zijn
            if (!sessie1.IsActievepeler())
            {
                this.AflegstapelBlauw.VerwerkBeurt(sessie1);
                this.AflegstapelRood.VerwerkBeurt(sessie1);
                this.AflegstapelWit.VerwerkBeurt(sessie1);
                this.AflegstapelGroen.VerwerkBeurt(sessie1);
                this.AflegstapelGeel.VerwerkBeurt(sessie1);
            }
            else
            {
                this.AflegstapelBlauw.VerwerkBeurt(sessie1);
                this.AflegstapelRood.VerwerkBeurt(sessie1);
                this.AflegstapelWit.VerwerkBeurt(sessie1);
                this.AflegstapelGroen.VerwerkBeurt(sessie1);
                this.AflegstapelGeel.VerwerkBeurt(sessie1);
            }


        }



    }
}
