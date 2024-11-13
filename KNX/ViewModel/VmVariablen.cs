using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace KNX.ViewModel;

public partial class VmKnx
{
    [ObservableProperty] private bool _boolEnableStartButton;
    [ObservableProperty] private string? _textBoxInfo;
    [ObservableProperty] private Brush? _brushStopButtonColor;
    [ObservableProperty] private Brush? _brushStartButtonColor;
    [ObservableProperty] private ObservableCollection<string>? _comboBoxItems;

    private int _selectedIndex;

    public int SelectorIndex
    {
        get => _selectedIndex;
        set
        {
            _selectedIndex = value;
            _modelKnx.SelectedIndexChanged(_selectedIndex);
            OnPropertyChanged();
        }
    }
}
