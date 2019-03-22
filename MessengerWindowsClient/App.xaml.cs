using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MessengerWindowsClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Router _router;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _router = new Router();
        }
    }
}
