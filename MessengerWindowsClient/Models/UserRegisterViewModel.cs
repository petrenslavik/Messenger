using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MessengerWindowsClient.Managers;

namespace MessengerWindowsClient.Models
{
    public class UserRegisterViewModel : IDataErrorInfo
    {
        public ServiceManager ServiceManager { get; set; }
        private Regex emailRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        private Regex passwordRegex = new Regex(@"\w{6,20}",RegexOptions.CultureInvariant);

        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (Name.Length==0)
                        {
                            error = "Name is required.";
                        }
                        break;
                    case "Username":
                        if (!(Username.Length >= 3 && Username.Length <= 15))
                        {
                            error = "Username length should be between 3 and 15.";
                        }
                        else if (!(ServiceManager.IsValidUsername(Username)))
                        {
                            error = "Username is occupied.";
                        }
                        break;
                    case "Email":
                        if (!emailRegex.Match(Email).Success)
                        {
                            error = "Invalid email.";
                        }
                        break;
                    case "Password":
                        if (!(Password.Length >= 6 && Password.Length <= 20))
                        {
                            error = "Password length should be between 6 and 20.";
                        }
                        else
                        {
                            var matchPassword = passwordRegex.Match(Password);
                            if (!matchPassword.Success)
                            {
                                error = "Only latin letters and digits are allowed for password.";
                            }
                        }
                        break;
                    case "RepeatPassword":
                        error = Password != RepeatPassword ? "Password aren't matches." : null;
                        break;
                }
                return error;
            }
        }

        public string Error { get; set; }

        public UserRegisterViewModel(ServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
        }
    }
}
