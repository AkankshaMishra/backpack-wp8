using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Usebackpack.Model;


namespace Usebackpack
{
    public partial class CourseDetail : PhoneApplicationPage
    {
        public CourseDetail()
        {
            InitializeComponent();
            lstCourse.ItemsSource = GetDeadlines();
        }

        private static List<Deadlines> GetDeadlines()
        {
            List<Deadlines> lstDeadlines = new List<Deadlines>();
            lstDeadlines.Add(new Deadlines() { DeadlineId = 535, Body = "The presentations will be held on Nov 11, 14, 18, and 21. You will need to form a group of 8-10 students. There are 8 candidate papers. Each group will present one paper. The titles of the papers are in the presentation sheet of goo.gl/ciiyEU Please form your group and select a paper. You will need to fill in your roll number against the title of the paper in the spreadsheet by the deadline. I will do the scheduling of presentations after that.\n You can also select a topic on a new technology. For that, you will need to get my permission beforehand", Title = "Presentations", LastUpdated = "Vinayak Naik updated 1 day ago", DueOn = "Due: Monday, October 7 2013, 11:30 (7 days left)" });
            lstDeadlines.Add(new Deadlines() { DeadlineId = 535, Body = "The presentations will be held on Nov 11, 14, 18, and 21. You will need to form a group of 8-10 students. There are 8 candidate papers. Each group will present one paper. The titles of the papers are in the presentation sheet of goo.gl/ciiyEU Please form your group and select a paper. You will need to fill in your roll number against the title of the paper in the spreadsheet by the deadline. I will do the scheduling of presentations after that.\n You can also select a topic on a new technology. For that, you will need to get my permission beforehand", Title = "MidSem Demo", LastUpdated = "Vinayak Naik updated 2 days ago ", DueOn = "Due: Tuesday, October 22 2013, 11:00 (22 days left)" });
            lstDeadlines.Add(new Deadlines() { DeadlineId = 535, Body = "The presentations will be held on Nov 11, 14, 18, and 21. You will need to form a group of 8-10 students. There are 8 candidate papers. Each group will present one paper. The titles of the papers are in the presentation sheet of goo.gl/ciiyEU Please form your group and select a paper. You will need to fill in your roll number against the title of the paper in the spreadsheet by the deadline. I will do the scheduling of presentations after that.\n You can also select a topic on a new technology. For that, you will need to get my permission beforehand", Title = "Homework2", LastUpdated = "Vinayak Naik updated 19 days ago", DueOn = "Due: Thursday, September 19 2013, 11:30 (Deadline passed)" });
            return lstDeadlines;
        }
    }
}