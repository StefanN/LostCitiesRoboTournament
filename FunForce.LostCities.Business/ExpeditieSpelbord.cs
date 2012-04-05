using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class ExpeditieSpelbord : Spelbord, IExpeditieBord
    {
        public ExpeditieSpelbord()
        {
            base.stapels = new ExpeditieStapel[5];
            for (int i = 0; i < stapels.Length; i++)
            {
                stapels[i] = new ExpeditieStapel();
            }
        }

        /// <summary>
        /// legt een kaart aan, aan de juiste stapel op het bord
        /// </summary>
        /// <param name="kaart">de kaart die wordt aangelegd</param>
        public override void LegKaartAan(Kaart kaart)
        {
            //ExpeditieKaart bovensteExpKaart;
            //ExpeditieKaart expKaart;

            ExpeditieStapel kleurStapel = (ExpeditieStapel)this.GetExpeditieStapel(kaart.Kleur);

            string melding = String.Empty;

            if (kleurStapel.KanAangelegd(kaart, out melding))
            {
                GetKleurStapel(kaart.Kleur).AddKaart(kaart);
            }
               else
                    throw new LostCitiesException(melding);
        }

        public int GetScore()
        {
            int score = 0;
            foreach (ExpeditieStapel stapel in stapels)
            {
                score += stapel.GetScore();
            }
            return score;
        }

        #region IExpeditieBord Members

        public IExpeditieStapel GetExpeditieStapel(Kleur kleur)
        {
            return (IExpeditieStapel)base.GetKleurStapel(kleur);
        }

        #endregion
    }
}

