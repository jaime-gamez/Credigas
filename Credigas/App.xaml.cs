namespace Credigas
{
    using Credigas.Models;
    using Credigas.Services;
    using Credigas.ViewModels;
    using Credigas.Views;
    using System;
    using Xamarin.Forms;

    public partial class App : Application
    {
        #region Services
        ApiService apiService;
        DataService dataService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion



        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }

        public static MasterView Master
        {
            get;
            internal set;
        }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            TokenResponse token = dataService.First<TokenResponse>(false);

            if( token != null && token.IsRemembered && token.Expires > DateTime.Now ) 
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = token;
                mainViewModel.RegisterDevice();
                mainViewModel.Home = new HomeViewModel();

                mainViewModel.Home.CurrentStatistics = new Statistics
                {
                    Date = DateTime.Today,
                    Portfolio = 50000.00F,
                    Collected = 15000.00F,
                    OutstandingBalance = 35000.00F,
                    ClosedCards = 25
                };

                navigationService.SetMainPage("MasterView");
            }
            else
            {
                navigationService.SetMainPage("LoginView");
            }
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
