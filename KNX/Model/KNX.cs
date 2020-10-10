namespace KNX.Model
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows;

    public class Knx
    {
        public Einstellungen KnxEinstellungen { get; set; }
        private int _selectedIndex;
        private bool _enableBothButtons;
        private readonly StringBuilder _textBoxText;
        private readonly DateienUndOrdner _dDo;

        public Knx()
        {
            _textBoxText = new StringBuilder();
            _dDo = new DateienUndOrdner();
            KnxEinstellungen = Newtonsoft.Json.JsonConvert.DeserializeObject<Einstellungen>(File.ReadAllText("Einstellungen.json"));
            KnxEinstellungen.AlleKnxProjekte.Insert(0, new KnxProjekte("Bitte Projekt auswählen!"));
        }

        private void OrdnerStrukturAnpassen()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var progDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            var localAppData = new[] {
                    //"C:\\Users\\kurt.linder\\AppData\\Local\\KNX\\ETS5",
                    "KNX\\ETS5\\Cache",
                    "KNX\\ETS5\\Log",
                    "KNX\\ETS5\\My Dynamic Folders",
                    "KNX\\ETS5\\My Products",
                    "KNX\\ETS5\\ProjectTemplates",
                    "KNX\\ETS5\\Usages",
                    "KNX\\ETS5\\Workspaces"
                    };

            var programmData = new[] {
                    //"C:\\ProgramData\\KNX\\ETS5",
                    "KNX\\ETS5\\Apps",
                    "KNX\\ETS5\\AppUpdate",
                    //"C:\\ProgramData\\KNX\\ETS5\\Installer", --> installer.guid
                    "KNX\\ETS5\\LabelCreator",
                    "KNX\\ETS5\\OnlineCatalog",
                    "KNX\\ETS5\\ProductStore",
                    "KNX\\ETS5\\ProjectStore",
                    "KNX\\ETS5\\Updater"
                    };

            _enableBothButtons = false;
            _textBoxText.Clear();

            foreach (var ordner in localAppData) _textBoxText.Append(DateienUndOrdner.OrdnerLoeschen(appDataFolder + "\\" + ordner));
            foreach (var ordner in programmData) _textBoxText.Append(DateienUndOrdner.OrdnerLoeschen(progDataFolder + "\\" + ordner));

            _textBoxText.Append("\n");

            _textBoxText.Append(_dDo.OrdnerKopieren(KnxEinstellungen.AlleKnxProjekte[_selectedIndex].Quelle + "\\AppData", appDataFolder));
            _textBoxText.Append(_dDo.OrdnerKopieren(KnxEinstellungen.AlleKnxProjekte[_selectedIndex].Quelle + "\\ProgramData", progDataFolder));

            try
            {
                Process.Start("c:\\Program Files (x86)\\ETS5\\ETS5.exe");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 12 caught.");
            }

            _textBoxText.Append("\n");
            _textBoxText.Append("ETS5 starten");
            _selectedIndex = 0;
        }

        internal bool BothButtonsEnabled() { return _enableBothButtons; }
        internal object TextBoxText() { return _textBoxText; }
        internal int GetSelectedIndex() { return _selectedIndex; }

        internal void SelectedIndexChanched(object selectedIndex)
        {
            var i = (int)selectedIndex;
            _enableBothButtons = i > 0;
            _selectedIndex = i;
            _textBoxText.Clear();
            _textBoxText.Append(KnxEinstellungen.AlleKnxProjekte[i].Kommentar);
        }

        internal void TasterStart() { System.Threading.Tasks.Task.Run(OrdnerStrukturAnpassen); }
        internal void TasterStop()
        {
            try
            {
                foreach (var proc in Process.GetProcessesByName("ets5")) proc.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _selectedIndex = 0;
            _enableBothButtons = false;
        }
    }
}