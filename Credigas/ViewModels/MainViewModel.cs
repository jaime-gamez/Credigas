namespace Credigas.ViewModels
{

    using Credigas.Models;
    using Credigas.Services;
    using System;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using Credigas.Interfaces;

    public class MainViewModel
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;

            navigationService = new NavigationService();



            Login = new LoginViewModel();
            LoadMenu();
        }
        #endregion

        #region Properties
        public ObservableCollection<Models.Menu> MyMenu
        {
            get;
            set;
        }

        public LoginViewModel Login
        {
            get;
            set;
        }

        public HomeViewModel Home
        {
            get;
            set;
        }

        public LoadRouteViewModel LoadRoute
        {
            get;
            set;
        }

        public WorkRouteViewModel WorkRoute
        {
            get;
            set;
        }

        public SyncRouteViewModel SyncRoute
        {
            get;
            set;
        }

        public PaymentsViewModel Payments
        {
            get;
            set;
        }

        public PasswordRecoveryViewModel PasswordRecovery
        {
            get;
            set;
        }

        public NewUserViewModel NewUser
        {
            get;
            set;
        }

        public PaymentsClientViewModel PaymentsClient
        {
            get;
            set;
        }

        public TokenResponse Token
        {
            get;
            set;

        }



        #endregion

        #region Methods
        public void RegisterDevice()
        {
            var register = DependencyService.Get<IRegisterDevice>();
            register.RegisterDevice();
        }

        void LoadMenu()
        {
            MyMenu = new ObservableCollection<Models.Menu>();

            MyMenu.Add(new Models.Menu
            {
                Icon = "ic_settings",
                PageName = "LoadRouteView",
                Title = "Cargar Ruta",
            });

            MyMenu.Add(new Models.Menu
            {
                Icon = "ic_map",
                PageName = "WorkRouteView",
                Title = "Trabajar Ruta",
            });

            MyMenu.Add(new Models.Menu
            {
                Icon = "ic_sync",
                PageName = "SyncRouteView",
                Title = "Sincronizar Ruta",
            });

            MyMenu.Add(new Models.Menu
            {
                Icon = "ic_sync",
                PageName = "PaymentsView",
                Title = "Consultar Abonos",
            });

            MyMenu.Add(new Models.Menu
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginView",
                Title = "Cerrar sesion",
            });
        }
        #endregion

        #region Sigleton
        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

    }
}
