using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using FunForce.LostCities.Facade.Interface;
using FunForce.LostCities.Facade;
using System.Diagnostics;
using FunForce.LostCities.MyBot;
using System.Configuration;

namespace FunForce.LostCities.Presentation.Client
{

    /// <summary>
    /// Bord van het spel
    /// </summary>
    public partial class BordScherm : Window
    {

       // private const string _botsFolder = ConfigurationSettings.AppSettings["BotsPath"];
        private IList<Type> _types;
        ISpelerSessie _sessie1 = null;
        ISpelerSessie _sessie2 = null;
        BordViewModel _bord;

        /// <summary>
        /// Constructor
        /// </summary>
        public BordScherm()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(BordScherm_Loaded);
        }


        private void BordScherm_Loaded(object sender, RoutedEventArgs e)
        {
            
            FillTegenstanderComboBox();

            this.SimuleerBeurtButton.IsEnabled = false;
            this.SimuleerBeurtButton.Content = "Simuleer beurt";

            _bord = new BordViewModel();
            this.DataContext = _bord;


        }

        private void SetSimuleerButtonTekst()
        {
            string naam;
            if (_sessie1.IsActievepeler())
            {
                naam = _sessie1.Naam;
            }
            else
            {
                naam = _sessie2.Naam;
            }

            this.SimuleerBeurtButton.Content = "Simuleer beurt van " + naam;
        }

        private void FillTegenstanderComboBox()
        {
            try
            {
                string _appPathName = Process.GetCurrentProcess().MainModule.FileName;

                DirectoryInfo basePath = new DirectoryInfo(ConfigurationSettings.AppSettings["BotsPath"]);
                FileInfo[] files = basePath.GetFiles("*.dll");
                string[] assemblyPaths = new string[files.Length];
                int index = 0;
                foreach (FileInfo file in files)
                {
                    assemblyPaths[index] = file.FullName;
                    TegenstanderComboBox.Items.Add(file.Name);
                    index++;
                }

                _types = TypeLoader.LoadTypes(assemblyPaths, typeof(ISimulatieSpeler));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fout tijdens het vullen van de tegenstander combobox", ex);

            }

        }


        private void CreateSessies()
        {
            
            BusinessFacade facade = BusinessFacade.GetInstance();

            //gekozen bot
            ISimulatieSpeler strategy1 = (ISimulatieSpeler)System.Activator.CreateInstance(_types[TegenstanderComboBox.SelectedIndex]);
            _sessie1 = facade.CreateSessie(strategy1.Naam, strategy1);
            //te ontwikkelen my bot
            ISimulatieSpeler strategy2 = new LostCitiesBot();
            _sessie2 = facade.CreateSessie(strategy2.Naam, strategy2);
        }

        private bool IsEindeRonde()
        {
            return _bord.TrekStapel.AantalKaarten == 0;
        }

        private void SimuleerBeurt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessFacade facade = BusinessFacade.GetInstance();
                facade.SimuleerBeurt();
                _bord.VerwerkBeurt(_sessie1, _sessie2);

                SetSimuleerButtonTekst();

                if (IsEindeRonde())
                {
                    SetEindeRonde();
                }
               
            }
            catch (Exception ex)
            {
                _bord.Speler1.ClearBeurt();
                _bord.Speler2.ClearBeurt();

                if (_sessie1.IsActievepeler())
                {
                    _bord.Speler1.Beurt.Melding = ex.Message;
                }
                else
                {
                    _bord.Speler2.Beurt.Melding = ex.Message;
                }
                
                
               // MessageBox.Show(ex.ToString());
            }

        }

        private void SetEindeRonde()
        {
            SimuleerBeurtButton.IsEnabled = false;

            if (_bord.Speler1.Score > _bord.Speler2.Score)
            {
                _bord.Speler1.Beurt.Melding = "Gewonnen";
                _bord.Speler2.Beurt.Melding = "Verloren";
            }
            else if (_bord.Speler1.Score < _bord.Speler2.Score)
            {
                _bord.Speler1.Beurt.Melding = "Verloren";
                _bord.Speler2.Beurt.Melding = "Gewonnen";
            }
            else
            {
                _bord.Speler1.Beurt.Melding = "Gelijk";
                _bord.Speler2.Beurt.Melding = "Gelijk";
            }
        }

        private void StartSpel_Click(object sender, RoutedEventArgs e)
        {
            if (TegenstanderComboBox.SelectedIndex > -1)
            {

                BusinessFacade.Reset();

                //InitialiseerBots();
                CreateSessies();

                BusinessFacade facade = BusinessFacade.GetInstance();

                facade.CreateSpel();

                _bord = new BordViewModel(_sessie1, _sessie2);
                this.DataContext = _bord;

                SetSimuleerButtonTekst();

                SimuleerBeurtButton.IsEnabled = true;
            }
        }

  



     }
}

