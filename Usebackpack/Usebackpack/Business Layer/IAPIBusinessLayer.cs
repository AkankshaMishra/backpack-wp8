using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usebackpack.Model;

namespace Usebackpack.Business_Layer
{
    /// <summary>
    /// Interface for Business Layer API
    /// Author: Kumar Abhinav,Pooja Agarwal,Prabhat Mishra,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    interface IAPIBusinessLayer
    {
        Task<GoogleToken> GoogleAuthentication(string code);

        Task<string> BackpackSignIn(string googleOAuthToken);

        Task<string> RetrieveUserId(string cookie);

        Task<Users> RetrieveUserDetailsByUserId(int userId,string cookie);

        Task<Discussions> RetrieveDiscussionsByDiscussionId(string cookie,int dicussionId);

        Task<Course> RetrieveCoursesByCourseId(string cookie,int courseId);

        Task<List<Deadlines>> RetrieveDeadlinesByCourseId(string cookie,int courseId);

        Task<List<Usebackpack.Model.Resources>> RetrieveResourcesByCourseId(string cookie,int courseId);

        Task<int> DeleteDiscussion(string cookie, int discussionId);

        Task<int> DeleteResources(string cookie, int resourceId);

        Task<int> DeleteDeadlines(string cookie, int deadlineId);

        Task<int> DeleteReply(string cookie, int replyId);

        Task<int> DeleteComment(string cookie, int commentId);

        Task<int> PostDeadline(string title, string courseId, string userId, string datePart, string timePart, string cookie);

        Task<int> PostDiscussion(string courseId, string userId, string body, string subject, string cookie);
    }
}
