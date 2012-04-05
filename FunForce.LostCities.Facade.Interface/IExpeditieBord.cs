using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade.Interface
{
    public interface IExpeditieBord
    {
        IExpeditieStapel GetExpeditieStapel(Kleur kleur);
    }
}
