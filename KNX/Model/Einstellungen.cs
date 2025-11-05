using System.Collections.ObjectModel;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace KNX.Model;

public class Einstellungen
{
    public ObservableCollection<KnxProjekte> AlleKnxProjekte { get; set; } = [];
}
public class KnxProjekte(string beschreibung)
{
    public string Beschreibung { get; set; } = beschreibung;
    public string? Kommentar { get; set; } = string.Empty;
    public string? Quelle { get; set; } = String.Empty;
}
