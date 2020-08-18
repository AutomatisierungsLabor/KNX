namespace KNX.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class KnxViewModel
    {

        private readonly Model.Knx _knx;
        // ReSharper disable once UnusedMember.Global
        public Model.Knx Knx => _knx;
        public VisuAnzeigen ViAnzeige { get; set; }

        public KnxViewModel()
        {
            _knx = new Model.Knx();
            ViAnzeige = new VisuAnzeigen(_knx);
        }

        

        #region BtnStart
        private ICommand _btnStart;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnStart
        {
            get { return _btnStart ?? (_btnStart = new RelayCommand(p => _knx.TasterStart(), p => true)); }
        }
        #endregion

        #region BtnStop
        private ICommand _btnStop;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnStop
        {
            get { return _btnStop ?? (_btnStop = new RelayCommand(p => _knx.TasterStop(), p => true)); }
        }
        #endregion
        
    }
}
