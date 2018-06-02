using System;
using System.Windows;
using MessengerWindowsClient.Events;

namespace MessengerWindowsClient
{
    public partial class MainWindow : Window
    {
        private ServiceManager _serviceManager;
        private UIElement _currentPage;

        public MainWindow()
        {
            InitializeComponent();
            _currentPage = this.Container.Children[0];
            this.RegisterPage.RegisterReady += RegisterUser;
            this.RegisterPage.ChangePage += ChangePage;
            this.WelcomePage.ChangePage += ChangePage;
            this.LoginPage.ChangePage += ChangePage;
            _serviceManager = new ServiceManager();
        }

        private void ChangePage(object sender, ChangePageEventArgs e)
        {
            switch (e.Direction)
            {
                case ChangePageDirection.Forward:
                    AnimationManager.AnimateForwardPage(e.NewPage, e.OldPage, Container, this.ActualWidth);
                    break;
                case ChangePageDirection.Backward:
                    AnimationManager.AnimateBackwardPage(e.NewPage, e.OldPage, Container, this.ActualWidth);
                    break;
            }
        }

        private async void RegisterUser(object sender, RegisterEventArgs e)
        {
            var isSucceed = await _serviceManager.RegisterUser(e.Name, e.Username, e.Password.ToString(), e.Email);
            e.Password.Dispose();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _serviceManager.Dispose();
        }

        private void Container_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateLayout();
        }
    }
}
