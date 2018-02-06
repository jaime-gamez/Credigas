namespace Credigas.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Credigas.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class SyncRouteViewModel: INotifyPropertyChanged
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
        public SyncRouteViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
            IsRunning = false;
            IsLoaded = false;
            Date = System.DateTime.Today;
        }
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        bool _isLoaded;
        DateTime _date;
        #endregion

        #region Properties
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Date)));
                }
            }
        }

        public bool IsLoaded
        {
            get
            {
                return _isLoaded;

            }
            set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value; ;
                    _isLoaded = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsLoaded)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand SyncRouteCommand
        {
            get
            {
                return new RelayCommand(SyncRoute);
            }
        }

        async void SyncRoute()
        {
            IsRunning = false;
            IsLoaded = true;
            await dialogService.ShowMessage(
                    "Crédigas",
                    "Valide no queden pendientes.");
            await navigationService.BackOnMaster();
        }
        #endregion

    }
}
