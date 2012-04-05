using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class SpelerCpuTests
    {
        [Test]
        public void BepaalLegActiesTest()
        {
            //maak bord
            ExpeditieSpelbord bord = new ExpeditieSpelbord();
            bord.LegKaartAan(new WeddenschapsKaart(Kleur.Wit));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Blauw, 2));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Groen, 5));
            
            //maak hand
            Stapel hand = new Stapel();
            hand.AddKaart(new WeddenschapsKaart(Kleur.Geel));    //ok
            hand.AddKaart(new WeddenschapsKaart(Kleur.Wit));    //nok
            hand.AddKaart(new WeddenschapsKaart(Kleur.Groen));  //nok
            hand.AddKaart(new ExpeditieKaart(Kleur.Geel, 5));   //ok
            hand.AddKaart(new ExpeditieKaart(Kleur.Blauw, 5));   //ok
            hand.AddKaart(new ExpeditieKaart(Kleur.Groen, 7));   //ok
            hand.AddKaart(new ExpeditieKaart(Kleur.Groen, 4));   //nok

            //voorspelling
            int[] voorspelling = new int[] { 0, 1, 3, 4, 5 };

            int[] werkelijk = SimulatieStrategie.BepaalMogelijkeLegActies(hand, bord);

            Assert.AreEqual(voorspelling, werkelijk);
            
        }


        [Test]
        public void BepaalLegActiesBugTest()
        {
            //maak bord
            ExpeditieSpelbord bord = new ExpeditieSpelbord();
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Geel, 8));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Rood, 3));
            bord.LegKaartAan(new ExpeditieKaart(Kleur.Groen, 6));

            //maak hand
            Stapel hand = new Stapel();
            hand.AddKaart(new WeddenschapsKaart(Kleur.Rood));    //nok


            //voorspelling
            int[] voorspelling = new int[] { };

            int[] werkelijk = SimulatieStrategie.BepaalMogelijkeLegActies(hand, bord);

            Assert.AreEqual(voorspelling, werkelijk);

        }


    }
}
