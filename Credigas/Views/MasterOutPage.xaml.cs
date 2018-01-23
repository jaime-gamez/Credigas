using System;
using System.Collections.Generic;
using Credigas.ViewModels;
using Xamarin.Forms;

namespace Credigas.Views
{
    public partial class MasterOutPage : ContentPage
    {
        public MasterOutPage()
        {
            BindingContext = new MasterOutViewModel();
            InitializeComponent();

            signinButton.SetBinding(Button.CommandProperty, "SigninCommand");
        }
    }
}
