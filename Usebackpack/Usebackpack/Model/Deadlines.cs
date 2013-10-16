using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    [DataContract]
    public class Deadlines
    {
       
        public int DeadlineId { get; set; }
        
        [DataMember(Name="title")]
        public string Title { get; set; }

        [DataMember(Name="body")]
        public string Body { get; set; }

       
        public Course Course { get; set; }
       
        [DataMember(Name="timestamp")]
        public string LastUpdated { get; set; }

       [DataMember(Name="endDateTime")]
        public string DueOn { get; set; }

        [DataMember(Name="user_id")]
       public Users User { get; set; }


    }
}
