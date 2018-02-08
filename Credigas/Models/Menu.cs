namespace Credigas.Models
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using ViewModels;

    public class Menu
    {
        #region Services
        DataService dataService;
        NavigationService navigationService;
        #endregion

        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Constructors
        public Menu()
        {
            dataService = new DataService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        async void Navigate()
        {
            switch (PageName)
            {
                case "LoginView":
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token.IsRemembered = false;
                    dataService.Update(mainViewModel.Token);
                    mainViewModel.Login = new LoginViewModel();
                    navigationService.SetMainPage(PageName);
                    break;
                case "LoadRouteView":
                    MainViewModel.GetInstance().LoadRoute = new LoadRouteViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                case "WorkRouteView":
                    MainViewModel.GetInstance().WorkRoute = new WorkRouteViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                case "SyncRouteView":
                    MainViewModel.GetInstance().SyncRoute = new SyncRouteViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                case "PaymentsView":
                    MainViewModel.GetInstance().Payments = new PaymentsViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
            }
        }
        #endregion
    }
}
