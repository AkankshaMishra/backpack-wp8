using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    public class Submissions
    {

        public int SubmissionId { get; set; }

        public DateTime Timestamp { get; set; }

        public Users User { get; set; }

        public Deadlines Deadline { get; set; }

        public string AttachementFileName { get; set; }

        public string AttachementContentType { get; set; }

        public int AttachementFileSize { get; set; }

        public DateTime AttachementUpdatedAt { get; set; }

        public int SubmissionUpdated { get; set; }
    }
}
