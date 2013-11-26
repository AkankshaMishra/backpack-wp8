using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    [DataContract]
    public class Notifications
    {
        [DataMember(Name="id")]
        public int NotificationId { get; set; }

        [DataMember(Name = "text")]
        public string NotificationText { get; set; }

        public string Icon { get; set; }

        public string Link { get; set; }

         [DataMember(Name = "type")]
        public string NotificationType { get; set; }
    }
}
