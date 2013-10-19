using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usebackpack.Model;
using Usebackpack.Common;
using Usebackpack.Service_Layer;

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
            return await objAPIServiceLayer.RetrieveUserDetailsByUserId(userId, cookie);
        }

        /// <summary>
        /// Method to retrieve discussions by discussionId
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public async Task<Discussions> RetrieveDiscussionsByDiscussionId(string cookie,int discussionId)
        {
            return await objAPIServiceLayer.RetrieveDiscussionsByDiscussionId(cookie, discussionId);
        }

        /// <summary>
        /// Method to retrieve the course details by courseId
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<List<Course>> RetrieveCoursesByCourseId(string cookie,int courseId)
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
    }
}
