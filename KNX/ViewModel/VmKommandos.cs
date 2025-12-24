using CommunityToolkit.Mvvm.Input;

namespace KNX.ViewModel;

public partial class ViewModel
{
    [RelayCommand]
    private void ButtonStart() => _model.TasterStart();

    [RelayCommand]
    private void ButtonStop() => _model.TasterStop();
}
