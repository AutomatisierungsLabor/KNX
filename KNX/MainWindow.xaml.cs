namespace KNX
{
    public partial class MainWindow
    {
        public ViewModel.KnxViewModel KnxViewModel { get; set; }
        public MainWindow()
        {
            KnxViewModel = new ViewModel.KnxViewModel();

            InitializeComponent();
            DataContext = KnxViewModel;
        }
    }
}
