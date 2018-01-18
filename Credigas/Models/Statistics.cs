using System;
namespace Credigas.Models
{
    public class Statistics
    {
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        private float _portfolio;
        public float Portfolio
        {
            get => _portfolio;
            set => _portfolio = value;
        }

        private float _collected;
        public float Collected
        {
            get => _collected;
            set => _collected = value;
        }

        private float _outstandingBalance;
        public float OutstandingBalance
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