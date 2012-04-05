using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class SpelStatus : ISpelStatus
    {

        #region ISpelStatus Members
        private IStapel hand;
        private IAflegBord aflegbord;
        private IExpeditieBord bord;
        private IExpeditieBord bordTegenstander;
        private int score;
        private int scoreTegenstander;
        private int aantalKaartenTrekstapel;
            

        public SpelStatus(IStapel hand, IAflegBord aflegbord, IExpeditieBord bord,
                IExpeditieBord bordTegenstander, int score, int scoreTegenstander, int aantalKaartenTrekstapel)
        {
            this.hand = hand;
            this.aflegbord = aflegbord;
            this.bord = bord;
            this.bordTegenstander = bordTegenstander;
            this.score = score;
            this.scoreTegenstander = scoreTegenstander;
            this.aantalKaartenTrekstapel = aantalKaartenTrekstapel;
        }


        public int ScoreTegenstander
        {
            get {return scoreTegenstander; }
        }

        public int Score
        {
            get { return score; }
        }

        public IStapel Hand
        {
            get {return hand; }
        }

        public int AantalKaartenOpTrekstapel
        {
            get { return aantalKaartenTrekstapel; }
        }

        public IAflegBord AflegBord
        {
            get { return this.aflegbord; }
        }

        public IExpeditieBord Bord
        {
            get { return this.bord;}
        }

        public IExpeditieBord BordTegenstander
        {
            get{return bordTegenstander;}
        }

        #endregion
    }
}
