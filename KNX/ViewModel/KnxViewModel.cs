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
            knx = new KNX.Model.Knx();
            ViAnzeige = new VisuAnzeigen(knx);
        }

        public Model.Knx Knx { get { return knx; } }

        #region BtnStart
        private ICommand _btnStart;
        public ICommand BtnStart
        {
            get
            {
                if (_btnStart == null)
                {
                    _btnStart = new RelayCommand(p => knx.TasterStart(), p => true);
                }
                return _btnStart;
            }
        }
        #endregion

        #region BtnStop
        private ICommand _btnStop;
        public ICommand BtnStop
        {
            get
            {
                if (_btnStop == null)
                {
                    _btnStop = new RelayCommand(p => knx.TasterStop(), p => true);
                }
                return _btnStop;
            }
        }
        #endregion
        
    }
}
