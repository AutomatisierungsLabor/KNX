using CommunityToolkit.Mvvm.ComponentModel;
using KNX.Model;
using System.Diagnostics;
using System.Windows.Media;

namespace KNX.ViewModel;

public partial class VmKnx : ObservableObject
{
    private readonly ModelKnx _modelKnx;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public VmKnx(ModelKnx modelKnx, CancellationTokenSource cancellationTokenSource)
    {
        _modelKnx = modelKnx;
        _cancellationTokenSource = cancellationTokenSource;

        ComboBoxItems = _modelKnx.GetItems();
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

            if (TextBoxInfo == null || _modelKnx == null) { continue; }

            var etsAktiv = Process.GetProcesses().Any(process => process.ProcessName.Contains("ETS"));

            BrushStartButtonColor = etsAktiv ? Brushes.LightGray : Brushes.LawnGreen;
            BrushStopButtonColor = etsAktiv ? Brushes.Red : Brushes.LightGray;

            TextBoxInfo = _modelKnx.GetTextBoxInfo();
            SelectorIndex = _modelKnx.GetSelectedIndex();
            BoolEnableStartButton = _modelKnx.BothButtonsEnabled();
            ComboBoxItems = _modelKnx.GetItems();
        }
    }
}
