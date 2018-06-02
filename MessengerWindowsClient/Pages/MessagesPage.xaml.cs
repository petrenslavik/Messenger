using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for MessagesPage.xaml
    /// </summary>
    public partial class MessagesPage : UserControl
    {
        private ObservableCollection<string> Conversations;
        public ObservableCollection<Message> Messages { get; set; }

        public MessagesPage()
        {
            InitializeComponent();
            Conversations = new ObservableCollection<string> {"Text", "Text","text","text","text","text","text","text","GHGH","fasfa","Afsaf"};
            Messages = new ObservableCollection<Message>
            {
                new Message() {Content = "Some text", IsAuthor = true},
                new Message() {Content = "Some text", IsAuthor = false},
                new Message() {Content = "Some text", IsAuthor = true},
                new Message() {Content = "Some text", IsAuthor = true},
                new Message() {Content = "Some text", IsAuthor = false},
                new Message() {Content = "Some text", IsAuthor = true}
            };
            ConversationsListBox.ItemsSource = Conversations;
            MessagesListBox.ItemsSource = Messages;
        }

        private void MessageTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (e.KeyboardDevice.Modifiers & ModifierKeys.Shift) != ModifierKeys.Shift)
            {
                Messages.Add(new Message() {Content = MessageTextBox.Text, IsAuthor = true});
                MessageTextBox.Clear();
                e.Handled = true;
            }
        }
    }

    public class Message
    {
        public string Content { get; set; }
        public bool IsAuthor { get; set; }
    }
}
