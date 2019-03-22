using MessengerWindowsClient.Events;
using System.Windows;
using System.Windows.Controls;

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    [Page("Login")]
    public partial class LoginPage : UserControl
    {
        public event LoginEvent LoginReady;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void BackButton_OnMouseLeftButtonDown(object sender, RoutedEventArgs routedEventArgs)
        {
            Router.Instance.ChangePage("Welcome", ChangePageDirection.Backward);
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                ValidationText.Visibility = Visibility.Visible;
                ValidationText.Text = "Please fill all fields.";
                return;
            }
            LoginReady(this, new LoginEventArgs(UsernameTextBox.Text, PasswordTextBox.Password));
        }
    }
}
