using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class LaatsteBeurt:ILaatsteBeurt
    {
        private IKaart _legKaart;
        private IKaart _pakKaart;
        private IStapel _legStapel;
        private IStapel _pakStapel;

        public IKaart LegKaart
        {
            get { return _legKaart; }
            set { _legKaart = value; }
        }

        public IKaart PakKaart
        {
            get { return _pakKaart; }
            set { _pakKaart = value; }
        }


        public IStapel LegStapel
        {
            get { return _legStapel; }
            set { _legStapel = value; }
        }

        public IStapel PakStapel
        {
            get { return _pakStapel; }
            set { _pakStapel = value; }
        }

    }
}
