using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Credigas.Views
{
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();

            Master = new MasterOutPage();
            Detail = new NavigationPage(new SigninPage());
        }
    }
}
