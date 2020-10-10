namespace KNX.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class KnxViewModel
    {
        // ReSharper disable once UnusedMember.Global
        public Model.Knx Knx { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public KnxViewModel()
        {
            Knx = new Model.Knx();
            ViAnzeige = new VisuAnzeigen(Knx);
        }

        private ICommand _btnStart;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnStart => _btnStart ??= new RelayCommand(p => Knx.TasterStart(), p => true);

        private ICommand _btnStop;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnStop => _btnStop ??= new RelayCommand(p => Knx.TasterStop(), p => true);
    }
}
