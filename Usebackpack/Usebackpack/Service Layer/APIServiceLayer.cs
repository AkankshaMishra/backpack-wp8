using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Usebackpack.Common;
using Usebackpack.Model;

namespace Usebackpack.Service_Layer
{
    /// <summary>
    /// Class for calling API of backpack
    /// Author: Kumar Abhinav,Pooja Agarwal,Prabhat Mishra,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    public class APIServiceLayer:IAPIServiceLayer
    {
        /// <summary>
        /// Method to return the instance of APIService Layer
        /// </summary>
        /// <returns></returns>
        public static APIServiceLayer APISeviceInstance()
        {
            try
            {
                return new APIServiceLayer();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to authenticate with google and get the access token on success
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<GoogleToken> GoogleAuthentication(string code)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp("https://accounts.google.com/o/oauth2/token");
                var token = await request.GetValueFromRequest<GoogleToken>(string.Format(
                    "code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type={4}",
                    code,
                    Constant.CLIENTID,
                    Constant.CLIENTSECRET,
                    "http://localhost",
                    "authorization_code"));
                return token;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        /// <summary>
        /// Method to authenticate the request from backpack by passing token received from google
        /// </summary>
        /// <param name="googleOAuthToken"></param>
        /// <returns></returns>
        public async Task<string> BackpackSignIn(string googleOAuthToken)
        {
            try
            {
                using (HttpClient signInClient = new HttpClient())
                {
                    signInClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");

                    Dictionary<string, string> dicToken = new Dictionary<string, string>();
                    dicToken.Add("token", googleOAuthToken);
                    HttpContent requestContent = new FormUrlEncodedContent(dicToken);

                    HttpResponseMessage signInResponse = await signInClient.PostAsync(Constant.BASEURL + Constant.BACKPACKSIGNIN, requestContent);

                    List<string> lstcookie = signInResponse.Headers.GetValues("Set-Cookie").ToList<string>();

                    string cookieValue = lstcookie[0].ToString();

                    string responseAsString = await signInResponse.Content.ReadAsStringAsync();

                    String[] str = cookieValue.Split(';');

                    String[] str1 = str[1].Split(',');

                    string cookie = str1[1];
                    return cookie;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to retrieve the user id
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async Task<string> RetrieveUserId(string cookie)
        {
            try
            {
                using (HttpClient userIdClient = new HttpClient())
                {
                    userIdClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    userIdClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    HttpContent cont = null;
                    HttpResponseMessage hrUserId = await userIdClient.PostAsync(Constant.BASEURL + Constant.RETRIEVEUSERID, cont);

                    string responseUserId = await hrUserId.Content.ReadAsStringAsync();
                    return responseUserId;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to retrieve the user details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cookie"></param>
        public async Task<Users> RetrieveUserDetailsByUserId(int userId,string cookie)
        {
            try
            {
                using (HttpClient userDetailsClient = new HttpClient())
                {

                    userDetailsClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    userDetailsClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    string responseUserDetails = await userDetailsClient.GetStringAsync(new Uri(Constant.BASEURL + Constant.RETRIEVEUSERDETAILSBYUSERID + userId + "", UriKind.Absolute));

                    Users userDetails = JsonConvert.DeserializeObject<Users>(responseUserDetails);
       
                    return userDetails;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to retrieve discussions by discussionId
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="discussionId"></param>
        public async Task<Discussions> RetrieveDiscussionsByDiscussionId(string cookie,int discussionId)
        {
            try
            {
                using (HttpClient retrieveDiscussionClient = new HttpClient())
                {

                    retrieveDiscussionClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    retrieveDiscussionClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    string responseDiscussion = await retrieveDiscussionClient.GetStringAsync(new Uri(Constant.BASEURL + Constant.RETRIEVEDISCUSSIONS + discussionId, UriKind.Absolute));

                    Discussions userDetails = JsonConvert.DeserializeObject<Discussions>(responseDiscussion);
                    return userDetails;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to retrieve the course details by courseId
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<Course> RetrieveCoursesByCourseId(string cookie,int courseId)
        {
            try
            {
                using (HttpClient retrieveCoursesClient = new HttpClient())
                {

                    retrieveCoursesClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    retrieveCoursesClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    string responseCourseDetails = await retrieveCoursesClient.GetStringAsync(new Uri(Constant.BASEURL + Constant.RETRIEVECOURSEDETAILS + courseId, UriKind.Absolute));
                    Course courseDetails = JsonConvert.DeserializeObject<Course>(responseCourseDetails);
                    return courseDetails;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to retrieve deadlines for a course
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="deadlineId"></param>
        /// <returns></returns>
        public async Task<List<Deadlines>> RetrieveDeadlinesByCourseId(string cookie,int deadlineId)
        {
            try
            {
                using (HttpClient retrieveDeadlinesClient = new HttpClient())
                {

                    retrieveDeadlinesClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    retrieveDeadlinesClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    string responseDeadlines = await retrieveDeadlinesClient.GetStringAsync(new Uri(Constant.BASEURL + Constant.RETRIEVEDEADLINES + deadlineId, UriKind.Absolute));

                    List<Deadlines> deadlineDetails = JsonConvert.DeserializeObject<List<Deadlines>>(responseDeadlines);
                    return deadlineDetails;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to retrieve all the resources for a course
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<List<Usebackpack.Model.Resources>> RetrieveResourcesByCourseId(string cookie,int courseId)
        {
            try
            {
                using (HttpClient retrieveResourceClient = new HttpClient())
                {
                    retrieveResourceClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    retrieveResourceClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    string responseResources = await retrieveResourceClient.GetStringAsync(new Uri(Constant.BASEURL + Constant.RETRIEVERESOURCES + courseId, UriKind.Absolute));
                    List<Usebackpack.Model.Resources> lstResources = JsonConvert.DeserializeObject<List<Usebackpack.Model.Resources>>(responseResources);
                    return lstResources;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to delete discussions
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public async Task<int> DeleteDiscussion(string cookie, int discussionId)
        {
            try
            {
                using (HttpClient deleteDiscussionClient = new HttpClient())
                {
                    deleteDiscussionClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    deleteDiscussionClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    HttpResponseMessage responseDeleteDiscussion = await deleteDiscussionClient.DeleteAsync(Constant.BASEURL + Constant.DELETEDISCUSSION + discussionId);
                    string responseStringDeleteDiscussion = await responseDeleteDiscussion.Content.ReadAsStringAsync();
                    return Convert.ToInt32(responseStringDeleteDiscussion);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to delete resources
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public async Task<int> DeleteResources(string cookie, int resourceId)
        {

            try
            {
                using (HttpClient deleteResourceClient = new HttpClient())
                {

                    deleteResourceClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    deleteResourceClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    HttpResponseMessage responseMessageDeleteResource = await deleteResourceClient.DeleteAsync(Constant.BASEURL + Constant.DELETERESOURCES + resourceId);

                    string responseDeleteResource = await responseMessageDeleteResource.Content.ReadAsStringAsync();

                    return Convert.ToInt32(responseDeleteResource);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        /// <summary>
        /// Method to delete deadlines
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="deadlineId"></param>
        /// <returns></returns>
        public async Task<int> DeleteDeadlines(string cookie, int deadlineId)
        {
            try
            {
                using (HttpClient deleteDeadlinesClient = new HttpClient())
                {

                    deleteDeadlinesClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    deleteDeadlinesClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    HttpResponseMessage responseMessageDeleteDeadlines = await deleteDeadlinesClient.DeleteAsync(Constant.BASEURL + Constant.DELETEDEADLINES + deadlineId);

                    string responseDeleteDeadlines = await responseMessageDeleteDeadlines.Content.ReadAsStringAsync();
                    return Convert.ToInt32(responseDeleteDeadlines);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Methods to delete reply
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="replyId"></param>
        /// <returns></returns>
        public async Task<int> DeleteReply(string cookie,int replyId)
        {

            try
            {
                using (HttpClient deleteReplyClient = new HttpClient())
                {

                    deleteReplyClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    deleteReplyClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    HttpResponseMessage responseMessageDeleteReply = await deleteReplyClient.DeleteAsync(Constant.BASEURL + Constant.DELETEREPLY + replyId);

                    string responseDeleteReply = await responseMessageDeleteReply.Content.ReadAsStringAsync();

                    return Convert.ToInt32(responseDeleteReply);
                }
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        /// <summary>
        /// Methods to delete comment
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<int> DeleteComment(string cookie, int commentId)
        {
            try
            {
                using (HttpClient deleteCommentClient = new HttpClient())
                {

                    deleteCommentClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    deleteCommentClient.DefaultRequestHeaders.Add("Cookie", cookie);


                    HttpResponseMessage responseMessageDeleteComment = await deleteCommentClient.DeleteAsync(Constant.BASEURL + Constant.DELETECOMMENT + commentId);

                    string responseDeleteComment = await responseMessageDeleteComment.Content.ReadAsStringAsync();

                    return Convert.ToInt32(responseDeleteComment);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to post a deadline
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

            try
            {
                using (HttpClient postDeadlineClient = new HttpClient())
                {

                    postDeadlineClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    postDeadlineClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    Dictionary<string, string> dictPostDeadlines = new Dictionary<string, string>();

                    dictPostDeadlines.Add("title", title);
                    dictPostDeadlines.Add("course_Id", courseId);
                    dictPostDeadlines.Add("user_Id", userId);
                    dictPostDeadlines.Add("date_part", datePart);
                    dictPostDeadlines.Add("time_part", timePart);

                    HttpContent postDeadlineContent = new FormUrlEncodedContent(dictPostDeadlines);

                    HttpResponseMessage responseMessagePostDeadlines = await postDeadlineClient.PostAsync(Constant.BASEURL + Constant.POSTDEADLINES, postDeadlineContent);

                    string responsePostDeadlines = await responseMessagePostDeadlines.Content.ReadAsStringAsync();

                    return Convert.ToInt32(responsePostDeadlines);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to post a new discussion
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async Task<int> PostDiscussion(string courseId,string userId,string body,string subject,string cookie)
        {
            try
            {
                using (HttpClient postDiscussionClient = new HttpClient())
                {

                    postDiscussionClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
                    postDiscussionClient.DefaultRequestHeaders.Add("Cookie", cookie);

                    Dictionary<string, string> dicPostDiscussion = new Dictionary<string, string>();
                    dicPostDiscussion.Add("course_Id", courseId);
                    dicPostDiscussion.Add("user_Id", userId);
                    dicPostDiscussion.Add("body", body);
                    dicPostDiscussion.Add("subject", subject);
                    HttpContent postDiscussionContent = new FormUrlEncodedContent(dicPostDiscussion);

                    HttpResponseMessage responseMessagePostDiscussion = await postDiscussionClient.PostAsync(Constant.BASEURL + Constant.POSTDISCUSSION, postDiscussionContent);

                    string responsePostDiscussion = await responseMessagePostDiscussion.Content.ReadAsStringAsync();

                    return Convert.ToInt32(responsePostDiscussion);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }    
}
