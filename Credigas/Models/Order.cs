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
    using Xamarin.Forms;

    public class Order : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
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

        [ForeignKey(typeof(Customer))]  // Specify the foreign key
        [JsonProperty(PropertyName = "fk_cliente")]
        public long CustomerId { get; set; }

        [ManyToOne]      // Many to one relationship with Stock
        public Customer Customer { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "precio")]
        public double Total { get; set; }

        [JsonProperty(PropertyName = "fk_tipo_venta")]
        public int OrderType { get; set; }

        [JsonProperty(PropertyName = "fk_cobrador")]
        public long DebCollector { get; set; }

        [JsonProperty(PropertyName = "estatus")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "observaciones")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "fk_municipio")]
        public long Municipio { get; set; }

        [JsonProperty(PropertyName = " pagada")]
        public string Closed { get; set; }

        public DateTime DateModified { get; set; }

        private bool _modified;

        [JsonIgnore]
        public bool Modified{
            get => _modified;
            set{
                value = _modified;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Modified)));
            } 
        }

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

        public List<Payment> _payments;
        [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
        public List<Payment> Payments {
            get => _payments; 
            set{
                _payments = value;
                CopyPayments();
                PropertyChanged?.Invoke( this,
                                        new PropertyChangedEventArgs(nameof(Payments)));
            } 
        }

        [Ignore]
        public ObservableCollection<Payment> PaymentsView { get; set; }
        #endregion

        #region Constructors
        public Order()
        {
            apiService = new ApiService();
            dataService = new DataService();
            navigationService = new NavigationService();
            dialogService = new DialogService();

            PaymentsView = new ObservableCollection<Payment>();
            _modified = false;
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

            Payment next = new Payment();
            next.OrderId = this.OrderId;
            next.PaymentId = dataService.GetNextIdForPayment();
            next.Date = DateTime.Today;
            next.Total = result;

            try
            {
                dataService.Insert<Payment>(next);
                Customer.Modified = true;
                dataService.Update<Customer>(Customer);
                WorkRouteViewModel workRoute = MainViewModel.GetInstance().WorkRoute;
                workRoute.UpdateClient(Customer);
                //next.Order.Modified = true;
            }
            catch (Exception ex)
            {
                

            }

            
            this.Payments.Add(next);

            CopyPayments();

            //Save payment in Server
            SavePaymentToServer(next);

            PropertyChanged?.Invoke(
                        this,
                new PropertyChangedEventArgs(nameof(Payments)));


            PropertyChanged?.Invoke(
                        this,
                new PropertyChangedEventArgs(nameof(Collected)));

            PropertyChanged?.Invoke(
                        this,
                new PropertyChangedEventArgs(nameof(OutstandingBalance)));

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home.CurrentStatistics = LoadStatistics();

            return;
        }
        #endregion

        #region Methods

        public bool CopyPayments(){
            bool isClosed = false;
            double total = 0.0;
            PaymentsView.Clear();
            foreach (var item in this.Payments)
            {
                total += item.Total;
                PaymentsView.Add(item);
            }
            if (total >= this.Total)
            {
                dataService.CloseOrder( this.OrderId);
                isClosed = true;
            }
            return isClosed;
        }

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

        Statistics LoadStatistics()
        {
            Statistics statistics = new Statistics();
            statistics = dataService.LoadStatistics();
            return statistics;
        }

        async public void SavePaymentToServer(Payment payment){

            var connection = await apiService.CheckConnection();
            if (connection == null)
            {
                await dialogService.ShowMessage("Crédigas", "Solo se guardará local");
                return;
            }

            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Crédigas", "Solo se guardará local");
                return;
            }

            TokenResponse token = MainViewModel.GetInstance().Token;
            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var response = await apiService.Put<Payment>(urlAPI, "payments", token.City, token.TokenType, token.AccessToken, payment);
            if(response.IsSuccess){
                payment.IsSync = 1;
                dataService.Update<Payment>(payment);

            }
            return;
        }
        #endregion
    }
}
