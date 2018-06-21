using System;
using System.Windows;
using MessengerWindowsClient.Events;
using MessengerWindowsClient.Managers;

namespace MessengerWindowsClient
{
    public partial class MainWindow : Window
    {
        private ServiceManager _serviceManager;

        public MainWindow()
        {
            InitializeComponent();
            this.RegisterPage.RegisterReady += RegisterUser;
            this.RegisterPage.ChangePage += ChangePage;
            this.WelcomePage.ChangePage += ChangePage;
            this.LoginPage.ChangePage += ChangePage;
            this.LoginPage.LoginReady += LoginUser;
            _serviceManager = new ServiceManager();
            this.MessagesPage.ServiceManager = _serviceManager;
            this.RegisterPage.ServiceManager = _serviceManager;
        }

        private void ChangePage(object sender, ChangePageEventArgs e)
        {
            switch (e.Direction)
            {
                case ChangePageDirection.Forward:
                    AnimationManager.AnimateForwardPage(e.NewPage, e.OldPage, Container, this.ActualWidth-20);
                    break;
                case ChangePageDirection.Backward:
                    AnimationManager.AnimateBackwardPage(e.NewPage, e.OldPage, Container, this.ActualWidth-20);
                    break;
            }
        }

        private async void RegisterUser(object sender, RegisterEventArgs e)
        {
            var isSucceed = await _serviceManager.RegisterUser(e.Name, e.Username, e.Password, e.Email);
            if (isSucceed)
                AnimationManager.AnimateForwardPage(MessagesPage, RegisterPage, Container, this.ActualWidth);
            else
            {
                RegisterPage.ValidationText.Text = "Wrong data or no connection.";
                RegisterPage.ValidationText.Visibility = Visibility.Visible;
            }
        }

        private async void LoginUser(object sender, LoginEventArgs e)
        {
            var isSucceed = await _serviceManager.Login(e.Username, e.Password);
            if (isSucceed)
                AnimationManager.AnimateForwardPage(MessagesPage, LoginPage, Container, this.ActualWidth);
            else
            {
                RegisterPage.ValidationText.Text = "Wrong credentials or no connection.";
                RegisterPage.ValidationText.Visibility = Visibility.Visible;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _serviceManager.Dispose();
        }
    }
}
