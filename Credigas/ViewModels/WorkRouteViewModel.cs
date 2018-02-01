namespace Credigas.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Credigas.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class WorkRouteViewModel : INotifyPropertyChanged
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
        public WorkRouteViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            LoadClients();
        }
        #endregion

        #region Properties
        private Statistics _statistics;
        public Statistics CurrentStatistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                PropertyChanged?.Invoke(
                        this,
                    new PropertyChangedEventArgs(nameof(CurrentStatistics)));
            }
        }

        public ObservableCollection<Customer> Clients
        {
            get;
            set;
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        async void Navigate()
        {
            await dialogService.ShowMessage(
                    "Crédigas",
                    "Valide carga de ruta.");
        }
        #endregion

        #region Methods
        void LoadClients()
        {
            Clients = new ObservableCollection<Customer>();

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 110, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                OrderId = "170-0001",
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });
        }
        #endregion
    }
}
