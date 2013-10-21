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
            txtOverview.Text = objCourse.Overview;
            txtTextBooks.Text = objCourse.TextBooks;
            txtOfficeHours.Text = objCourse.OfficeHours;
            txtEvaluation.Text = objCourse.Evaluation;
            txtClassTiming.Text = objCourse.Timings;

            //course info pivot binding
            CourseInfoPivot.Title = objCourse.CourseName + "-" + objCourse.CourseCode;

            //deadlines binding
            listDeadlines = await RetrieveDeadlines();
            List<Deadlines> sortedDeadlines = new List<Deadlines>();
            //sorting the deadlines based on DueOn date
            sortedDeadlines=listDeadlines.OrderByDescending(s => s.DueOn).ToList<Deadlines>();
            lstDeadlines.ItemsSource = sortedDeadlines;

            //Resources bonding
            listResources = await RetrieveResources();
            //List<Model.Resources> sortedResources = new List<Model.Resources>();
            var sortedResources = listResources.GroupBy(s => s.ResourceType).ToList();
            lstResource.ItemsSource = listResources;
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
    }
}