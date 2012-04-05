using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using FunForce.LostCities.Facade.Interface;
using System.Collections;


namespace FunForce.LostCities.Presentation.Client
{
    /// <summary>
    /// Viewmodel van de hand
    /// </summary>
    public class HandViewModel : ObservableCollection<KaartViewModel>
    {

        /// <summary>
        /// Constructor.
        /// Sorteer de initiele stapel en ken de kaarten gesorteerd toe aan de hand
        /// </summary>
        /// <param name="stapel">Stapel met kaarten van de hand</param>
        public HandViewModel(IStapel stapel)
        {
            SortedList<string, KaartViewModel> sortedStapel = GetSortedStapel(stapel);

            foreach (KeyValuePair<string, KaartViewModel> kvp in sortedStapel)
            {
                this.Add(kvp.Value);
            }
        }

        /// <summary>
        /// Verwerk de nieuwe stapel in de gesorteerde hand
        /// </summary>
        /// <param name="stapel">Actuele stapel kaarten van de hand</param>
        public void VerwerkBeurt(IStapel stapel)
        {
            SortedList<string, KaartViewModel> sortedStapel = GetSortedStapel(stapel);

            int i = 0;
            foreach (KeyValuePair<string, KaartViewModel> kvp in sortedStapel)
            {
                if (!IsZelfdeKaart(kvp.Value, this[i]))
                {
                    this[i] = kvp.Value;
                }
                i++;
            }
        }

        /// <summary>
        /// Methode geeft obv van een stapel een gesorteerde stapel terug
        /// </summary>
        /// <param name="stapel">de stapel kaarten</param>
        /// <returns>gesorteerde lijst van kaarten</returns>
        private SortedList<string, KaartViewModel> GetSortedStapel(IStapel stapel)
        {
            SortedList<string, KaartViewModel> sortedStapel = new SortedList<string, KaartViewModel>();
            for (int i = 0; i < stapel.AantalKaarten; i++)
            {

                KaartViewModel kaart = new KaartViewModel(stapel[i]);
                kaart.Offset = "0";
                string value = "00";
                if (kaart.Waarde.ToLower() != "w")
                {
                    value = kaart.Waarde.PadLeft(2, '0');
                }

                //add i omdat key uniek moet zijn
                string key = kaart.KleurDonker + value + i.ToString();
                sortedStapel.Add(key, kaart);

            }

            return sortedStapel;
        }


        /// <summary>
        /// Methode geeft obv van een stapel een gesorteerde stapel terug
        /// </summary>
        /// <param name="hand">de hand</param>
        /// <returns>gesorteerde lijst van kaarten</returns>
        private SortedList<string, KaartViewModel> GetSortedStapel(ObservableCollection<KaartViewModel> hand)
        {
            SortedList<string, KaartViewModel> sortedStapel = new SortedList<string, KaartViewModel>();
            for (int i = 0; i < hand.Count; i++)
            {

                KaartViewModel kaart = hand[i];
                //kaart.Offset = "0";
                string value = "00";
                if (kaart.Waarde.ToLower() != "w")
                {
                    value = kaart.Waarde.PadLeft(2, '0');
                }

                //add i omdat key uniek moet zijn
                string key = kaart.KleurDonker + value + i.ToString();
                sortedStapel.Add(key, kaart);

            }

            return sortedStapel;
        }
        

       
        /// <summary>
        /// COntroleer of 2 kaarten hetzelfde zijn
        /// </summary>
        /// <param name="kaart1">kaart</param>
        /// <param name="kaart2">kaart</param>
        /// <returns>Boolean geeft aan of het dezelfde kaarten zijn</returns>
        private bool IsZelfdeKaart(KaartViewModel kaart1, KaartViewModel kaart2)
        {
            return kaart1.Equals(kaart2);
        }


    }
}
