using System;
using Credigas.Models;
using Credigas.Views;
using Xamarin.Forms;

namespace Credigas.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel()
        {
            Statistics = new Statistics { 
                Date = DateTime.Today,
                Portfolio = 50000.00F,
                Collected = 15000.00F,
                OutstandingBalance = 35000.00F,
                ClosedCards = 25
            };


        }

        private Statistics _statistics;
        public Statistics Statistics
        {
            get => _statistics;
            set { 
                _statistics = value; 
                OnPropertyChanged();
            }
        }

        Command _loadRouteCommand;
        public Command LoadRouteCommand
        {
            get
            {
                return _loadRouteCommand ?? (_loadRouteCommand = new Command(ExecuteLoadRouteCommand, CanSave));
            }
        }
        void ExecuteLoadRouteCommand()
        {
            // TODO: Implement logic to persist Entry in a later chapter.
            Application.Current.MainPage.DisplayAlert("MainPage", "Load Route Command", "Ok");
        }


        Command _workRouteCommand;
        public Command WorkRouteCommand
        {
            get
            {
                return _workRouteCommand ?? (_workRouteCommand = new Command(ExecuteWorkRouteCommand, CanSave));
            }
        }

        void ExecuteWorkRouteCommand()
        {
            // TODO: Implement logic to persist Entry in a later chapter.
            Application.Current.MainPage.DisplayAlert("MainPage", "Work Route Command", "Ok");
        }

        Command _syncRouteCommand;
        public Command SyncRouteCommand
        {
            get
            {
                return _syncRouteCommand ?? (_syncRouteCommand = new Command(ExecuteSyncRouteCommand, CanSave));
            }
        }

        void ExecuteSyncRouteCommand()
        {
            // TODO: Implement logic to persist Entry in a later chapter.
            Application.Current.MainPage.DisplayAlert("MainPage", "Sync Route Command", "Ok");
        }

        Command _loadPaymentsCommand;
        public Command LoadPaymentsCommand
        {
            get
            {
                return _loadPaymentsCommand ?? (_loadPaymentsCommand = new Command(ExecuteLoadPaymentsCommand, CanSave));
            }
        }

        void ExecuteLoadPaymentsCommand()
        {
            // TODO: Implement logic to persist Entry in a later chapter.
            Application.Current.MainPage.DisplayAlert("MainPage", "Load Payments Command", "Ok");
        }


        bool CanSave() => true;

    }
}
