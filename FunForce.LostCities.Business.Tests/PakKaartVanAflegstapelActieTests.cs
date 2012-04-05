using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class PakKaartVanAflegstapelActieTests
    {
        [Test]
        public void GetKleurTest()
        {
            PakKaartVanAflegStapelActie a = new PakKaartVanAflegStapelActie(null, Kleur.Rood);
            Assert.AreEqual(Kleur.Rood, a.Kleur);
        }


    }
}
