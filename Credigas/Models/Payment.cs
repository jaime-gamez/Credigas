namespace Credigas.Models
{
    using System;


    public class Payment
    {
        #region Properties
        public long PaymentId { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Constructors
        public Payment()
        {
            PaymentId = 0;
            Total = 0.0;
            Date = DateTime.Now;
        }
        #endregion
    }
}
