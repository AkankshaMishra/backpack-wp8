﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    [DataContract]
    public class Reply
    {
        [DataMember(Name = "user")]
        public int UserId { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "weight")]
        public int Weight { get; set; }

        [DataMember(Name = "timestamp")]
        public DateTime Timestamp { get; set; }

        public int DiscussionId { get; set; }

        public string AttachmentFileName { get; set; }

        public string AttachmentContenttype { get; set; }

        public int AttachmentFileSize { get; set; }

        public DateTime AttachmentUpdatedOn { get; set; }

        [DataMember(Name = "comments")]
        public List<Comment> Comments { get; set; }
    }
}