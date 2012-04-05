using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{
    public class SimulationFailException : Exception
	{
        private ISimulatieSpeler strategie;
        public SimulationFailException(ISimulatieSpeler strategie, string message)
            : base(message)
		{
            this.strategie = strategie;
		}

        public ISimulatieSpeler Strategy
        {
            get { return this.strategie; }
        }
    }
}
