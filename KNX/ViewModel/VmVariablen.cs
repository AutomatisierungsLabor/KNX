using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace KNX.ViewModel;

public partial class ViewModel
{
    [ObservableProperty] private bool _boolEnableStartButton;
    [ObservableProperty] private string? _textBoxInfo;
    [ObservableProperty] private Brush? _brushStopButtonColor;
    [ObservableProperty] private Brush? _brushStartButtonColor;
    [ObservableProperty] private ObservableCollection<string>? _comboBoxItems;

    public int SelectorIndex
    {
        get;
        set
        {
            field = value;
            _model.SelectedIndexChanged(field);
            OnPropertyChanged();
        }
    }
}
