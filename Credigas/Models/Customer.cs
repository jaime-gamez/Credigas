namespace Credigas.Models
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using ViewModels;
    using System.Linq;
    using SQLite;
    using Newtonsoft.Json;

    public class Customer
    {
        #region Services
        DataService dataService;
        NavigationService navigationService;
        #endregion

        #region Properties
        [PrimaryKey]
        [JsonProperty(PropertyName = "id_cliente")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "ape_paterno")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "ape_materno")]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Name { get; set; }

        [Ignore]
        public string FullName { 
            get{
                return Name?.ToString() + " " + LastName?.ToString() +" " + MiddleName?.ToString();
            } 
        }

        [JsonProperty(PropertyName = "calle")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "no_exterior")]
        public string InteriorNumber { get; set; }

        [JsonProperty(PropertyName = "no_interior")]
        public string ExteriorNumber { get; set; }

        [JsonProperty(PropertyName = "colonia")]
        public string Street2 { get; set; }

        [JsonProperty(PropertyName = "entre_calle1")]
        public string Street3 { get; set; }

        [JsonProperty(PropertyName = "entre_calle2")]
        public string Street4 { get; set; }

        [JsonProperty(PropertyName = "cp")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "observaciones")]
        public string Notes { get; set; }

        [Ignore]
        public string Address { 
            get{
                return Street?.ToString() + " " + InteriorNumber?.ToString() + " " + ExteriorNumber?.ToString()
                              + " " + Street2?.ToString() + " " + Street3?.ToString() + " " + Street4?.ToString()
                              + " " + Zip?.ToString();
            } 
        }

        public string Icon { 
            get
            {
                return "icons8_user_male_circle_filled.png";
            }
        }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        /*
        [Ignore]
        public Order Order { get; set; }

        [Ignore]
        public string OrderId { 
            get{
                if (Order != null)
                    return Order.OrderId;
                else
                    return "SINORDEN";
            } 
        }
        */

        [JsonProperty(PropertyName = "telefono")]
        public string Phone1 { get; set; }

        [JsonProperty(PropertyName = "movil")]
        public string Phone2 { get; set; }

        [Ignore]
        public bool Collected { get; set; }

        [Ignore]
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

        /*
        [Ignore]
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
        */

        #endregion

        #region Constructors
        public Customer()
        {
            dataService = new DataService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        [Ignore]
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
