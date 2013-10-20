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
using Usebackpack.Common;

namespace Usebackpack
{
    /// <summary>
    /// Class for displaying the list of course for which user has enrolled
    /// </summary>
    public partial class MyCourses : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        Users user = null;
        public MyCourses()
        {
            InitializeComponent();
            Loaded += MyCourses_Loaded;
        }

        private void MyCourses_Loaded(object sender, RoutedEventArgs e)
        {
            var cookieApp = App.Current as App;
            cookie = cookieApp.Cookie;
            var userApp = App.Current as App;
            user = userApp.User;
            lstCourses.ItemsSource = user.UserCourses;
        }

        private void btnCourseName_Click(object sender, RoutedEventArgs e)
        {
            Button btnCourse = sender as Button;
            string courseName = btnCourse.Content.ToString();
            var courseId = user.UserCourses.Where(c => c.CourseName == courseName).FirstOrDefault();

            var courseApp = App.Current as App;
            courseApp.CourseId = courseId.CourseId;

            NavigationService.Navigate(new Uri(Constant.COURSEDETAIL, UriKind.Relative));
        }
    }
}