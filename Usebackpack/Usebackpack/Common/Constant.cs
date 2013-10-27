using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Common
{
    /// <summary>
    /// Class to store all the constant variables used in the application
    /// Author: Kumar Abhinav,Pooja Agarwal,Prabhat Mishra,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    public class Constant
    {
        public const string CLIENTID = "768769367411-33ai0ahr127tgap0o5m51cvqrbu519k8.apps.googleusercontent.com";
        public const string CLIENTSECRET = "iSBUy-sQUNtPihlo8VsA7poV";
        public const string BACKPACKAPIKEY = "92c6936cfde51eeef53d23b871642049";
        public const string GOOGLELOGINURL = "https://accounts.google.com/o/oauth2/auth?response_type=code&client_id={0}&scope=https://www.googleapis.com/auth/userinfo.email&redirect_uri=http://localhost&approval_prompt=force";
        
        //XAML page URL
        public const string MYHOME = "/MyHome.xaml";
        public const string MYCOURSES = "/MyCourses.xaml";
        public const string MYPROFILE="/MyProfile.xaml";
        public const string NOTIFICATION = "/Notification.xaml";
        public const string SETTINGS = "/Settings.xaml";
        public const string COURSEDETAIL = "/CourseDetail.xaml";
        public const string COURSESUPCOMINGDEADLINES = "/CoursesUpcomingDeadlines.xaml";
        public const string DEADLINEDETAILS = "/DeadlineDetails.xaml";

        //API URL   
        public const string BASEURL = "http://www.usebackpack.com";
        public const string BACKPACKSIGNIN = "/api/signin";
        public const string RETRIEVEUSERID = "/api/currentuser";
        public const string RETRIEVEUSERDETAILSBYUSERID = "/api/users/";
        public const string RETRIEVEDISCUSSIONS = "/api/discussions/";
        public const string RETRIEVECOURSEDETAILS = "/api/courses/";
        public const string RETRIEVEDEADLINES = "/api/deadlines/";
        public const string RETRIEVERESOURCES = "/api/resources/";
        public const string DELETEDISCUSSION = "/api/discussions/";
        public const string DELETERESOURCES = "/api/resources/";
        public const string DELETEDEADLINES = "/api/deadlines/";
        public const string DELETEREPLY = "/api/reply/";
        public const string DELETECOMMENT = "/api/comment/";
        public const string POSTDEADLINES = "/api/deadline";
        public const string POSTDISCUSSION = "/api/discussion";

        //Messages
        public const string NODEADLINES = "You don't have any deadlines";
        public const string NOCOURSEDEADLINES = "No deadlines for this course yet";
        public const string NORESOURCES = "No Resources available for this course";
    }
}
