namespace KNX.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class KnxViewModel
    {

        public Model.Knx knx;
        public VisuAnzeigen ViAnzeige { get; set; }

        public KnxViewModel()
        {
            knx = new Model.Knx();
            ViAnzeige = new VisuAnzeigen(knx);
        }

        public Model.Knx Knx => knx;

        #region BtnStart
        private ICommand _btnStart;
        public ICommand BtnStart
        {
            get { return _btnStart ?? (_btnStart = new RelayCommand(p => knx.TasterStart(), p => true)); }
        }
        #endregion

        #region BtnStop
        private ICommand _btnStop;
        public ICommand BtnStop
        {
            get { return _btnStop ?? (_btnStop = new RelayCommand(p => knx.TasterStop(), p => true)); }
        }
        #endregion
        
    }
}
