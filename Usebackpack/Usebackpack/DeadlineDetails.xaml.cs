using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Usebackpack.Business_Layer;
using Usebackpack.Common;
using Usebackpack.Model;

namespace Usebackpack
{
    public partial class DeadlineDetails : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        Users user = null;
        public DeadlineDetails()
        {
            InitializeComponent();
            Loaded += DeadlineDetails_Loaded;
        }

        void DeadlineDetails_Loaded(object sender, RoutedEventArgs e)
        {
            var cookieApp = App.Current as App;
            cookie = cookieApp.Cookie;
            var userApp = App.Current as App;
            user = userApp.User;
            if (user.UpcomingDeadlines.Count != 0)
            {
                lstUpcomingDeadlines.ItemsSource = user.UpcomingDeadlines;
            }
            else
            {
                lstUpcomingDeadlines.Visibility = System.Windows.Visibility.Collapsed;
                tbNoDeadlines.Visibility = System.Windows.Visibility.Visible;
                tbNoDeadlines.Text = Constant.NODEADLINES;
            }
        }
    }
}