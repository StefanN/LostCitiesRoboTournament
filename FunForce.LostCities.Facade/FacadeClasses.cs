using System;
using System.Collections.Generic;
using System.Text;

namespace FunForce.LostCities.Facade
{
    public class SpelBord 
    {
        public AflegBord aflegBord;
        public Speler speler1;
        public Speler speler2;
        public Stapel[] trekStapel;

        public SpelBord()
        {
            aflegBord = new AflegBord();
            speler1 = new Speler(new ExpeditieBord(), new Stapel());
            speler2 = new Speler(new ExpeditieBord(), new Stapel());
            trekStapel = new Stapel[5];
        }
    }

    public class Speler 
    {
        public ExpeditieBord expeditieBord;
        public Stapel handStapel;

        public Speler(ExpeditieBord bord, Stapel stapel)
        {
            expeditieBord = bord;
            handStapel = stapel;
        }
    }

    public class ExpeditieBord 
    {
        public Stapel[] expeditieStapels;
    }

    public class AflegBord 
    {
        public Stapel[] aflegbordStapels;
    }

    public class Stapel 
    {
        public Kaart[] kaarten;
    }

    public class Kaart
    {
        public FunForce.LostCities.Business.Kleur kleur;
        public int waarde;
    }
}
