namespace KNX.Model;

using System;
using System.IO;
public class DateienUndOrdner
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public static string OrdnerLoeschen(string ordner)
    {
        try
        {
            Directory.Delete(ordner, true);
            return ordner + " gelöscht\n";
        }
        catch (Exception ex)
        {
            Log.Debug("Ordner konnte nicht gelöscht werden: " + ex);
        }
        return $"\nKann {ordner} nicht löschen!";
    }
    public string OrdnerKopieren(string quellOrdner, string zielOrdner)
    {
        try
        {
            var diSource = new DirectoryInfo(quellOrdner);
            var diTarget = new DirectoryInfo(zielOrdner);

            CopyAll(diSource, diTarget);
            return $"\n{zielOrdner} kopiert";
        }
        catch (Exception ex)
        {
            Log.Debug("Ordner konnte nicht kopiert werden: " + ex);
        }
        return $"\nKann {quellOrdner} -> {zielOrdner} nicht kopieren!";
    }
    public void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);

        foreach (var fi in source.GetFiles())
        {
            Console.WriteLine($@"Copying {target.FullName}\{fi.Name}");
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        foreach (var diSourceSubDir in source.GetDirectories())
        {
            var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }
}