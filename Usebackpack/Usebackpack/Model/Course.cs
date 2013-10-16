using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    [DataContract]
    public class Course
    {
        public int CourseId { get; set; }

        [DataMember(Name="code")]
        public string CourseCode { get; set; }
        
        [DataMember(Name="name")]
        public string CourseName { get; set; }

        [DataMember(Name="overview")]
        public string Overview { get; set; }

        [DataMember(Name="credits")]
        public int Credits { get; set; }

        [DataMember(Name="description")]
        public string Description { get; set; }

       [DataMember(Name="prerequisties")]
        public string Prerequisites { get; set; }

        [DataMember(Name="evaluation")]
        public string Evaluation { get; set; }
        
        [DataMember(Name="officehours")]
        public string OfficeHours { get; set; }

        [DataMember(Name="textbooks")]
        public string TextBooks { get; set; }

        [DataMember(Name="semester")]
        public int SemesterId { get; set; }

        [DataMember(Name="timings")]
        public string Timings { get; set; }

    }
}
