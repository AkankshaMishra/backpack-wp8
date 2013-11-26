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
using Usebackpack.Resources;
using Usebackpack.Model;
using Usebackpack.Common;
using Microsoft.Phone.Tasks;

namespace Usebackpack
{
    public partial class MainPage : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        Users user = null;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
          
            Loaded += MainPage_Loaded;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        /// <summary>
        /// Retrieving the stored data on page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var cookieApp = App.Current as App;
            cookie = cookieApp.Cookie;
            var userApp = App.Current as App;
            user = userApp.User;
        }

        /// <summary>
        /// Navigate to My Course page on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyCourse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(Constant.MYCOURSES, UriKind.Relative));
        }

        /// <summary>
        /// Navigate to My Profile page on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(Constant.MYPROFILE, UriKind.Relative));
        }

        /// <summary>
        /// Navigate to Notification page on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(Constant.NOTIFICATION, UriKind.Relative));
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(Constant.ABOUT, UriKind.Relative));
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(Constant.FEEDBACK, UriKind.Relative));
        }

        private void UpcomingDeadlines_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(Constant.DEADLINEDETAILS, UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}