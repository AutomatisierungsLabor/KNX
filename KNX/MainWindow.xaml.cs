using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;


namespace KNX
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Tuple<int, string, string, string>> ListeRadioButtons = new List<Tuple<int, string, string, string>>();
        int ProjektNummer = 0;
        public MainWindow()
        {
            InitializeComponent();
            XML_EinstellungenLesen();
        }

        private void XML_EinstellungenLesen()
        {
            int LaufendeNummer = 0;
            string Ordner = "";
            string Beschriftung = "";
            string Beschreibung = "";

            if (!File.Exists("Einstellungen.xml"))
            {
                var result = System.Windows.MessageBox.Show("Die Datei Einstellungen.xml fehlt!");
                Environment.Exit(0);
            }

            XDocument doc = XDocument.Load("Einstellungen.xml");

            foreach (XElement el in doc.Root.Descendants())
            {
                switch (el.Name.LocalName)
                {
                    case "Beschriftung": Beschriftung = el.Value; break;
                    case "Ordner": Ordner = el.Value; break;
                    case "Beschreibung": Beschreibung = el.Value; break;
                    default:
                        break;
                }

                if (Beschriftung != "" && Ordner != "" && Beschreibung != "")
                {
                    // Kombination eintragen
                    ListeRadioButtons.Add(Tuple.Create(LaufendeNummer, Ordner, Beschriftung, Beschreibung));
                    LaufendeNummer++;
                    Ordner = "";
                    Beschriftung = "";
                    Beschreibung = "";
                }
            }

            foreach (Tuple<int, string, string, string> t in ListeRadioButtons)
            {

                RadioButton rdo = new RadioButton
                {
                    GroupName = "KNX Gruppe",
                    Name = "Button" + "_" + t.Item1.ToString(),
                    FontSize = 14,
                    Content = t.Item3,
                    VerticalAlignment = VerticalAlignment.Top
                };

                rdo.Checked += new RoutedEventHandler(KNX_RadioButton_Aktiviert);

                StackPanelKNX.Children.Add(rdo);
            }
        }

        public void KNX_RadioButton_Aktiviert(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string[] words = rb.Name.Split('_');

            ProjektNummer = Int32.Parse(words[1]);
            txt_Box.Text = ListeRadioButtons[ProjektNummer].Item4;

            btn_Start.IsEnabled = true;
            btn_Stop.IsEnabled = true;
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            OrdnerStrukturAnpassen();
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("ets5"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
