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
using MessengerClient.Events;

namespace MessengerClient.Pages
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

        public event RegisterEvent RegisterReady;

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text;
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.SecurePassword;
            var email = EmailTextBox.Text;
            RegisterReady(this, new RegisterEventArgs(name, username, password, email));
        }
    }
}
