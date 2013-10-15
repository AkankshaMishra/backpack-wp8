using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Usebackpack.Business_Layer;

namespace Usebackpack
{
    public partial class MyCourses : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        public MyCourses()
        {
            InitializeComponent();
        }
    }
}