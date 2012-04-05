using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Facade.Interface;
namespace FunForce.LostCities.Business.Tests
{
    [TestFixture]
    public class LegKaartAanActieTests
    {
        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void IllegalIndexTest_TeLaag()
        {
            new LegKaartAanActie(-1, new DommeSpeler());
        }

        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void IllegalIndexTest_TeHoog()
        {
            new LegKaartAanActie(8, new DommeSpeler());
        }

        [Test]
        public void CreateActie()
        {
            LegKaartAanActie actie = new LegKaartAanActie(4, new DommeSpeler());
            Assert.AreEqual(4, actie.IndexInHand);
        }
    
    }
}
