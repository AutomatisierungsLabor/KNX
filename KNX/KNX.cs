using System;
using System.IO;

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

            txt_Box.Text = "";

            foreach (string Ordner in LocalAppData)
            {
                OrdnerLoeschen(AppDataFolder + "\\" + Ordner);
            }

            foreach (string Ordner in ProgrammData)
            {
                OrdnerLoeschen(ProgDataFolder + "\\" + Ordner);
            }

            txt_Box.AppendText("\n");

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
        }

        private void OrdnerLoeschen(string Ordner)
        {
            try
            {
                System.IO.Directory.Delete(Ordner, true);
                txt_Box.AppendText(Ordner + " gelöscht\n");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 1 caught.");
            }
        }

        private void OrdnerKopieren(string QuellOrdner, string ZielOrdner)
        {
            try
            {
                DirectoryInfo diSource = new DirectoryInfo(QuellOrdner);
                DirectoryInfo diTarget = new DirectoryInfo(ZielOrdner);

                CopyAll(diSource, diTarget);
                txt_Box.AppendText(ZielOrdner + " kopiert\n");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 2 caught.");
            }
        }

        public void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine($@"Copying {target.FullName}\{fi.Name}");
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

    }
}