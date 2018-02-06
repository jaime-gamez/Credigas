namespace Credigas.Models
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using ViewModels;
    using System.Linq;

    public class Customer
    {
        #region Services
        DataService dataService;
        NavigationService navigationService;
        #endregion

        #region Properties
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Icon { get; set; }

        public string City { get; set; }

        public Order Order { get; set; }

        public string OrderId { 
            get{
                if (Order != null)
                    return Order.OrderId;
                else
                    return "SINORDEN";
            } 
        }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public bool Collected { get; set; }

        public string FullAddress { 
            get{
                return Address + "," + City;
            }
        }



        public string Semaphore
        {
            get
            {
                if( Collected )
                    return "semaphore_green";
                else
                    return "semaphore_red";
            }
        }

        public string Phones
        {
            get
            {
                if (Phone2 != null && Phone2.Length > 0)
                    return Phone1?.ToString() + " -- " + Phone2.ToString();
                else
                    return Phone1?.ToString();
            }
        }

        public double TodayPayment
        {
            get
            {
                if (Order != null)
                {
                    var payment = (from p in this.Order.Payments
                                  where p.Date.Year == DateTime.Today.Year
                                  && p.Date.Month == DateTime.Today.Month
                                  && p.Date.Day == DateTime.Today.Day
                                   select p).FirstOrDefault<Payment>();
                    if (payment != null)
                        return payment.Total;
                    else
                        return 0.0;
                }
                else
                    return 0.0;
            }
        }

        #endregion

        #region Constructors
        public Customer()
        {
            dataService = new DataService();
            navigationService = new NavigationService();
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
            MainViewModel.GetInstance().PaymentsClient = new PaymentsClientViewModel();
            MainViewModel.GetInstance().PaymentsClient.CurrentCustomer = this;
            await navigationService.NavigateOnMaster("PaymentsClientView");
        }
        #endregion

    }
}
