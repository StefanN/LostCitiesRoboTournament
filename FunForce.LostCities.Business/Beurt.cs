using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Business
{
    public class Beurt
    {
        private LegActie legActie;
        private PakActie pakActie;
        
        public Beurt(LegActie legActie, PakActie pakActie)
        {
            this.legActie = legActie;
            this.pakActie = pakActie;
        }

        public LegActie LegActie
        {
            get { return legActie; }
        }

        public PakActie PakActie
        {
            get { return pakActie; }
        }
    }
}
