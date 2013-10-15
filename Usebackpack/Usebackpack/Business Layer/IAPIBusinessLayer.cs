using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Business_Layer
{
    interface IAPIBusinessLayer
    {
        void GoogleAuthentication();

        void BackpackSignIn();

        void RetrieveUserId();

        void RetrieveUserDetailsByUserId();

        void RetrieveDiscussionsByDiscussionId();

        void RetrieveCoursesByCourseId();

        void RetrieveDeadlinesByCourseId();

        void RetrieveResourcesByCourseId();
    }
}
