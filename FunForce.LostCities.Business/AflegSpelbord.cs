using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
     public class AflegSpelbord : Spelbord, IAflegBord
    {
        public AflegSpelbord()
        {
            base.stapels = new AflegStapel[5];
            for (int i = 0; i < stapels.Length; i++)
            {
                stapels[i] = new AflegStapel();
            }
        }

         public Kaart PakBovensteKaart(Kleur kleur)
         {
             Stapel kleurStapel = GetKleurStapel(kleur);
             if (kleurStapel.AantalKaarten > 0)
             {
                 Kaart bovensteKaart = kleurStapel.PakBovensteKaart();
                 return bovensteKaart;
             }
             else
             {
                 throw new ApplicationException("Kaart van gekozen kleur is niet beschikbaar");
             }
         }

         #region IAflegBord Members

         public IAflegStapel GetAflegStapel(Kleur kleur)
         {
             return (IAflegStapel) base.GetKleurStapel(kleur);
         }

 
         #endregion
     }
}
