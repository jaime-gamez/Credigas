namespace Credigas.Services
{
    using System.Threading.Tasks;
    using Credigas.ViewModels;
    using Views;
    using Xamarin.Forms;

    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginView":
                    Application.Current.MainPage = new NavigationPage(new LoginView());
                    break;
                case "MasterView":
                    Application.Current.MainPage = new MasterView();
                    break;
            }
        }

        public async Task NavigateOnMaster(string pageName)
        {
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "HomeView":
                    await App.Navigator.PushAsync(
                        new HomeView());
                    break;
                case "LoadRouteView":
                    await App.Navigator.PushAsync(
                        new LoadRouteView());
                    break;
                case "WorkRouteView":
                    await App.Navigator.PushAsync(
                        new WorkRouteView());
                    break;
                case "SyncRouteView":
                    await App.Navigator.PushAsync(
                        new SyncRouteView());
                    break;
                case "PaymentsView":
                    await App.Navigator.PushAsync(
                        new PaymentsView());
                    break;
            }
        }

        public async Task NavigateOnLogin(string pageName)
        {
            switch (pageName)
            {
                case "NewUserView":
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new NewUserView());
                    break;
            }
        }

        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }

        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
