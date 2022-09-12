using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using KNX.Model;

namespace KNX.ViewModel;

public partial class VmKnx : ObservableObject
{
    private readonly ModelKnx _modelKnx;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public VmKnx(ModelKnx modelKnx, CancellationTokenSource cancellationTokenSource)
    {
        _modelKnx = modelKnx;
        _cancellationTokenSource=cancellationTokenSource;

        TextBoxInfo = "new ()";

        BoolEnableStartButton = false;

        System.Threading.Tasks.Task.Run(KnxTask);
    }
    private void KnxTask()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            Thread.Sleep(100);

            if (TextBoxInfo == null || _modelKnx == null) continue;

            TextBoxInfo = _modelKnx.GetTextBoxInfo();
            SelectorIndex = _modelKnx.GetSelectedIndex();
            BoolEnableStartButton = _modelKnx.BothButtonsEnabled();
            ComboBoxItems = _modelKnx.GetItems();
        }
    }
}