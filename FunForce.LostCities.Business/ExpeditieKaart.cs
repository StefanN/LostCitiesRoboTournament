using System;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
	/// <summary>
	/// Description of ExpeditieKaart.
	/// </summary>
	public class ExpeditieKaart : Kaart, IExpeditieKaart
	{
		private int waarde = 0;
		public ExpeditieKaart(Kleur kleur, int waarde): base(kleur)
		{
			if(waarde<2)
				throw new LostCitiesException("ongeldige waarde");
			
			if(waarde>10)
				throw new LostCitiesException("ongeldige waarde");
			
			this.waarde = waarde;
			
		}
		public int Waarde
		{
			get{return this.waarde;}
		}
	}
	
}
