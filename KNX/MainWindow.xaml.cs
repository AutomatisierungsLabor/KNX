using KNX.ViewModel;
using System.Reflection;

namespace KNX;

// ReSharper disable once UnusedMember.Global
public partial class MainWindow
{
    private static readonly log4net.ILog s_log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? throw new InvalidOperationException());

    private CancellationTokenSource CancellationTokenSource { get; }

    public MainWindow()
    {
        s_log.Debug("Konstruktor - startet");

        CancellationTokenSource = new CancellationTokenSource();

        var modelKnx = new Model.ModelKnx();
        var vmKnx = new VmKnx(modelKnx, CancellationTokenSource);

        InitializeComponent();
        DataContext = vmKnx;
    }
}
