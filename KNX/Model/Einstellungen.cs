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
    public string Kommentar { get; set; } = "";
    public string Quelle { get; set; } = "";
    public override string ToString() => Beschreibung;
}
