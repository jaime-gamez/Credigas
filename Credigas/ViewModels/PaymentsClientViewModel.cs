﻿namespace Credigas.ViewModels
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


            Orders = dataService.GetOrdersByCustomer(customer.CustomerId);
            CurrentCustomer = customer;
            foreach (var item in Orders)
            {
                item.Payments = dataService.GetPaymentsByOrder(item.OrderId);
            }
            CurrentCustomer.Orders = Orders;
            CurrentCustomer.Order = Orders[0];

        }
        #endregion

        #region Properties
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

        #endregion
    }
}
