using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{
    public interface ISimulatieSpeler
    {
        /// <summary>
        /// geeft de naam van de speler
        /// </summary>
        string Naam { get; }

        /// <summary>
        /// bepaal de acties voor de beurt op basis van de status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        ISpelBeurt BepaalBeurt(ISpelStatus status);

    }
}
