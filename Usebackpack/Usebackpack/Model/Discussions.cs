using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    [DataContract]
    public class Discussions
    {
        [DataMember(Name="id")]
        public int DiscussionId { get; set; }

        [DataMember(Name="subject")]
        public string Subject { get; set; }

        [DataMember(Name="body")]
        public string Body { get; set; }

        public Users User { get; set; }

        //[DataMember(Name="course")]
        //public Course Course { get; set; }
        
        [DataMember(Name="timestamp")]
        public DateTime Timestamp { get; set; }

        public bool Sticky { get; set; }

        public string AttachementFileName { get; set; }

        public string AttachementContentType { get; set; }

        public int AttachementFileSize { get; set; }

        public DateTime AttachementUpdatedAt { get; set; }

        public bool Anonymous { get; set; }

        //based on backpack data for dicussions
        [DataMember(Name="user")]
        public int UserId { get; set; }

        [DataMember(Name="username")]
        public string UserName { get; set; }

        [DataMember(Name="course")]
        public int CourseId { get; set; }

        [DataMember(Name="replies")]
        public List<Replies> Replies { get; set; }
    }
}
