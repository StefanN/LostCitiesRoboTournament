using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class ExpeditieStapel: Stapel, IExpeditieStapel
    {
        public bool KanAangelegd(IKaart kaart, out string melding)
        {
            melding = String.Empty;
            if (this.AantalKaarten == 0)
            {
                return true;
            }

            Kaart bovensteKaart = this[this.AantalKaarten - 1]; ;

            if (kaart is IExpeditieKaart && bovensteKaart is ExpeditieKaart)
            {
                if (((ExpeditieKaart)kaart).Waarde <= ((ExpeditieKaart)bovensteKaart).Waarde)
                {
                    melding = "Kaarten mogen alleen in oplopende waarde aangelegd worden aan een expeditie";
                    return false;
                }
            }
            else if (kaart is IWeddenschapsKaart && bovensteKaart is ExpeditieKaart)
            {
                melding = "Na een expeditiekaart mag geen weddenschapskaart worden aangelegd";
                return false;
            }


            return true;
        }

        public int GetScore()
        {
            int score = 0;
            int multiplier = 1;

            for (int i = 0; i < Kaarten.Count; i++)
            {
                Kaart kaart = (Kaart)Kaarten[i];
                if (kaart is WeddenschapsKaart)
                {
                    multiplier += 1;
                }
                else if (kaart is ExpeditieKaart)
                {
                    score += ((ExpeditieKaart)kaart).Waarde;
                }
            }

            if (Kaarten.Count > 0)
                score -= 20;

            score *= multiplier;

            if (Kaarten.Count >= 8)
                score += 20;

            return score;
        }

        #region IExpeditieStapel Members

        public IKaart GetKaart(int index)
        {
            return this[index];
        }

        #endregion
    }
}
