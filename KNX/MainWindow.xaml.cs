using Microsoft.Extensions.Logging;

namespace KNX;

// ReSharper disable once UnusedMember.Global
public partial class MainWindow
{
    public MainWindow(ViewModel.ViewModel viewModel, ILogger<MainWindow>logger)
    {
        logger.LogDebug("Konstruktor - startet");

        InitializeComponent();
        DataContext = viewModel;
    }
}
