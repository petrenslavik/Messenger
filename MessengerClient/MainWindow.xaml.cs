using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessengerClient.Events;

namespace MessengerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceManager _serviceManager;
        private UIElement _currentPage;

        public MainWindow()
        {
            InitializeComponent();
            _currentPage = this.Container.Children[0];
            this.WelcomePage.ToLoginPage.Click += ToLoginPage;
            this.WelcomePage.ToRegisterPage.Click += ToRegisterPage;
            this.RegisterPage.RegisterReady += RegisterUser;
            _serviceManager = new ServiceManager();
        }

        private void ToRegisterPage(object sender, RoutedEventArgs e)
        {
            AnimationManager.AnimateNextPage(RegisterPage,(Control)_currentPage,Container);
            _currentPage = RegisterPage;
        }

        private void ToLoginPage(object sender, RoutedEventArgs e)
        {
            AnimationManager.AnimateNextPage(LoginPage,(Control)_currentPage,Container);
            _currentPage = LoginPage;
        }

        private async void RegisterUser(object sender, RegisterEventArgs e)
        {
            var isSucceed = await _serviceManager.RegisterUser(e.Name, e.Username, e.Password.ToString(), e.Email);
            e.Password.Dispose();
        }

        private void BackArrow_MouseEnter(object sender, MouseEventArgs e)
        {
            var myEffect = new DropShadowEffect()
            {
                BlurRadius = 5,
                Color = Colors.Gray,
                RenderingBias = RenderingBias.Quality
            };
            BackArrow.Effect = myEffect;
            BackArrow.Opacity = 1;
        }

        private void BackArrow_MouseLeave(object sender, MouseEventArgs e)
        {
            BackArrow.Effect = null;
            BackArrow.Opacity = 0.5;
        }

        private void BackArrow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var effect = new DropShadowEffect()
            {
                BlurRadius = 15,
                Color = Colors.Cornsilk,
                ShadowDepth = 0
            };
            BackArrow.Effect = effect;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _serviceManager.Dispose();
        }
    }
}
