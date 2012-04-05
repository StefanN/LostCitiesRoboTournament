using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{
    public interface IAflegBord
    {
        IAflegStapel GetAflegStapel(Kleur kleur);
    }
}
