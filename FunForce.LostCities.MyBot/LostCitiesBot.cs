using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;
using System.Collections;

namespace FunForce.LostCities.MyBot
{
    public class LostCitiesBot : ISimulatieSpeler
    {
        #region ISpeler Members

        public string Naam
        {
            //Vul hier de naam van je bot. Maximaal 20 karakters. 
            get { return "MyBot"; }
        }

        public ISpelBeurt BepaalBeurt(ISpelStatus status)
        {
            int[] mogelijkeLegActies = BepaalMogelijkeLegActies(status.Hand, status.Bord);

            SpelBeurt beurt = new SpelBeurt();

            if (mogelijkeLegActies.Length > 0)
            {
                //leg de eerste mogelijke kaart aan
                beurt.LegkaartAan(mogelijkeLegActies[0]);
                //beurt.PakKaartVanAflegstapel(
                //status.Bord.GetExpeditieStapel(
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

        #endregion


        private static int[] BepaalMogelijkeLegActies(IStapel hand, IExpeditieBord bord)
        {
            ArrayList mogelijkheden = new ArrayList(); ;
            string melding = String.Empty;
            for (int i = 0; i < hand.AantalKaarten; i++)
            {
                IKaart kaart = hand[i];
                IExpeditieStapel stapel = (IExpeditieStapel)bord.GetExpeditieStapel(kaart.Kleur);

                if (KanAangelegd(kaart, bord))
                {
                    mogelijkheden.Add(i);
                }
            }
            return (int[])mogelijkheden.ToArray(typeof(int));
        }


        private static bool KanAangelegd(IKaart kaart, IExpeditieBord bord)
        {

            IExpeditieStapel stapel = bord.GetExpeditieStapel(kaart.Kleur);

            if (stapel.AantalKaarten == 0)
            {
                return true;
            }

            IKaart bovensteKaart = stapel.GetKaart(stapel.AantalKaarten-1);

            if (kaart is IExpeditieKaart && bovensteKaart is IExpeditieKaart)
            {
                if (((IExpeditieKaart)kaart).Waarde <= ((IExpeditieKaart)bovensteKaart).Waarde)
                {
                    return false;
                }
            }
            else if (kaart is IWeddenschapsKaart && bovensteKaart is IExpeditieKaart)
            {
                return false;
            }

            return true;
        }

    }
}
