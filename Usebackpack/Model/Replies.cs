using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    public class Replies
    {

        public int ReplyId { get; set; }

        public Users User { get; set; }

        public string Body { get; set; }

        public DateTime Timestamp { get; set; }

        public int Weight { get; set; }

        public Discussions Discussion { get; set; }

        public string AttachementFileName { get; set; }

        public string AttachementContentType { get; set; }

        public int AttachementFileSize { get; set; }

        public DateTime AttachementUpdatedAt { get; set; }

        public bool Anonymous { get; set; }
    }
}
