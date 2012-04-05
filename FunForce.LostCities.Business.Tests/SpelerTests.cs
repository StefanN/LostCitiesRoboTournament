using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class SpelerTests
    {

        [Test]
        public void CreateSpeler()
        {
            Speler speler = new Speler();
            Assert.IsNotNull(speler.Bord);
            Assert.IsNotNull(speler.Hand);
        }

        [Test]
        public void BepaalActieTest_NieuwSpel()
        {
            Stapel stapel = new Stapel();
            stapel.AddKaart(new ExpeditieKaart(Kleur.Wit, 3));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Wit, 4));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Wit, 5));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Wit, 6));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Wit, 7));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Wit, 8));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Wit, 9));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Wit, 10));

            Spelbord bord = new ExpeditieSpelbord();
            Spelbord bordTegenstander = new ExpeditieSpelbord();

            Speler speler = new Speler();
            speler.Hand = stapel;

            Beurt beurt = speler.BepaalActies();
            Assert.IsNotNull(beurt.LegActie);
            Assert.IsNotNull(beurt.PakActie);
        }
   
    }


}
