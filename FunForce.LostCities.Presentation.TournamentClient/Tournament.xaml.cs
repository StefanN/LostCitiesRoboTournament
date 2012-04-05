using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using FunForce.LostCities.Tournament.Business;
using FunForce.LostCities.Facade.Interface;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using System.Configuration;



namespace FunForce.LostCities.Presentation.TournamentClient
{
    /// <summary>
    /// Interaction logic for Tournament.xaml
    /// </summary>

    public partial class Tournament : System.Windows.Window
    {





        private string BOTS_FOLDER = string.Empty;
        private const int AANTAL_RESULTATEN = 10;
        private const int WEDSTRIJD_PAUZE = 200;
        //private const int RESULTAAT_PAUZE = 100;
        private const int RESULTAAT_PAUZE = 10;

        private IList<Match> _matches;
        private IList<SimulatedPlayer> _spelers;
        private IList<Type> _types;

        private WPFMatches _wpfMatches;


        private WPFStand _wpfStand;
        private bool _stopSimulatie = true;
        //Current number to check 
        private int _wedstrijdNummer = 0;
        private int _resultaatNummer = 0;
        private bool _newMatch = true;
        private double _progressBarFactor;
        private Slider _slider;

        public delegate void VolgendeMatchSimulatieDelegate();


        public Tournament()
        {
            InitializeComponent();

            DirectoryInfo basePath = new DirectoryInfo(ConfigurationSettings.AppSettings["BotsPath"]);
            BOTS_FOLDER = basePath.FullName;

            this.Loaded += new RoutedEventHandler(Tournament_Loaded);

        }

        void Tournament_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiseerBots();
            SetupSpelers();
            SetupMatches();

            _wpfMatches = new WPFMatches();
            _wpfStand = new WPFStand(_spelers);
            _slider = new Slider();
            _slider.Value = RESULTAAT_PAUZE;
            //ResultaatSlider.DataContext = _slider;
            StandListBox.DataContext = _wpfStand;
            this.DataContext = _wpfMatches;
        }

 

        private void SetupMatches()
        {
            //setup matches
            IList<Match> matches = MatchMaker.SetupMatches(_spelers);
            _matches = new List<Match>();
            
            Random rand = new Random();

            while (matches.Count > 0)
            {
                int i = rand.Next(matches.Count - 1);
                _matches.Add(matches[i]);
                matches.RemoveAt(i);
            }

            _progressBarFactor = ResultaatProgressBar.Maximum / (_matches.Count * AANTAL_RESULTATEN);
        }

        private void SetupSpelers()
        {
            //setup spelers
            _spelers = MatchMaker.SetupPlayers(_types);
        }

        private void InitialiseerBots()
        {
            DirectoryInfo basePath = new DirectoryInfo(BOTS_FOLDER);
            FileInfo[] files = basePath.GetFiles("*.dll");
            string[] assemblyPaths = new string[files.Length];
            int index = 0;
            foreach (FileInfo file in files)
            {
                assemblyPaths[index] = file.FullName;
                index++;
            }

            _types = TypeLoader.LoadTypes(assemblyPaths, typeof(ISimulatieSpeler));
        }

        public void StartOfStop(object sender, EventArgs e)
        {
            if (!_stopSimulatie)
            {
                _stopSimulatie = true;
                buttonStartStop.Content = "Wieter";
            }
            else
            {
                _stopSimulatie = false;
                buttonStartStop.Content = "Pauze";
                buttonStartStop.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new VolgendeMatchSimulatieDelegate(this.SimulateMatches));
            }

        }

        private void SimulateMatches()
        {
            if (_newMatch)
            {
                Thread.Sleep(WEDSTRIJD_PAUZE);

                if (_wedstrijdNummer == 0)
                {
                    ResultaatProgressBar.Value = 0;
                    _wpfMatches.Clear();
                    _wpfStand.Reset(_spelers);
               }

                Match match = _matches[_wedstrijdNummer];
                match.Simulate(AANTAL_RESULTATEN);

                WPFMatch wpfMatch = new WPFMatch(match);
                _wpfMatches.Insert(0, wpfMatch);

                _newMatch = false;

                buttonStartStop.Dispatcher.BeginInvoke(
                    System.Windows.Threading.DispatcherPriority.SystemIdle,
                    new VolgendeMatchSimulatieDelegate(this.SimulateMatches));
            }
            else
            {
                WPFMatch wpfMatch = _wpfMatches[0];
                wpfMatch.Update(_resultaatNummer);
 
                ResultaatProgressBar.Value += _progressBarFactor;

                Thread.Sleep(_slider.Value);

                if (_resultaatNummer == (AANTAL_RESULTATEN - 1))
                {
                    if (_matches.Count == _wedstrijdNummer + 1)
                    {
                        _stopSimulatie = true;
                        buttonStartStop.Content = "Start";
                        _wedstrijdNummer = 0;

                    }
                    else
                    {
                        _wedstrijdNummer++;
                    }
                    _resultaatNummer = -1;
                    _newMatch = true;
                    wpfMatch.Gespeeld = true;

                    _wpfStand.Update(wpfMatch);
                }

                _resultaatNummer++;
                if (!_stopSimulatie)
                {
                    buttonStartStop.Dispatcher.BeginInvoke(
                        System.Windows.Threading.DispatcherPriority.SystemIdle,
                        new VolgendeMatchSimulatieDelegate(this.SimulateMatches));
                }
            }

        }

    }
}