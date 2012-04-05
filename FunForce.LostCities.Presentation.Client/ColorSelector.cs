using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunForce.LostCities.Presentation.Client
{
    /// <summary>
    /// Selecteert de kleuren van de kaarten en aflegstapels op basis van de kleurstring
    /// meegegeven in de constructor
    /// </summary>
    public class ColorSelector
    {
        string _kleurDonker;
        string _kleurLicht;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kleur">de kleurstring</param>
        public ColorSelector(string kleur)
        {
            SetKleur(kleur);
        }

        /// <summary>
        /// Property: Donkere kleur van de kaart
        /// </summary>
        public string KleurDonker
        {
            get { return _kleurDonker; }
            set { _kleurDonker = value; }
        }

        /// <summary>
        /// Property: Lichte kleur van de kaart
        /// </summary>
        public string KleurLicht
        {
            get { return _kleurLicht; }
            set { _kleurLicht = value; }
        }

        /// <summary>
        /// Set de kleuren obv de kleurstring
        /// </summary>
        /// <param name="kleur">kleurstring</param>
        private void SetKleur(string kleur)
        {
            switch (kleur)
            {
                case "Groen":
                    _kleurDonker = "Green";
                    _kleurLicht = "LightGreen";
                    break;
                case "Rood":
                    _kleurDonker = "Red";
                    _kleurLicht = "LightGoldenrodYellow";
                    break;
                case "Wit":
                    _kleurDonker = "LightGray";
                    _kleurLicht = "White";
                    break;
                case "Blauw":
                    _kleurDonker = "Blue";
                    _kleurLicht = "LightBlue";
                    break;
                case "Geel":
                    _kleurDonker = "Yellow";
                    _kleurLicht = "LightYellow";
                    break;
                case "Transparant":
                    _kleurDonker = "Gray";
                    _kleurLicht = "Transparent";
                    break;
            }
        }


    }
}
