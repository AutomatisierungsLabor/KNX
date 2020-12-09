using System.Collections.ObjectModel;

namespace KNX.Model
{
    public class Einstellungen
    {
        public ObservableCollection<KnxProjekte> AlleKnxProjekte { get; set; } = new ObservableCollection<KnxProjekte>();
    }

    public class KnxProjekte
    {
        public KnxProjekte(string beschreibung)
        {
            Beschreibung = beschreibung;
            Kommentar = "";
            Quelle = "";
        }

        public string Beschreibung { get; set; }
        public string Kommentar { get; set; }
        public string Quelle { get; set; }
        public override string ToString() => Beschreibung;
    }
}
