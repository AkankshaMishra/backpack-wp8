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
    public partial class BackpackSignin : PhoneApplicationPage
    {
        string cookie = null;
        Users objUser = new Users();
        private IAPIBusinessLayer objAPIBusinessLayer = APIBusinessLayer.APIBusinessInstance();
        string googleToken = null;
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

            objUser = await objAPIBusinessLayer.RetrieveUserDetailsByUserId(objUser.UserId, cookie);

            //Setting the application level variable--Cookie
            var cookieApp = App.Current as App;
            cookieApp.Cookie = cookie;

            //Setting the application level variable--User object
            var userApp = App.Current as App;
            userApp.User = objUser;

            NavigationService.Navigate(new Uri(Constant.MYHOME, UriKind.Relative));
        }
    }
}