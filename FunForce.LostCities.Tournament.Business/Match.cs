using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Tournament.Business
{
    public class Match
    {
        private int matchId;
        private SimulatedPlayer player1;
        private SimulatedPlayer player2;
        private IList<MatchResult> results;

        public Match()
        {
        }

        public Match(int matchId, SimulatedPlayer player1, SimulatedPlayer player2)
        {
            this.matchId = matchId;
            this.player1 = player1;
            this.player2 = player2;
        }

        public int MatchId
        {
            get { return matchId; }
        }

        public SimulatedPlayer Speler1
        {
            get { return this.player1; }
        }

        public SimulatedPlayer Speler2
        {
            get { return this.player2; }
        }


        public IList<MatchResult> Results
        {
            get { return results; }
        }


        public void Simulate()
        {
            Simulate(1);
        }

        public void Simulate(int times)
        {
            results = new List<MatchResult>();
            for (int i = 0; i < times; i++)
            {
                results.Add(SimulateSingleMatch());
            }
        }

  

        public int GetScorePlayer1(int topIndex)
        {
            int total = 0;
            int index = 0;
            foreach (MatchResult result in results)
            {
                if (index > topIndex)
                    break;
                index++;
                total += result.ScorePlayer1;
            }
            return total;
        }

        public int GetScorePlayer2(int topIndex)
        {
            int total = 0;
            int index = 0;
            foreach (MatchResult result in results)
            {
                if (index > topIndex)
                    break;
                total += result.ScorePlayer2;
                index++;
            }
            return total;
        }

        private MatchResult SimulateSingleMatch()
        {
            BusinessFacade.Reset();
            ISpelerSessie sessie1=null;
            ISpelerSessie sessie2=null;
            MatchResult result;
            try
            {
                sessie1 = BusinessFacade.GetInstance().CreateSessie(player1.Naam, player1.Strategy);
                sessie2 = BusinessFacade.GetInstance().CreateSessie(player2.Naam, player2.Strategy);
                
                //create en run spel is uit createsessie verwijderd om in ieder geval 2 instanties van sessie te hebben voor exceptionhandling
                BusinessFacade.GetInstance().CreateAndRunSpel();

                result = new MatchResult(sessie1.GetScore(), sessie2.GetScore());
            }
            catch (SimulationFailException simEx)
            {
                //naam van bot wordt nu doorgegeven aan CPU speler
                //naam moet vanaf nu uniek zijn
                //todo maak een check
                if(simEx.Strategy.Naam == player1.Strategy.Naam)
                {
                    //player1 caused a crash
                    int score = 0;
                    if (sessie2 != null)
                    {
                        score = sessie2.GetScore();
                    }

                    result = new MatchResult(-10000, score);
                    result.MessagePlayer1 = simEx.Message;
                }
                else
                {
                    int score = 0;
                    if (sessie1 != null)
                    {
                        score = sessie1.GetScore();
                    }
                    //player2 caused a crash
                    result = new MatchResult(score, -10000);
                    result.MessagePlayer2 = simEx.Message;
                }
            }
            return result;
        }


    }
}
