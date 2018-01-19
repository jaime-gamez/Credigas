using System;
using System.Collections.Generic;
using Credigas.ViewModels;
using Xamarin.Forms;

namespace Credigas.Views
{
    public partial class SigninPage : ContentPage
    {
        public SigninPage()
        {
            BindingContext = new SigninViewModel();
            InitializeComponent();


            this.userEntry.SetBinding(Entry.TextProperty, "User", BindingMode.TwoWay);
            this.passwordEntry.SetBinding(Entry.TextProperty, "Password", BindingMode.TwoWay);
            this.cityPicker.SetBinding(Picker.SelectedItemProperty, "City");
            this.sessionSwitch.SetBinding(Switch.IsToggledProperty, "AutomaticSession", BindingMode.TwoWay);

            signinButton.SetBinding(Button.CommandProperty, "SigninCommand");
            signupButton.SetBinding(Button.CommandProperty, "SignupCommand");
        }
    }
}
