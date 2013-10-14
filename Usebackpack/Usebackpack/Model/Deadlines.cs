using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    public class Deadlines
    {
        public int DeadlineId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Course Course { get; set; }

        public string LastUpdated { get; set; }

        public string DueOn { get; set; }

    }
}
