using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;

namespace KNX.ViewModel;

public partial class ViewModel : ObservableObject
{
    private readonly Model.Model _model;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public ViewModel(Model.Model model, CancellationTokenSource cancellationTokenSource)
    {
        _model = model;
        _cancellationTokenSource = cancellationTokenSource;

        ComboBoxItems = _model.GetItems();
        TextBoxInfo = "new ()";
        BoolEnableStartButton = false;
        BrushStopButtonColor = Brushes.Beige;
        BrushStartButtonColor = Brushes.Beige;

        _ = Task.Run(KnxTask);
    }
    private void KnxTask()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            Thread.Sleep(100);

            if (TextBoxInfo == null || _model == null) { continue; }

            var etsAktiv = Process.GetProcesses().Any(process => process.ProcessName.Contains("ETS"));

            BrushStartButtonColor = etsAktiv ? Brushes.LightGray : Brushes.LawnGreen;
            BrushStopButtonColor = etsAktiv ? Brushes.Red : Brushes.LightGray;

            TextBoxInfo = _model.GetTextBoxInfo();
            SelectorIndex = _model.GetSelectedIndex();
            BoolEnableStartButton = _model.BothButtonsEnabled();
            ComboBoxItems = _model.GetItems();
        }
    }
}
