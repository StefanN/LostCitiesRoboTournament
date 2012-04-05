using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FunForce.LostCities.Presentation.TournamentClient
{
    public class Slider : INotifyPropertyChanged
    {
        private int _value;
        
        public int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
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
