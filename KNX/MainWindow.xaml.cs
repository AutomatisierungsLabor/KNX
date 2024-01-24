using KNX.ViewModel;

namespace KNX;

// ReSharper disable once UnusedMember.Global
public partial class MainWindow
{
    private static readonly log4net.ILog s_log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public CancellationTokenSource CancellationTokenSource { get; set; }

    public MainWindow()
    {
        s_log.Debug("Konstruktor - startet");

        CancellationTokenSource=new CancellationTokenSource();
      
        var modelKnx = new Model.ModelKnx();
        var vmKnx = new VmKnx(modelKnx, CancellationTokenSource);

        InitializeComponent();
        DataContext = vmKnx;
    }
}
