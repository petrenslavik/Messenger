using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using CommonLibrary;
using MessengerWindowsClient.Managers;
using MessengerWindowsClient.Pages;

namespace MessengerWindowsClient.Models
{
    public class MessengerViewModel : INotifyBaseClass
    {
        public ServiceManager ServiceManager { get; set; }

        private ObservableCollection<Conversation> _conversations;
        private Conversation _currentConversation;
        private MessagesPage _parent;
        private List<Conversation> _tempList;
        private bool _canClear;
        private bool _isSearching;
        private Random _random;
        private DateTime _lastUpdate;

        public ObservableCollection<Conversation> Conversations
        {
            get { return _conversations; }
            set { SetField(ref _conversations, value,""); }
        }

        public Conversation CurrentConversation
        {
            get { return _currentConversation; }
            set
            {
                _parent.MessagesListBox.ItemsSource = value.Messages;

                if (_tempList != null)
                {
                    ClearSearchUsers();
                    _parent.ConversationsListBox.Items.Refresh();
                }

                SetField(ref _currentConversation, value,"");
                _parent.MessagesListBox.ScrollIntoView(CurrentConversation.Messages[CurrentConversation.Messages.Count - 1]);
            }
        }

        public MessengerViewModel(ServiceManager serviceManager, MessagesPage page)
        {
            _random = new Random();
            _parent = page;
            ServiceManager = serviceManager;
            _conversations = new ObservableCollection<Conversation>();
        }

        public async void LoadConversations()
        {
            var messages = await ServiceManager.GetConversations();
            LoadMessages(messages);
            _lastUpdate = DateTime.Now;

            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += UpdateMessages;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1.5);
            dispatcherTimer.Start();
        }

        public async void AddMessage(string text)
        {
            var reference = CurrentConversation;
            var id = await ServiceManager.WriteMessage(text, reference.InterlocutorId);
            if (id != null)
            {
                if (!Conversations.Contains(reference))
                {
                    Conversations.Insert(0, reference);
                }
                else
                {
                    Conversations.Move(Conversations.IndexOf(reference), 0);
                }

                reference.Messages.Add(new Message()
                {
                    Content = text,
                    IsAuthor = true,
                    Id = id
                });
            }
            _parent.MessagesListBox.ScrollIntoView(CurrentConversation.Messages[CurrentConversation.Messages.Count - 1]);
        }

        public async void SearchPossibleUsers(string text)
        {
            if (_isSearching)
                return;
            _isSearching = true;
            ClearSearchUsers();
            _canClear = false;

            var temp = new List<Conversation>();
            var possibleUsers = await ServiceManager.GetPossibleUsers(text);

            if (string.IsNullOrEmpty(_parent.SearchTextBox.Text))
            {
                _isSearching = false;
                return;
            }

            foreach (var conversation in Conversations)
            {
                conversation.IsActive = conversation.Username.StartsWith(text);
            }

            foreach (var user in possibleUsers)
            {
                if (Conversations.All(x => x.Username != user.Username))
                {
                    var conversation = new Conversation()
                    {
                        InterlocutorId = user.UserId,
                        Name = user.Name,
                        Username = user.Username,
                        Messages = new ObservableCollection<Message>(),
                        IsActive = true,
                        Color = new SolidColorBrush(Color.FromRgb((byte)_random.Next(30, 255),
                            (byte)_random.Next(30, 255), (byte)_random.Next(30, 255))),
                    };

                    Conversations.Add(conversation);
                    temp.Add(conversation);
                }
            }
            _tempList = temp;
            _parent.ConversationsListBox.Items.Refresh();
            _canClear = true;
            _isSearching = false;
        }

        public void ClearSearchUsers()
        {
            if (_canClear)
            {
                _canClear = false;
                foreach (var conversation in _tempList)
                {
                    Conversations.Remove(conversation);
                }

                foreach (var conversation in Conversations)
                {
                    conversation.IsActive = true;
                }

                _tempList.Clear();
                _tempList = null;
            }
        }

        private async void UpdateMessages(object sender, EventArgs eventArgs)
        {
            var messages = await ServiceManager.GetNewMessages(_lastUpdate);
            LoadMessages(messages);
            _lastUpdate = DateTime.Now;
        }

        private void LoadMessages(List<MessageDTO> messages)
        {
            messages.Sort((x, y) => x.SendDate.CompareTo(y.SendDate));
            foreach (var message in messages)
            {
                if (Conversations.All(x => x.Username != message.Username))
                {
                    var conversation = new Conversation()
                    {
                        InterlocutorId = message.UserId,
                        Name = message.Name,
                        Username = message.Username,
                        Messages = new ObservableCollection<Message>(),
                        IsActive = true,
                        Color = new SolidColorBrush(Color.FromRgb((byte)_random.Next(30, 255),
                            (byte)_random.Next(30, 255), (byte)_random.Next(30, 255))),
                    };

                    conversation.Messages.Add(new Message()
                    {
                        Content = message.Content,
                        Id = message.Id,
                        IsAuthor = message.IsAuthor
                    });

                    Conversations.Add(conversation);
                }
                else
                {
                    var conversation = Conversations.First(x => x.Username == message.Username);
                    if (conversation.Messages.All(x => x.Id != message.Id))
                    {
                        conversation.Messages.Add(new Message()
                        {
                            Content = message.Content,
                            Id = message.Id,
                            IsAuthor = message.IsAuthor
                        });
                    }
                }
            }
            _parent.MessagesListBox.Items.Refresh();
        }
    }
}
