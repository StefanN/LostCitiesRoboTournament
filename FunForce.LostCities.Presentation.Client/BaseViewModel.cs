using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FunForce.LostCities.Presentation.Client
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {
        }

        protected void OnPropertyChanged(string PropertyName)
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
