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

            signoutButton.SetBinding(Button.CommandProperty, "SignoutCommand");
        }

        public void SignedIn(){
            //MasterOutViewModel data = this.BindingContext as MasterOutViewModel;
            this.welcomeLabel.Text = "Jaime Gámez";
            this.nameLabel.Text = "JAGALU";
            this.loginLabel.Text = "Mazatlán";
        }

        public void SignedOut()
        {
            //MasterOutViewModel data = this.BindingContext as MasterOutViewModel;
            this.welcomeLabel.Text = "Bienvenido";
            this.nameLabel.Text = "Inicie sesión por favor";
            this.loginLabel.Text = "Su usuario";
        }
    }
}
