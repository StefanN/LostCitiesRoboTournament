using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FunForce.LostCities.Presentation.TournamentClient
{
    public class WPFResult : INotifyPropertyChanged
    {
        private string _resultaatTeam1;

        public string ResultaatTeam1
        {
            get { return _resultaatTeam1; }
            set { _resultaatTeam1 = value; OnPropertyChanged("ResultaatTeam1"); }
        }
        private string _resultaatTeam2;

        public string ResultaatTeam2
        {
            get { return _resultaatTeam2; }
            set { _resultaatTeam2 = value; OnPropertyChanged("ResultaatTeam2"); }

        }

        private string _resultaatInfo;

        public string ResultaatInfo
        {
            get { return _resultaatInfo; }
            set { _resultaatInfo = value; OnPropertyChanged("ResultaatInfo"); }

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
