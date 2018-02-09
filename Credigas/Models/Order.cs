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
    using SQLiteNetExtensions.Attributes;
    using System.Collections.Generic;

    public class Order : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DataService dataService;
        NavigationService navigationService;
        DialogService dialogService;
        private readonly double EPSILON = 0;
        #endregion

        #region Properties
        [PrimaryKey]
        [JsonProperty(PropertyName = "id_pedido")]
        public long OrderId { get; set; }

        [JsonProperty(PropertyName = "no_tarjeta")]
        public string CardId { get; set; }

        [ForeignKey(typeof(Customer))]
        [JsonProperty(PropertyName = "fk_cliente")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "precio")]
        public double Total { get; set; }

        [JsonProperty(PropertyName = "fk_tipo_venta")]
        public int OrderType { get; set; }

        [JsonProperty(PropertyName = "fk_cobrador")]
        public double DebCollector { get; set; }

        [JsonProperty(PropertyName = "estatus")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "observaciones")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "fk_municipio")]
        public long Municipio { get; set; }

        [JsonProperty(PropertyName = " pagada")]
        public string Closed { get; set; }



        public double Collected
        {
            get
            {
                double total = 0.0;
                /*
                foreach (var payment in Payments)
                {
                    total += payment.Total;
                }
                */
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

        [OneToMany]
        public List<Payment> Payments { get; set; }

        //public ObservableCollection<Payment> Payments { get; set; }
        #endregion

        #region Constructors
        public Order()
        {
            dataService = new DataService();
            navigationService = new NavigationService();
            dialogService = new DialogService();

            //Payments = new ObservableCollection<Payment>();
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

            if (Math.Abs(result) <= EPSILON)
                return;
            /*
            this.Payments.Add(new Payment
            {
                PaymentId = 100,
                Total = result,
                Date = DateTime.Today,
            });
            */

            PropertyChanged?.Invoke(
                        this,
                new PropertyChangedEventArgs(nameof(Collected)));

            PropertyChanged?.Invoke(
                        this,
                new PropertyChangedEventArgs(nameof(OutstandingBalance)));
            return;
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
