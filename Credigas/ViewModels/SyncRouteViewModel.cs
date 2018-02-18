namespace Credigas.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Credigas.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Linq;
    using Xamarin.Forms;

    public class SyncRouteViewModel: INotifyPropertyChanged
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
        public SyncRouteViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
            IsRunning = false;
            IsLoaded = false;
            Date = System.DateTime.Today;
            LoadClients();
        }
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        bool _isLoaded;
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

        public ObservableCollection<Customer> Clients
        {
            get;
            set;
        }

        public ObservableCollection<Payment> PaymentsForView
        {
            get;
            set;
        }
        #endregion

        #region Commands
        public ICommand SyncRouteCommand
        {
            get
            {
                return new RelayCommand(SyncRoute);
            }
        }

        async void SyncRoute()
        {
            IsRunning = true;

            ClearClients();

            IsLoaded = true;
            IsRunning = false;
            IsEnabled = false;

            await dialogService.ShowMessage(
                    "Crédigas",
                    "Valide no queden pendientes.");
            

            //await navigationService.BackOnMaster();
        }
        #endregion

        #region Methods
        void LoadClients()
        {
            Clients = new ObservableCollection<Customer>();
            PaymentsForView = new ObservableCollection<Payment>();//
            List<Payment> payments = dataService.GetPendingPaymentsWithChildren(DateTime.Today);
            PaymentsForView.Clear();
            Clients.Clear();
            foreach (var item in payments)
            {
                Clients.Add(item.Order.Customer);
                PaymentsForView.Add(item);
            }
        }

        async void ClearClients(){

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }


            //Task.Delay(1000);
            var payments = (from p in PaymentsForView
                            select p).ToList<Payment>();

            TokenResponse token = MainViewModel.GetInstance().Token;
            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            List<Payment> pendings = new List<Payment>();

            foreach (var payment in payments)
            {
                var response = await apiService.Put<Payment>(urlAPI, "payments", token.City, token.TokenType, token.AccessToken, payment);
                if (response.IsSuccess)
                {
                    payment.IsSync = 1;
                    dataService.Update<Payment>(payment);

                }else{
                    pendings.Add(payment);
                }
            }
            PaymentsForView.Clear();
            Clients.Clear();
            foreach (var item in pendings)
            {
                Clients.Add(item.Order.Customer);
                PaymentsForView.Add(item);
            }
        }

        #endregion

    }
}
