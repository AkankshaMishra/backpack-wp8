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
    public partial class MyProfile : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        Users user = null;
        public MyProfile()
        {
            InitializeComponent();
            Loaded += MyProfile_Loaded;
        }

        void MyProfile_Loaded(object sender, RoutedEventArgs e)
        {
            var cookieApp = App.Current as App;
            cookie = cookieApp.Cookie;
            var userApp = App.Current as App;
            user = userApp.User;
        }
    }
}