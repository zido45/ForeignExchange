﻿

namespace ForeignExchange.ViewModels
{
    using ForeignExchange.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Net.Http;
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
        #endregion


        #region Propiedades
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
                await Application.Current.MainPage.DisplayAlert(
                    Lenguages.ErrorTitle,
                    Lenguages.AmountValidation,
                     Lenguages.Accept);
                return;
            }

            if (!decimal.TryParse(Amount, out decimal amount))
            {
                await Application.Current.MainPage.DisplayAlert(
                Lenguages.ErrorTitle,
                  Lenguages.ValueValidation,
                Lenguages.Accept);
                return;
            }

            if (SourceRate==null)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Lenguages.ErrorTitle,
                   Lenguages.SourceRate,
                   Lenguages.Accept);
                return;
            }

            if (TargetRate == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Lenguages.ErrorTitle,
                   Lenguages.TargetRate,
                   Lenguages.Accept);
                return;
            }

            var amountConverted = amount / (decimal)SourceRate.TaxRate * (decimal)TargetRate.TaxRate;
            
            Result = string.Format("{0} {1:C2} ={2} {3:c2} ",SourceRate.Code,amount,TargetRate.Code,amountConverted);
        }
        #endregion


        public MainViewModel()
        {
            LoadRates(); 
            
        }

        #region Methods

        async void LoadRates()
        {
            IsRunning = true;
            Result = Lenguages.LoadingRates;

            try
            {
                var client = new HttpClient(); //instanciamos el cliente para consumir
                client.BaseAddress = new Uri("http://apiexchangerates.azurewebsites.net"); //definimos la url base del servicio
                var controller = "/api/rates"; //resto de la direccion del servicio
                //hasta hora solo hemos puesto las balas a la pistola pero aun no hemos disparado
                var response = await client.GetAsync(controller); //aqui disparamos
                var result = await response.Content.ReadAsStringAsync();

                if(!response.IsSuccessStatusCode)
                {
                    IsRunning = false;
                    Result = result;
                }
                //si si hay resultado hay que convertir el string al objeto deseado en este caso una lista de rates
                var rates = JsonConvert.DeserializeObject<List<Rate>>(result);
                // hay que convertirlo en una observable collection
                Rates = new ObservableCollection<Rate>(rates);
                IsRunning = false;
                Result = Lenguages.ReadytoConvert;
                IsEnabled = true;
            }
            catch (Exception ex)
            {

                IsRunning = false;
                Result = ex.Message;
            }
        }
        #endregion
    }
}
