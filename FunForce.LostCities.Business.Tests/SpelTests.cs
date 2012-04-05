using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Business;
using FunForce.LostCities.Facade.Interface;


namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class SpelTests
    {

        private Spel spel = null;

        [SetUp]
        public void SetUp()
        {
            spel = new Spel();
        }

        [Test]
        public void CreateSpelTests()
        {
            spel.Initialiseer();
            Assert.AreEqual(44, spel.TrekStapel.AantalKaarten);
            Assert.AreEqual(8, spel.Speler1.Hand.AantalKaarten);
            Assert.AreEqual(8, spel.Speler2.Hand.AantalKaarten);

            Assert.IsNotNull(spel.AflegBord);
            Assert.IsNotNull(spel.Speler1.Bord);
            Assert.IsNotNull(spel.Speler2.Bord);
        }


        [Ignore]
        public void SimuleerSpelMetNormaleSpelers()
        {
            spel.Initialiseer();
            // RZ: De test wordt genegeerd omdat simuleerspel altijd de eerste kaart in de
            // hand van een speler aanlegt. Er is dus geen enkele garantie dat de kaarten in oplopende waarde
            // worden aangelegd. Deze test zal nu dus vrijwel altijd fout gaan
            // 
            SimuleerSpel(spel);
            Assert.AreEqual(0, spel.AflegBord.AantalKaarten());
        }

        [Test]
        [Ignore]
        public void SimuleerSpelMetDommeSpelers()
        {
            //naar idee van Robert Lelieveld...
            //spel voor dummies. Altijd kaart wegleggen en nieuwe pakken...
            Speler speler1 = new DommeSpeler();
            Speler speler2 = new DommeSpeler();

            spel.Initialiseer(speler1, speler2);

            SimuleerSpel(spel);

            Assert.AreEqual(8, speler1.Hand.AantalKaarten);
            Assert.AreEqual(8, speler2.Hand.AantalKaarten);
            Assert.AreEqual(44, spel.AflegBord.AantalKaarten());
        }

        [Test]
        [Ignore]
        public void SimuleerSpelMetDommeSpelersScoreNul()
        {
            //naar idee van Robert Lelieveld...
            //spel voor dummies. Altijd kaart wegleggen en nieuwe pakken...
            Speler speler1 = new DommeSpeler();
            Speler speler2 = new DommeSpeler();

            spel.Initialiseer(speler1, speler2);

            SimuleerSpel(spel);

            // Beide spelers score 0
            Assert.IsTrue(speler1.Bord.GetScore() == 0);
            Assert.IsTrue(speler2.Bord.GetScore() == 0);
        }

        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void VoerActieUitNaAfloopSpel()
        {
            Speler speler1 = new DommeSpeler();
            Speler speler2 = new DommeSpeler();

            spel.Initialiseer(speler1, speler2);

            SimuleerSpel(spel);

            spel.VoerActiesUit(new Beurt(new LegKaartAanActie(0, speler1), new PakKaartVanTrekStapelActie(speler1)));
        }

        private void SimuleerSpel(Spel spel)
        {
            while (!spel.IsAfgelopen())
            {
                spel.VoerActiesUit(spel.HuidigeSpeler.BepaalActies());
            }
        }

        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void SimuleerNietHuidigeSpelerSpeelt()
        {
            Speler speler1 = new DommeSpeler();
            Speler speler2 = new DommeSpeler();

            spel.Initialiseer(speler1, speler2);

            if (spel.HuidigeSpeler == spel.Speler1)
            {
                spel.VoerActiesUit(spel.Speler2.BepaalActies());
            }
            else
            {
                spel.VoerActiesUit(spel.Speler1.BepaalActies());
            }
        }

    }
}
