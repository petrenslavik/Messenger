using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MessengerWindowsClient.Models
{
    public class Conversation : INotifyBaseClass
    {
        private string _interlocutorId;
        private ObservableCollection<Message> _messages;
        private string _name;
        private string _username;
        private Brush _brush;
        private bool _isActive;
        private int _unreadMessages;

        public bool IsActive
        {
            get { return _isActive; }
            set { SetField(ref _isActive, value,""); }
        }

        public string InterlocutorId
        {
            get { return _interlocutorId; }
            set { SetField(ref _interlocutorId, value,""); }
        }

        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value,""); }
        }

        public Brush Color
        {
            get { return _brush; }
            set { SetField(ref _brush, value,""); }
        }

        public string ShortName
        {
            get
            {
                var arr = _name.Split(' ');
                if (arr.Length == 1)
                    return arr[0].ToUpper()[0].ToString();
                return arr.Aggregate((x, y) => $"{x[0]}{y[0]}").ToUpper();
            }
        }

        public string Username
        {
            get { return _username; }
            set { SetField(ref _username, value,""); }
        }

        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set { SetField(ref _messages, value,""); }
        }

        public int UnreadMessages
        {
            get { return _unreadMessages; }
            set { SetField(ref _unreadMessages, value, ""); }
        }
    }
}
