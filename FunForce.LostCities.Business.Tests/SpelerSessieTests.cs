using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class SpelerSessieTests
    {

        [Test]
        public void InitieleSessie()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            Assert.AreEqual(0, sessie1.GetScore());
            Assert.AreEqual("A", sessie1.Naam);
            Assert.AreEqual(false, sessie1.IsActievepeler());
            Assert.IsNull(sessie1.GetHand());
            Assert.IsNull(sessie1.GetAflegBord());
            Assert.IsNull(sessie1.GetSpelerBord());
            Assert.IsNull(sessie1.GetTegenstanderBord());
            Assert.AreEqual(0, sessie1.GetAantalKaartenOpTrekstapel());
            //RZ 24-05-2008
            Assert.AreEqual(0, sessie1.GetAantalKaartenOpAflegstapel(Kleur.Blauw));
            Assert.AreEqual(0, sessie1.GetAantalKaartenOpAflegstapel(Kleur.Geel));
            Assert.AreEqual(0, sessie1.GetAantalKaartenOpAflegstapel(Kleur.Groen));
            Assert.AreEqual(0, sessie1.GetAantalKaartenOpAflegstapel(Kleur.Rood));
            Assert.AreEqual(0, sessie1.GetAantalKaartenOpAflegstapel(Kleur.Wit));
        }


        [Test]
        public void InitieleSessieNaStartSpel()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);

            Assert.AreEqual(0, sessie1.GetScore());
            Assert.IsNotNull(sessie1.GetHand());
            Assert.IsNotNull(sessie1.GetAflegBord());
            Assert.IsNotNull(sessie1.GetSpelerBord());
            Assert.IsNotNull(sessie1.GetTegenstanderBord());
            Assert.AreEqual(44, sessie1.GetAantalKaartenOpTrekstapel());
        }


        [Test]
        public void VoerBeurtUit_LegKaartWegEnPakVanTrekstapel()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);

            IStapel hand = sessie1.GetHand();
            IKaart kaart = hand[0];
            Kleur kleur = kaart.Kleur;

            sessie1.LegKaart(0, BordType.AflegBord);
            sessie1.PakVanTrekStapel();

            Assert.AreEqual(1, sessie1.GetAantalKaartenOpAflegstapel(kleur));
            Assert.AreEqual(false, sessie1.IsActievepeler());
        }

        [Test]
        public void VoerBeurtUit_LegKaartAanEnPakVanTrekstapel()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);
            sessie1.LegKaart(0, BordType.ExpeditieBord);
            sessie1.PakVanTrekStapel();

            Assert.AreEqual(false, sessie1.IsActievepeler());
        }



        [Test]
        public void VoerBeurtUit_Speler1LegWegSpeler2PakVanAflegstapel()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);
            //speler1 legt eerste kaart weg
            IKaart kaartSpeler1 = sessie1.GetHand()[0];
            Kleur kleur = kaartSpeler1.Kleur;
            sessie1.LegKaart(0, BordType.AflegBord);
            sessie1.PakVanTrekStapel();

            //speler2 legt eerste kaart aan en pakt van aflegstapel
            sessie2.LegKaart(0, BordType.ExpeditieBord);
            sessie2.PakVanAflegStapel(kleur);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void VoerBeurtUit_PakKaartVanTrekstapelVoordatWeggelegdIsIllegaal()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);
            //speler1 legt eerste kaart weg
            sessie1.PakVanTrekStapel();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void VoerBeurtUit_PakKaartVanAflegstapelVoordatWeggelegdIsIllegaal()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);
            //speler1 legt eerste kaart weg
            sessie1.PakVanAflegStapel(Kleur.Groen);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void VoerBeurtUit_Speler1LegTweeKaartenAanIllegaal()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);
            //speler1 legt eerste kaart weg
            sessie1.LegKaart(0, BordType.AflegBord);
            sessie1.LegKaart(0, BordType.AflegBord);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void VoerBeurtUit_Speler1PakTweeKaartenIllegaal()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);
            //speler1 legt eerste kaart weg
            sessie1.LegKaart(0, BordType.AflegBord);
            sessie1.PakVanTrekStapel();
            sessie1.PakVanTrekStapel();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void VoerBeurtUit_Speler1VoerTweeBeurtenUitIllegaal()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);
            //speler1 legt eerste kaart weg
            sessie1.LegKaart(0, BordType.AflegBord);
            sessie1.PakVanTrekStapel();
            //speler1 legt tweede kaart weg
            sessie1.LegKaart(0, BordType.AflegBord);
        }

        [Test]
        public void GetTegenstanderBorden()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            Spel spel = StartSpel(sessie1, sessie2);
            Assert.AreSame(sessie2.GetSpelerBord(), sessie1.GetTegenstanderBord());
            Assert.AreSame(sessie1.GetSpelerBord(), sessie2.GetTegenstanderBord());
        }


        [Test]
        public void BeurtWisselEventBeideSessies()
        {
            SpelerSessie sessie1 = SpelFactory.CreateSpelerSessie("A");
            SpelerSessie sessie2 = SpelFactory.CreateSpelerSessie("B");
            
            Spel spel = StartSpel(sessie1, sessie2);

            SpelerSessieEventTester tester = new SpelerSessieEventTester(sessie1, sessie2);

            sessie1.LegKaart(0, FunForce.LostCities.Facade.Interface.BordType.AflegBord);
            sessie1.PakVanTrekStapel();

            Assert.AreEqual(1, tester.AantalBeurtWisselEventsSessie1);
            Assert.AreEqual(1, tester.AantalBeurtWisselEventsSessie2);
        }

        private Spel StartSpel(SpelerSessie sessie1, SpelerSessie sessie2)
        {
            Spel spel = SpelFactory.CreateSpel(sessie1, sessie2);
            spel.Initialiseer();
            return spel;
        }

    }
}
