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
        private int selectedIndex;
        private bool enableBothButtons;
        private readonly StringBuilder textBoxText;
        private readonly DateienUndOrdner dDO;

        public Knx()
        {
            textBoxText = new StringBuilder();
            dDO = new DateienUndOrdner();
            KnxEinstellungen = Newtonsoft.Json.JsonConvert.DeserializeObject<Einstellungen>(File.ReadAllText("Einstellungen.json"));
            KnxEinstellungen.AlleKnxProjekte.Insert(0, new KnxProjekte("Bitte Projekt auswählen!"));
        }

        private void OrdnerStrukturAnpassen()
        {
            string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string ProgDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            string[] LocalAppData = new string[] {
                    //"C:\\Users\\kurt.linder\\AppData\\Local\\KNX\\ETS5",
                    "KNX\\ETS5\\Cache",
                    "KNX\\ETS5\\Log",
                    "KNX\\ETS5\\My Dynamic Folders",
                    "KNX\\ETS5\\My Products",
                    "KNX\\ETS5\\ProjectTemplates",
                    "KNX\\ETS5\\Usages",
                    "KNX\\ETS5\\Workspaces"
                    };

            string[] ProgrammData = new string[] {
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

            enableBothButtons = false;
            textBoxText.Clear();

            foreach (string Ordner in LocalAppData) textBoxText.Append(DateienUndOrdner.OrdnerLoeschen(AppDataFolder + "\\" + Ordner));
            foreach (string Ordner in ProgrammData) textBoxText.Append(DateienUndOrdner.OrdnerLoeschen(ProgDataFolder + "\\" + Ordner));

            textBoxText.Append("\n");

            textBoxText.Append(dDO.OrdnerKopieren(KnxEinstellungen.AlleKnxProjekte[selectedIndex].Quelle + "\\AppData", AppDataFolder));
            textBoxText.Append(dDO.OrdnerKopieren(KnxEinstellungen.AlleKnxProjekte[selectedIndex].Quelle + "\\ProgramData", ProgDataFolder));

            try
            {
                System.Diagnostics.Process.Start("c:\\Program Files (x86)\\ETS5\\ETS5.exe");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 12 caught.");
            }

            textBoxText.Append("\n");
            textBoxText.Append("ETS5 starten");
            selectedIndex = 0;
        }

        internal bool BothButtonsEnabled() { return enableBothButtons; }
        internal object TextBoxText() { return textBoxText; }
        internal int GetSelectedIndex() { return selectedIndex; }

        internal void SelectedIndexChanched(object selectedIndex)
        {
            int i = (int)selectedIndex;
            if (i > 0) enableBothButtons = true; else enableBothButtons = false;
            this.selectedIndex = i;
            textBoxText.Clear();
            textBoxText.Append(KnxEinstellungen.AlleKnxProjekte[i].Kommentar);
        }

        internal void TasterStart() { System.Threading.Tasks.Task.Run(() => OrdnerStrukturAnpassen()); }
        internal void TasterStop()
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("ets5")) proc.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            selectedIndex = 0;
            enableBothButtons = false;
        }
    }
}