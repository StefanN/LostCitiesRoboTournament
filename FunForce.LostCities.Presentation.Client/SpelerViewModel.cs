using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FunForce.LostCities.Facade.Interface;


namespace FunForce.LostCities.Presentation.Client
{
    public class SpelerViewModel : BaseViewModel
    {
        HandViewModel _hand;
        ExpeditieViewModel _expeditieBlauw;
        ExpeditieViewModel _expeditieRood;
        ExpeditieViewModel _expeditieWit;
        ExpeditieViewModel _expeditieGeel;
        ExpeditieViewModel _expeditieGroen;
        ISpelerSessie _sessie;
        BeurtViewModel _beurt;
        string _naam;
        int _score;

 

        public SpelerViewModel(ISpelerSessie sessie)
        {
            _sessie = sessie;
            _naam = sessie.Naam;
            this.Score = sessie.GetScore();

            this.Beurt = new BeurtViewModel();

            CreateHand();
            CreateExpedities();

        }

        public SpelerViewModel()
        {
            _naam = "...";
            this.Hand = null;
            this.ExpeditieBlauw = new ExpeditieViewModel(Kleur.Blauw.ToString());
            this.ExpeditieRood = new ExpeditieViewModel(Kleur.Rood.ToString());
            this.ExpeditieWit = new ExpeditieViewModel(Kleur.Wit.ToString());
            this.ExpeditieGroen = new ExpeditieViewModel(Kleur.Groen.ToString());
            this.ExpeditieGeel = new ExpeditieViewModel(Kleur.Geel.ToString());


        }


        public BeurtViewModel Beurt
        {
            get { return _beurt; }
            set { _beurt = value; OnPropertyChanged("Beurt"); }
        }
        public void ClearBeurt()
        {
            this.Beurt.Clear();
        }

        public void VerwerkBeurt(ISpelerSessie sessie)
        {
            this.Beurt.Clear();

            if (!sessie.IsActievepeler())
            {
                _sessie = sessie;
                this.Score = sessie.GetScore();
                WijzigHand();
                WijzigExpedities();
                WijzigBeurt();
            }
        }

        private void WijzigBeurt()
        {
            this.Beurt.LaatstGelegdeKaart = new KaartViewModel(_sessie.GetLaatsteBeurt().LegKaart);
            this.Beurt.LaatstGepakteKaart = new KaartViewModel(_sessie.GetLaatsteBeurt().PakKaart);

            if (_sessie.GetLaatsteBeurt().LegStapel is IAflegStapel)
            {
                this.Beurt.LaatstOpgelegdAan = "naar aflegstapel";
            }
            else if (_sessie.GetLaatsteBeurt().LegStapel is IExpeditieStapel)
            {
                this.Beurt.LaatstOpgelegdAan = "naar expedite";
            }
 
            if (_sessie.GetLaatsteBeurt().PakStapel is IAflegStapel)
            {
                this.Beurt.LaatstGepaktVan = "van aflegstapel";
            }
            else 
            {
                this.Beurt.LaatstGepaktVan = "van trekstapel";
            }
 

           

        }
        
        public HandViewModel Hand
        {
            get { return _hand; }
            set { _hand = value; OnPropertyChanged("Hand"); }
        }

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; OnPropertyChanged("Naam"); }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; OnPropertyChanged("Score"); }
        }

        public ExpeditieViewModel ExpeditieBlauw
        {
            get { return _expeditieBlauw; }
            set { _expeditieBlauw = value; OnPropertyChanged("ExpeditieBlauw"); }
        }

        public ExpeditieViewModel ExpeditieRood
        {
            get { return _expeditieRood; }
            set { _expeditieRood = value; OnPropertyChanged("ExpeditieRood"); }
        }

        public ExpeditieViewModel ExpeditieWit
        {
            get { return _expeditieWit; }
            set { _expeditieWit = value; OnPropertyChanged("ExpeditieWit"); }
        }

        public ExpeditieViewModel ExpeditieGeel
        {
            get { return _expeditieGeel; }
            set { _expeditieGeel = value; OnPropertyChanged("ExpeditieGeel"); }
        }

        public ExpeditieViewModel ExpeditieGroen
        {
            get { return _expeditieGroen; }
            set { _expeditieGroen = value; OnPropertyChanged("ExpeditieGroen"); }
        }

        private void CreateExpedities()
        {
            this.ExpeditieBlauw = new ExpeditieViewModel(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Blauw), Kleur.Blauw.ToString());
            this.ExpeditieRood = new ExpeditieViewModel(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Rood), Kleur.Rood.ToString());
            this.ExpeditieWit = new ExpeditieViewModel(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Wit), Kleur.Wit.ToString());
            this.ExpeditieGroen = new ExpeditieViewModel(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Groen), Kleur.Groen.ToString());
            this.ExpeditieGeel = new ExpeditieViewModel(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Geel), Kleur.Geel.ToString());
        }

        private void WijzigExpedities()
        {
            this.ExpeditieBlauw.VerwerkBeurt(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Blauw));
            this.ExpeditieRood.VerwerkBeurt(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Rood));
            this.ExpeditieWit.VerwerkBeurt(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Wit));
            this.ExpeditieGroen.VerwerkBeurt(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Groen));
            this.ExpeditieGeel.VerwerkBeurt(_sessie.GetSpelerBord().GetExpeditieStapel(Kleur.Geel));
        }

     
       
        private void CreateHand()
        {
            this.Hand = new HandViewModel(_sessie.GetHand());
       }

        private void WijzigHand()
        {
            this.Hand.VerwerkBeurt(_sessie.GetHand());
        }


    }
}
