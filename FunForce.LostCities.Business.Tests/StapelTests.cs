using System;
using NUnit.Framework;
using FunForce.LostCities.Business;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
	/// <summary>
	/// Description of StapelTests.
	/// </summary>
	[TestFixture]
	public class StapelTests
	{
		private Kaart kaart1 = new WeddenschapsKaart(Kleur.Blauw);
		private Kaart kaart2 = new ExpeditieKaart(Kleur.Blauw, 2);
		private ExpeditieStapel stapel;


        [SetUp]
        public void SetUp()
        {
            stapel = new ExpeditieStapel();
            Assert.AreEqual(0, stapel.AantalKaarten);
        }

		[Test]
		public void CreateStapelMetTweeKaarten()
		{
			stapel.AddKaart(kaart1);
			stapel.AddKaart(kaart2);
			Assert.AreEqual(2, stapel.AantalKaarten);
		}
		
		[Test]
		public void PakKaartVanStapel()
		{
			stapel.AddKaart(kaart1);
			stapel.AddKaart(kaart2);

			Kaart kaart = stapel.PakBovensteKaart();
			Assert.AreSame(kaart2, kaart);
		}
		
		[Test]
		[ExpectedException(typeof(LostCitiesException))]
		public void PakKaartVanLegeStapel()
		{
			Kaart kaart = stapel.PakBovensteKaart();
		}


        [Test]
        public void GetKaartVanStapel()
        {
            stapel.AddKaart(kaart1);
            stapel.AddKaart(kaart2);

            Kaart kaart = stapel[1];
            Assert.AreSame(kaart2, kaart);
        }


        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void GetKaartVanTeKleineStapel()
        {
            stapel.AddKaart(kaart1);
            Kaart kaart = stapel[1];
        }

        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void GetKaartVanLegeStapel()
        {
            Kaart kaart = stapel[0];
        }

        [Test]
        [ExpectedException(typeof(LostCitiesException))]
        public void GetKaartUitTeKleineStapel()
        {
            stapel.AddKaart(kaart1);
            Kaart kaart = stapel.PakKaart(1);
        }

        [Test]
        public void GetKaartVanExpeditieStapel()
        {
            IExpeditieStapel s = stapel;
            stapel.AddKaart(kaart1);
            IKaart kaart = stapel.GetKaart(0);
        }


        [Test]
        public void SchudKaarten()
        {

            Kaart[] kaarten = new Kaart[6];
            
            kaarten[0] = new ExpeditieKaart(Kleur.Geel, 2);
            kaarten[1] = new ExpeditieKaart(Kleur.Geel, 3);
            kaarten[2] = new ExpeditieKaart(Kleur.Geel, 4);
            kaarten[3] = new ExpeditieKaart(Kleur.Geel, 5);
            kaarten[4] = new ExpeditieKaart(Kleur.Geel, 6);
            kaarten[5] = new ExpeditieKaart(Kleur.Geel, 7);

            Stapel stapel = new Stapel();
            for (int i = 0; i < kaarten.Length; i++)
            {
                stapel.AddKaart(kaarten[i]);
            }

            stapel.Schud();
        }

        [Test]
        public void ScoreVanLegeStapel()
        {
            Assert.IsTrue(stapel.GetScore() == 0);
        }

        [Test]
        public void ScoreVanStapelMetBlauwTwee()
        {
            stapel.AddKaart(kaart2);

            Assert.IsTrue(stapel.GetScore() == -18);
        }

        [Test]
        public void ScoreVanStapelMetBlauwTweeEnWeddenschap()
        {
            stapel.AddKaart(kaart1);
            stapel.AddKaart(kaart2);

            Assert.AreEqual(-36, stapel.GetScore());
        }

        [Test]
        public void ScoreVanStapelMetWeddenschap()
        {
            stapel.AddKaart(kaart1);

            Assert.AreEqual(-40, stapel.GetScore());
        }

        [Test]
        public void ScoreVanGroteStapel()
        {
            stapel.AddKaart(new WeddenschapsKaart(Kleur.Blauw));
            stapel.AddKaart(new WeddenschapsKaart(Kleur.Blauw));

            stapel.AddKaart(new ExpeditieKaart(Kleur.Blauw, 2));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Blauw, 5));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Blauw, 7));
            stapel.AddKaart(new ExpeditieKaart(Kleur.Blauw, 9));

            Assert.AreEqual(9, stapel.GetScore());
        }

    }
}
