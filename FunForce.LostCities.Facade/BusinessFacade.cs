using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;
using FunForce.LostCities.Business;

namespace FunForce.LostCities.Facade
{
    public class BusinessFacade
    {
        private static BusinessFacade instance;
        private SpelerSessie sessie1;
        private SpelerSessie sessie2;
        private Spel _spel;

        public static BusinessFacade GetInstance()
        {
            if (instance == null)
                instance = new BusinessFacade();
            return instance;
        }

        private BusinessFacade() { }

        //public event EventHandler BeurtGeweest;

        public ISpelerSessie CreateSessie(string spelerNaam)
        {
            return CreateSessie(spelerNaam, false);
        }

        public ISpelerSessie CreateSessie(string spelerNaam, ISimulatieSpeler strategie)
        {
            SpelerSessie sessie;

            if (sessie1 != null && sessie2 != null)
                throw new ApplicationException("geen plek meer!");

            if (sessie1 == null)
            {
                sessie = CreateSpelerSessie(spelerNaam, strategie);
                sessie1 = sessie;
            }
            else
            {
                sessie = CreateSpelerSessie(spelerNaam, strategie);
                sessie2 = sessie;

                //CreateSpel(sessie1, sessie2);

            }
            return sessie;
        }


        public ISpelerSessie CreateSessie(string spelerNaam, bool cpu)
        {
            SpelerSessie sessie;

            if (sessie1 != null && sessie2 != null)
                throw new ApplicationException("geen plek meer!");

            if (sessie1 == null)
            {
                sessie = CreateSpelerSessie(spelerNaam, cpu);
                sessie1 = sessie;
            }
            else
            {
                sessie = CreateSpelerSessie(spelerNaam, cpu);
                sessie2 = sessie;

                //CreateSpel(sessie1, sessie2);

            }
            return sessie;
        }

        public static void Reset()
        {
            instance = null;
        }

        private static SpelerSessie CreateSpelerSessie(string spelerNaam, ISimulatieSpeler strategie)
        {
            return SpelFactory.CreateSpelerSessie(spelerNaam, strategie);
        }    

        private static SpelerSessie CreateSpelerSessie(string spelerNaam, bool cpu)
        {
            return SpelFactory.CreateSpelerSessie(spelerNaam, cpu);
        }

        //meth0d used to simulate single match
        public void CreateAndRunSpel()
        {
            Spel spel = CreateSpel(sessie1, sessie2);
            spel.Simuleer();


        }

        public void CreateSpel()
        {
            _spel = CreateSpel(sessie1, sessie2);
        }

        public void SimuleerSpel()
        {
            _spel.Simuleer();

        }

        public void SimuleerBeurt()
        {
            _spel.SimuleerBeurt();
        }


        private static Spel CreateSpel(SpelerSessie sessie1, SpelerSessie sessie2)
        {
            return SpelFactory.CreateSpel(sessie1, sessie2);
        }


    }


    
}
