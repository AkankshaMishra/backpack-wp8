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
using Usebackpack.Model;

namespace Usebackpack
{
    public partial class Notification : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        Users user = null;
        public Notification()
        {
            InitializeComponent();
            Loaded += Notification_Loaded;
            lstNotifications.ItemsSource = GetNotifications();
        }

        void Notification_Loaded(object sender, RoutedEventArgs e)
        {
            var cookieApp = App.Current as App;
            cookie = cookieApp.Cookie;
            var userApp = App.Current as App;
            user = userApp.User;
        }

        private static List<Notifications> GetNotifications()
        {
            List<Notifications> lstNotifications = new List<Notifications>();
            lstNotifications.Add(new Notifications() { NotificationId = 1 , NotificationText = "Srikanta Bedathur started a new discussion in Advanced Programming", Link = "Codechef registrations" });
            lstNotifications.Add(new Notifications() { NotificationId = 2 , NotificationText = "Srikanta Bedathur has posted a new resource in Advanced Programming", Link = "Apache commons collections" });
            lstNotifications.Add(new Notifications() { NotificationId = 3, NotificationText = "Srikanta Bedathur has posted a new resource in Advanced Programming", Link = "Google Guava libraries" });
            return lstNotifications;
        }

    }
}