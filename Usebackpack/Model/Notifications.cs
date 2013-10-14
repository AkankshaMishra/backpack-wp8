using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Model
{
    public class Notifications
    {
        public int NotificationId { get; set; }

        public int NotificationText { get; set; }

        public string Icon { get; set; }

        public string Link { get; set; }

        public string NotificationType { get; set; }
    }
}
