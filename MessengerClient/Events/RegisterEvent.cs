using System;
using System.Security;

namespace MessengerClient.Events
{
    public delegate void RegisterEvent(object sender, RegisterEventArgs e);

    public class RegisterEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public SecureString Password { get; set; }
        public string Email { get; set; }

        public RegisterEventArgs(string name, string username, SecureString password, string email)
        {
            Name = name;
            Username = username;
            Password = password;
            Email = email;
        }
    }
}