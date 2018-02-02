namespace Credigas.Models
{
    using System.Collections.ObjectModel;

    public class Order
    {
        #region Properties
        public string OrderId { get; set; }
        public double Total { get; set; }
        public double Collected { 
            get{
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
        public ObservableCollection<Payment> Payments{ get; set; }
        #endregion

        #region Constructors
        public Order()
        {
            Payments = new ObservableCollection<Payment>();
        }
        #endregion
    }
}
