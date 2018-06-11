using System;
using System.Collections.Generic;
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

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : UserControl
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        public event ChangePageEvent ChangePage;
        public event RegisterEvent RegisterReady;

        public Control WelcomePage
        {
            get { return (Control)this.GetValue(WelcomePageProperty); }
            set { this.SetValue(WelcomePageProperty, value); }
        }

        public static readonly DependencyProperty WelcomePageProperty = DependencyProperty.Register(
            "WelcomePage", typeof(Control), typeof(RegisterPage), new PropertyMetadata(null));

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text;
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Password;
            var email = EmailTextBox.Text;
            RegisterReady(this, new RegisterEventArgs(name, username, password, email));
        }

        private void BackButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangePage(this, new ChangePageEventArgs(WelcomePage, this, ChangePageDirection.Backward));
        }
    }
}
