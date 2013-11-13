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
        HTMLParser objParser = new HTMLParser();
        List<Discussions> lstDiscussion = new List<Discussions>();
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

                string htmlOverview = WebUtility.HtmlDecode(objCourse.Overview);
                string overview = objParser.ParseHTMLContent(htmlOverview);
                txtOverview.Text = overview;
            }

            if (objCourse.TextBooks==null)
            {
                tbTextBooks.Visibility = System.Windows.Visibility.Collapsed;
                txtTextBooks.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                string htmlTextbook = WebUtility.HtmlDecode(objCourse.TextBooks);
                string textbooks = objParser.ParseHTMLContent(htmlTextbook);
                txtTextBooks.Text = textbooks;
            }

            if (objCourse.OfficeHours==null)
            {
                tbOfficeHours.Visibility = System.Windows.Visibility.Collapsed;
                txtOfficeHours.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                string htmlOfficeHours = WebUtility.HtmlDecode(objCourse.OfficeHours);
                string officeHours = objParser.ParseHTMLContent(htmlOfficeHours);
                txtOfficeHours.Text = officeHours;
            }

            if (objCourse.Evaluation==null)
            {
                tbEvaluation.Visibility = System.Windows.Visibility.Collapsed;
                txtEvaluation.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                string htmlEvaluation = WebUtility.HtmlDecode(objCourse.Evaluation);
                string evaluation = objParser.ParseHTMLContent(htmlEvaluation);
                txtEvaluation.Text = evaluation;
            }

            if (objCourse.Timings==null)
            {
                tbClassTimings.Visibility = System.Windows.Visibility.Collapsed;
                txtClassTiming.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                string htmlTimings = WebUtility.HtmlDecode(objCourse.Timings);
                string timings = objParser.ParseHTMLContent(htmlTimings);
                txtClassTiming.Text = timings;
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

            //Fetching all the resources
            listResources = await RetrieveResources();

            //Lecture resource
            var lectureResources = listResources.Where(s => s.ResourceType == "Lecture").ToList<Usebackpack.Model.Resources>();
            //tutorial resource
            var tutorialResources = listResources.Where(s => s.ResourceType == "Tutorial").ToList<Usebackpack.Model.Resources>();
            //video resource
            var videoResources = listResources.Where(s => s.ResourceType == "Video").ToList<Usebackpack.Model.Resources>();
            //research paper resources
            var researchPaperResources = listResources.Where(s => s.ResourceType == "Research Paper").ToList<Usebackpack.Model.Resources>();
            //exam resources
            var examResources = listResources.Where(s => s.ResourceType == "Exam").ToList<Usebackpack.Model.Resources>();
            //project resources
            var projectResources = listResources.Where(s => s.ResourceType == "Project").ToList<Usebackpack.Model.Resources>();


            if (lectureResources.Count != 0)
            {
                txtLectureRType.Visibility = System.Windows.Visibility.Visible;
                lstLectureResource.Visibility = System.Windows.Visibility.Visible;
                lstLectureResource.ItemsSource = lectureResources;
            }

            if (tutorialResources.Count != 0)
            {
                txtTutorialRType.Visibility = System.Windows.Visibility.Visible;
                lstTutorialsResource.Visibility = System.Windows.Visibility.Visible;
                lstTutorialsResource.ItemsSource = tutorialResources;
            }

            if (videoResources.Count != 0)
            {
                txtVideoRType.Visibility = System.Windows.Visibility.Visible;
                lstVideosResource.Visibility = System.Windows.Visibility.Visible;
                lstVideosResource.ItemsSource = videoResources;
            }

            if (researchPaperResources.Count != 0)
            {
                txtReserachPaperRType.Visibility = System.Windows.Visibility.Visible;
                lstResearchPaperResource.Visibility = System.Windows.Visibility.Visible;
                lstResearchPaperResource.ItemsSource = researchPaperResources;
            }

            if (examResources.Count != 0)
            {
                txtExamRType.Visibility = System.Windows.Visibility.Visible;
                lstExamsResource.Visibility = System.Windows.Visibility.Visible;
                lstExamsResource.ItemsSource = examResources;
            }

            if (examResources.Count == 0 || researchPaperResources.Count == 0 || videoResources.Count == 0||lectureResources.Count==0||tutorialResources.Count==0)
            {
                tbNoResources.Visibility = System.Windows.Visibility.Visible;
                tbNoResources.Text = Constant.NORESOURCES;
            }

            //Code to bind discussions
            lstDiscussion = objCourse.Discussion;
            if (lstDiscussion.Count != 0)
            {
                listDiscussion.ItemsSource = lstDiscussion;
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

        private async void hlDiscussionSubject_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hlSubject = sender as HyperlinkButton;
            string content = hlSubject.Content.ToString();
            string disId = hlSubject.Tag.ToString();

            int discussionId = Convert.ToInt32(disId);
            await objAPIBusinessLayer.RetrieveDiscussionsByDiscussionId(cookie, discussionId);

            var app = App.Current as App;
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