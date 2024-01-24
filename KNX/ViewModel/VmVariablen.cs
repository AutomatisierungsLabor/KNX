using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace KNX.ViewModel;


public partial class VmKnx
{
    [ObservableProperty] private bool _boolEnableStartButton;

    [ObservableProperty] private string? _textBoxInfo;

    [ObservableProperty] private ObservableCollection<string>? _comboBoxItems;


    private int _selectedIndex;
    public int SelectorIndex
    {
        get => _selectedIndex;
        set
        {
            _selectedIndex=value;
            _modelKnx.SelectedIndexChanched(_selectedIndex);
            OnPropertyChanged();
        }
    }
}
