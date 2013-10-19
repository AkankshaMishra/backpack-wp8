﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usebackpack.Model;

namespace Usebackpack.Service_Layer
{
    /// <summary>
    /// Interface for service layer API
    /// Author: Kumar Abhinav,Pooja Agarwal,Prabhat Mishra,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    interface IAPIServiceLayer
    {
        Task<GoogleToken> GoogleAuthentication(string code);

        Task<string> BackpackSignIn(string googleOAuthToken);

        Task<string> RetrieveUserId(string cookie);

        Task<Users> RetrieveUserDetailsByUserId(int userId,string cookie);

        Task<Discussions> RetrieveDiscussionsByDiscussionId(string cookie,int discussionId);

        Task<List<Course>> RetrieveCoursesByCourseId(string cookie,int courseId);

        Task<List<Deadlines>> RetrieveDeadlinesByCourseId(string cookie,int discussionId);

        Task<List<Usebackpack.Model.Resources>> RetrieveResourcesByCourseId(string cookie,int courseId);
    }
}
