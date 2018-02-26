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

    public class PaymentsClientViewModel: INotifyPropertyChanged
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
        public PaymentsClientViewModel( Customer customer)
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            _customerTemp = customer;


            SeleccOrder();

        }
        #endregion

        #region Properties
        private Customer _customerTemp;
        private List<Order> _orders;
        public List<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                PropertyChanged?.Invoke(
                        this,
                    new PropertyChangedEventArgs(nameof(Orders)));
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

        #endregion

        #region Methods
        async public void SeleccOrder(){
            Orders = dataService.GetOrdersByCustomer(_customerTemp.CustomerId);

            List<string> orders = new List<string>();
            foreach (var item in Orders)
            {
                orders.Add(item.CardId);
                item.Customer = _customerTemp;
                item.Payments = dataService.GetPaymentsByOrder(item.OrderId);
            }
            _customerTemp.Orders = Orders;

            /*
            if (Orders.Count > 1)
            {
                var orderSelected = await dialogService.ShowOptions("Seleccione pedido del cliente", orders.ToArray());
                var current = _customerTemp.Orders.Find(o => o.CardId == orderSelected);
                if (current != null)
                    _customerTemp.Order = current;
                else
                    _customerTemp.Order = Orders[0];

            }
            else
            {
                _customerTemp.Order = Orders[0];
            }
            */
            _customerTemp.Order = Orders[0];
            CurrentCustomer = _customerTemp;
            PropertyChanged?.Invoke(
                        this,
                    new PropertyChangedEventArgs(nameof(CurrentCustomer)));
            
        }
        #endregion
    }
}
