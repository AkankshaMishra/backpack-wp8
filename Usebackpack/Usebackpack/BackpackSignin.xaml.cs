using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.Phone.Shell;
using Usebackpack.Business_Layer;
using Usebackpack.Common;
using Usebackpack.Model;

namespace Usebackpack
{
    public partial class BackpackSignin : PhoneApplicationPage
    {
        string cookie = null;
        Users objUser = new Users();
        private IAPIBusinessLayer objAPIBusinessLayer = APIBusinessLayer.APIBusinessInstance();
        string googleToken = null;
        HttpNotificationChannel httpChannel = null;
        public BackpackSignin()
        {
            InitializeComponent();
        }

        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var tokenApp = App.Current as App;
            googleToken = tokenApp.GoogleToken;

            cookie = await objAPIBusinessLayer.BackpackSignIn(googleToken);
            objUser.UserId = Convert.ToInt32(await objAPIBusinessLayer.RetrieveUserId(cookie));
            int userId = objUser.UserId;
            objUser = await objAPIBusinessLayer.RetrieveUserDetailsByUserId(objUser.UserId, cookie);
            objUser.UserId = userId;
            //Setting the application level variable--Cookie
            var cookieApp = App.Current as App;
            cookieApp.Cookie = cookie;

            //Setting the application level variable--User object
            var userApp = App.Current as App;
            userApp.User = objUser;

            //For Push Notification
            if (!TryFindChannel())
                DoConnect();

            NavigationService.Navigate(new Uri(Constant.MYHOME, UriKind.Relative));
        }



        #region CodeForPUSHNOTIFICATIO
        private bool TryFindChannel()
        {
            bool bRes = false;
            Trace("Getting IsolatedStorage for current Application ");

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Trace("Checking channel data");
                if (isf.FileExists(Constant.FILENAME))
                {
                    Trace("Channel data exists! Loading...");
                    using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(Constant.FILENAME, FileMode.Open, isf))
                    {
                        using (StreamReader sr = new StreamReader(isfs))
                        {
                            string uri = sr.ReadLine();
                            Trace("Finding channel");
                            httpChannel = HttpNotificationChannel.Find(Constant.CHANNELNAME);

                            if (null != httpChannel)
                            {
                                if (httpChannel.ChannelUri.ToString() == uri)
                                {
                                    Trace("Channel retrieved");
                                    SubscribeToChannelEvents();
                                    SubscribeToService();
                                    bRes = true;
                                }

                                sr.Close();
                            }
                        }
                    }
                }
                else
                    Trace("Channel data not found");
            }

            return bRes;
        }

        private void Trace(string message)
        {
            Debug.WriteLine(message);
        }



        private void DoConnect()
        {
            try
            {

                //First, try to pick up existing channel
                httpChannel = HttpNotificationChannel.Find(Constant.CHANNELNAME);

                if (httpChannel == null)
                {
                    httpChannel = new HttpNotificationChannel(Constant.CHANNELNAME);
                    SubscribeToChannelEvents();
                    httpChannel.Open();
                    (App.Current as App).MPNSUrl = httpChannel.ChannelUri.ToString();
                }
                else
                {
                    (App.Current as App).MPNSUrl = httpChannel.ChannelUri.ToString();
                    SubscribeToChannelEvents();

                    SubscribeToService();
                    SubscribeToNotifications();

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void SubscribeToService()
        {
            objAPIBusinessLayer.PushNotification(cookie);
        }

        private void SubscribeToChannelEvents()
        {
            //Register to UriUpdated event - occurs when channel successfully opens   
            httpChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(httpChannel_ChannelUriUpdated);

            //Subscribed to Raw Notification
            httpChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(httpChannel_HttpNotificationReceived);

            //Subscribed to Toast Notification
            httpChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(httpChannel_ShellToastNotificationReceived);

            //general error handling for push channel
            httpChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(httpChannel_ExceptionOccurred);
        }


        private void SubscribeToNotifications()
        {
            try
            {
                //Registering to Toast Notifications
                if (httpChannel.IsShellToastBound == false)
                    httpChannel.BindToShellToast();
            }
            catch (Exception ex)
            {
                //Nothing to do - channel already exists and subscribed
            }

            try
            {
                if (httpChannel.IsShellTileBound == false)
                    httpChannel.BindToShellTile();
            }
            catch (Exception ex)
            {
                //Nothing to do - channel already exists and subscribed
            }
        }


        private void httpChannel_ExceptionOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

     

        void httpChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            try
            {
                foreach (var key in e.Collection.Keys)
                {
                    string msg = e.Collection[key];
                    (App.Current as App).Message = msg;
                    System.Diagnostics.Debugger.Break();
                    Trace(msg);
                    //Dispatcher.BeginInvoke(() => UpdateStatus("Tile message: " + msg));
                }
                NavigationService.Navigate(new Uri("/Notification.xaml", UriKind.Relative));
            }
            catch (Exception)
            {
                throw;
            }
        }

        void httpChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            Trace("Raw Notification arrived");
        }


        void httpChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            Trace("Channel opened. Got Uri:\n" + httpChannel.ChannelUri.ToString());

            Trace("Subscribing to channel events");
            SubscribeToService();
            SubscribeToNotifications();
        }
        #endregion
    }
}