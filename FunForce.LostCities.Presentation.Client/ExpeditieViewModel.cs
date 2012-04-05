using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using FunForce.LostCities.Facade.Interface;


namespace FunForce.LostCities.Presentation.Client
{
    //ViewModel van de expeditie
    public class ExpeditieViewModel : BaseViewModel
    {
        ColorSelector selector = null;
        ObservableCollection<KaartViewModel> _expeditieStapel;
        int _score;

        /// <summary>
        /// Constructor, welke constructor wordt niet gebruikt
        /// </summary>
        /// <param name="stapel">stapel kaarten</param>
        /// <param name="kleur">kleur van de expeditie</param>
        public ExpeditieViewModel(IExpeditieStapel stapel, string kleur)
        {
            _expeditieStapel = new ObservableCollection<KaartViewModel>();
            SetKleur(kleur);
            VulExpeditieAan(stapel);
            this.Score = 0;

        }

        /// <summary>
        /// Constructor, welke constructor wordt niet gebruikt
        /// </summary>
        /// <param name="kleur">kleur van de expeditie</param>
        public ExpeditieViewModel(string kleur)
        {
            _expeditieStapel = new ObservableCollection<KaartViewModel>();
            SetKleur(kleur);
            this.Score = 0;
        }

        /// <summary>
        /// Score van de expeditie
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { _score = value; OnPropertyChanged("Score"); }
        }


        /// <summary>
        /// Bepaal de kleuren van de expeditie obv de kleur v.d. kaarten in de business layer
        /// </summary>
        /// <param name="kleur"></param>
        private void SetKleur(string kleur)
        {
            selector = new ColorSelector(kleur);
        }

        /// <summary>
        /// Verwerk beurt in iedere expeditie
        /// </summary>
        /// <param name="stapel">stapel kaarten van de expeditie</param>
        public void VerwerkBeurt(IExpeditieStapel stapel)
        {
            bool expeditieGewijzigd = (stapel.AantalKaarten != _expeditieStapel.Count);

            if (expeditieGewijzigd)
            {
                VulExpeditieAan(stapel);
                this.Score = stapel.GetScore();
            }
        }

 
        /// <summary>
        /// Vul expeditie aan met nieuwe kaarten.  Aanvullen is is fraaier dan hele collectie vullen 
        /// ivm renderen scherm
        /// </summary>
        /// <param name="stapel">alle kaarten in de expeditie</param>
        private void VulExpeditieAan(IExpeditieStapel stapel)
        {
            //optimalisatie: Kaarten kunnen alleen maar toegevoegd worden
            for (int i = _expeditieStapel.Count; i < stapel.AantalKaarten; i++)
            {
                KaartViewModel kaart = new KaartViewModel(stapel.GetKaart(i));

                kaart.Offset = (i * -33).ToString();
                _expeditieStapel.Add(kaart);
            }
        }

        public ObservableCollection<KaartViewModel> ExpeditieStapel 
        { 
            get {return _expeditieStapel;}
            set { _expeditieStapel = value; OnPropertyChanged("ExpeditieStapel"); } 
        }

        /// <summary>
        /// De donkere kleur van de expeditie
        /// </summary>
        public string KleurDonker
        {
            get { return selector.KleurDonker; }
            set { selector.KleurDonker = value;  }
        }

        /// <summary>
        /// De lichte kleur van de expeditie
        /// </summary>
        public string KleurLicht
        {
            get { return selector.KleurLicht; }
            set { selector.KleurLicht = value;  }
        }


    }
}
