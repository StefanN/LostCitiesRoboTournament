using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Business;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class SpelFactory
    {

        public static SpelerSessie CreateSpelerSessie(string spelerNaam)
        {
            return CreateSpelerSessie(spelerNaam, false);
        }

        public static SpelerSessie CreateSpelerSessie(string spelerNaam, bool cpu)
        {
            SpelerSessie sessie = new SpelerSessie(spelerNaam);
            Speler speler;
            if(cpu)
                speler = new SpelerCPU(new SimulatieStrategie());
            else
                speler = new Speler();

            sessie.SetSpeler(speler);
            return sessie;
        }

        public static SpelerSessie CreateSpelerSessie(string spelerNaam, ISimulatieSpeler strategie)
        {
            SpelerSessie sessie = new SpelerSessie(spelerNaam);
            Speler speler = new SpelerCPU(strategie);
            sessie.SetSpeler(speler);
            return sessie;
        }

        public static Spel CreateSpel(SpelerSessie sessie1, SpelerSessie sessie2)
        {
            Spel spel = new Spel();
            spel.Initialiseer(sessie1.Speler, sessie2.Speler);
            sessie1.SetSpel(spel);
            sessie2.SetSpel(spel);

            return spel;
        }
    }
}
