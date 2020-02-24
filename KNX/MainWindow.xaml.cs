using System.Windows;

namespace KNX
{
    public partial class MainWindow : Window
    {
        public ViewModel.KnxViewModel knxViewModel { get; set; }

        public MainWindow()
        {
            knxViewModel = new ViewModel.KnxViewModel();

            InitializeComponent();
            DataContext = knxViewModel;
        }
    }
}
