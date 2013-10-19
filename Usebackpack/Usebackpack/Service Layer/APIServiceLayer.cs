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
            return new APIServiceLayer();
        }

        /// <summary>
        /// Method to authenticate with google and get the access token on success
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<GoogleToken> GoogleAuthentication(string code)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp("https://accounts.google.com/o/oauth2/token");
            var token= await request.GetValueFromRequest<GoogleToken>(string.Format(
                "code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type={4}",
                code,
                Constant.CLIENTID,
                Constant.CLIENTSECRET,
                "http://localhost",
                "authorization_code"));
            return token;
            
        }

        /// <summary>
        /// Method to authenticate the request from backpack by passing token received from google
        /// </summary>
        /// <param name="googleOAuthToken"></param>
        /// <returns></returns>
        public async Task<string> BackpackSignIn(string googleOAuthToken)
        {
            HttpClient signInClient = new HttpClient();
            signInClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");

            Dictionary<string, string> dicToken = new Dictionary<string, string>();
            dicToken.Add("token", googleOAuthToken);
            HttpContent requestContent = new FormUrlEncodedContent(dicToken);

            HttpResponseMessage signInResponse = await signInClient.PostAsync(Constant.BASEURL+Constant.BACKPACKSIGNIN, requestContent);

            List<string> lstcookie = signInResponse.Headers.GetValues("Set-Cookie").ToList<string>();

            string cookieValue = lstcookie[0].ToString();

            string responseAsString = await signInResponse.Content.ReadAsStringAsync();

            String[] str = cookieValue.Split(';');

            String[] str1 = str[1].Split(',');

            string cookie = str1[1];
            return cookie;
        }

        /// <summary>
        /// Method to retrieve the user id
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async Task<string> RetrieveUserId(string cookie)
        {
            HttpClient userIdClient = new HttpClient();

            userIdClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
            userIdClient.DefaultRequestHeaders.Add("Cookie", cookie);

            HttpContent cont = null;
            HttpResponseMessage hrUserId = await userIdClient.PostAsync(Constant.BASEURL+Constant.RETRIEVEUSERID, cont);

            string responseUserId = await hrUserId.Content.ReadAsStringAsync();
            return responseUserId;
        }

        /// <summary>
        /// Method to retrieve the user details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cookie"></param>
        public async Task<Users> RetrieveUserDetailsByUserId(int userId,string cookie)
        {
            HttpClient userDetailsClient = new HttpClient();

            userDetailsClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
            userDetailsClient.DefaultRequestHeaders.Add("Cookie", cookie);

            string responseUserDetails = await userDetailsClient.GetStringAsync(new Uri(Constant.BASEURL+Constant.RETRIEVEUSERDETAILSBYUSERID + userId + "", UriKind.Absolute));

            Users userDetails = JsonConvert.DeserializeObject<Users>(responseUserDetails);
            return userDetails;
        }

        /// <summary>
        /// Method to retrieve discussions by discussionId
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="discussionId"></param>
        public async Task<Discussions> RetrieveDiscussionsByDiscussionId(string cookie,int discussionId)
        {
            HttpClient retrieveDiscussionClient = new HttpClient();

            retrieveDiscussionClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
            retrieveDiscussionClient.DefaultRequestHeaders.Add("Cookie", cookie);

            string responseDiscussion = await retrieveDiscussionClient.GetStringAsync(new Uri(Constant.BASEURL+Constant.RETRIEVEDISCUSSIONS+discussionId, UriKind.Absolute));

            Discussions userDetails = JsonConvert.DeserializeObject<Discussions>(responseDiscussion);
            return userDetails;
        }

        /// <summary>
        /// Method to retrieve the course details by courseId
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<List<Course>> RetrieveCoursesByCourseId(string cookie,int courseId)
        {
            HttpClient retrieveCoursesClient = new HttpClient();

            retrieveCoursesClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
            retrieveCoursesClient.DefaultRequestHeaders.Add("Cookie", cookie);

            string responseCourseDetails = await retrieveCoursesClient.GetStringAsync(new Uri(Constant.BASEURL+Constant.RETRIEVECOURSEDETAILS+courseId, UriKind.Absolute));
            List<Course> courseDetails = JsonConvert.DeserializeObject<List<Course>>(responseCourseDetails);
            return courseDetails;
        }

        /// <summary>
        /// Method to retrieve deadlines for a course
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="deadlineId"></param>
        /// <returns></returns>
        public async Task<List<Deadlines>> RetrieveDeadlinesByCourseId(string cookie,int deadlineId)
        {
            HttpClient retrieveDeadlinesClient = new HttpClient();

            retrieveDeadlinesClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
            retrieveDeadlinesClient.DefaultRequestHeaders.Add("Cookie", cookie);

            string responseDeadlines = await retrieveDeadlinesClient.GetStringAsync(new Uri(Constant.BASEURL+Constant.RETRIEVEDEADLINES+deadlineId, UriKind.Absolute));

            List<Deadlines> deadlineDetails = JsonConvert.DeserializeObject<List<Deadlines>>(responseDeadlines);
            return deadlineDetails;
        }

        /// <summary>
        /// Method to retrieve all the resources for a course
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<List<Usebackpack.Model.Resources>> RetrieveResourcesByCourseId(string cookie,int courseId)
        {
            HttpClient retrieveResourceClient = new HttpClient();
            retrieveResourceClient.DefaultRequestHeaders.Add("Authorization", "Token token=" + Constant.BACKPACKAPIKEY + "");
            retrieveResourceClient.DefaultRequestHeaders.Add("Cookie", cookie);

            string responseResources = await retrieveResourceClient.GetStringAsync(new Uri(Constant.BASEURL+Constant.RETRIEVERESOURCES+courseId, UriKind.Absolute));
            List<Usebackpack.Model.Resources> deadlineDetails = JsonConvert.DeserializeObject<List<Usebackpack.Model.Resources>>(responseResources);
            return deadlineDetails;
        }
    }    
}
