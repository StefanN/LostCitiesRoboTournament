using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FunForce.LostCities.Tournament.Business;
using System.ComponentModel;
using System.Windows.Data;

namespace FunForce.LostCities.Presentation.TournamentClient
{
    public class WPFStand : ObservableCollection<WPFStandItem>
    {

        public WPFStand(IList<SimulatedPlayer> spelers)
        {
            Init(spelers);
        }


        public WPFStandItem FindStandItem(string teamNaam)
        {
            foreach (WPFStandItem wpfStandItem in this.Items)
            {
                if (wpfStandItem.TeamNaam == teamNaam)
                {
                    return wpfStandItem;
                }
            }
            return null;
        }

        public void Reset(IList<SimulatedPlayer> spelers)
        {
            this.Clear();
            Init(spelers);
        }

        private void Init(IList<SimulatedPlayer> spelers)
        {
            foreach (SimulatedPlayer speler in spelers)
            {
                WPFStandItem wpfStandItem = new WPFStandItem();
                wpfStandItem.TeamNaam = speler.Naam;
                wpfStandItem.AantalGespeeld = 0;
                wpfStandItem.Punten = 0;
                wpfStandItem.Voor = 0;
                wpfStandItem.Tegen = 0;
                this.Add(wpfStandItem);
            }
        }

        public void Update(WPFMatch wpfMatch)
        {
            WPFStandItem wpfStandItem = this.FindStandItem(wpfMatch.NaamTeam1);
            wpfStandItem.AantalGespeeld++;
            wpfStandItem.Punten += wpfMatch.PuntenTeam1;
            wpfStandItem.Voor += wpfMatch.TotaalScoreTeam1;
            wpfStandItem.Tegen += wpfMatch.TotaalScoreTeam2;

            wpfStandItem = this.FindStandItem(wpfMatch.NaamTeam2);
            wpfStandItem.AantalGespeeld++;
            wpfStandItem.Punten += wpfMatch.PuntenTeam2;
            wpfStandItem.Voor += wpfMatch.TotaalScoreTeam2;
            wpfStandItem.Tegen += wpfMatch.TotaalScoreTeam1;

            SortStand();

        }

        private void SortStand()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(this);
            view.SortDescriptions.Add(new SortDescription("Punten", ListSortDirection.Descending));
            view.SortDescriptions.Add(new SortDescription("AantalGespeeld", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Voor", ListSortDirection.Descending));
            view.SortDescriptions.Add(new SortDescription("Tegen", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("TeamNaam", ListSortDirection.Ascending));

        }
    }

}
