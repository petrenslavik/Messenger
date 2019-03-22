using MessengerWindowsClient.Events;
using MessengerWindowsClient.Managers;
using System;
using System.Windows;

namespace MessengerWindowsClient
{
    public partial class MainWindow : Window
    {
        private ServiceManager _serviceManager;
        private AnimationManager _animationManager;
        private Router _router;

        public MainWindow()
        {
            InitializeComponent();
            _router = Router.Instance;
            _serviceManager = new ServiceManager();
        }

        //private async void RegisterUser(object sender, RegisterEventArgs e)
        //{
        //    var isSucceed = await _serviceManager.RegisterUser(e.Name, e.Username, e.Password, e.Email);
        //    if (isSucceed)
        //    {
        //        _router.ChangePage("Messages", ChangePageDirection.Forward);
        //    }
        //    else
        //    {
        //        RegisterPage.ValidationText.Text = "Wrong data or no connection.";
        //        RegisterPage.ValidationText.Visibility = Visibility.Visible;
        //    }
        //}

        //private async void LoginUser(object sender, LoginEventArgs e)
        //{
        //    var isSucceed = await _serviceManager.Login(e.Username, e.Password);
        //    if (isSucceed)
        //    {
        //        _router.ChangePage("Messages", ChangePageDirection.Forward);
        //    }
        //    else
        //    {
        //        RegisterPage.ValidationText.Text = "Wrong credentials or no connection.";
        //        RegisterPage.ValidationText.Visibility = Visibility.Visible;
        //    }
        //}

        private void Window_Closed(object sender, EventArgs e)
        {
            _serviceManager.Dispose();
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            _animationManager = new AnimationManager(Container, ActualWidth - 20);
            _router.AnimationManager = _animationManager;
            _router.ChangePage("Welcome", ChangePageDirection.Forward);
        }
    }
}
