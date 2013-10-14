using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    public class Resources
    {
        public int ResourceId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public User User { get; set; }

        public Course Course { get; set; }

        public string ResourceType { get; set; }

        public DateTime Date { get; set; }

        public string AttachementFileName { get; set; }

        public string AttachementContentType { get; set; }

        public int FileSize { get; set; }
    }
}
