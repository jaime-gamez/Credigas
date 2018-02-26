namespace Credigas.Services
{
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "Acceptar");
        }

        public async Task<bool> ShowConfirm(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "Si",
                "No");
        }

        public async Task<string> ShowImageOptions()
        {
            return await Application.Current.MainPage.DisplayActionSheet(
                "Where do you take the image?",
                "Cancelar",
                null,
                "From Gallery",
                "From Camera");
        }

        public async Task<string> ShowOptions(string title, string[] buttons )
            {
            return await Application.Current.MainPage.DisplayActionSheet(
                title,
                "Cancelar",
                null,
                buttons
            );
        }
    }
}
