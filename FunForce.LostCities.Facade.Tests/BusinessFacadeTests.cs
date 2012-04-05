using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FunForce.LostCities.Facade.Interface;


namespace FunForce.LostCities.Facade.Tests
{
    [TestFixture]
    public class BusinessFacadeTests
    {

        [SetUp]
        public void Reset()
        {
            BusinessFacade.Reset();
        }
 
        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void VoegSessiesToe()
        {
            BusinessFacade facade = BusinessFacade.GetInstance();
            ISpelerSessie sessie1 = facade.CreateSessie("player 1");
            //na het maken van de eerste sessie is player1 niet actief
            Assert.AreEqual("player 1", sessie1.Naam);
            Assert.IsFalse(sessie1.IsActievepeler());
            Assert.IsNull(sessie1.GetHand());
            //na het maken van de tweede sessie is player1 actief
            ISpelerSessie sessie2 = facade.CreateSessie("player 2");
            Assert.IsTrue(sessie1.IsActievepeler());
            Assert.IsFalse(sessie2.IsActievepeler());
            facade.CreateSessie("player 3");
        }

        [Test, ExpectedException(typeof(ApplicationException))]
        public void LegTweeKeerAan()
        {
            BusinessFacade facade = BusinessFacade.GetInstance();
            ISpelerSessie sessie1 = facade.CreateSessie("player 1");
            ISpelerSessie sessie2 = facade.CreateSessie("player 2");
            IStapel stapel = sessie1.GetHand();
            sessie1.LegKaart(0, BordType.AflegBord);
            sessie1.LegKaart(1, BordType.AflegBord);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void PakTweeKeer()
        {
            BusinessFacade facade = BusinessFacade.GetInstance();
            ISpelerSessie sessie1 = facade.CreateSessie("player 1");
            ISpelerSessie sessie2 = facade.CreateSessie("player 2");
            sessie1.PakVanTrekStapel();
            sessie1.PakVanTrekStapel();
        }

        [Test]
        public void EersteBeurt()
        {
            BusinessFacade facade = BusinessFacade.GetInstance();
            ISpelerSessie sessie1 = facade.CreateSessie("player 1");
            ISpelerSessie sessie2 = facade.CreateSessie("player 2");
            IStapel stapel = sessie1.GetHand();
            sessie1.LegKaart(0, BordType.AflegBord);
            sessie1.PakVanTrekStapel();
            
        }

        [Test]
        public void ScoreVoorEersteBeurt()
        {
            BusinessFacade facade = BusinessFacade.GetInstance();
            
            ISpelerSessie sessie1 = facade.CreateSessie("player 1");
            ISpelerSessie sessie2 = facade.CreateSessie("player 2");
            
            Assert.AreEqual(0, sessie1.GetScore());
            Assert.AreEqual(0, sessie2.GetScore());
        }

        [Test]
        public void ScoreNaEersteBeurtSpelers()
        {
            //Moeilijk te testen. Het berekenen van de score wordt in de business gedaan.
            //Deze test moet alleen aantonen dat de verwachte score wordt teruggeven door
            //de spelersessie.
            BusinessFacade facade = BusinessFacade.GetInstance();
           
            ISpelerSessie sessie1 = facade.CreateSessie("player 1");
            ISpelerSessie sessie2 = facade.CreateSessie("player 2");

            Assert.AreEqual(GeefVerwachteScoreEersteBeurt(sessie1), sessie1.GetScore());
            Assert.AreEqual(GeefVerwachteScoreEersteBeurt(sessie2), sessie2.GetScore());
 
        }

        [Test]
        public void GeenScoreNaAlleenAfleggenEersteKaart()
        {
            BusinessFacade facade = BusinessFacade.GetInstance();

            ISpelerSessie sessie1 = facade.CreateSessie("player 1");
            ISpelerSessie sessie2 = facade.CreateSessie("player 2");

            sessie1.LegKaart(0, BordType.ExpeditieBord);

            //Er is geen kaart gepakt na afleggen de score moet 0 zijn
            Assert.AreEqual(0, sessie1.GetScore());

        }

        [Test]
        public void DoeBeurtEnLeesStatus()
        {
            BusinessFacade facade = BusinessFacade.GetInstance();

            ISpelerSessie sessie1 = facade.CreateSessie("player 1");
            ISpelerSessie sessie2 = facade.CreateSessie("player 2");
            
         }

        /// <summary>
        /// Leg de eerste kaart in de hand aan op expeditie en pak een kaart
        /// </summary>
        /// <param name="sessie">spelersessie</param>
        /// <returns>De verwachte score van de expeditie</returns>
        private int GeefVerwachteScoreEersteBeurt(ISpelerSessie sessie)
        {
            IStapel stapel = sessie.GetHand();
            int verwachteScore = -20;
            if (stapel[0] is IExpeditieKaart) //er is een score
            {
                verwachteScore += ((IExpeditieKaart)stapel[0]).Waarde;
            }
            sessie.LegKaart(0, BordType.ExpeditieBord);
            sessie.PakVanTrekStapel();

            return verwachteScore;
        }


    }
}
