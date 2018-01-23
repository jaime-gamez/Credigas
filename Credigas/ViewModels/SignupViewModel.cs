using System;
using Credigas.Models;
using Credigas.Views;
using Xamarin.Forms;

namespace Credigas.ViewModels
{
    public class SignupViewModel: BaseViewModel
    {
        public SignupViewModel()
        {
            this.User = "";
            this.Password = "";
            this.Password2 = "";
            this.City = "";

        }

        private string _user;
        public string User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
                SignupCommand.ChangeCanExecute();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                SignupCommand.ChangeCanExecute();
            }
        }

        private string _password2;
        public string Password2
        {
            get => _password2;
            set
            {
                _password2 = value;
                OnPropertyChanged();
                SignupCommand.ChangeCanExecute();
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
                SignupCommand.ChangeCanExecute();
            }
        }

        private bool _automaticSession;
        public bool AutomaticSession
        {
            get => _automaticSession;
            set
            {
                _automaticSession = value;
                OnPropertyChanged();
                SignupCommand.ChangeCanExecute();
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
        }

        Command _signupCommand;
        public Command SignupCommand
        {
            get
            {
                return _signupCommand ?? (_signupCommand = new Command(ExecuteSignupCommand, CanSave));
            }
        }
        void ExecuteSignupCommand()
        {
            // TODO: Implement logic to persist Entry in a later chapter.
            Application.Current.MainPage.DisplayAlert("Singup", "Singup Command", "Ok");
        }

        bool CanSave()
        {
            bool result = true;


            if (this.User.Length == 0)
                result = false;

            if (this.Password.Length == 0)
                result = false;

            if (this.Password2.Length == 0)
                result = false;

            if (this.City.Length == 0)
                result = false;


            return result;
        }
    }
}
