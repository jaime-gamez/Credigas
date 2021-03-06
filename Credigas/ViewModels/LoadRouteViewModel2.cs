﻿namespace Credigas.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Credigas.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class LoadRouteViewModel2: INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DataService dataService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        string _status;
        DateTime _minimumDate;
        DateTime _maximumdate;
        DateTime _date;
        DateTime _date2;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
        string _diaVisita;
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsToggled
        {
            get
            {
                return _isToggled;
            }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(StartDate)));
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _date2;
            }
            set
            {
                if (_date2 != value)
                {
                    _date2 = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EndDate)));
                }
            }
        }

        public DateTime MinumumDate
        {
            get => _minimumDate;
            set
            {
                if (_minimumDate != value)
                {
                    _minimumDate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(MinumumDate)));
                }
            }
        }

        public DateTime Maximumdate
        {
            get
            {
                return _maximumdate;
            }
            set
            {
                if (_maximumdate != value)
                {
                    _maximumdate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Maximumdate)));
                }
            }
        }



        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }

        public string DiaVisita
        {
            get
            {
                return _diaVisita;
            }
            set
            {
                if (_diaVisita != value)
                {
                    _diaVisita = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(DiaVisita)));
                }
            }
        }

        private List<Customer> Clients { get; set; }
        private List<Order> Orders { get; set; }
        private List<Payment> Payments { get; set; }
        private List<Visit> Visits { get; set; }
        #endregion

        #region Constructors
        public LoadRouteViewModel2()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
            IsToggled = true;
            _maximumdate = System.DateTime.Today;
            _minimumDate = _maximumdate.AddDays(-150);
            StartDate = _maximumdate.AddDays(-30);
            EndDate = _maximumdate;
            Status = "Presione Cargar Ruta";

            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    _diaVisita = "LUNES";
                    break;
                case DayOfWeek.Tuesday:
                    _diaVisita = "MARTES";
                    break;
                case DayOfWeek.Wednesday:
                    _diaVisita = "MIERCOLES";
                    break;
                case DayOfWeek.Thursday:
                    _diaVisita = "JUEVES";
                    break;
                case DayOfWeek.Friday:
                    _diaVisita = "VIERNES";
                    break;
                case DayOfWeek.Saturday:
                    _diaVisita = "SABADO";
                    break;
                case DayOfWeek.Sunday:
                    _diaVisita = "DOMINGO";
                    break;
            }

            List<Payment> payments = dataService.GetPendingPaymentsWithChildren(DateTime.Today);
            List<Visit> visits = dataService.GetAllPendingVisits();
            if(payments.Count > 0 || visits.Count > 0){
                Status = "Sincronice pendientes.";
                IsEnabled = false;
            }

        }
        #endregion

        #region Commands
        public ICommand RecoverPasswordCommand
        {
            get
            {
                return new RelayCommand(RecoverPassword);
            }
        }

        async void RecoverPassword()
        {
            MainViewModel.GetInstance().PasswordRecovery = new PasswordRecoveryViewModel();
            await navigationService.NavigateOnLogin("PasswordRecoveryView");
        }


        public ICommand LoginWithFacebookCommand
        {
            get
            {
                return new RelayCommand(LoginWithFacebook);
            }
        }

        async void LoginWithFacebook()
        {
            await navigationService.NavigateOnLogin("LoginFacebookView");
        }

        public ICommand RegisterNewUserCommand
        {
            get
            {
                return new RelayCommand(RegisterNewUser);
            }
        }

        async void RegisterNewUser()
        {
            MainViewModel.GetInstance().NewUser = new NewUserViewModel();
            await navigationService.NavigateOnLogin("NewUserView");
        }


        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        async void Login()
        {
            
            IsRunning = true; 
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Crédigas", "Solo se descarga su ruta cuando este conectado a Internet.");
                return;
            }
            /*
            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            var response = await apiService.GetToken(
                urlAPI,
                "pancholin",
                "12345678",
                "mazatlan");
            */
            var res = await LoadClients();
            res = await LoadOrders();
            res = await LoadPayments();
            res = await LoadVisits();

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home.CurrentStatistics = LoadStatistics();

            Status = "Ruta cargada al 100%";

            await dialogService.ShowMessage( "Crédigas", "Carga completada");

            IsRunning = false;
            IsEnabled = false;
        }
        #endregion

        #region Methods
        async Task<bool> LoadClients()
        {
            Status = "Validando conexión...";
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", connection.Message);
                return false;
            }

            Status = "Recuperando clientes...";
            var mainViewModel = MainViewModel.GetInstance();
            User currentUser = mainViewModel.User;
            TokenResponse token = mainViewModel.Token;

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var filters = new ClientFilters
            {
                StartDate = StartDate,
                EndDate = EndDate,
                VisitDay = DiaVisita,
            };

            //Get all clients for current user
            var response = await apiService.GetListWithPost<Customer>(urlAPI, "clients", token.City, token.TokenType, token.AccessToken, currentUser.CobradorId, filters);

            Status = "Clientes recuperados...";

            if (response == null)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", "No se puede contactar al servidor.");
                return false;
            }

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", response.Message);
                return false;
            }

            Clients = (List<Customer>)response.Result;
            await SaveClientsOnDB();

            return true;
        }

        async Task<bool> SaveClientsOnDB()
        {
            Status = "Copiando clientes...";
            dataService.DeleteAllCustomers();
            dataService.InsertAll<Customer>(Clients);

            //var clients = dataService.GetAllCustomers();
            Status = "Clientes copiados..";

            return true;
        }

        async Task<bool> LoadOrders()
        {
            Status = "Validando conexión...";
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", connection.Message);
                return false;
            }

            Status = "Recuperando pedidos...";
            var mainViewModel = MainViewModel.GetInstance();
            User currentUser = mainViewModel.User;
            TokenResponse token = mainViewModel.Token;

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var filters = new ClientFilters
            {
                StartDate = StartDate,
                EndDate = EndDate,
                VisitDay = DiaVisita,
            };

            //Get all clients for current user
            var response = await apiService.GetListWithPost<Order>(urlAPI, "orders", token.City, token.TokenType, token.AccessToken, currentUser.CobradorId, filters);

            Status = "Pedidos recuperados...";

            if (response == null)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", "No se puede contactar al servidor.");
                return false;
            }

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", response.Message);
                return false;
            }

            Orders = (List<Order>)response.Result;
            await SaveOrdersOnDB();

            return true;
        }

        async Task<bool> SaveOrdersOnDB()
        {
            Status = "Copiando pedidos...";
            dataService.DeleteAllOrders();
            dataService.InsertAll<Order>(Orders);
            Status = "Pedidos copiados..";
            return true;
        }

        async Task<bool> LoadPayments()
        {
            Status = "Validando conexión...";
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", connection.Message);
                return false;
            }

            Status = "Recuperando abonos...";
            var mainViewModel = MainViewModel.GetInstance();
            User currentUser = mainViewModel.User;
            TokenResponse token = mainViewModel.Token;

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var filters = new ClientFilters
            {
                StartDate = StartDate,
                EndDate = EndDate,
                VisitDay = DiaVisita,
            };

            //Get all clients for current user
            var response = await apiService.GetListWithPost<Payment>(urlAPI, "payments", token.City, token.TokenType, token.AccessToken, currentUser.CobradorId, filters);
            Status = "Abonos recuperados...";

            if (response == null)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", "No se puede contactar al servidor.");
                return false;
            }

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", response.Message);
                return false;
            }

            Payments = (List<Payment>)response.Result;
            await SavePaymentsOnDB();

            return true;
        }

        async Task<bool> SavePaymentsOnDB()
        {
            Status = "Copiando abonos...";
            dataService.DeleteAllPayments();
            dataService.InsertAll<Payment>(Payments);
            Status = "Abonos copiados..";
            return true;
        }

        async Task<bool> LoadVisits()
        {
            Status = "Validando conexión...";
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", connection.Message);
                return false;
            }

            Status = "Recuperando visitas...";
            var mainViewModel = MainViewModel.GetInstance();
            User currentUser = mainViewModel.User;
            TokenResponse token = mainViewModel.Token;

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var filters = new ClientFilters
            {
                StartDate = StartDate,
                EndDate = EndDate,
                VisitDay = DiaVisita,
            };

            //Get all clients for current user
            var response = await apiService.GetListWithPost<Visit>(urlAPI, "visits", token.City, token.TokenType, token.AccessToken, currentUser.CobradorId, filters);
            Status = "Visitas recuperados...";

            if (response == null)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", "No se puede contactar al servidor.");
                return false;
            }

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                Status = "";
                await dialogService.ShowMessage("Error", response.Message);
                return false;
            }

            Visits = (List<Visit>)response.Result;
            await SaveVisitsOnDB();
            return true;
        }

        async Task<bool> SaveVisitsOnDB()
        {
            Status = "Copiando visitas...";
            dataService.DeleteAllVisits();
            dataService.InsertAll<Visit>(Visits);
            Status = "Visitas copiados..";
            return true;
        }

        Statistics LoadStatistics(){
            Statistics statistics = new Statistics();
            statistics = dataService.LoadStatistics();
            return statistics;
        }
        #endregion
    }
}
