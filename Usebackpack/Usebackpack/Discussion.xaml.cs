using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Usebackpack.Business_Layer;
using Usebackpack.Model;

namespace Usebackpack
{
    public partial class Discussion : PhoneApplicationPage
    {
        TextBox tbReply = new TextBox();
        private IAPIBusinessLayer objAPIBusinessLayer = APIBusinessLayer.APIBusinessInstance();
        Discussions objDiscussion = new Discussions();
        string cookie = null;
        int discussionId = 0;
        string subject = null;
        public Discussion()
        {
            InitializeComponent();
            Loaded += Discussion_Loaded;
            
        }

        public async void Discussion_Loaded(object sender, RoutedEventArgs e)
        {
            var discussion = App.Current as App;
            cookie = discussion.Cookie;
            discussionId = discussion.Discussion.DiscussionId;
            subject = discussion.Discussion.Subject;
            objDiscussion = await RetrieveDiscussions(cookie, discussionId);
            //discussion value
            txtDiscussion.Text = objDiscussion.Body;

            StackPanel spComments = new StackPanel();
            for (int i = 0; i < objDiscussion.Replies.Count; i++)
            {

                //Generate rectangle 1st, then add 2 textbolck--one for Name,one for comments text and then add that rectangle to stackpanel 

                //Creating border for each reply and attaching it to main spReply StackPanel
                Border breachReply = new Border();
                breachReply.Name = "breachReply" + i;
                breachReply.BorderThickness = new Thickness(2, 2, 2, 2);
                breachReply.CornerRadius = new CornerRadius(5);
                breachReply.BorderBrush = new SolidColorBrush(Colors.White);
                breachReply.Margin = new Thickness(0, 15, 0, 0);
                
                //parent of reply border
                spReply.Children.Add(breachReply);

                //stack panel to hold a reply and all the comments and add it to border(parent)
                StackPanel spCommentAndReply = new StackPanel();
                breachReply.Child = spCommentAndReply;


                StackPanel speachReply = new StackPanel();

                //Adding textbox for reply
                TextBlock tbName = new TextBlock();

                //tbName.Text = "Kumar" + i;
                tbName.Text = objDiscussion.Replies[i].UserName;

                TextBlock tbRply = new TextBlock();
                //tbRply.Text = "Reply" + i;
                tbRply.Text = objDiscussion.Replies[i].Body;

                speachReply.Children.Add(tbName);
                speachReply.Children.Add(tbRply);
                //added reply  to spCommentReply stack panel
                spCommentAndReply.Children.Add(speachReply);

                //Adding textbox for comments for each reply
                for (int j = 0; j < objDiscussion.Replies[i].Comments.Count; j++)
                {
                    //Boder for comments
                    Border brComments = new Border();
                    brComments.BorderThickness = new Thickness(2, 2, 2, 2);
                    brComments.CornerRadius = new CornerRadius(5);
                    brComments.BorderBrush = new SolidColorBrush(Colors.White);
                    brComments.Margin = new Thickness(5, 10, 5, 0);

                    StackPanel speachComments = new StackPanel();
                    //added border child speachcomment
                    brComments.Child = speachComments;

                    //Adding textblock for comments
                    TextBlock tbNameComments = new TextBlock();
                    //tbNameComments.Text = "Kumar" + j;
                    tbNameComments.Text = objDiscussion.Replies[i].Comments[j].UserName;

                    TextBlock tbComment = new TextBlock();
                    //tbComment.Text = "Comment" + j;
                    tbComment.Text = objDiscussion.Replies[i].Comments[j].comment;

                    speachComments.Children.Add(tbNameComments);
                    speachComments.Children.Add(tbComment);
                    spCommentAndReply.Children.Add(brComments);
                }

                //stack panel for sending comments
                StackPanel spsendComment = new StackPanel();
                spsendComment.Orientation = System.Windows.Controls.Orientation.Horizontal;
                //Adding textbox at last to enter comments for each reply
                TextBox tbLastComment = new TextBox();
                //assign id to a comment textbox
                tbLastComment.Name = "LastComment" + i;
                tbLastComment.Width = 300;
                tbLastComment.Text = "Enter your comments";
                spsendComment.Children.Add(tbLastComment);
                Button btnSend = new Button();
                //i will have discussion id
                btnSend.Click += btnSend_Click;
                btnSend.Tag = i;
                btnSend.Name = "btnComnt" + i;
                btnSend.Content = "Comments";
                //btnSend.Width = 30;
                spsendComment.Children.Add(btnSend);
                spCommentAndReply.Children.Add(spsendComment);
            }

            //adding border for send reply
            Border brsendReply = new Border();
            brsendReply.BorderThickness = new Thickness(2, 2, 2, 2);
            brsendReply.CornerRadius = new CornerRadius(5);
            brsendReply.BorderBrush = new SolidColorBrush(Colors.White);
            brsendReply.Margin = new Thickness(0, 25, 0, 0);


            //Adding textbox at last to enter reply..created stackpanel for placing textbox and button as c chold of spreply
            StackPanel spSendReply = new StackPanel();
            brsendReply.Child = spSendReply;
            spSendReply.Orientation = System.Windows.Controls.Orientation.Vertical;
            //TextBox tbReply = new TextBox();
            tbReply.Text = "Enter Reply";
            spSendReply.Children.Add(tbReply);
            Button btnReply = new Button();

            btnReply.Content = "Reply";
            btnReply.HorizontalAlignment = HorizontalAlignment.Right;
            btnReply.Click += btnReply_Click;
            //btnReply.Width = 30;
            spSendReply.Children.Add(btnReply);
            spReply.Children.Add(brsendReply);
        }

        //button for sending reply
        void btnReply_Click(object sender, RoutedEventArgs e)
        {
            //discussion id from previous page
            string replyName = tbReply.Text;

        }

        //button for sending comments
        void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Button btnName = sender as Button;
            //get the name of the button clicked
            string name = btnName.Name;
            int id = Convert.ToInt32(btnName.Tag.ToString());
            //var obj=spReply.FindName("breachReply0");
            //find the parent element of the button which is stackpanel
            var obj1 = btnName.Parent;
            if (obj1 is StackPanel)
            {

                StackPanel sp = (StackPanel)obj1;
                //from the stackpanel , get the textbox from its id
                var obj2 = sp.FindName("LastComment"+id);

                TextBox txt = (TextBox)obj2;
                //get the text of the element

                string commentsText = txt.Text;

                if (commentsText.Equals(""))
                {
                    MessageBox.Show("Please enter comments");
                }
                else
                {
                    
                }
            }
        }

        /// <summary>
        /// Method for retrieving discussions
        /// </summary>
        /// <returns></returns>
        private async Task<Discussions> RetrieveDiscussions(string cookie,int discussionId)
        {
            return await objAPIBusinessLayer.RetrieveDiscussionsByDiscussionId(cookie, discussionId);
        }

        //button for sending reply
    }
}