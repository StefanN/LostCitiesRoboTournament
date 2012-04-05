using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Tournament.Business
{
    public class SimulatedPlayer
    {
        public ISimulatieSpeler strategy;
        private string naam;
        //private int totalScore=0;

        public SimulatedPlayer(string naam, ISimulatieSpeler strategy)
        {
            this.naam = naam;
            this.strategy = strategy;
        }

        public string Naam
        {
            get { return this.naam; }
        }

        public ISimulatieSpeler Strategy
        {
            get { return this.strategy; }
        }
    }
}
