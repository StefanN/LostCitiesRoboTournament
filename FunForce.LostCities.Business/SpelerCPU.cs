using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class SpelerCPU : Speler, ISimulatieSpeler
    {

        #region ISpeler Members

        private ISimulatieSpeler simulatieStrategie;
        private string naam;

        public SpelerCPU(ISimulatieSpeler simulatieStrategie)
        {
            this.simulatieStrategie = simulatieStrategie;
            this.naam = simulatieStrategie.Naam;
        }

 
        public string Naam
        {
            get { return this.naam; }
        }

        public ISpelBeurt BepaalBeurt(ISpelStatus status)
        {

            return simulatieStrategie.BepaalBeurt(status);

        }

        #endregion


        public static int[] BepaalMogelijkeLegActies(IStapel hand, IExpeditieBord bord)
        {
            ArrayList mogelijkheden = new ArrayList(); ;
            string melding = String.Empty;
            for (int i = 0; i < hand.AantalKaarten; i++)
            {
                IKaart kaart = hand[i];
                ExpeditieStapel stapel = (ExpeditieStapel)bord.GetExpeditieStapel(kaart.Kleur);

                if (stapel.KanAangelegd(kaart, out melding))
                {
                    mogelijkheden.Add(i);
                }
            }
            return (int[])mogelijkheden.ToArray(typeof(int));
        }
    }


}
