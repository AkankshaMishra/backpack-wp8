using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usebackpack.Common;
using Usebackpack.Model;

namespace Usebackpack.Service_Layer
{
    /// <summary>
    /// Class for calling API of backpack
    /// Author: Kumar Abhinav,Pooja Agarwal,Nishtha Phutela,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    public class APIServiceLayer:IAPIServiceLayer
    {
        public static APIServiceLayer APISeviceInstance()
        {
            return new APIServiceLayer();
        }

        public void GoogleAuthentication()
        {
        }

        public void BackpackSignIn()
        {
        }

        public void RetrieveUserId()
        {
        }

        public void RetrieveUserDetailsByUserId()
        {
        }

        public void RetrieveDiscussionsByDiscussionId()
        {
        }

        public void RetrieveCoursesByCourseId()
        {

        }

        public void RetrieveDeadlinesByCourseId()
        {
        }

        public void RetrieveResourcesByCourseId()
        {
        }
    }
}
