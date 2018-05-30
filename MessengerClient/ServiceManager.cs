using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient
{
    public class ServiceManager
    {
        private MessengerService.MessengerClient _client;

        public ServiceManager()
        {
            _client = new MessengerService.MessengerClient();
        }

        public Task<bool> RegisterUser(string name, string username, string password, string email)
        {
            return _client.RegisterUserAsync(name, username, password, email);
        }

        public void Dispose()
        {
            _client.Close();
        }
    }
}
