using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Usebackpack.Business_Layer;
using Usebackpack.Model;

namespace Usebackpack
{
    public partial class PostDiscussion : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        int userId = 0;
        int courseId = 0;
        public PostDiscussion()
        {
            InitializeComponent();
            Loaded += PostDiscussion_Loaded;
        }

        private void PostDiscussion_Loaded(object sender, RoutedEventArgs e)
        {
            var app = App.Current as App;
            cookie = app.Cookie;
            //Retrieve the user object
            userId = app.User.UserId;
            courseId = app.CourseId;
        }

        private async void btnPost_Click(object sender, RoutedEventArgs e)
        {
            int ret = await PostDiscussions();
        }

        private async Task<int> PostDiscussions()
        {
          return await objAPIServiceLayer.PostDiscussion(courseId.ToString(), userId.ToString(), txtDetails.Text, txtSubject.Text, cookie);
        }
    }
}