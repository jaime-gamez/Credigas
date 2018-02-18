namespace Credigas.Models
{
    using System;

    public class Statistics
    {
        public Statistics(){
            _totalCustomers = 0;
            _customersWithPayment = 0;
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        private double _portfolio;
        public double Portfolio
        {
            get => _portfolio;
            set => _portfolio = value;
        }

        public string PortfolioString
        {
            get
            {
                return String.Format("{0:C2}({1})",Portfolio,TotalCustomers);
            }
        }

        private double _collected;
        public double Collected
        {
            get => _collected;
            set => _collected = value;
        }

        private double _collectedToday;
        public double CollectedToday
        {
            get => _collectedToday;
            set => _collectedToday = value;
        }

        public string CollectedTodayString
        {
            get
            {
                return String.Format("{0:C2}({1})", CollectedToday, CustomersWithPayment);
            }
        }

        private double _outstandingBalance;
        public double OutstandingBalance
        {
            get => _outstandingBalance;
            set => _outstandingBalance = value;
        }

        public string OutstandingBalanceString
        {
            get
            {
                return String.Format("{0:C2}({1})", OutstandingBalance, CustomersWithoutPayment);
            }
        }

        private int _closedCards;
        public int ClosedCards
        {
            get => _closedCards;
            set => _closedCards = value;
        }

        private int _customersWithPayment;
        public int CustomersWithPayment
        {
            get => _customersWithPayment;
            set => _customersWithPayment = value;
        }


        public int CustomersWithoutPayment
        {
            get {
                return _totalCustomers - _customersWithPayment;
            }
        }

        private int _totalCustomers;
        public int TotalCustomers
        {
            get => _totalCustomers;
            set => _totalCustomers = value;
        }


    }
}