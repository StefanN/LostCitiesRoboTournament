using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class LegKaartAanActie : LegActie
    {
        public LegKaartAanActie(int indexInHand, Speler speler) : base(indexInHand, speler)
        {
        }
    }
}
