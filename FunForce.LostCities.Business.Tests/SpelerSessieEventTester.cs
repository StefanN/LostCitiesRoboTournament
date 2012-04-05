using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business.Tests
{
    public class SpelerSessieEventTester
    {
        private int aantalBeurtWisselEventsSessie1 = 0;
        private int aantalBeurtWisselEventsSessie2 = 0;

        public SpelerSessieEventTester(ISpelerSessie sessie1, ISpelerSessie sessie2)
        {
            sessie1.BeurtWissel+=new EventHandler(sessie1_BeurtWissel);
            sessie2.BeurtWissel+=new EventHandler(sessie2_BeurtWissel);
        }

        public int AantalBeurtWisselEventsSessie1
        {
            get{return aantalBeurtWisselEventsSessie1;}
        }

        public int AantalBeurtWisselEventsSessie2
        {
            get{return aantalBeurtWisselEventsSessie2;}
        }
        
        private void sessie1_BeurtWissel(object sender, EventArgs e)
        {
 	        aantalBeurtWisselEventsSessie1++;
        }

        private void  sessie2_BeurtWissel(object sender, EventArgs e)
        {
 	        aantalBeurtWisselEventsSessie2++;
        }
    }
}
