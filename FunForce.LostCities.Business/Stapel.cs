using System;
using System.Collections;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
	/// <summary>
	/// Description of Stapel.
	/// </summary>
	public class Stapel : IStapel
	{
		//TODO: vervang ArrayList door Generic<Kaart>
		private ArrayList kaarten = new ArrayList();
        private static Random r = new Random(DateTime.Now.Millisecond);
		
        public Stapel()
		{

		}

        protected ArrayList Kaarten
        {
            get{return kaarten;}
        }

		public void AddKaart(Kaart kaart)
		{
			kaarten.Add(kaart);
		}
		
		public int AantalKaarten
		{
			get{return kaarten.Count;}
		}
		
		public Kaart PakBovensteKaart()
		{
			int indexBovenste = kaarten.Count-1;
            return PakKaart(indexBovenste);
		}

        public Kaart PakKaart(int index)
        {
            if (kaarten.Count == 0)
                throw new LostCitiesException("kan geen kaart pakken van lege stapel");
			
            if(index > kaarten.Count-1)
                throw new LostCitiesException("geen kaart op deze index");

            Kaart kaart = (Kaart)kaarten[index];

            kaarten.RemoveAt(index);
            
            return kaart;
        
        }

        public Kaart this[int index]
        {
            get {

                if (kaarten.Count == 0)
                    throw new LostCitiesException("kan geen kaart pakken van lege stapel");
                
                if(index>kaarten.Count-1)
                    throw new LostCitiesException("geen geldige index");

                return (Kaart)kaarten[index]; 
            }
        }

        public void Schud()
        {
            //gebruik Fisher-Yates algoritme
            for(int i=0;i<kaarten.Count;i++)
            {
                int n = r.Next(i, kaarten.Count);
                Object temp = kaarten[n];
                kaarten[n] = kaarten[i];
                kaarten[i] = temp;
            }
        }



        #region IStapel Members


        IKaart IStapel.this[int index]
        {
            get { return (IKaart)this[index]; }
        }

        #endregion


        #region IAflegStapel Members

        public IKaart GetBovensteKaart()
        {
            if (AantalKaarten == 0)
                return null;

            int indexBovenste = kaarten.Count - 1;
            return this[indexBovenste];
        }

        #endregion
 

    }
}
