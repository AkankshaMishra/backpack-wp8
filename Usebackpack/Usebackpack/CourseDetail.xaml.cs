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
using Resource = Usebackpack.Model;
using Usebackpack.Business_Layer;
using System.Threading.Tasks;
using Usebackpack.Common;
using System.Windows.Data;
using System.Globalization;


namespace Usebackpack
{
    public partial class CourseDetail : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIBusinessLayer = APIBusinessLayer.APIBusinessInstance();
        string cookie = null;
        Users user = null;
        int courseId=0;
        Course objCourse = new Course();
        List<Deadlines> listDeadlines = new List<Deadlines>();
        List<Model.Resources> listResources = new List<Model.Resources>();
        public CourseDetail()
        {
            InitializeComponent();
            Loaded += CourseDetail_Loaded;
        }

        /// <summary>
        /// This event is fired when the Course detail page will be loaded. This method fetch the course info,deadlines,resources and discussion details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CourseDetail_Loaded(object sender, RoutedEventArgs e)
        {
            var cookieApp = App.Current as App;
            cookie = cookieApp.Cookie;
            var userApp = App.Current as App;
            user = userApp.User;

            var courseApp=App.Current as App;
            courseId = courseApp.CourseId;

            //course info binding
            await RetrieveCourseDetails();
            if (objCourse.Overview==null)
            {
                tbOverview.Visibility = System.Windows.Visibility.Collapsed;
                txtOverview.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                txtOverview.Text = objCourse.Overview;
            }

            if (objCourse.TextBooks==null)
            {
                tbTextBooks.Visibility = System.Windows.Visibility.Collapsed;
                txtTextBooks.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                txtTextBooks.Text = objCourse.TextBooks;
            }

            if (objCourse.OfficeHours==null)
            {
                tbOfficeHours.Visibility = System.Windows.Visibility.Collapsed;
                txtOfficeHours.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
            txtOfficeHours.Text = objCourse.OfficeHours;
            }

            if (objCourse.Evaluation==null)
            {
                tbEvaluation.Visibility = System.Windows.Visibility.Collapsed;
                txtEvaluation.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                txtEvaluation.Text = objCourse.Evaluation;
            }

            if (objCourse.Timings==null)
            {
                tbClassTimings.Visibility = System.Windows.Visibility.Collapsed;
                txtClassTiming.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                txtClassTiming.Text = objCourse.Timings;
            }
            

            //course info pivot binding
            CourseInfoPivot.Title = objCourse.CourseName + "-" + objCourse.CourseCode;

            //deadlines binding
            listDeadlines = await RetrieveDeadlines();
            if (listDeadlines.Count != 0)
            {
                List<Deadlines> sortedDeadlines = new List<Deadlines>();
                //sorting the deadlines based on DueOn date
                sortedDeadlines = listDeadlines.OrderByDescending(s => s.DueOn).ToList<Deadlines>();
                lstDeadlines.ItemsSource = sortedDeadlines;
            }
            else
            {
                tbNoDeadLines.Visibility = System.Windows.Visibility.Visible;
                tbNoDeadLines.Text = Constant.NOCOURSEDEADLINES;
                lstDeadlines.Visibility = System.Windows.Visibility.Collapsed;
            }

            //Resources bonding
            listResources = await RetrieveResources();
            if (listResources.Count != 0)
            {
                //List<Model.Resources> sortedResources = new List<Model.Resources>();
                var sortedResources = listResources.GroupBy(s => s.ResourceType).ToList();
                lstResource.ItemsSource = listResources;
            }
            else
            {
                tbNoResources.Visibility = System.Windows.Visibility.Visible;
                tbNoResources.Text = Constant.NORESOURCES;
                lstResource.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Method to retrieve course details
        /// </summary>
        /// <returns></returns>
        private async Task<Course> RetrieveCourseDetails()
        {
            objCourse = await objAPIBusinessLayer.RetrieveCoursesByCourseId(cookie, courseId);
            return objCourse;
        }

        /// <summary>
        /// Method to retrieve deadlines for a course
        /// </summary>
        /// <returns></returns>
        private async Task<List<Deadlines>> RetrieveDeadlines()
        {
            return await objAPIBusinessLayer.RetrieveDeadlinesByCourseId(cookie, courseId);
        }

        /// <summary>
        /// Metod to retrieve resources
        /// </summary>
        /// <returns></returns>
        private async Task<List<Model.Resources>> RetrieveResources()
        {
            return await objAPIBusinessLayer.RetrieveResourcesByCourseId(cookie, courseId);
        }

        private void hlDeadlineTitle_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(Constant.DEADLINEDETAILS, UriKind.Relative));
        }
    }

    //Delete this if its nt wrking
    public class StringLengthVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}