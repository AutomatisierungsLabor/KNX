namespace KNX.Model
{
    using System;
    using System.IO;
    public class DateienUndOrdner
    {
        public static string OrdnerLoeschen(string ordner)
        {
            try
            {
                Directory.Delete(ordner, true);
                return ordner + " gelöscht\n";
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 1 caught.");
            }
            return "uups\n";
        }

        public string OrdnerKopieren(string quellOrdner, string zielOrdner)
        {
            try
            {
                DirectoryInfo diSource = new DirectoryInfo(quellOrdner);
                DirectoryInfo diTarget = new DirectoryInfo(zielOrdner);

                CopyAll(diSource, diTarget);
                return zielOrdner + " kopiert\n";
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 2 caught.");
            }
            return "uups\n";
        }

        public void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine($@"Copying {target.FullName}\{fi.Name}");
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}