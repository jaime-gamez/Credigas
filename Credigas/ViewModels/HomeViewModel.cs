namespace Credigas.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Credigas.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class HomeViewModel : INotifyPropertyChanged
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
        public HomeViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

        }
        #endregion

        #region Properties
        private Statistics _statistics;
        public Statistics CurrentStatistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                PropertyChanged?.Invoke(
                        this,
                    new PropertyChangedEventArgs(nameof(CurrentStatistics)));
            }
        }
        #endregion

        #region Commands
        public ICommand LoadRouteCommand
        {
            get
            {
                return new RelayCommand(LoadRoute);
            }
        }

        async void LoadRoute()
        {
            MainViewModel.GetInstance().LoadRoute = new LoadRouteViewModel();
            await navigationService.NavigateOnMaster("LoadRouteView");
        }

        public ICommand WorkRouteCommand
        {
            get
            {
                return new RelayCommand(WorkRoute);
            }
        }

        async void WorkRoute()
        {
            MainViewModel.GetInstance().WorkRoute = new WorkRouteViewModel();
            await navigationService.NavigateOnMaster("WorkRouteView");
        }

        public ICommand SyncRouteCommand
        {
            get
            {
                return new RelayCommand(SyncRoute);
            }
        }

        async void SyncRoute()
        {
            MainViewModel.GetInstance().SyncRoute = new SyncRouteViewModel();
            await navigationService.NavigateOnMaster("SyncRouteView");
        }

        public ICommand PaymentsCommand
        {
            get
            {
                return new RelayCommand(PaymentsRoute);
            }
        }

        async void PaymentsRoute()
        {
            MainViewModel.GetInstance().Payments = new PaymentsViewModel();
            await navigationService.NavigateOnMaster("PaymentsView");
        }
        #endregion
    }
}
