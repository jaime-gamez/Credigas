namespace Credigas.ViewModels
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

    public class LoadRouteViewModel : INotifyPropertyChanged
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

        #region Constructors
        public LoadRouteViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
            IsRunning = false;
            IsLoaded = false;
            Date = System.DateTime.Today;
            Status = "Presione Cargar Ruta";
        }
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        bool _isLoaded;
        string _status;
        DateTime _date;
        #endregion

        #region Properties
        public DateTime Date
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
                        new PropertyChangedEventArgs(nameof(Date)));
                }
            }
        }

        public bool IsLoaded
        {
            get
            {
                return _isLoaded;

            }
            set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value; ;
                    _isLoaded = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsLoaded)));
                }
            }
        }

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

        private List<Customer> Clients { get; set; }
        private List<Order> Orders { get; set; }
        private List<Payment> Payments { get; set; }
        #endregion

        #region Commands
        public ICommand LoadRouteCommand
        {
            get
            {
                return new RelayCommand(LoadRoute);
            }
        }

        async void LoadRoute()
        {
            IsRunning = true;
            IsEnabled = false;
            Status = "...";

            var res = await LoadClients();

            if (res.Equals(true))
            {
                var res2 = await LoadOrders();
                if (res2.Equals(true))
                { 
                    var res3 = await LoadPayments();
                }
            }


            IsRunning = false;
            IsLoaded = true;
            Status = "Proceso Terminado OK";

            await navigationService.BackOnMaster();

            return;

        }
        #endregion

        #region Methods

        async Task<bool> LoadClients(){
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

            //Get all clients for current user
            var response = await apiService.GetList<Customer>(urlAPI, "clients", token.City, token.TokenType, token.AccessToken, currentUser.CobradorId);

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
            SaveClientsOnDB();

            return true;
        }

        void SaveClientsOnDB()
        {
            Status = "Copiando clientes...";
            dataService.DeleteAllCustomers();
            foreach (var client in Clients)
            {
                dataService.Insert(client);
            }
            Status = "Clientes copiados..";
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

            //Get all clients for current user
            var response = await apiService.GetList<Order>(urlAPI, "orders", token.City, token.TokenType, token.AccessToken, currentUser.CobradorId);

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
            SaveOrdersOnDB();

            return true;
        }

        void SaveOrdersOnDB()
        {
            Status = "Copiando pedidos...";
            dataService.DeleteAllOrders();
            foreach (var order in Orders)
            {
                dataService.Insert(order);
            }
            Status = "Pedidos copiados..";
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

            //Get all clients for current user
            var response = await apiService.GetList<Payment>(urlAPI, "payments", token.City, token.TokenType, token.AccessToken, currentUser.CobradorId);

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
            SavePaymentsOnDB();

            return true;
        }

        void SavePaymentsOnDB()
        {
            Status = "Copiando abonos...";
            dataService.DeleteAllPayments();
            foreach (var payment in Payments)
            {
                dataService.Insert(payment);
            }
            Status = "Abonos copiados..";
        }
        #endregion
    }
}
