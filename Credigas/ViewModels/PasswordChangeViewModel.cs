namespace Credigas.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Credigas.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class PasswordChangeViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DataService dataService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        string _user;
        string _name;
        string _city;
        string _password;
        string _newpassword;
        string _newpassword2;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsToggled
        {
            get
            {
                return _isToggled;
            }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public string NewPassword
        {
            get
            {
                return _newpassword;
            }
            set
            {
                if (_newpassword != value)
                {
                    _newpassword = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(NewPassword)));
                }
            }
        }

        public string NewPassword2
        {
            get
            {
                return _newpassword2;
            }
            set
            {
                if (_newpassword2 != value)
                {
                    _newpassword2 = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(NewPassword2)));
                }
            }
        }

        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(User)));
                }
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }


        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(City)));
                }
            }
        }
        #endregion

        #region Constructors
        public PasswordChangeViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
            IsToggled = true;
        }
        #endregion

        #region Commands
        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(Cancel);
            }
        }

        async void Cancel()
        {
            await navigationService.BackOnLogin();
        }


        public ICommand ChangePasswordCommand
        {
            get
            {
                return new RelayCommand(ChangePassword);
            }
        }

        async void ChangePassword()
        {
            if (string.IsNullOrEmpty(User))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Debe capturar su usuario.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Debe capturar su contraseña.");
                return;
            }

            if (string.IsNullOrEmpty(NewPassword))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Debe capturar su nueva contraseña.");
                return;
            }

            if (string.IsNullOrEmpty(NewPassword2))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Debe capturar repetir su nueva contraseña.");
                return;
            }

            if( Password == NewPassword){
                await dialogService.ShowMessage(
                    "Error",
                    "La nueva contraseña no puede ser igual a la actual.");
                return;
            }

            if (NewPassword != NewPassword2)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Capture la misma nueva contraseña.");
                return;
            }

            if (string.IsNullOrEmpty(City))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Debe seleccionar una ciudad.");
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (connection == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "No se puede contactar al servidor.");
                return;
            }

            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            var response = await apiService.GetToken(
                urlAPI,
                User,
                Password,
                ToCity());

            if (response == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "No se puede contactar al servidor.");
                Password = null;
                return;
            }

            if (string.IsNullOrEmpty(response.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.ErrorDescription);
                Password = null;
                return;
            }

            /*
            response.IsRemembered = IsToggled;
            response.Password = Password;
            dataService.DeleteAllTokensAndInsert(response);
            */

            PasswordChangeData newData = new PasswordChangeData{
                City = response.City,
                User = User,
                Pwd = Password,
                NewPwd = NewPassword,
                NewPwd2 = NewPassword2
            };

            var userResponse = await apiService.Put2<User>(urlAPI, "users", response.City, response.TokenType, response.AccessToken, newData);

            //Get all data for current user
            //var userResponse = await apiService.Get<Models.User>(urlAPI, "users", response.City, response.TokenType, response.AccessToken, response.Login);

            if (userResponse == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "No se puede contactar al servidor.");
                Password = null;
                NewPassword = null;
                NewPassword2 = null;
                return;
            }

            if (!userResponse.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", userResponse.Message);
                Password = null;
                return;
            }
            /*
            Models.User CurrentUser = (Models.User)userResponse.Result;
            dataService.DeleteAllUsersAndInsert(CurrentUser);

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = response;
            mainViewModel.User = CurrentUser;
            mainViewModel.RegisterDevice();
            mainViewModel.Home = new HomeViewModel();

            mainViewModel.Home.CurrentStatistics = dataService.LoadStatistics();
            navigationService.SetMainPage("MasterView");
            */

            User = null;
            Password = null;

            IsRunning = false;
            IsEnabled = true;
            await navigationService.BackOnLogin();
        }
        #endregion

        #region Methods
        private string ToCity()
        {
            string result = "";

            switch (City)
            {
                case "Mazatlán":
                    result = "MAZATLAN";
                    break;
                case "Culiacán":
                    result = "CULIACAN";
                    break;
                default:
                    result = City.ToUpper();
                    break;
            }

            return result;
        }
        #endregion
    }
}
