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
    public partial class Settings : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        public Settings()
        {
            InitializeComponent();
            Loaded += Settings_Loaded;
            llsEmailNotification.ItemsSource = getEmailNotification();
        }

        void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            var cookieApp = App.Current as App;
            cookie = cookieApp.Cookie;
        }

        private static List<EmailNotification> getEmailNotification()
        {
            List<EmailNotification> lstEmailNotification = new List<EmailNotification>();
            lstEmailNotification.Add(new EmailNotification() { EmailNotificationId = 1, EmailNotificationText = "Instructor or TA posts a deadline or discussion" });
            lstEmailNotification.Add(new EmailNotification() { EmailNotificationId = 2, EmailNotificationText = "Someone starts a discussion" });
            lstEmailNotification.Add(new EmailNotification() { EmailNotificationId = 3, EmailNotificationText = "Instructor or TA posts a new resource" });
            lstEmailNotification.Add(new EmailNotification() { EmailNotificationId = 4, EmailNotificationText = "Someone replies to my discussions" });
            lstEmailNotification.Add(new EmailNotification() { EmailNotificationId = 5, EmailNotificationText = "I get a new badge or level up on Backpack" });
            return lstEmailNotification;
        }

    }
}