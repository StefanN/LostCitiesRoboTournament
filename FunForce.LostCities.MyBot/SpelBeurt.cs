using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.MyBot
{
    public class SpelBeurt : ISpelBeurt
    {
        public SpelBeurt() { }

        private int legKaartIndex = 0;
        private LegBord legBord;

        private PakStapel pakStapel;
        private Kleur pakKleur;

        public void LegKaartWeg(int index)
        {
            this.legKaartIndex = index;
            this.legBord = LegBord.AflegBord;
        }

        public void LegkaartAan(int index)
        {
            this.legKaartIndex = index;
            this.legBord = LegBord.ExpeditieBord;
        }

        public void PakKaartVanTrekstapel()
        {
            this.pakStapel = PakStapel.TrekStapel;
        }

        public void PakKaartVanAflegstapel(Kleur kleur)
        {
            this.pakStapel = PakStapel.AflegStapel;
            this.pakKleur = kleur;
        }

        #region ISpelBeurt Members

        public int KaartIndexLegActie
        {
            get { return legKaartIndex; }
        }

        public LegBord LegBord
        {
            get { return legBord; }
        }

        public PakStapel PakStapel
        {
            get { return pakStapel; }
        }

        public Kleur PakKleurAflegStapel
        {
            get { return pakKleur; }
        }

        #endregion
    }
}
