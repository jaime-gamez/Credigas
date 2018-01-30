using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Credigas.Views
{
    public partial class SigninMasterPage : ContentPage
    {
        public SigninMasterPage()
        {
            BindingContext = new SigninMasterPage();
            InitializeComponent();

            signinButton.SetBinding(Button.CommandProperty, "SigninCommand");
        }
    }
}
