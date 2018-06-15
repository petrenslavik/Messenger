using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using MessengerWindowsClient.Events;
using MessengerWindowsClient.Managers;
using MessengerWindowsClient.Models;

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for MessagesPage.xaml
    /// </summary>
    public partial class MessagesPage : UserControl
    {
        public ServiceManager ServiceManager
        {
            private get { return _serviceManager; }
            set
            {
                _serviceManager = value;
                _viewModel.ServiceManager = _serviceManager;
            }
        }

        private MessengerViewModel _viewModel;
        private ServiceManager _serviceManager;

        public MessagesPage()
        {
            InitializeComponent();
            _viewModel = new MessengerViewModel(ServiceManager, this);
            DataContext = _viewModel;
        }

        private void MessageTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (e.KeyboardDevice.Modifiers & ModifierKeys.Shift) != ModifierKeys.Shift)
            {
                _viewModel.AddMessage(MessageTextBox.Text);
                MessageTextBox.Clear();
                e.Handled = true;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text))
                _viewModel.SearchPossibleUsers(SearchTextBox.Text);
            else
            {
                _viewModel.ClearSearchUsers();
                ConversationsListBox.Items.Refresh();
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
                _viewModel.LoadConversations();
        }
    }
}
