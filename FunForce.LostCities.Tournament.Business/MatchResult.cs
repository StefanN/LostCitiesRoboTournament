using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Tournament.Business
{
    public class MatchResult
    {

        private int scorePlayer1 = 0;
        private int scorePlayer2 = 0;
        private int pointsPlayer1 = 0;
        private int pointsPlayer2 = 0;
        private string msgPlayer1 = String.Empty;
        private string msgPlayer2 = String.Empty;

        public MatchResult(int pointsPlayer1, int pointsPlayer2)
        {
            this.pointsPlayer1 = pointsPlayer1;
            this.pointsPlayer2 = pointsPlayer2;

            if (pointsPlayer1 == pointsPlayer2)
            {
                scorePlayer1 = 1;
                scorePlayer2 = 1;
            }
            else if (pointsPlayer1 > pointsPlayer2)
            {
                scorePlayer1 += 3;
            }
            else
            {
                scorePlayer2 += 3;
            }
        }

        public string MessagePlayer1
        {
            get { return msgPlayer1; }
            set { msgPlayer1 = value; }
        }

        public string MessagePlayer2
        {
            get { return msgPlayer2; }
            set { msgPlayer2 = value; }
        }

        public int PointsPlayer1
        {
            get { return pointsPlayer1; }
        }

        public int PointsPlayer2
        {
            get { return pointsPlayer2; }
        }

        public int ScorePlayer1
        {
            get { return scorePlayer1; }
        }

        public int ScorePlayer2
        {
            get { return scorePlayer2; }
        }

    }
}
