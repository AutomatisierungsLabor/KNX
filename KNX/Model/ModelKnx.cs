using System.Collections.ObjectModel;

namespace KNX.Model;

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

public class ModelKnx
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public Einstellungen KnxEinstellungen { get; set; }
    private int _selectedIndex;
    private bool _enableBothButtons;
    private readonly StringBuilder _stringBuilderInfo;
    private readonly DateienUndOrdner _dateienUndOrdner;

    public ModelKnx()
    {
        _stringBuilderInfo = new StringBuilder();

        _dateienUndOrdner = new DateienUndOrdner();

        try
        {
            KnxEinstellungen = Newtonsoft.Json.JsonConvert.DeserializeObject<Einstellungen>(File.ReadAllText("Einstellungen.json"));
            KnxEinstellungen?.AlleKnxProjekte.Insert(0, new KnxProjekte("Bitte Projekt auswählen!"));

        }
        catch (Exception ex)
        {
            Log.Debug("Einstellungen.json konnten nicht gelesen werden: " + ex);
        }
    }
    private void OrdnerStrukturAnpassen()
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var progDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        var localAppData = new[] {
            //"C:\\Users\\kurt.linder\\AppData\\Local\\KNX\\ETS5",
            "KNX\\ETS5\\Cache",
            "KNX\\ETS5\\Log",
            "KNX\\ETS5\\My Dynamic Folders",
            "KNX\\ETS5\\My Products",
            "KNX\\ETS5\\ProjectTemplates",
            "KNX\\ETS5\\Usages",
            "KNX\\ETS5\\Workspaces"
        };

        var programmData = new[] {
            //"C:\\ProgramData\\KNX\\ETS5",
            "KNX\\ETS5\\Apps",
            "KNX\\ETS5\\AppUpdate",
            //"C:\\ProgramData\\KNX\\ETS5\\Installer", --> installer.guid
            "KNX\\ETS5\\LabelCreator",
            "KNX\\ETS5\\OnlineCatalog",
            "KNX\\ETS5\\ProductStore",
            "KNX\\ETS5\\ProjectStore",
            "KNX\\ETS5\\Updater"
        };

        _enableBothButtons = false;
        _stringBuilderInfo.Clear();

        foreach (var ordner in localAppData) _stringBuilderInfo.Append(DateienUndOrdner.OrdnerLoeschen(Path.Combine(appDataFolder, ordner)));
        foreach (var ordner in programmData) _stringBuilderInfo.Append(DateienUndOrdner.OrdnerLoeschen(Path.Combine(progDataFolder, ordner)));

        _stringBuilderInfo.Append("\n");

        _stringBuilderInfo.Append(_dateienUndOrdner.OrdnerKopieren(Path.Combine(KnxEinstellungen.AlleKnxProjekte[_selectedIndex].Quelle, "AppData"), appDataFolder));
        _stringBuilderInfo.Append(_dateienUndOrdner.OrdnerKopieren(Path.Combine(KnxEinstellungen.AlleKnxProjekte[_selectedIndex].Quelle, "ProgramData"), progDataFolder));

        try
        {
            var namePfadEts5 = Path.Combine("c", "Program Files (x86)", "ETS5", "ETS5.exe");
            Process.Start(namePfadEts5);
            _stringBuilderInfo.Append("\nETS5 starten");
        }
        catch (Exception ex)
        {
            Log.Debug("ETS5 konnte nicht gestartet werden!" + ex);
            _stringBuilderInfo.Append("\nETS5 konnte nicht gestartet werden!");
        } 
        _selectedIndex = 0;
    }
    internal bool BothButtonsEnabled() => _enableBothButtons;
    internal string GetTextBoxInfo() => _stringBuilderInfo != null ? _stringBuilderInfo.ToString() : "-";
    internal int GetSelectedIndex() => _selectedIndex;
    internal void SelectedIndexChanched(int selectedIndex)
    {
        _enableBothButtons = selectedIndex > 0;
        if (selectedIndex > 0) _stringBuilderInfo.Clear();

        _selectedIndex = selectedIndex;
        _stringBuilderInfo.Append(KnxEinstellungen.AlleKnxProjekte[selectedIndex].Kommentar);
    }
    internal void TasterStart() => System.Threading.Tasks.Task.Run(OrdnerStrukturAnpassen);
    internal void TasterStop()
    {
        try
        {
            foreach (var proc in Process.GetProcessesByName("ets5")) proc.Kill();
        }
        catch (Exception ex)
        {
            Log.Debug("ETS5 konnte nicht gestoppt werden: " + ex);
        }
        _selectedIndex = 0;
        _enableBothButtons = false;
    }
    public ObservableCollection<string> GetItems()
    {
        var items = new ObservableCollection<string>();
        foreach (var knxProjekte in KnxEinstellungen.AlleKnxProjekte)
        {
            items.Add(knxProjekte.Beschreibung);
        }

        return items;
    }
}