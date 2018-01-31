using System;
using Credigas.Models;
using Credigas.Views;
using Xamarin.Forms;

namespace Credigas.ViewModels
{
    public class MasterOutViewModel: BaseViewModel
    {
        public MasterOutViewModel()
        {
            this.User = "Su usuario";
            this.Name = "Jaime Gámez Luna";
            this.City = "Por definir";

        }

    private string _user;
    public string User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
            
        }
    }

    private string _name;
    public string Name
    {
            get => _name;
        set
        {
                _name = value;
            OnPropertyChanged();
            
        }
    }


    private string _city;
    public string City
    {
        get => _city;
        set
        {
            _city = value;
            OnPropertyChanged();
            
        }
    }


    Command _signinCommand;
    public Command SigninCommand
    {
        get
        {
            return _signinCommand ?? (_signinCommand = new Command(ExecuteSigninCommand, () => true));
        }
    }
    void ExecuteSigninCommand()
    {
        // TODO: Implement logic to persist Entry in a later chapter.
        //Application.Current.MainPage.DisplayAlert("Singup", "Singin Command", "Ok");
        MasterDetailPage current = Application.Current.MainPage as MasterDetailPage;
        current.Detail = new NavigationPage(new SigninPage());
            current.MasterBehavior = MasterBehavior.Default;
    }

        Command _signoutCommand;
        public Command SignoutCommand
        {
            get
            {
                return _signoutCommand ?? (_signoutCommand = new Command(ExecuteSignoutCommand, () => true));
            }
        }
        void ExecuteSignoutCommand()
        {
            // TODO: Implement logic to persist Entry in a later chapter.
            //Application.Current.MainPage.DisplayAlert("Singup", "Singin Command", "Ok");
            MasterDetailPage current = Application.Current.MainPage as MasterDetailPage;
            MasterOutPage page = current.Master as MasterOutPage;
            page.SignedOut(
                new SigninModel
                {
                    UserName = "Bienvenido",
                    User = "Inicie sesión por favor",
                    Password = "",
                    City = "Sin Ciudad",
                    AutomaticSession = false
                }
            );

            current.Detail = new NavigationPage(new SigninPage());

            //current.MasterBehavior = MasterBehavior.Default;




        }

    bool CanSave()
    {
        bool result = true;


        return result;
    }
    }
}
