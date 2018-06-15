using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessengerWindowsClient.Models
{
    public class Message : INotifyBaseClass
    {
        private string _id;
        private string _content;
        private bool _isAuthor;


        public string Id
        {
            get { return _id; }
            set { SetField(ref _id, value,""); }
        }

        public string Content
        {
            get { return _content; }
            set { SetField(ref _content, value,""); }
        }

        public bool IsAuthor
        {
            get { return _isAuthor; }
            set { SetField(ref _isAuthor, value,""); }
        }
    }
}
