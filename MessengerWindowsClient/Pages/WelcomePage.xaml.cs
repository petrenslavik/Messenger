using MessengerWindowsClient.Events;
using System.Windows;
using System.Windows.Controls;

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    [Page("Welcome")]
    public partial class WelcomePage : UserControl
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void ToLoginPage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Router.Instance.ChangePage("Login", ChangePageDirection.Forward);
        }

        private void ToRegisterPage_Click(object sender, RoutedEventArgs e)
        {
            Router.Instance.ChangePage("Register", ChangePageDirection.Forward);
        }
    }
}
