namespace Credigas.Models
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System;
    using ViewModels;
    using System.ComponentModel;
    using Credigas.Popups;
    using System.Threading.Tasks;
    using Rg.Plugins.Popup.Services;
    using SQLite;
    using Newtonsoft.Json;

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
        [PrimaryKey, AutoIncrement]
        [JsonProperty(PropertyName = "access_token")]
        public int TokenResponseId { get; set; }

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
            double result = await OpenCancellableMoneyInputAlertDialog();

            if (result == 0.0)
                return;
            
            this.Payments.Add(new Payment
            {
                PaymentId = 100,
                Total = result,
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

        #region Methods
        private async Task<double> OpenCancellableMoneyInputAlertDialog()
        {
            // create the TextInputView
            var inputView = new MoneyInputCancellableView(
                "De cuanto es el abono?", "Capturelo aquí...", "Grabar", "Cancelar", "Ops! Debe ser una cantidad mayor a cero!");

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<double>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                if ( ((MoneyInputCancellableView)sender).MoneyInputResult > 0.0)
                    {
                    ((MoneyInputCancellableView)sender).IsValidationLabelVisible = false;
                    popup.PageClosedTaskCompletionSource.SetResult(((MoneyInputCancellableView)sender).MoneyInputResult);
                    }
                    else
                    {
                    ((MoneyInputCancellableView)sender).IsValidationLabelVisible = true;
                    }
                };

            // subscribe to the TextInputView's Button click event
            inputView.CancelButtonEventHandler +=
                (sender, obj) =>
                {
                    popup.PageClosedTaskCompletionSource.SetResult(0.0);
                };

            // Push the page to Navigation Stack
            await PopupNavigation.PushAsync(popup);

            // await for the user to enter the text input
            var result = await popup.PageClosedTask;

            // Pop the page from Navigation Stack
            await PopupNavigation.PopAsync();

            // return user inserted text value
            return result;
        }
#endregion
    }
}
