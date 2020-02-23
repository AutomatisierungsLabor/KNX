using System;
using System.IO;

namespace KNX.Model
{
    public class DateiSystem
    {
        private void OrdnerLoeschen(string Ordner)
        {
            try
            {
               /*
                System.IO.Directory.Delete(Ordner, true);
                this.Dispatcher.Invoke(() =>
                {
                    txt_Box.AppendText(Ordner + " gelöscht\n");
                });
                */
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
                /*
                this.Dispatcher.Invoke(() =>
                {
                    txt_Box.AppendText(ZielOrdner + " kopiert\n");
                });
                */
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