using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Credigas.ViewModels;
using Credigas.Models;

namespace Credigas.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainViewModel();
            InitializeComponent();

            this.dateLabel.SetBinding(Label.TextProperty, "Statistics.Date",stringFormat: "{0:dd/MM/yyyy}");
            this.portfolioLabel.SetBinding(Label.TextProperty, "Statistics.Portfolio", stringFormat: "{0:C2}");
            this.collectedLabel.SetBinding(Label.TextProperty, "Statistics.Collected", stringFormat: "{0:C2}");
            this.outstandingBalanceLabel.SetBinding(Label.TextProperty, "Statistics.OutstandingBalance", stringFormat: "{0:C2}");
            this.closedCardsLabel.SetBinding(Label.TextProperty, "Statistics.ClosedCards");

            loadRouteButton.SetBinding(Button.CommandProperty,"LoadRouteCommand");
            workRouteButton.SetBinding(Button.CommandProperty, "WorkRouteCommand");
            syncRouteButton.SetBinding(Button.CommandProperty, "SyncRouteCommand");
            loadPaymentsButton.SetBinding(Button.CommandProperty, "LoadPaymentsCommand");

            MasterDetailPage current = Application.Current.MainPage as MasterDetailPage;
            MasterOutPage page = current.Master as MasterOutPage;
            page.SignedIn();


        }


    }
}
