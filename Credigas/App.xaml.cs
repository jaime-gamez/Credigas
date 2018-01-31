namespace Credigas
{
    using Credigas.Services;
    using Credigas.Views;
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

            navigationService.SetMainPage("LoginView");
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
