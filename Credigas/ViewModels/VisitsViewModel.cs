namespace Credigas.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Credigas.Models;
    using Credigas.Popups;
    using GalaSoft.MvvmLight.Command;
    using Rg.Plugins.Popup.Services;
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
            string result = await OpenCancellableTextInputAlertDialog();

            if (result == null || result.Length == 0)
                return;

            Visit next = new Visit
            {
                VisitId = dataService.GetNextIdForVisit(),
                DebCollectorId = this.CurrentCustomer.Order.DebCollector,
                CustomerId = this.CurrentCustomer.CustomerId,
                OrderId = this.CurrentCustomer.Order.OrderId,
                Date = DateTime.Now,
                Notes = result,
                Outstanding = this.CurrentCustomer.Order.Payments.Select(p => p.Total).Sum(),
                IsSync = 1,
            };

            try
            {
                dataService.Insert<Visit>(next);
            }
            catch (Exception ex)
            {


            }


            this.ObservableVisits.Add(next);


            //Save visit in Server
            var saved = await SaveVisitToServer(next);
            if(saved){
                next.IsSync = 1;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ObservableVisits)));
            return;
        }
        #endregion

        #region Methods
        async public Task<bool> SaveVisitToServer(Visit visit)
        {

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return false;
            }

            TokenResponse token = MainViewModel.GetInstance().Token;
            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var response = await apiService.Put<Visit>(urlAPI, "visits", token.City, token.TokenType, token.AccessToken, visit);
            if (response.IsSuccess)
            {
                visit.IsSync = 1;
                dataService.Update<Visit>(visit);
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<string> OpenCancellableTextInputAlertDialog()
        {
            // create the TextInputView
            var inputView = new TextInputCancellableView(
                "Registre notas sobre su visita:", "Capturelo aquí...", "Grabar", "Cancelar", "Ops! Debe ser una cantidad mayor a cero!");

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (((TextInputCancellableView)sender).TextInputResult.Length > 0)
                    {
                        ((TextInputCancellableView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((TextInputCancellableView)sender).TextInputResult);
                    }
                    else
                    {
                    ((TextInputCancellableView)sender).IsValidationLabelVisible = true;
                    }
                };

            // subscribe to the TextInputView's Button click event
            inputView.CancelButtonEventHandler +=
                (sender, obj) =>
                {
                    popup.PageClosedTaskCompletionSource.SetResult("");
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

        void LoadVisits()
        {
            ObservableVisits.Clear();
            ObservableVisits = new ObservableCollection<Visit>(dataService.GetVisits(CurrentCustomer.Order.DebCollector, CurrentCustomer.CustomerId, CurrentCustomer.Order.OrderId));

            //List<Visit> visits = dataService.GetVisits(CurrentCustomer.Order.DebCollector, CurrentCustomer.CustomerId, CurrentCustomer.Order.OrderId);

            PropertyChanged?.Invoke(
                       this,
                       new PropertyChangedEventArgs(nameof(ObservableVisits)));
        }
        #endregion
    }
}
