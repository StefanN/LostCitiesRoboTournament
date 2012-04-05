using System;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
	/// <summary>
	/// Description of WeddenschapsKaart.
	/// </summary>
	public class WeddenschapsKaart : Kaart, IWeddenschapsKaart
	{
		public WeddenschapsKaart(Kleur kleur) : base(kleur)
		{
		}
	}
}
