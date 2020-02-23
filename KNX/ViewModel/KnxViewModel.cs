namespace KNX.ViewModel
{
    using Commands;
    using KNX.Model;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Input;

    public class KnxViewModel : INotifyPropertyChanged
    {

        public readonly Model.Knx knx;

   
        public KnxViewModel(MainWindow mainWindow)
        {
            knx = new KNX.Model.Knx(mainWindow);          
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

        public event PropertyChangedEventHandler PropertyChanged;

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
