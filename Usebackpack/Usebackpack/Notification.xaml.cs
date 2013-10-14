using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Usebackpack.Model;

namespace Usebackpack
{
    public partial class Notification : PhoneApplicationPage
    {
        public Notification()
        {
            InitializeComponent();
            lstNotifications.ItemsSource = GetNotifications();
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