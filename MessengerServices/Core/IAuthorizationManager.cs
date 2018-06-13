using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerServiceData.Entities;

namespace MessengerServices.Core
{
    public interface IAuthorizationManager
    {
        string GetUniqueAuthorizationToken(string username, string password, byte[] aesKey);
        string GetUniqueAuthorizationToken(User user, byte[] aesKey);
        Session GetCurrentSession(string token);
    }
}
