using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FunForce.LostCities.Presentation.TournamentClient
{
    public class WPFStandItem : INotifyPropertyChanged
    {
        private string _teamNaam;

        public string TeamNaam
        {
            get { return _teamNaam; }
            set { _teamNaam = value; OnPropertyChanged("TeamNaam"); }
        }

        private int _gespeeld;

        public int AantalGespeeld
        {
            get { return _gespeeld; }
            set { _gespeeld = value; OnPropertyChanged("AantalGespeeld"); }
        }
        private int _punten;

        public int Punten
        {
            get { return _punten; }
            set { _punten = value; OnPropertyChanged("Punten"); }
        }
        private int _voor;

        public int Voor
        {
            get { return _voor; }
            set { _voor = value; OnPropertyChanged("Voor"); }
        }
        private int _tegen;

        public int Tegen
        {
            get { return _tegen; }
            set { _tegen = value; OnPropertyChanged("Tegen"); }
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
