using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;
using System.Collections;

namespace FunForce.LostCities.Business
{
    public class SimulatieStrategie : ISimulatieSpeler
    {
        public string Naam
        {
            get { return "CPU"; }
        }

        public ISpelBeurt BepaalBeurt(ISpelStatus status)
        {
            int[] mogelijkeLegActies = BepaalMogelijkeLegActies(status.Hand, status.Bord);

            SpelBeurt beurt = new SpelBeurt();

            if (mogelijkeLegActies.Length > 0)
            {
                //leg de eerste mogelijke kaart aan
                beurt.LegkaartAan(mogelijkeLegActies[0]);
            }
            else
            {
                //leg de eerste kaart in de hand af
                beurt.LegKaartWeg(0);
            }

            //pak een kaart van de trekstapel
            beurt.PakKaartVanTrekstapel();

            return beurt;

        }

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
