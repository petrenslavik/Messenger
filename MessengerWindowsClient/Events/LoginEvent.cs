using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerWindowsClient.Events
{
    public delegate void LoginEvent(object sender, LoginEventArgs e);

    public class LoginEventArgs : EventArgs
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginEventArgs(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
