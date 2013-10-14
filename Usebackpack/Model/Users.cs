using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    public class Users
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string RollNo { get; set; }

        public string Website { get; set; }

        public DateTime Birthday { get; set; }

        public Semester Semester { get; set; }

        public int Level { get; set; }

        public string UserType { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateJoined { get; set; }

        public bool IsSuperUser { get; set; }

        public bool Approved { get; set; }

        public string RefreshToken { get; set; }

        public string Provider { get; set; }

        public string UID { get; set; }

        public string AccessToken { get; set; }

        public DateTime Expires { get; set; }

        public string NotificationPrefs { get; set; }

        public List<Course> UserCourses { get; set; }


    }
}
