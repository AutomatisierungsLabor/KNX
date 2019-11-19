using System;

namespace KNX
{
    public partial class MainWindow
    {
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

            this.Dispatcher.Invoke(() =>
            {
                btn_Start.IsEnabled = false;
                txt_Box.Text = "";
            });

            foreach (string Ordner in LocalAppData) OrdnerLoeschen(AppDataFolder + "\\" + Ordner);
            foreach (string Ordner in ProgrammData) OrdnerLoeschen(ProgDataFolder + "\\" + Ordner);

            this.Dispatcher.Invoke(() =>
            {
                txt_Box.AppendText("\n");
            });


            OrdnerKopieren(ListeRadioButtons[ProjektNummer].Item2 + "\\AppData", AppDataFolder);
            OrdnerKopieren(ListeRadioButtons[ProjektNummer].Item2 + "\\ProgramData", ProgDataFolder);

            try
            {
                System.Diagnostics.Process.Start("c:\\Program Files (x86)\\ETS5\\ETS5.exe");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 12 caught.");
            }

            this.Dispatcher.Invoke(() =>
            {
                txt_Box.AppendText("\n");
                txt_Box.AppendText("ETS5 starten");
                btn_Start.IsEnabled = true;
            });
        }

    }
}