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

        private void BackButton_OnMouseLeftButtonDown_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangePage(this, new ChangePageEventArgs(WelcomePage, this, ChangePageDirection.Backward));
        }
    }
}
