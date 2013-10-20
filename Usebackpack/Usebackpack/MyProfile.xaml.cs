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
    public partial class MyProfile : PhoneApplicationPage
    {
        private IAPIBusinessLayer objAPIServiceLayer = APIBusinessLayer.APIBusinessInstance();
        public MyProfile()
        {
            InitializeComponent();
            Loaded += MyProfile_Loaded;
        }

        void MyProfile_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}