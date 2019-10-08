using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KNX
{
    public partial class MainWindow
    {
        public void OrdnerStrukturAnpassen()
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

            if (rb_Alle_Projekte_Loeschen.IsChecked == true)
            {
                // ein Leeres Projekt hineinkopieren
                OrdnerKopieren("s:\\Linder Kurt\\KNX\\LeeresProjekt\\AppData", AppDataFolder);
                OrdnerKopieren("s:\\Linder Kurt\\KNX\\LeeresProjekt\\ProgramData", ProgDataFolder);
            }

            if (rb_MDT_verwenden.IsChecked == true)
            {
                // ein Leeres Projekt hineinkopieren
                OrdnerKopieren("s:\\Linder Kurt\\KNX\\MDT\\AppData", AppDataFolder);
                OrdnerKopieren("s:\\Linder Kurt\\KNX\\MDT\\ProgramData", ProgDataFolder);
            }
        }

      
        public void OrdnerLoeschen(string Ordner)
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

        public void OrdnerKopieren(string QuellOrdner, string ZielOrdner)
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

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine($@"Copying {target.FullName}\{fi.Name}");
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }


    }
}