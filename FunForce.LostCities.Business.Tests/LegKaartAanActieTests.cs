using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Facade.Interface;
namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class LegKaartWegActieTests
    {
        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void IllegalIndexTest_TeLaag()
        {
            new LegKaartWegActie(-1, new DommeSpeler());
        }

        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void IllegalIndexTest_TeHoog()
        {
            new LegKaartWegActie(8, new DommeSpeler());
        }

        [Test]
        public void CreateActie()
        {
            LegKaartWegActie actie = new LegKaartWegActie(4, new DommeSpeler());
            Assert.AreEqual(4, actie.IndexInHand);
        }
    
    }
}
