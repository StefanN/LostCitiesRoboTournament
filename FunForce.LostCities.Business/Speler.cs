using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    /// <summary>
    /// speler binnen het spel
    /// </summary>
    public class Speler
    {
        private ExpeditieSpelbord bord;
        private Stapel hand;
        private LaatsteBeurt _laatsteBeurt;


        /// <summary>
        /// constructor voor speler
        /// </summary>
        /// <param name=bord">het spelbord waarop de speler zijn expedities aanlegt</param>
        /// <param name="hand">de hand met kaarten</param>
        public Speler()
        {
            bord = new ExpeditieSpelbord();
            hand = new Stapel();
            _laatsteBeurt = new LaatsteBeurt();
        }

 


        /// <summary>
        /// het spelbord waarop de speler zijn expedities aanlegt
        /// </summary>
        public ExpeditieSpelbord Bord
        {
            get { return bord; }
        }

        /// <summary>
        /// de kaarten die de speler heeft
        /// </summary>
        public Stapel Hand
        {
            get { return hand; }
            set { hand = value; }
        }


        public LaatsteBeurt LaatsteBeurt
        {
            get { return _laatsteBeurt; }
            set { _laatsteBeurt = value; }
        }

        /// <summary>
        /// Bepaalt de actie die de speler wil uitvoeren
        /// </summary>
        /// <returns></returns>
        public Beurt BepaalActies()
        {
            //om te bepalen wat de volgend actie is, 
            //heeft een speler nodig
            //1) zijn kaarten
            //2) het spelbord
            //3) zijn spelbord
            //4) het spelbord van de tegenstander
            //5) de huidige score
            //6) het aantal kaarten dat nog op de trekstapel ligt
            

            //voor nu, leg altijd eerste kaart weg....
            return new Beurt(new LegKaartAanActie(0, this), new PakKaartVanTrekStapelActie(this)); 
        }


    }
}
