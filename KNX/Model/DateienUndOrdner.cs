using System.Reflection;

namespace KNX.Model;

public static class DateienUndOrdner
{
    private static readonly log4net.ILog s_log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? throw new InvalidOperationException());

    public static string OrdnerLoeschen(string ordner)
    {
        if (!Directory.Exists(ordner))
        {
            s_log.Debug("Ordner ist nicht vorhanden: " + ordner);
            return $"\n{ordner} fehlt!";
        }

        try
        {
            Directory.Delete(ordner, true);
            return ordner + " gelöscht\n";
        }
        catch (Exception ex)
        {
            s_log.Debug("Ordner konnte nicht gelöscht werden: " + ex);
        }
        return $"\nKann {ordner} nicht löschen!";
    }
    public static string OrdnerKopieren(string quellOrdner, string zielOrdner)
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
            s_log.Debug("Ordner konnte nicht kopiert werden: " + ex);
        }
        return $"\nKann {quellOrdner} -> {zielOrdner} nicht kopieren!";
    }
    private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        _ = Directory.CreateDirectory(target.FullName);

        foreach (var fi in source.GetFiles())
        {
            Console.WriteLine($@"Copying {target.FullName}\{fi.Name}");
            _ = fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        foreach (var diSourceSubDir in source.GetDirectories())
        {
            var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }
}
