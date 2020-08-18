namespace KNX
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            var knxViewModel = new ViewModel.KnxViewModel();

            InitializeComponent();
            DataContext = knxViewModel;
        }
    }
}