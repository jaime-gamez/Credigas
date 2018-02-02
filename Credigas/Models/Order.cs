namespace Credigas.Models
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System;
    using ViewModels;
    using System.ComponentModel;

    public class Order : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DataService dataService;
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Properties
        public string OrderId { get; set; }
        public double Total { get; set; }
        public double Collected
        {
            get
            {
                double total = 0.0;
                foreach (var payment in Payments)
                {
                    total += payment.Total;
                }
                return total;
            }
        }
        public double OutstandingBalance
        {
            get
            {
                return Total - Collected;
            }
        }
        public ObservableCollection<Payment> Payments { get; set; }
        #endregion

        #region Constructors
        public Order()
        {
            dataService = new DataService();
            navigationService = new NavigationService();
            dialogService = new DialogService();

            Payments = new ObservableCollection<Payment>();
        }
        #endregion

        #region Commands
        public ICommand AddPaymentCommand
        {
            get
            {
                return new RelayCommand(AddPayment);
            }
        }

        async void AddPayment()
        {
            await dialogService.ShowMessage(
                    "Crédigas",
                    "Agregar Abono.");

            this.Payments.Add(new Payment
            {
                PaymentId = 100,
                Total = 50.0,
                Date = DateTime.Today,
            });

            PropertyChanged?.Invoke(
                        this,
                new PropertyChangedEventArgs(nameof(Collected)));

            PropertyChanged?.Invoke(
                        this,
                new PropertyChangedEventArgs(nameof(OutstandingBalance)));
        }
        #endregion
    }
}
