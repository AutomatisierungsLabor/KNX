namespace KNX.Model
{
    using System;
    using System.IO;
    public class DateienUndOrdner
    {
        public  DateienUndOrdner()
        {
            //
        }
        public static string OrdnerLoeschen(string Ordner)
        {
            try
            {
                System.IO.Directory.Delete(Ordner, true);
                return (Ordner + " gelöscht\n");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 1 caught.");
            }
            return "uups\n";
        }

        public string OrdnerKopieren(string QuellOrdner, string ZielOrdner)
        {
            try
            {
                DirectoryInfo diSource = new DirectoryInfo(QuellOrdner);
                DirectoryInfo diTarget = new DirectoryInfo(ZielOrdner);

                CopyAll(diSource, diTarget);
                return (ZielOrdner + " kopiert\n");
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