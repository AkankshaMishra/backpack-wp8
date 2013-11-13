using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usebackpack.Model;
using Usebackpack.Common;
using Usebackpack.Service_Layer;
using System.Globalization;

namespace Usebackpack.Business_Layer
{
    /// <summary>
    /// Class for calling Service layer API methods
    /// Author: Kumar Abhinav,Pooja Agarwal,Nishtha Phutela,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    public class APIBusinessLayer:IAPIBusinessLayer
    {
        private IAPIServiceLayer objAPIServiceLayer = APIServiceLayer.APISeviceInstance();

        /// <summary>
        /// Method to return the instance of APIBusiness Layer
        /// </summary>
        /// <returns></returns>
        public static APIBusinessLayer APIBusinessInstance()
        {
            return new APIBusinessLayer();
        }

        /// <summary>
        /// Method to authenticate with google and get the access token on success
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<GoogleToken> GoogleAuthentication(string code)
        {
            return await objAPIServiceLayer.GoogleAuthentication(code);
        }

        /// <summary>
        /// Method to authenticate the request from backpack by passing token received from google
        /// </summary>
        /// <param name="googleOAuthToken"></param>
        /// <returns></returns>
        public async Task<string> BackpackSignIn(string googleOAuthToken)
        {
            return await objAPIServiceLayer.BackpackSignIn(googleOAuthToken);
        }

        /// <summary>
        /// Method to retrieve the user id
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async Task<string> RetrieveUserId(string cookie)
        {
            return await objAPIServiceLayer.RetrieveUserId(cookie);
        }

        /// <summary>
        /// Method to retrieve the user details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async Task<Users> RetrieveUserDetailsByUserId(int userId,string cookie)
        {
            Users userDetails = new Users();
            userDetails= await objAPIServiceLayer.RetrieveUserDetailsByUserId(userId, cookie);
            string concatCourseId = userDetails.APICourseId;
            string concatCourseName=userDetails.APICourseName;
            int courseIdlength = concatCourseId.Length;
            int courseNamelength = concatCourseName.Length;
            string subCourseId = concatCourseId.Substring(1, courseIdlength -2);
            string subCourseName = concatCourseName.Substring(1, courseNamelength - 2);
            string[] courseIdArray=new string[30];
            string[] courseNameArray=new string[30];
            courseIdArray= subCourseId.Split(',');
            courseNameArray=subCourseName.Split(',');

            userDetails.UserCourses = new List<Course>();
            for (int i = 0; i < courseIdArray.Length; i++)
            {
                Course objCourses=new Course();
                objCourses.CourseId=Convert.ToInt32(courseIdArray[i]);
                objCourses.CourseName = Convert.ToString(courseNameArray[i]);
                userDetails.UserCourses.Add(objCourses);

            }
            //counting the no. of courses
            var courseCountApp = App.Current as App;
            courseCountApp.CourseCount = courseIdArray.Length;

            //foreach (var item in userDetails.UpcomingDeadlines)
            for(int i=0;i<userDetails.UpcomingDeadlines.Count;i++)
            {
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0,DateTimeKind.Local).AddSeconds(Convert.ToDouble(userDetails.UpcomingDeadlines[i].EndTime)).ToLocalTime();
                DateTime epochIST = epoch.AddSeconds(19800);
                userDetails.UpcomingDeadlines[i].DeadLineTime = epochIST;
            }
            return userDetails;
        }

        /// <summary>
        /// Method to retrieve discussions by discussionId
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public async Task<List<Discussions>> RetrieveDiscussionsByDiscussionId(string cookie, int discussionId)
        {
            return await objAPIServiceLayer.RetrieveDiscussionsByDiscussionId(cookie, discussionId);
        }

        /// <summary>
        /// Method to retrieve the course details by courseId
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<Course> RetrieveCoursesByCourseId(string cookie,int courseId)
        {
            return await objAPIServiceLayer.RetrieveCoursesByCourseId(cookie, courseId);
        }

        /// <summary>
        /// Method to retrieve deadlines for a course
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public async Task<List<Deadlines>> RetrieveDeadlinesByCourseId(string cookie,int discussionId)
        {
            return await objAPIServiceLayer.RetrieveDeadlinesByCourseId(cookie, discussionId);
        }

        /// <summary>
        /// Method to retrieve all the resources for a course
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<List<Usebackpack.Model.Resources>> RetrieveResourcesByCourseId(string cookie,int courseId)
        {
            return await objAPIServiceLayer.RetrieveResourcesByCourseId(cookie, courseId);
        }

        /// <summary>
        /// Method to delete discussion
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public async Task<int> DeleteDiscussion(string cookie, int discussionId)
        {
            return await objAPIServiceLayer.DeleteDiscussion(cookie, discussionId);
        }

        /// <summary>
        /// Method to delete resources
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public async Task<int> DeleteResources(string cookie, int resourceId)
        {
            return await objAPIServiceLayer.DeleteResources(cookie, resourceId);
        }

        /// <summary>
        /// Method to delete deadlines
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="deadlineId"></param>
        /// <returns></returns>
        public async Task<int> DeleteDeadlines(string cookie, int deadlineId)
        {
            return await objAPIServiceLayer.DeleteDeadlines(cookie, deadlineId);
        }

        /// <summary>
        /// Method to delete reply
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="replyId"></param>
        /// <returns></returns>
        public async Task<int> DeleteReply(string cookie, int replyId)
        {
            return await objAPIServiceLayer.DeleteReply(cookie, replyId);
        }

        /// <summary>
        /// Method to delete comment
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<int> DeleteComment(string cookie, int commentId)
        {
            return await objAPIServiceLayer.DeleteComment(cookie, commentId);
        }

        /// <summary>
        /// Method to post deadline
        /// </summary>
        /// <param name="title"></param>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <param name="datePart"></param>
        /// <param name="timePart"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async Task<int> PostDeadline(string title, string courseId, string userId, string datePart, string timePart, string cookie)
        {
            return await objAPIServiceLayer.PostDeadline(title, courseId, userId, datePart, timePart, cookie);
        }

        /// <summary>
        /// Method to post discussion
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async Task<int> PostDiscussion(string courseId, string userId, string body, string subject, string cookie)
        {
            return await objAPIServiceLayer.PostDiscussion(courseId, userId, body, subject, cookie);
        }
        
    }
}
