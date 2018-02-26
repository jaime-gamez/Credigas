namespace Credigas.Models
{
    using System;

    public class Statistics
    {
        public Statistics(){
            _totalOrders = 0;
            _ordersWithPayment = 0;
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
                return String.Format("{0:C2}({1})",Portfolio,TotalOrders);
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
                return String.Format("{0:C2}({1})", CollectedToday, OrdersWithPayment);
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
                return String.Format("{0:C2}({1})", OutstandingBalance, OrdersWithoutPayment);
            }
        }

        private int _closedCards;
        public int ClosedCards
        {
            get => _closedCards;
            set => _closedCards = value;
        }

        private int _ordersWithPayment;
        public int OrdersWithPayment
        {
            get => _ordersWithPayment;
            set => _ordersWithPayment = value;
        }


        public int OrdersWithoutPayment
        {
            get {
                return TotalOrders - OrdersWithPayment;
            }
        }

        private int _totalOrders;
        public int TotalOrders
        {
            get => _totalOrders;
            set => _totalOrders = value;
        }


    }
}