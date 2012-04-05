using System;
using System.Collections.Generic;
using NUnit.Framework;
using FunForce.LostCities.Business;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
	/// <summary>
	/// Description of KaartTests.
	/// </summary>
	[TestFixture]
	public class KaartTests
	{
		Kleur groen = Kleur.Groen;
		Kleur geel = Kleur.Geel;

        [Test]
        public void FailureTest()
        {
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void CreateExpeditieKaart()
        {
            ExpeditieKaart kaart = new ExpeditieKaart(geel, 10);
            Assert.AreEqual(geel, kaart.Kleur);
            Assert.AreEqual(10, kaart.Waarde);
            Assert.AreEqual(false, kaart.IsGedekt);
        }


		[Test]
		public void CreateExpeditieKaart_Geel()
		{
			ExpeditieKaart kaart = new ExpeditieKaart(geel, 10);
			Assert.AreEqual(geel, kaart.Kleur);
			Assert.AreEqual(10, kaart.Waarde);
		}
		
		[Test]
		public void CreateExpeditieKaart_Groen()
		{
			ExpeditieKaart kaart = new ExpeditieKaart(groen, 4);
			Assert.AreEqual(groen, kaart.Kleur);
			Assert.AreEqual(4, kaart.Waarde);
		}

		[Test]
		[ExpectedException(typeof(LostCitiesException))]
		public void CreateExpeditieKaart_OngeldigeWaarde_Laag()
		{
			new ExpeditieKaart(groen, 1);
		}
		
		[Test]
		[ExpectedException(typeof(LostCitiesException))]
		public void CreateExpeditieKaart_OngeldigeWaarde_Hoog()
		{
			new ExpeditieKaart(groen, 11);
		}
		
		
		[Test]
		public void CreateWeddenschapsKaart_Geel()
		{
			WeddenschapsKaart kaart = new WeddenschapsKaart(geel);
			Assert.AreEqual(geel, kaart.Kleur);
		}

		[Test]
		public void CreateWeddenschapsKaart_Groen()
		{
			WeddenschapsKaart kaart = new WeddenschapsKaart(groen);
			Assert.AreEqual(groen, kaart.Kleur);
		}
		
	}
}
