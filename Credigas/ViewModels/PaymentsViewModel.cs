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

    public class PaymentsViewModel: INotifyPropertyChanged
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
        public PaymentsViewModel()
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
            ObservableCollection<Payment> Payments = new ObservableCollection<Payment>();

            Payments.Add(new Payment
            {
                PaymentId = 1,
                Total = 101.0,
                Date = DateTime.Now,//DateTime.Parse("01/29/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 1,
                Total = 100.0,
                Date = new DateTime(2018, 1, 29),//DateTime.Parse("01/29/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 2,
                Total = 100.0,
                Date = new DateTime(2018, 1, 22),//DateTime.Parse("01/22/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 3,
                Total = 100.0,
                Date = new DateTime(2018, 1, 15),//DateTime.Parse("01/15/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 4,
                Total = 100.0,
                Date = new DateTime(2018, 1, 8),//DateTime.Parse("01/08/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 5,
                Total = 100.0,
                Date = new DateTime(2018, 1, 1),//DateTime.Parse("01/01/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 6,
                Total = 100.0,
                Date = new DateTime(2017, 12, 25),//DateTime.Parse("12/25/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 7,
                Total = 100.0,
                Date = new DateTime(2017, 12, 18),//DateTime.Parse("12/18/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 8,
                Total = 100.0,
                Date = new DateTime(2017, 12, 11),//DateTime.Parse("12/10/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 9,
                Total = 100.0,
                Date = new DateTime(2017, 12, 4),//DateTime.Parse("12/02/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 10,
                Total = 100.0,
                Date = new DateTime(2017, 11, 27),//DateTime.Parse("11/25/2017"),
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            //***
            Payments.Add(new Payment
            {
                PaymentId = 1,
                Total = 100.0,
                Date = new DateTime(2018, 1, 29),//DateTime.Parse("01/29/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 2,
                Total = 100.0,
                Date = new DateTime(2018, 1, 22),//DateTime.Parse("01/22/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 3,
                Total = 100.0,
                Date = new DateTime(2018, 1, 15),//DateTime.Parse("01/15/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 4,
                Total = 100.0,
                Date = new DateTime(2018, 1, 8),//DateTime.Parse("01/08/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 5,
                Total = 100.0,
                Date = new DateTime(2018, 1, 1),//DateTime.Parse("01/01/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 6,
                Total = 100.0,
                Date = new DateTime(2017, 12, 25),//DateTime.Parse("12/25/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 7,
                Total = 100.0,
                Date = new DateTime(2017, 12, 18),//DateTime.Parse("12/18/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 8,
                Total = 100.0,
                Date = new DateTime(2017, 12, 11),//DateTime.Parse("12/10/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 9,
                Total = 100.0,
                Date = new DateTime(2017, 12, 4),//DateTime.Parse("12/02/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 10,
                Total = 100.0,
                Date = new DateTime(2017, 11, 27),//DateTime.Parse("11/25/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 1,
                Total = 100.0,
                Date = new DateTime(2018, 1, 29),//DateTime.Parse("01/29/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 2,
                Total = 100.0,
                Date = new DateTime(2018, 1, 22),//DateTime.Parse("01/22/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 3,
                Total = 100.0,
                Date = new DateTime(2018, 1, 15),//DateTime.Parse("01/15/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 4,
                Total = 100.0,
                Date = new DateTime(2018, 1, 8),//DateTime.Parse("01/08/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 5,
                Total = 100.0,
                Date = new DateTime(2018, 1, 1),//DateTime.Parse("01/01/2018"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 6,
                Total = 100.0,
                Date = new DateTime(2017, 12, 25),//DateTime.Parse("12/25/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 7,
                Total = 100.0,
                Date = new DateTime(2017, 12, 18),//DateTime.Parse("12/18/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 8,
                Total = 100.0,
                Date = new DateTime(2017, 12, 11),//DateTime.Parse("12/10/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 9,
                Total = 100.0,
                Date = new DateTime(2017, 12, 4),//DateTime.Parse("12/02/2017"),
            });

            Payments.Add(new Payment
            {
                PaymentId = 10,
                Total = 100.0,
                Date = new DateTime(2017, 11, 27),//DateTime.Parse("11/25/2017"),
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });

            Clients.Add(new Models.Customer
            {
                FullName = "Jaime Gámez Luna",
                City = "García",
                Address = "Constanza 1100, Mitras Poniente Sector Jordan",
                Icon = "icons8_user_male_circle_filled.png",
                Collected = false,
                Order = new Order
                {
                    OrderId = "170-0001",
                    Total = 1560.0,
                    Payments = Payments
                },
                Phone1 = "81 1600 4236",
                Phone2 = "81 1043 0434"
            });
        }
        #endregion
    }
}
