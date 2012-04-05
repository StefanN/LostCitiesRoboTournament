using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FunForce.LostCities.Tournament.Business;
using System.Collections.Specialized;

namespace FunForce.LostCities.Presentation.TournamentClient 
{
    public class WPFMatch : INotifyPropertyChanged
    {
        private Match _match;

        private WPFResults _resultaten;
        public WPFMatch(Match match)
        {
            _match = match;

            this.NaamTeam1 = _match.Speler1.Naam;
            this.NaamTeam2 = _match.Speler2.Naam;

            this._resultaten = new WPFResults();
            this.Gespeeld = false;
        }

        public void Update(int resultaatNummer)
        {
            this.TotaalScoreTeam1 = _match.GetScorePlayer1(resultaatNummer);
            this.TotaalScoreTeam2 = _match.GetScorePlayer2(resultaatNummer);
            AddNewResultaat(resultaatNummer);
        }

        private void AddNewResultaat(int resultaatNummer)
        {
            MatchResult matchResult = _match.Results[resultaatNummer];
            WPFResult wpfResult = new WPFResult();
            if (matchResult.PointsPlayer1 == -10000)
            {
                wpfResult.ResultaatTeam1 = "X";
            }
            else
            {
                wpfResult.ResultaatTeam1 = matchResult.PointsPlayer1.ToString();

            }
            if (matchResult.PointsPlayer2 == -10000)
            {
                wpfResult.ResultaatTeam2 = "X";
            }
            else
            {
                wpfResult.ResultaatTeam2 = matchResult.PointsPlayer2.ToString();

            }

            if (matchResult.MessagePlayer1.Length > 0 && matchResult.MessagePlayer2.Length > 0)
            {
                wpfResult.ResultaatInfo = "Fout beide spelers kan niet";
            }
            else if (matchResult.MessagePlayer1.Length > 0)
            {
                wpfResult.ResultaatInfo = matchResult.MessagePlayer1;
            }
            else if (matchResult.MessagePlayer2.Length > 0)
            {
                wpfResult.ResultaatInfo = matchResult.MessagePlayer2;
            }
            else
            {
                if (matchResult.PointsPlayer1 > matchResult.PointsPlayer2)
                {
                    wpfResult.ResultaatInfo = _match.Speler1.Naam + " wint";
                }
                else if (matchResult.PointsPlayer1 < matchResult.PointsPlayer2)
                {
                    wpfResult.ResultaatInfo = _match.Speler2.Naam + " wint";
                }
                else if (matchResult.PointsPlayer1 == matchResult.PointsPlayer2)
                {
                    wpfResult.ResultaatInfo = "Geen winnaar";
                }
                else
                {
                    wpfResult.ResultaatInfo = "???";
                }
            }

            this.Resultaten.Insert(0, wpfResult);
        }

        private int _puntenTeam1;

        public int PuntenTeam1
        {
            get 
            {
                if (this.Gespeeld)
                {
                    if (_totaalScoreTeam1 > _totaalScoreTeam2)
                    {
                        return 3;
                    }
                    else if (_totaalScoreTeam1 == _totaalScoreTeam2)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set { _puntenTeam1 = value; OnPropertyChanged("PuntenTeam1"); }
        }

        private int _puntenTeam2;

        public int PuntenTeam2
        {
            get 
            {
                if (this.Gespeeld)
                {
                    if (_totaalScoreTeam1 < _totaalScoreTeam2)
                    {
                        return 3;
                    }
                    else if (_totaalScoreTeam1 == _totaalScoreTeam2)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set { _puntenTeam2 = value; OnPropertyChanged("PuntenTeam2"); }
        }

        private string _naamTeam1;

        public string NaamTeam1
        {
            get { return _naamTeam1; }
            set { _naamTeam1 = value; OnPropertyChanged("NaamTeam1"); }
        }
        private string _naamTeam2;

        public string NaamTeam2
        {
            get { return _naamTeam2; }
            set { _naamTeam2 = value; OnPropertyChanged("NaamTeam2"); }

        }

        private bool _gespeeld;
        public bool Gespeeld
        {
            get { return _gespeeld; }
            set { _gespeeld = value; OnPropertyChanged("Gespeeld"); }

        }
        private int _totaalScoreTeam1;

        public int TotaalScoreTeam1
        {
            get { return _totaalScoreTeam1; }
            set { _totaalScoreTeam1 = value; OnPropertyChanged("TotaalScoreTeam1"); }
        }
        private int _totaalScoreTeam2;

        public int TotaalScoreTeam2
        {
            get { return _totaalScoreTeam2; }
            set { _totaalScoreTeam2 = value; OnPropertyChanged("TotaalScoreTeam2"); }
        }


        public WPFResults Resultaten
        {
            get { return _resultaten; }
            set { _resultaten = value; }
        }



        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler p = PropertyChanged;
            if (p != null)
            {
                p(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion
    }
}
