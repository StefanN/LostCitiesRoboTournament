using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Business;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class SpelerSessie : ISpelerSessie
    {
        private string name;
        private Spel huidigSpel;
        private Speler speler;
        private LegActie legActie;

        public event EventHandler BeurtWissel;

        public SpelerSessie(string name)
        {
            this.name = name;
        }

        public void SetSpel(Spel spel)
        {
            this.huidigSpel = spel;

            this.huidigSpel.BeurtWissel += new EventHandler(huidigSpel_BeurtWissel);
        }

        void huidigSpel_BeurtWissel(object sender, EventArgs e)
        {
            OnBeurtWissel();
        }

        private void OnBeurtWissel()
        {
            if (BeurtWissel != null)
                BeurtWissel(this, new EventArgs());
        }

        public void SetSpeler(Speler speler)
        {
            this.speler = speler;
        }

        public Speler Speler
        {
            get { return this.speler; }
        }

        public int GetScore()
        {
            return this.speler.Bord.GetScore();
        }

        public string Naam
        {
            get { return this.name; }
        }

        public ILaatsteBeurt GetLaatsteBeurt()
        {
             return speler.LaatsteBeurt;
        }

        public bool IsActievepeler()
        {
            if (this.huidigSpel == null)
            {
                return false;
            }
            else
            {
                return (this.speler == this.huidigSpel.HuidigeSpeler);
            }
        }

        public IStapel GetHand()
        {
            if (this.huidigSpel == null)
            {
                return null;
            }
            else
            {
                return this.speler.Hand;
            }

        }

        public void LegKaart(int indexInHand, BordType bordType)
        {

            if (!IsActievepeler())
                throw new ApplicationException("speler is niet aan de beurt");

            if (legActie != null)
                throw new ApplicationException("legActie bestaat al");

            switch (bordType)
            {
                case BordType.AflegBord:
                    this.legActie = new LegKaartWegActie(indexInHand, speler);
                    break;
                case BordType.ExpeditieBord:
                    this.legActie = new LegKaartAanActie(indexInHand, speler);
                    break;
            }
        }

        public void PakVanTrekStapel()
        {
            if (this.legActie == null)
            {
                throw new ApplicationException("Je kunt pas pakken als je eerst een kaart weggelegd hebt");
            }

            Beurt beurt = new Beurt(this.legActie, new PakKaartVanTrekStapelActie(this.speler));
            this.huidigSpel.VoerActiesUit(beurt);
            this.legActie = null;
        }

        public void PakVanAflegStapel(Kleur kleur)
        {
            if (this.legActie == null)
            {
                throw new ApplicationException("Je kunt pas pakken als je eerst een kaart weggelegd hebt");
            }

            Beurt beurt = new Beurt(this.legActie, new PakKaartVanAflegStapelActie(this.speler, kleur));
            this.huidigSpel.VoerActiesUit(beurt);
            this.legActie = null;
        }


        #region ISpelerSessie Members

        public int GetAantalKaartenOpTrekstapel()
        {
            if (huidigSpel == null)
                return 0;

            return huidigSpel.TrekStapel.AantalKaarten;
        }

        public int GetAantalKaartenOpAflegstapel(Kleur kleur)
        {
            if (huidigSpel == null)
                return 0;

            return huidigSpel.AflegBord.AantalKaarten(kleur);
        }

        public IAflegBord GetAflegBord()
        {
            if (huidigSpel == null)
                return null;

            return huidigSpel.AflegBord;
        }

        public IExpeditieBord GetSpelerBord()
        {
            if (huidigSpel == null)
                return null;

            return speler.Bord;
        }

        public string NaamTegenstander
        {
            get{return String.Empty;}
        }

        public int GetScoreTegenstander()
        {
            if (speler.Equals(huidigSpel.Speler1))
            {
                return huidigSpel.Speler2.Bord.GetScore();
            }
            else
            {
                return huidigSpel.Speler1.Bord.GetScore();
            }
        }

        public IExpeditieBord GetTegenstanderBord()
        {
            if (huidigSpel == null)
                return null;
            
            if(speler.Equals(huidigSpel.Speler1))
            {
                return huidigSpel.Speler2.Bord;
            }
            else
            {
                return huidigSpel.Speler1.Bord;
            }
        }

        #endregion
    }
}
