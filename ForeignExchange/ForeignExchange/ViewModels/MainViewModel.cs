

namespace ForeignExchange.ViewModels
{
    using ForeignExchange.Helpers;
    using ForeignExchange.Services;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
 

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        bool _isRunning;
        string _result;
        bool _isenabled;
        ObservableCollection<Rate> _rates;
        Rate _sourceRate;
        Rate _targetRate;
        String _status;
        List<Rate> rates;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogservice;
        DataService dataService;
        #endregion

        #region Propiedades

        public String Status {

            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
                }
            }

        }
        public string Amount { get; set; }
        public ObservableCollection<Rate> Rates {

            get
            {
                return _rates;
            }
            set
            {
                if (_rates != value)
                {
                    _rates = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rates)));
                }
            }



        }
        public Rate SourceRate {
            get
            {
                return _sourceRate;
            }
            set
            {
                if (_sourceRate != value)
                {
                    _sourceRate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceRate)));
                }
            }
        }
        public Rate TargetRate {
            get
            {
                return _targetRate;
            }
            set
            {
                if (_targetRate != value)
                {
                    _targetRate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetRate)));
                }
            }

        }
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
        public bool IsEnabled {
            get
            {
                return _isenabled;
            }
            set
            {
                if (_isenabled != value)
                {
                    _isenabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }

        }


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


        public ICommand ChangeCommand
        {
            get
            {
                return new RelayCommand(Change);
            }
        }

         private void Change()
        {
            var aux = SourceRate;
            SourceRate = TargetRate;
            TargetRate = aux;
            Convert();
        }

        async private void Convert()
        {
            if (string.IsNullOrEmpty(Amount))
            {

                await dialogservice.ShowMessage(Lenguages.Title, "You must enter a value in amount");
          
                return;
            }

            if (!decimal.TryParse(Amount, out decimal amount))
            {
                await dialogservice.ShowMessage(Lenguages.Title, "You must enter a numeric value in amount");
                return;
            }

            if (SourceRate==null)
            {
                await dialogservice.ShowMessage(Lenguages.Title, "You must select a source rate");
                return;
            }

            if (TargetRate == null)
            {
                await dialogservice.ShowMessage(Lenguages.Title, "You must select a target rate");

                return;
            }

            var amountConverted = amount / (decimal)SourceRate.TaxRate * (decimal)TargetRate.TaxRate;
            
            Result = string.Format("{0} {1:C2} ={2} {3:c2} ",SourceRate.Code,amount,TargetRate.Code,amountConverted);
        }
        #endregion


        public MainViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogservice = new DialogService();
            LoadRates(); 
            
        }

        #region Methods

        async void LoadRates()
        {

            IsRunning = true;
            Result = "Loading rates...";
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                //IsRunning = false;
                //Result = connection.Message;
                //return;

                LoadLocalData();
            }
            else
            {
                await LoaDataFromApi();
            }

            if (rates.Count==0)
            {
                IsRunning = false;
                IsEnabled = false;
                Result = "No hay conexion con internet y no hay precarga de datos";
                return;
            }

            Rates = new ObservableCollection<Rate>(rates);
            IsRunning = false;
            IsEnabled = true;
            Result = "Ready to convert";
           // Status = "Rates Loaded from internet";
        }

        async Task LoaDataFromApi()
        {
            var url = "http://apiexchangerates.azurewebsites.net"; //Application.Current.Resources["URLAPI"].ToString();
            var response = await apiService.GetList<Rate>(url, "api/rates");
            if (!response.IsSuccess)
            {
                // IsRunning = false;
                // Result = response.Message;
                //  return;
                LoadLocalData();
                return;
            }
            Status = "Datos cargados desde internet";
            //Storage data local
            rates = (List<Rate>)response.Result;
            dataService.DeleteAll<Rate>();
            dataService.Save(rates);
            IsRunning = false;
        }

        private void LoadLocalData()
        {
            rates = dataService.Get<Rate>();
            Status = "Datos cargados desde local";
        }
        #endregion
    }
}
