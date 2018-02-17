namespace Credigas.Models
{
    using System;

    public class Statistics
    {
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

        private double _outstandingBalance;
        public double OutstandingBalance
        {
            get => _outstandingBalance;
            set => _outstandingBalance = value;
        }

        private int _closedCards;
        public int ClosedCards
        {
            get => _closedCards;
            set => _closedCards = value;
        }


    }
}