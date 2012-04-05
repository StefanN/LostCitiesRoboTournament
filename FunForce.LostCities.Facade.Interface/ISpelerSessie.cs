using System;
namespace FunForce.LostCities.Facade.Interface
{
    public enum BordType
    {
        ExpeditieBord = 0,
        AflegBord = 1
    }
    public interface ISpelerSessie
    {
        event EventHandler BeurtWissel;

        int GetScore();
        IStapel GetHand();
        bool IsActievepeler();
        string Naam { get; }

        int GetAantalKaartenOpTrekstapel();

        //Nieuw RZ 24-05-2008
        int GetAantalKaartenOpAflegstapel(Kleur kleur);

        void LegKaart(int indexInHand, BordType bordType);
        void PakVanTrekStapel();
        void PakVanAflegStapel(Kleur kleur);

        IAflegBord GetAflegBord();

        string NaamTegenstander { get; }
        int GetScoreTegenstander();

        IExpeditieBord GetSpelerBord();
        IExpeditieBord GetTegenstanderBord();

        ILaatsteBeurt GetLaatsteBeurt();

    }
}
