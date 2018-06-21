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
using MessengerWindowsClient.Managers;
using MessengerWindowsClient.Models;

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : UserControl
    {
        public ServiceManager ServiceManager
        {
            get { return _serviceManager; }
            set { _serviceManager = value;
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
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(UsernameTextBox.Text) ||
                string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password) ||
                string.IsNullOrEmpty(PasswordRepeatTextBox.Password))
            {
                ValidationText.Visibility = Visibility.Visible;
                ValidationText.Text = "Please fill all fields.";
                return;
            }

            if(CheckError(model["Name"]))
                return;
            if (CheckError(model["Username"]))
                return;
            if (CheckError(model["Email"]))
                return;
            if (CheckError(model["Password"]))
                return;
            if (CheckError(model["RepeatPassword"]))
                return;

            var name = model.Name;
            var username = model.Username;
            var password = model.Password;
            var email = model.Email;
            RegisterReady(this, new RegisterEventArgs(name, username, password, email));
        }

        private void BackButton_OnMouseLeftButtonDown(object sender, RoutedEventArgs routedEventArgs)
        {
            ChangePage(this, new ChangePageEventArgs(WelcomePage, this, ChangePageDirection.Backward));
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
