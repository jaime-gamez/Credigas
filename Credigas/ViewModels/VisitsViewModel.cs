namespace Credigas.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Credigas.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class VisitsViewModel : INotifyPropertyChanged
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
        public VisitsViewModel(Customer customer)
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            CurrentCustomer = customer;
            //Visits = dataService.GetVisits(customer.Order.DebCollector, customer.CustomerId,customer.Order.OrderId);
            _observaleVisits = new ObservableCollection<Visit>();
            LoadVisits();
        }
        #endregion

        #region Properties
        private List<Visit> _visits;
        public List<Visit> Visits
        {
            get => _visits;
            set
            {
                _visits = value;
                PropertyChanged?.Invoke(
                        this,
                    new PropertyChangedEventArgs(nameof(Visits)));
            }
        }

        private ObservableCollection<Visit> _observaleVisits;
        public ObservableCollection<Visit> ObservableVisits
        {
            get => _observaleVisits;
            set
            {
                _observaleVisits = value;
                PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ObservableVisits)));
            }
        }

        private Customer _customer;
        public Customer CurrentCustomer
        {
            get => _customer;
            set
            {
                _customer = value;
                PropertyChanged?.Invoke(
                        this,
                    new PropertyChangedEventArgs(nameof(CurrentCustomer)));
            }
        }
        #endregion

        #region Commands
        public ICommand AddVisitCommand
        {
            get
            {
                return new RelayCommand(AddVisit);
            }
        }

        async void AddVisit()
        {
            await dialogService.ShowMessage("Crédigas","Agregar Visita");
        }
        #endregion

        #region Methods
        void LoadVisits()
        {
            ObservableVisits.Clear();
            ObservableVisits = new ObservableCollection<Visit>();

            ObservableVisits.Add(new Visit
            {
                VisitId = 1,
                DebCollectorId = CurrentCustomer.Order.DebCollector,
                CustomerId = CurrentCustomer.CustomerId,
                OrderId = CurrentCustomer.Order.OrderId,
                Date = DateTime.Now,
                Notes = "Mis Notas 1 Mis Notas 2 Mis Notas 3 Mis Notas 4 Mis Notas 5 Mis Notas 6 Mis Notas 7 Mis Notas 8 Mis Notas 9 Mis Notas 10 Mis Notas 11 Mis Notas 12 Mis Notas 13.",
                Outstanding = 100
            });

            ObservableVisits.Add(new Visit
            {
                VisitId = 2,
                DebCollectorId = CurrentCustomer.Order.DebCollector,
                CustomerId = CurrentCustomer.CustomerId,
                OrderId = CurrentCustomer.Order.OrderId,
                Date = DateTime.Now,
                Notes = "Mis Notas 2",
                Outstanding = 101
            });

            ObservableVisits.Add(new Visit
            {
                VisitId = 3,
                DebCollectorId = CurrentCustomer.Order.DebCollector,
                CustomerId = CurrentCustomer.CustomerId,
                OrderId = CurrentCustomer.Order.OrderId,
                Date = DateTime.Now,
                Notes = "Mis Notas 3",
                Outstanding = 101
            });

            ObservableVisits.Add(new Visit
            {
                VisitId = 4,
                DebCollectorId = CurrentCustomer.Order.DebCollector,
                CustomerId = CurrentCustomer.CustomerId,
                OrderId = CurrentCustomer.Order.OrderId,
                Date = DateTime.Now,
                Notes = "Mis Notas 4",
                Outstanding = 101
            });

            ObservableVisits.Add(new Visit
            {
                VisitId = 5,
                DebCollectorId = CurrentCustomer.Order.DebCollector,
                CustomerId = CurrentCustomer.CustomerId,
                OrderId = CurrentCustomer.Order.OrderId,
                Date = DateTime.Now,
                Notes = "Mis Notas 5",
                Outstanding = 101
            });

            PropertyChanged?.Invoke(
                       this,
                       new PropertyChangedEventArgs(nameof(ObservableVisits)));
        }
        #endregion
    }
}
