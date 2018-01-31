namespace Credigas.ViewModels
{
    using Credigas.Models;

    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
        }

        private Statistics _statistics;
        public Statistics CurrentStatistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                OnPropertyChanged();
            }
        }
    }
}
