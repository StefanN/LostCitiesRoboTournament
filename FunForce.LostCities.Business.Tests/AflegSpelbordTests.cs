using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class AflegSpelbordTests
    {
        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void GeenKaartBeschikbaar()
        {
            AflegSpelbord bord = new AflegSpelbord();
            Kaart kaart = bord.PakBovensteKaart(Kleur.Groen);
        }

        [Test]
        public void PakGroeneKaartVanAflegStapel()
        {
            AflegSpelbord bord = new AflegSpelbord();
            Kaart kaart = new WeddenschapsKaart(Kleur.Groen);
            bord.LegKaartAan(kaart);
            int aantalKaarten = bord.AantalKaarten();
            Kaart kaart2 = bord.PakBovensteKaart(Kleur.Groen);
            Assert.AreSame(kaart, kaart2);
            Assert.AreEqual(aantalKaarten - 1, bord.AantalKaarten());
        }

    
    }
}
