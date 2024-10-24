using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace KNX.Model;

public class ModelKnx
{
    private static readonly log4net.ILog s_log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? throw new InvalidOperationException());

    private Einstellungen? KnxEinstellungen { get; }
    private int _selectedIndex;
    private bool _enableBothButtons;
    private readonly StringBuilder _stringBuilderInfo;

    public ModelKnx()
    {
        _stringBuilderInfo = new StringBuilder();

        try
        {
            KnxEinstellungen = Newtonsoft.Json.JsonConvert.DeserializeObject<Einstellungen>(File.ReadAllText("Einstellungen.json"));
            if (KnxEinstellungen == null) { throw new Exception("Einstellungen.json leer!"); }
            KnxEinstellungen?.AlleKnxProjekte.Insert(0, new KnxProjekte("Bitte Projekt auswählen!"));
        }
        catch (Exception ex)
        {
            s_log.Debug("Einstellungen.json konnten nicht gelesen werden: " + ex);
        }
    }
    private void OrdnerStrukturAnpassen()
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var progDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        var localAppData = new[] {
            //"C:\\Users\\kurt.linder\\AppData\\Local\\KNX\\ETS5",
            @"KNX\ETS5\Cache",
            @"KNX\ETS5\Log",
            @"KNX\ETS5\My Dynamic Folders",
            @"KNX\ETS5\My Products",
            @"KNX\ETS5\ProjectTemplates",
            @"KNX\ETS5\Usages",
            @"KNX\ETS5\Workspaces"
        };

        var programmData = new[] {
            //"C:\\ProgramData\\KNX\\ETS5",
            @"KNX\ETS5\Apps",
            @"KNX\ETS5\AppUpdate",
            //"C:\\ProgramData\\KNX\\ETS5\\Installer", --> installer.guid
            @"KNX\ETS5\LabelCreator",
            @"KNX\ETS5\OnlineCatalog",
            @"KNX\ETS5\ProductStore",
            @"KNX\ETS5\ProjectStore",
            @"KNX\ETS5\Updater"
        };

        _enableBothButtons = false;
        _ = _stringBuilderInfo.Clear();

        foreach (var ordner in localAppData) { _ = _stringBuilderInfo.Append(DateienUndOrdner.OrdnerLoeschen(Path.Combine(appDataFolder, ordner))); }
        foreach (var ordner in programmData) { _ = _stringBuilderInfo.Append(DateienUndOrdner.OrdnerLoeschen(Path.Combine(progDataFolder, ordner))); }

        _ = _stringBuilderInfo.Append('\n');
        _ = _stringBuilderInfo.Append(DateienUndOrdner.OrdnerKopieren(Path.Combine(KnxEinstellungen!.AlleKnxProjekte[_selectedIndex].Quelle, "AppData"), appDataFolder));
        _ = _stringBuilderInfo.Append(DateienUndOrdner.OrdnerKopieren(Path.Combine(KnxEinstellungen.AlleKnxProjekte[_selectedIndex].Quelle, "ProgramData"), progDataFolder));

        try
        {
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86);
            var namePfadEts5 = Path.Combine(programFiles, "ETS5", "ETS5.exe");
            _ = Process.Start(namePfadEts5);
            _ = _stringBuilderInfo.Append("\nETS5 starten");
        }
        catch (Exception ex)
        {
            s_log.Debug("ETS5 konnte nicht gestartet werden!" + ex);
            _ = _stringBuilderInfo.Append("\nETS5 konnte nicht gestartet werden!");
        }
        _selectedIndex = 0;
    }
    internal bool BothButtonsEnabled() => _enableBothButtons;
    internal string GetTextBoxInfo() => _stringBuilderInfo.ToString();
    internal int GetSelectedIndex() => _selectedIndex;
    internal void SelectedIndexChanched(int selectedIndex)
    {
        _enableBothButtons = selectedIndex > 0;
        if (selectedIndex > 0) { _ = _stringBuilderInfo.Clear(); }

        _selectedIndex = selectedIndex;
        _ = _stringBuilderInfo.Append(KnxEinstellungen?.AlleKnxProjekte[selectedIndex].Kommentar);
    }
    internal void TasterStart() => Task.Run(OrdnerStrukturAnpassen);
    internal void TasterStop()
    {
        
        try
        {
            foreach (var proc in Process.GetProcessesByName("ets5")) { proc.Kill(); }
        }
        catch (Exception ex)
        {
            s_log.Debug("ETS5 konnte nicht gestoppt werden: " + ex);
        }
        _selectedIndex = 0;
        _enableBothButtons = false;
    }
    public ObservableCollection<string> GetItems()
    {
        var items = new ObservableCollection<string>();
        if (KnxEinstellungen?.AlleKnxProjekte == null) { return items; }
        foreach (var knxProjekte in KnxEinstellungen.AlleKnxProjekte) { items.Add(knxProjekte.Beschreibung); }
        return items;
    }
}
