using CommunityToolkit.Mvvm.Input;

namespace KNX.ViewModel;

public partial class VmKnx
{
    [RelayCommand]
    private void ButtonStart() => _modelKnx.TasterStart();

    [RelayCommand]
    private void ButtonStop() => _modelKnx.TasterStop();
}