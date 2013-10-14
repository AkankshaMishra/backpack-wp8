using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    public class Discussions
    {
        public int DiscussionId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public Users User { get; set; }

        public Course Course { get; set; }

        public DateTime Timestamp { get; set; }

        public bool Sticky { get; set; }

        public string AttachementFileName { get; set; }

        public string AttachementContentType { get; set; }

        public int AttachementFileSize { get; set; }

        public DateTime AttachementUpdatedAt { get; set; }

        public bool Anonymous { get; set; }
    }
}
