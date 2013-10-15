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
    /// Class for calling layer API
    /// Author: Kumar Abhinav,Pooja Agarwal,Nishtha Phutela,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    public class APIBusinessLayer:IAPIBusinessLayer
    {
        private IAPIServiceLayer objAPIServiceLayer = APIServiceLayer.APISeviceInstance();

        public static APIBusinessLayer APIBusinessInstance()
        {
            return new APIBusinessLayer();
        }

        public void GoogleAuthentication()
        {
            objAPIServiceLayer.GoogleAuthentication();
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
