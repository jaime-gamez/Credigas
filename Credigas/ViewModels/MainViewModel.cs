namespace Credigas.ViewModels
{

    using Credigas.Models;
    using Credigas.Services;
    using System;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using Credigas.Interfaces;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class MainViewModel
    {
        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        DataService dataService;
        ApiService apiService;
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;

            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();
            apiService = new ApiService();

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

        public LoadRouteViewModel2 LoadRoute
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

        public PasswordChangeViewModel PasswordChange
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

        public VisitsViewModel Visits
        {
            get;
            set;
        }

        public TokenResponse Token
        {
            get;
            set;

        }

        public User User
        {
            get;
            set;

        }



        #endregion

        #region Commands
        public ICommand VisitsCommand
        {
            get
            {
                return new RelayCommand(OpenVisits);
            }
        }

        async void OpenVisits()
        {
            this.Visits = new VisitsViewModel(this.PaymentsClient.CurrentCustomer);
            await navigationService.NavigateOnMaster("VisitsView");
            /*
            Visit visit = new Visit();
            visit.VisitId = dataService.GetNextIdForVisit();
            visit.DebCollectorId = this.User.CobradorId;
            visit.CustomerId = this.PaymentsClient.CurrentCustomer.CustomerId;
            visit.OrderId = this.PaymentsClient.CurrentCustomer.Order.OrderId;
            visit.Date = DateTime.Now;
            visit.Notes = "Notas de la visita al cliente: " + this.PaymentsClient.CurrentCustomer.FullName;
            visit.Outstanding = this.PaymentsClient.CurrentCustomer.Order.Payments.Select(p => p.Total).Sum();

            visit = dataService.Insert<Visit>(visit);
            SaveVisitToServer(visit);
            var response = await LoadVisits();

            await dialogService.ShowMessage(
                    "Crédigas",
                    "Visitas del cliente "+ this.PaymentsClient.CurrentCustomer.FullName);
            */
        }
        #endregion

        #region Methods
        async Task<bool> LoadVisits()
        {
            
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return false;
            }


            User currentUser = this.User;
            TokenResponse token = this.Token;

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var filters = new {
                id_customer = this.PaymentsClient.CurrentCustomer.CustomerId,
                id_order = this.PaymentsClient.CurrentCustomer.Order.OrderId
            };

            //Get all clients for current user
            var response = await apiService.GetListWithPost<Visit>(urlAPI, "visits", token.City, token.TokenType, token.AccessToken, currentUser.CobradorId, filters);

            if (response == null)
            {
                await dialogService.ShowMessage("Error", "No se puede contactar al servidor.");
                return false;
            }

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return false;
            }

            var visits = (List<Visit>)response.Result;
            //await SaveOrdersOnDB();

            return true;
        }

        async public void SaveVisitToServer(Visit visit)
        {

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            TokenResponse token = MainViewModel.GetInstance().Token;
            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var response = await apiService.Put<Visit>(urlAPI, "visits", token.City, token.TokenType, token.AccessToken, visit);
            if (response.IsSuccess)
            {
                return;

            }
            return;
        }

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
