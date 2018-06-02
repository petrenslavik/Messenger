using System;
using System.Windows;
using System.Windows.Controls;
using MessengerWindowsClient.Events;

namespace MessengerWindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        public Control RegisterPage
        {
            get { return (Control)this.GetValue(RegisterPageProperty); }
            set { this.SetValue(RegisterPageProperty, value); }
        }

        public Control LoginPage
        {
            get { return (Control)this.GetValue(LoginPageProperty); }
            set { this.SetValue(LoginPageProperty, value); }
        }

        public static readonly DependencyProperty RegisterPageProperty = DependencyProperty.Register(
            "RegisterPage", typeof(Control), typeof(WelcomePage), new PropertyMetadata(null));

        public static readonly DependencyProperty LoginPageProperty = DependencyProperty.Register(
            "LoginPage", typeof(Control), typeof(WelcomePage), new PropertyMetadata(null));

        public ChangePageEvent ChangePage;

        private void ToLoginPage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangePage(this,new ChangePageEventArgs(LoginPage,this,ChangePageDirection.Forward));
        }

        private void ToRegisterPage_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(this, new ChangePageEventArgs(RegisterPage, this, ChangePageDirection.Forward));
        }
    }
}
