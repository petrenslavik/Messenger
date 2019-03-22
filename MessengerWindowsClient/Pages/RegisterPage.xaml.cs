using MessengerWindowsClient.Events;
using MessengerWindowsClient.Managers;
using MessengerWindowsClient.Models;
using System.Windows;
using System.Windows.Controls;

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    [Page("Register")]
    public partial class RegisterPage : UserControl
    {
        public ServiceManager ServiceManager
        {
            get { return _serviceManager; }
            set
            {
                _serviceManager = value;
                model.ServiceManager = value;
            }
        }

        private UserRegisterViewModel model;
        private ServiceManager _serviceManager;

        public RegisterPage()
        {
            InitializeComponent();
            model = new UserRegisterViewModel(ServiceManager);
            this.DataContext = model;
        }

        public event RegisterEvent RegisterReady;

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(UsernameTextBox.Text) ||
                string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password) ||
                string.IsNullOrEmpty(PasswordRepeatTextBox.Password))
            {
                ValidationText.Visibility = Visibility.Visible;
                ValidationText.Text = "Please fill all fields.";
                return;
            }

            if (CheckError(model["Name"]))
            {
                return;
            }

            if (CheckError(model["Username"]))
            {
                return;
            }

            if (CheckError(model["Email"]))
            {
                return;
            }

            if (CheckError(model["Password"]))
            {
                return;
            }

            if (CheckError(model["RepeatPassword"]))
            {
                return;
            }

            var name = model.Name;
            var username = model.Username;
            var password = model.Password;
            var email = model.Email;
            RegisterReady(this, new RegisterEventArgs(name, username, password, email));
        }

        private void BackButton_OnMouseLeftButtonDown(object sender, RoutedEventArgs routedEventArgs)
        {
            Router.Instance.ChangePage("Welcome", ChangePageDirection.Backward);
        }

        private bool CheckError(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ValidationText.Visibility = Visibility.Visible;
                ValidationText.Text = error;
                return true;
            }

            return false;
        }
    }
}
