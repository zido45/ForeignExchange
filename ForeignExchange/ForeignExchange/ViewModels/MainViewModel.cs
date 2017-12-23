

namespace ForeignExchange.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        bool _isRunning;
        string _result;
        #endregion


        #region Propiedades
        public string Amount { get; set; }
        public ObservableCollection<Rate> Rates { get; set; }
        public Rate SourceRate { get; set; }
        public Rate TargetRate { get; set; }
        public bool IsRunning {
            get {
                return _isRunning;
            }
            set {
                if(_isRunning !=value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        public bool IsEnabled { get; set; }
        public String Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand ConvertCommand {
            get {
                return new RelayCommand(Convert);
            }
        }

      

        private void Convert()
        {
            throw new NotImplementedException();
        }
        #endregion


        public MainViewModel()
        {
            LoadRates(); 
            
        }

        #region Methods

        void LoadRates()
        {
            IsRunning = true;
            Result = "Loading rates...";
        }
        #endregion
    }
}
