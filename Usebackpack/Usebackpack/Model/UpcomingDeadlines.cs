using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    [DataContract]
    public class UpcomingDeadlines
    {
        [DataMember(Name="name")]
        public string DeadlineName { get; set; }

        [DataMember(Name="coursename")]
        public string CourseName { get; set; }

        [DataMember(Name = "endTime")]
        public long EndTime { get; set; }

        public DateTime DeadLineTime { get; set; }
        
    }
}
