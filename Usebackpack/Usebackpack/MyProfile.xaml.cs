using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Usebackpack.Business_Layer;
using Usebackpack.Common;
using Usebackpack.Model;

namespace Usebackpack
{
    /// <summary>
    /// Class for dislaying the user's profile
    /// </summary>
    public partial class MyProfile : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        Users user = null;
        int courseCount = 0;
        public MyProfile()
        {
            InitializeComponent();
            Loaded += MyProfile_Loaded;
        }

        /// <summary>
        /// This event is fired when the My Profile page will be loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyProfile_Loaded(object sender, RoutedEventArgs e)
        {
            //Retrieve the cookie value
            var cookieApp = App.Current as App;
            cookie = cookieApp.Cookie;
            //Retrieve the user object
            var userApp = App.Current as App;
            user = userApp.User;
            //Retrieve the no. of courses
            var courseCountApp = App.Current as App;
            courseCount = courseCountApp.CourseCount;
            //Binding the controls
            imgProfilePic.Source =  new BitmapImage(new Uri(@Constant.BASEURL + user.AvtarFileName));
            imgProfilePic.Width = 100;
            imgProfilePic.Height = 100;
            tbName.Text = user.FirstName + " " +user.LastName;
            tbRollNo.Text = user.RollNo;
            tbEmailId.Text = user.EmailId;
            //tbBirthday.Text = Convert.ToString(user.Birthday);
            tbLevels.Text = Convert.ToString(user.Level);
            tbPoints.Text = Convert.ToString(user.TotalScore);
            hlbCourses.Content = courseCount;            
        }
    }
}