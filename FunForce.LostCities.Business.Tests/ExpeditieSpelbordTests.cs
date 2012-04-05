using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Business;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class ExpeditieSpelbordTests
    {
        [Test]
        public void LegKaartAan()
        {
            ExpeditieSpelbord bord = new ExpeditieSpelbord();
            Kaart kaart = new ExpeditieKaart(Kleur.Blauw, 2);
            bord.LegKaartAan(kaart);
            Assert.AreEqual(1, bord.AantalKaarten(Kleur.Blauw));
            Assert.AreEqual(1, bord.AantalKaarten());
        }


        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void LegNietOplopendeKaartAan()
        {
            ExpeditieSpelbord bord = new ExpeditieSpelbord();
            Kaart kaart3 = new ExpeditieKaart(Kleur.Rood, 3);
            Kaart kaart4 = new ExpeditieKaart(Kleur.Rood, 4);
            bord.LegKaartAan(kaart4);
            bord.LegKaartAan(kaart3);
        }


        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void LegWeddenschapAanNaExpeditieKaart()
        {
            ExpeditieSpelbord bord = new ExpeditieSpelbord();
            Kaart kaart3 = new ExpeditieKaart(Kleur.Rood, 3);
            Kaart weddenschapskaart = new WeddenschapsKaart(Kleur.Rood);
            bord.LegKaartAan(kaart3);
            bord.LegKaartAan(weddenschapskaart);
        }

        [Test]
        public void BekijkKaartVanAflegBord_GeenKaart()
        {
            IAflegBord bord = new AflegSpelbord();
            IAflegStapel stapel = bord.GetAflegStapel(Kleur.Groen);
            Assert.IsNull(stapel.GetBovensteKaart());
        }

        [Test]
        public void BekijkKaartVanAflegBord_EenBlauweKaart()
        {
            AflegSpelbord bord = new AflegSpelbord();
            Kaart kaart = new ExpeditieKaart(Kleur.Blauw, 2);
            bord.LegKaartAan(kaart);

            IAflegBord aflegbord = bord;
            IAflegStapel stapel = aflegbord.GetAflegStapel(Kleur.Blauw);

            Assert.AreEqual(Kleur.Blauw, stapel.GetBovensteKaart().Kleur);
        }


        [Test]
        public void LegJuisteVolgordeAan()
        {
            ExpeditieSpelbord bord = new ExpeditieSpelbord();
            bord.LegKaartAan(new WeddenschapsKaart(Kleur.Blauw));
            bord.LegKaartAan(new WeddenschapsKaart(Kleur.Blauw));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 2));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 3));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 4));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 5));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 6));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 7));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 8));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 9));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 10));

            Assert.AreEqual(11, bord.AantalKaarten(Kleur.Blauw));

        }

        [Test]
        public void SpelBordScoreTest()
        {
            ExpeditieSpelbord bord = new ExpeditieSpelbord();
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 7));
            Assert.IsTrue(bord.GetScore() == -13);
        }


        [Test]
        public void GetExpeditieStapel()
        {
            ExpeditieSpelbord bord = new ExpeditieSpelbord();
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 7));
            IExpeditieStapel stapel = bord.GetExpeditieStapel(Kleur.Blauw);
            Assert.AreEqual(1, stapel.AantalKaarten);
        
        }

    }
}
