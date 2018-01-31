using System;
using System.Collections.Generic;
using Credigas.Models;
using Credigas.ViewModels;
using Xamarin.Forms;

namespace Credigas.Views
{
    public partial class MasterOutPage : ContentPage
    {
        MasterDetailPage rootPage;
        bool isLoggedIn = false;
        
        public MasterOutPage(MasterDetailPage rootPage)
        {
            BindingContext = new MasterOutViewModel();
            InitializeComponent( );

            this.rootPage = rootPage;

            signoutButton.SetBinding(Button.CommandProperty, "SignoutCommand");
        }

        public void SignedIn(SigninModel model){
            //MasterOutViewModel data = this.BindingContext as MasterOutViewModel;
            this.nameLabel.Text = model.UserName;
            this.loginLabel.Text = model.User;
            this.cityLabel.Text = model.City;
            this.rootPage.IsPresented = false;
            isLoggedIn = true;
            signoutButton.IsEnabled = isLoggedIn;
        }

        public void SignedOut(SigninModel model)
        {
            //MasterOutViewModel data = this.BindingContext as MasterOutViewModel;
            this.nameLabel.Text = model.UserName;;
            this.loginLabel.Text = model.User;
            this.cityLabel.Text = model.City;
            this.rootPage.IsPresented = false;
            isLoggedIn = false;
            signoutButton.IsEnabled = isLoggedIn;
        }
    }
}
