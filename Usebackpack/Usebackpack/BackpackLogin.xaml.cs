﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Usebackpack.Business_Layer;
using Usebackpack.Common;
using Usebackpack.Model;


namespace Usebackpack
{
    /// <summary>
    /// Class for Login which will handle google authentication, and on success redirect to home page
    /// Author: Kumar Abhinav,Pooja Agarwal,Prabhat Mishra,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    public partial class BackpackLogin : PhoneApplicationPage
    {
        string cookie = null;
        private IAPIBusinessLayer objAPIBusinessLayer = APIBusinessLayer.APIBusinessInstance();
        private string googleToken = null;
        Users objUser = new Users();
        List<Usebackpack.Model.Resources> lstResources = new List<Model.Resources>();
        public BackpackLogin()
        {
            InitializeComponent();
            this.OAuthBrowser.Navigating += OAuthBrowser_Navigating;
            this.OAuthBrowser.Navigate(
               new Uri(
                   string.Format(Constant.GOOGLELOGINURL, Constant.CLIENTID)));

        }

        public async void OAuthBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Uri.Query))
            {
                return;
            }
            string query = HttpUtility.UrlDecode(e.Uri.Query);

            var dic = query.TrimStart('?').Split('&').Select(p => new KeyValuePair<string, string>(p.Split('=').FirstOrDefault(), p.Split('=').LastOrDefault()));

            if (dic.Any(q => q.Key == "code"))
            {

                // get the code from the query string
                var code = dic.FirstOrDefault(k => k.Key == "code");
                var token=await objAPIBusinessLayer.GoogleAuthentication(code.Value);
                googleToken = token.AccessToken;

                var app = App.Current as App;
                app.GoogleToken = googleToken;

                if (googleToken != null)
                {
                    //Navigate to Home page
                    NavigationService.Navigate(new Uri(Constant.SIGNIN, UriKind.Relative));

                }
            }
        }
    }
}