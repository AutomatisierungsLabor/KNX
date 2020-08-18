namespace KNX.ViewModel
{
    using Model;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Knx _knx;

        public VisuAnzeigen(Knx knx)
        {
            _knx = knx;

            EnableButton = false;
            SelectedIndex = 0;
            TextBoxText = "";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                TextBoxText = _knx.TextBoxText();
                EnableButton = _knx.BothButtonsEnabled();
                SelectedIndex = _knx.GetSelectedIndex();

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        #region EnableButton
        private bool _enableButton;
        public bool EnableButton
        {
            get => _enableButton;
            set
            {
                _enableButton = value;
                OnPropertyChanged("EnableButton");
            }
        }
        #endregion

        #region SelectedIndex
        private object _selectedIndex;
        public object SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                _knx.SelectedIndexChanched(_selectedIndex);
                OnPropertyChanged("SelectedIndex");
            }
        }
        #endregion

        #region TextBoxText
        private object _textBoxText;
        public object TextBoxText
        {
            get => _textBoxText;
            set
            {
                _textBoxText = value;
                OnPropertyChanged("TextBoxText");
            }
        }
        #endregion


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}
