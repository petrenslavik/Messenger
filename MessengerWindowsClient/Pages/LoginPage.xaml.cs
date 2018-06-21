using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MessengerWindowsClient.Events;

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {

        public event ChangePageEvent ChangePage;
        public event LoginEvent LoginReady;

        public Control WelcomePage
        {
            get { return (Control)this.GetValue(WelcomePageProperty); }
            set { this.SetValue(WelcomePageProperty, value); }
        }

        public static readonly DependencyProperty WelcomePageProperty = DependencyProperty.Register(
            "WelcomePage", typeof(Control), typeof(LoginPage), new PropertyMetadata(null));

        public LoginPage()
        {
            InitializeComponent();
        }

        private void BackButton_OnMouseLeftButtonDown(object sender, RoutedEventArgs routedEventArgs)
        {
            ChangePage(this, new ChangePageEventArgs(WelcomePage, this, ChangePageDirection.Backward));
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
