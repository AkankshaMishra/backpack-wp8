using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseCode { get; set; }

        public string CourseName { get; set; }

        public string Overview { get; set; }

        public int Credits { get; set; }

        public string Description { get; set; }

        public string Prerequisites { get; set; }

        public string Evaluation { get; set; }

        public string OfficeHours { get; set; }

        public string TextBooks { get; set; }

    }
}
