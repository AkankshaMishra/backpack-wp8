using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    [DataContract]
    public class Users
    {
        public int UserId { get; set; }

        [DataMember(Name="first_name")]
        public string FirstName { get; set; }

        [DataMember(Name="last_name")]
        public string LastName { get; set; }
        
        [DataMember(Name="email")]
        public string EmailId { get; set; }

        [DataMember(Name="roll_no")]
        public string RollNo { get; set; }

        public string Website { get; set; }

        public DateTime Birthday { get; set; }

        public Semester Semester { get; set; }

        [DataMember(Name="level")]
        public int Level { get; set; }

        [DataMember(Name="points")]
        public int TotalScore { get; set; } 
       
        [DataMember(Name="avatar")]
        public string AvtarFileName { get; set; }

        public string UserType { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateJoined { get; set; }

        public int MyProperty { get; set; }

        public bool IsSuperUser { get; set; }

        public bool Approved { get; set; }

        public string RefreshToken { get; set; }

        public string Provider { get; set; }

        public string UID { get; set; }

        public string AccessToken { get; set; }

        public DateTime Expires { get; set; }

        public string NotificationPrefs { get; set; }

        [DataMember(Name="courses")]
        public string APICourseId { get; set; }

        [DataMember(Name="coursenames")]
        public string APICourseName { get; set; }

        [DataMember(Name="upcomingdeadlines")]
        public string UpcomingDeadlines { get; set; }

        public List<Course> UserCourses { get; set; }
    }
}
