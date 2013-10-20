using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
     [DataContract]
    public class Resources
    {
      
        public int ResourceId { get; set; }

         [DataMember(Name="title")]
        public string Title { get; set; }

         [DataMember(Name="body")]
        public string Body { get; set; }

        [DataMember(Name = "user_id")]
        public int APIUser { get; set; }

        public User User { get; set; }

        public Course Course { get; set; }

        [DataMember(Name="resourcetype")]
        public string ResourceType { get; set; }

        [DataMember(Name="date")]
        public DateTime Date { get; set; }

        [DataMember(Name="attachment")]
        public string AttachementFileName { get; set; }

        public string AttachementContentType { get; set; }

        public int FileSize { get; set; }

        [DataMember(Name="the_name")]
        public DateTime TheTime { get; set; }
    }
}
