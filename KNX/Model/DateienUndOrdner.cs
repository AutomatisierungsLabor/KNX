using Microsoft.Extensions.Logging;

namespace KNX.Model;

public partial class DateienUndOrdner(ILogger<DateienUndOrdner> logger)
{
    public string OrdnerLoeschen(string ordner)
    {
        if (!Directory.Exists(ordner))
        {
            LogWarningOrdnerFehlt(ordner);
            return $"\n{ordner} fehlt!";
        }

        try
        {
            Directory.Delete(ordner, true);
            return ordner + " gelöscht\n";
        }
        catch (Exception e)
        {
            LogWarningException(e);
        }
        return $"\nKann {ordner} nicht löschen!";
    }
    public string OrdnerKopieren(string quellOrdner, string zielOrdner)
    {
        if (string.IsNullOrEmpty(quellOrdner))
        {
            logger.LogDebug("Quellordner ist leer!");
            return string.Empty;
        }

        if (string.IsNullOrEmpty(zielOrdner))
        {
            logger.LogDebug("Zielordner ist leer!");
            return string.Empty;
        }

        if (!Directory.Exists(quellOrdner))
        {
            LogWarningQuellordnerFehlt(quellOrdner);
            return string.Empty;
        }

        if (!Directory.Exists(zielOrdner))
        {
            LogWarningZielordnerFehlt(zielOrdner);
            return string.Empty;
        }

        try
        {
            var diSource = new DirectoryInfo(quellOrdner);
            var diTarget = new DirectoryInfo(zielOrdner);

            CopyAll(diSource, diTarget);
            return $"\n{zielOrdner} kopiert";
        }
        catch (Exception e)
        {
            LogWarningException(e);
        }
        return $"\nKann {quellOrdner} -> {zielOrdner} nicht kopieren!";
    }
    private void CopyAll(DirectoryInfo source, DirectoryInfo target)
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


    [LoggerMessage(LogLevel.Warning, message: "Exception: {e}")]
    public partial void LogWarningException(Exception e);

    [LoggerMessage(LogLevel.Warning, message: "Ordner fehlt: {ordner}")]
    private partial void LogWarningOrdnerFehlt(string ordner);

    [LoggerMessage(LogLevel.Warning, message: "Quellordner fehlt: {ordner}")]
    private partial void LogWarningQuellordnerFehlt(string ordner);

    [LoggerMessage(LogLevel.Warning, message: "Zielordner fehlt: {ordner}")]
    private partial void LogWarningZielordnerFehlt(string ordner);

}
