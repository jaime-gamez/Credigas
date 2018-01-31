using System;
using Credigas.Models;
using Credigas.Views;
using Xamarin.Forms;

namespace Credigas.ViewModels
{
    public class SigninViewModel: BaseViewModel
    {
        public SigninViewModel()
        {
            /*
            Entry = new SigninModel
            {
                User = "",
                Password = "",
                City = CitiesEnum.None,
                AutomaticSession = false
            };
            */

            this.User = "";
            this.Password = "";
            this.City = "";
            this.AutomaticSession = false;
        }

        private SigninModel _entry;
        public SigninModel Entry
        {
            get => _entry;
            set
            {
                _entry = value;
                OnPropertyChanged();
                SigninCommand.ChangeCanExecute();
            }
        }

        private string _user;
        public string User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
                SigninCommand.ChangeCanExecute();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set {
                _password = value;
                OnPropertyChanged();
                SigninCommand.ChangeCanExecute();
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set {
                _city = value;
                OnPropertyChanged();
                SigninCommand.ChangeCanExecute();
            }
        }

        private bool _automaticSession;
        public bool AutomaticSession
        {
            get => _automaticSession;
            set {
                _automaticSession = value;
                OnPropertyChanged();
                SigninCommand.ChangeCanExecute();
            }
        }


        Command _signinCommand;
        public Command SigninCommand
        {
            get
            {
                return _signinCommand ?? (_signinCommand = new Command(ExecuteSigninCommand, CanSave));
            }
        }
        void ExecuteSigninCommand()
        {
            // TODO: Implement logic to persist Entry in a later chapter.
            //Application.Current.MainPage.DisplayAlert("Singin", "Singin Command", "Ok");
            MasterDetailPage current = Application.Current.MainPage as MasterDetailPage;
            //current.Master = new SigninMasterPage();
            current.Detail = new NavigationPage(new MainPage(
                new SigninModel
                {
                    UserName = "Jaime Gámez",
                    User = this.User,
                    Password = this.Password,
                    City = this.City,
                    AutomaticSession = this.AutomaticSession
                }
            ));
        }

        Command _signupCommand;
        public Command SignupCommand
        {
            get
            {
                return _signupCommand ?? (_signupCommand = new Command(ExecuteSignupCommand, () => true));
            }
        }
        void ExecuteSignupCommand()
        {
            // TODO: Implement logic to persist Entry in a later chapter.
            //Application.Current.MainPage.DisplayAlert("Singin", "Singup Command", "Ok");
            MasterDetailPage current = Application.Current.MainPage as MasterDetailPage;
            current.Detail = new NavigationPage(new SignupPage());
        }

        bool CanSave() {
            bool result = true;


            if( this.User.Length == 0)
                result = false;

            if (this.Password.Length == 0)
                result = false;

            if (this.City.Length == 0)
                result = false;


            return result;
        }
    }
}
