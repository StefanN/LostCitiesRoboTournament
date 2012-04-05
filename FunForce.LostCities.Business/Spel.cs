using System;
using System.Collections.Generic;
using System.Text;
using FunForce.LostCities.Facade.Interface;

namespace FunForce.LostCities.Business
{
    public class Spel
    {

        public event EventHandler BeurtWissel;
        private delegate ISpelBeurt DoWorkNeedsTimeoutDelegate(ISpelStatus status);

        private Stapel trekStapel;
        private AflegSpelbord aflegSpelbord;
        private Speler[] spelers;
        private int huidigeSpeler=0;
        private bool rondeAfgelopen = true;

        public Spel()
        {
            trekStapel = CreateStartStapel();
            rondeAfgelopen = false;

            aflegSpelbord = new AflegSpelbord();

            spelers = new Speler[2];
            spelers[0] = new Speler();
            spelers[1] = new Speler();
        }

        public bool IsAfgelopen()
        {
            return (this.trekStapel.AantalKaarten == 0);
        }

        private void OnBeurtWissel()
        {
            if (BeurtWissel != null)
                BeurtWissel(this, new EventArgs());
        }

        /// <summary>
        /// voert de door de speler gewenste acties uit
        /// </summary>
        /// <param name="acties"></param>
        /// <returns></returns>
        public void VoerActiesUit(Beurt beurt)
        {
            HuidigeSpeler.LaatsteBeurt.LegKaart = null;
            HuidigeSpeler.LaatsteBeurt.LegStapel = null;
            HuidigeSpeler.LaatsteBeurt.PakKaart = null;
            HuidigeSpeler.LaatsteBeurt.PakStapel = null;

            if (rondeAfgelopen)
                throw new LostCitiesException("De ronde is voorbij");

            //eerste actie mag alleen
            // LegKaartWeg of LegKaartAan zijn
            //tweede actie mag alleen
            //PakKaartVanBord zijn
            if (beurt.LegActie.Speler != HuidigeSpeler || beurt.PakActie.Speler != HuidigeSpeler)
            {
                throw new LostCitiesException("speler is niet aan de beurt");
            }
  
            if (IsAfgelopen())
            {
                throw new LostCitiesException("spel is afgelopen");
            }

            Speler speler = HuidigeSpeler;
            if (beurt.LegActie is LegKaartWegActie)
            {
                int index = ((LegKaartWegActie) beurt.LegActie).IndexInHand;
                Kaart kaart = HuidigeSpeler.Hand.PakKaart(index);
                this.AflegBord.LegKaartAan(kaart);

                HuidigeSpeler.LaatsteBeurt.LegKaart = kaart;
                HuidigeSpeler.LaatsteBeurt.LegStapel = (Stapel)this.AflegBord.GetAflegStapel(kaart.Kleur);

           }

            if (beurt.LegActie is LegKaartAanActie)
            {
                int index = ((LegKaartAanActie)beurt.LegActie).IndexInHand;
                Kaart kaart = HuidigeSpeler.Hand.PakKaart(index);
                HuidigeSpeler.Bord.LegKaartAan(kaart);

                HuidigeSpeler.LaatsteBeurt.LegKaart = kaart;
                HuidigeSpeler.LaatsteBeurt.LegStapel = (Stapel)HuidigeSpeler.Bord.GetExpeditieStapel(kaart.Kleur);
            }
   
            if (beurt.PakActie is PakKaartVanTrekStapelActie)
            {
                Kaart kaart = trekStapel.PakBovensteKaart();
                HuidigeSpeler.Hand.AddKaart(kaart);
                
                HuidigeSpeler.LaatsteBeurt.PakKaart = kaart;
                HuidigeSpeler.LaatsteBeurt.PakStapel = (Stapel)trekStapel;

                WisselBeurt();


            }
            
            if (beurt.PakActie is PakKaartVanAflegStapelActie)
            {
                Kleur kleur = (Kleur)((PakKaartVanAflegStapelActie)beurt.PakActie).KaartKleur;
                Kaart kaart = this.aflegSpelbord.PakBovensteKaart(kleur);
                HuidigeSpeler.Hand.AddKaart(kaart);

                HuidigeSpeler.LaatsteBeurt.PakKaart = kaart;
                HuidigeSpeler.LaatsteBeurt.PakStapel = (Stapel)HuidigeSpeler.Bord.GetExpeditieStapel(kaart.Kleur);

                WisselBeurt();
            }

        }

        private void WisselSpelerBeurt(bool simuleerSpel)
        {
            rondeAfgelopen = IsAfgelopen();

            if (rondeAfgelopen)
                //TODO: event gooien?
                return;

            OnBeurtWissel();

            if (simuleerSpel)
            {
                if (HuidigeSpeler is SpelerCPU)
                {
                    SimuleerSpel(simuleerSpel);
                }
            }


        }


        private void WisselBeurt()
        {
            huidigeSpeler = GetTegenstanderIndex();
        }

        /// <summary>
        /// speler die aan de beurt is
        /// </summary>
        public Speler HuidigeSpeler
        {
            get { return spelers[huidigeSpeler]; }
        }

        /// <summary>
        /// eerste speler
        /// </summary>
        public Speler Speler1
        {
            get { return spelers[0]; }
        }

        /// <summary>
        /// tweede speler
        /// </summary>
        public Speler Speler2
        {
            get { return spelers[1]; }
        }

        /// <summary>
        /// stapel waarvan de spelers hun kaarten trekken
        /// </summary>
        public Stapel TrekStapel
        {
            get { return trekStapel;}
        }

        /// <summary>
        /// spelbord waarop beide spelers hun kaarten afleggen
        /// </summary>
        public AflegSpelbord AflegBord
        {
            get { return aflegSpelbord; }
        }

        public void Initialiseer()
        {
            Initialiseer(Speler1, Speler2);
        }

        private ISpelStatus CreateSpelStatus(Speler speler)
        {
            return new SpelStatus(HuidigeSpeler.Hand, AflegBord, HuidigeSpeler.Bord,
                            GetTegenstander().Bord, HuidigeSpeler.Bord.GetScore(),
                            GetTegenstander().Bord.GetScore(), trekStapel.AantalKaarten);
        }


        public Speler GetTegenstander()
        {
            if (HuidigeSpeler == Speler1)
                return Speler2;
            else
                return Speler1;

        }

        public void Initialiseer(Speler speler1, Speler speler2)
        {
            spelers[0] = speler1;

            spelers[1] = speler2;

            speler1.Hand = new Stapel();
            speler2.Hand = new Stapel();

            this.trekStapel = CreateStartStapel();

            rondeAfgelopen = false;

            aflegSpelbord = new AflegSpelbord();

            Stapel[] stapels = new Stapel[2];
            stapels[0] = spelers[0].Hand;
            stapels[1] = spelers[1].Hand;

            TrekStapel.Schud();

            DeelKaarten(stapels);

            huidigeSpeler = 0;

            //if (Speler1 is SpelerCPU && Speler2 is SpelerCPU)
            //{
            //        SimuleerSpel();
            //}

        }

        public void Simuleer()
        {
            if (spelers[0] is SpelerCPU && spelers[1] is SpelerCPU)
            {
                SimuleerSpel(true);
             }

        }

        public void SimuleerBeurt()
        {
            if (spelers[0] is SpelerCPU && spelers[1] is SpelerCPU)
            {
                SimuleerSpel(false);
            }
        }


        private ISpelBeurt BepaalBeurtMetTimeOut(ISpelStatus status, ISimulatieSpeler speler, int millisecondsToWait)
        {
            DoWorkNeedsTimeoutDelegate deleg = new DoWorkNeedsTimeoutDelegate(speler.BepaalBeurt);

            IAsyncResult ar = deleg.BeginInvoke(status, null, new object());

            if (!ar.AsyncWaitHandle.WaitOne(millisecondsToWait, false))
            {
                throw new SimulationFailException(speler, "TimeOut");
            }
            else
            {
                return deleg.EndInvoke(ar);
            }
        }    

        public void SimuleerSpel(bool simuleerSpel)
        {
            ISpelStatus status = CreateSpelStatus(HuidigeSpeler);
            ISimulatieSpeler speler = ((ISimulatieSpeler)HuidigeSpeler);
       
            try
            {
                //ISpelBeurt spelbeurt = BepaalBeurtMetTimeOut(status, speler, 1);
                ISpelBeurt spelbeurt = BepaalBeurtMetTimeOut(status, speler, 200);
                //ISpelBeurt spelbeurt = BepaalBeurtMetTimeOut(status, speler, 360000);
                Beurt beurt = ConvertBeurt(spelbeurt, HuidigeSpeler);
                VoerActiesUit(beurt);
                WisselSpelerBeurt(simuleerSpel);


            }
            catch (LostCitiesException ex)
            {
                throw new SimulationFailException(((ISimulatieSpeler)HuidigeSpeler), ex.Message);
            }
            catch (Exception exc)
            {
                throw new SimulationFailException(((ISimulatieSpeler)HuidigeSpeler), exc.Message);
            }
        

        }

        private Beurt ConvertBeurt(ISpelBeurt spelBeurt, Speler speler)
        {
            LegActie legActie;
            PakActie pakActie;
            if(spelBeurt.LegBord==LegBord.AflegBord)
            {
                legActie = new LegKaartWegActie(spelBeurt.KaartIndexLegActie, speler);
            }
            else
            {
                legActie = new LegKaartAanActie(spelBeurt.KaartIndexLegActie, speler);
            }

            if(spelBeurt.PakStapel==PakStapel.TrekStapel)
            {
                pakActie = new PakKaartVanTrekStapelActie(speler);
            }
            else
            {
                pakActie = new PakKaartVanAflegStapelActie(speler, spelBeurt.PakKleurAflegStapel);
            }

            return new Beurt(legActie, pakActie);
        }


        private Stapel CreateStartStapel()
        {
            Stapel stapel = new Stapel();
            AddKleurreeks(stapel, Kleur.Blauw);
            AddKleurreeks(stapel, Kleur.Rood);
            AddKleurreeks(stapel, Kleur.Groen);
            AddKleurreeks(stapel, Kleur.Geel);
            AddKleurreeks(stapel, Kleur.Wit);
            return stapel;
        }

        private void AddKleurreeks(Stapel stapel, Kleur kleur)
        {
            for (int i = 2; i < 11; i++)
            {
                stapel.AddKaart(new ExpeditieKaart(kleur, i));
            }
            stapel.AddKaart(new WeddenschapsKaart(kleur));
            stapel.AddKaart(new WeddenschapsKaart(kleur));
            stapel.AddKaart(new WeddenschapsKaart(kleur));
        }


        private void DeelKaarten(Stapel[] stapels)
        {
            for (int i = 0; i < 8; i++)
            {
                stapels[0].AddKaart(trekStapel.PakBovensteKaart());
                stapels[1].AddKaart(trekStapel.PakBovensteKaart());
            }
        }

        private int GetTegenstanderIndex()
        {
            if (this.huidigeSpeler == 0)
                return 1;
            else
                return 0;
        }
    }
}
