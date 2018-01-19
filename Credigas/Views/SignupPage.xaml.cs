using System;
using System.Collections.Generic;
using Credigas.ViewModels;
using Xamarin.Forms;

namespace Credigas.Views
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            BindingContext = new SignupViewModel();
            InitializeComponent();



            this.userEntry.SetBinding(Entry.TextProperty, "User", BindingMode.TwoWay);
            this.passwordEntry.SetBinding(Entry.TextProperty, "Password", BindingMode.TwoWay);
            this.password2Entry.SetBinding(Entry.TextProperty, "Password2", BindingMode.TwoWay);
            this.cityPicker.SetBinding(Picker.SelectedItemProperty, "City");
           

            signinButton.SetBinding(Button.CommandProperty, "SigninCommand");
            signupButton.SetBinding(Button.CommandProperty, "SignupCommand");
        }
    }
}
