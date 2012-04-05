using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Tournament.Business
{
    public class MatchMaker
    {
        public static IList<SimulatedPlayer> SetupPlayers(IList<Type> strategies)
        {
     
            IList<SimulatedPlayer> spelers = new List<SimulatedPlayer>();

            Dictionary<string, int> nameCount = new Dictionary<string, int>();
            
            for (int i = 0; i < strategies.Count; i++)
            {
                ISimulatieSpeler strategy = (ISimulatieSpeler) System.Activator.CreateInstance(strategies[i]);
                string naam = strategy.Naam;
                if (naam.Length > 60)
                {
                    naam = naam.Substring(0, 60);
                }

                if (nameCount.ContainsKey(naam))
                {
                    nameCount[naam]++;
                    naam += "_" + nameCount[naam].ToString().PadLeft(2, '0'); 
                }
                else
                {
                    nameCount.Add(naam, 1);
                    naam = naam + "_01";
                }

                SimulatedPlayer speler = new SimulatedPlayer(naam, strategy);
                spelers.Add(speler);
            }
            return spelers;
        }


        public static IList<Match> SetupMatches(IList<SimulatedPlayer> players)
        {
            IList<Match> matches = new List<Match>();
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = i + 1; j < players.Count; j++)
                {
                    matches.Add(new Match((i+1), players[i], players[j]));
                }
            }
            return matches;
        }
    }
}
