using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Usebackpack.Model
{
    public class Comment
    {
        [DataMember(Name = "user")]
        public int UserId { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "comment")]
        public string comment { get; set; }

        [DataMember(Name = "weight")]
        public int Weight { get; set; }

        [DataMember(Name = "timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
