using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using MessengerServices.Core;

namespace MessengerServices.Managers
{
    public static class SessionManager
    {
        private static Dictionary<string, byte[]> _keys;
        private static IRequestHelper _requestHelper;

        static SessionManager()
        {
            _keys = new Dictionary<string, byte[]>();
            _requestHelper = new RequestHelper(); 
        }

        public static void AddKeyForCurrentClient(byte[] key)
        {
            _keys.Add(_requestHelper.GetClientIp(),key);
        }

        public static byte[] PopKeyForCurrentClient()
        {
            var ip = _requestHelper.GetClientIp();
            if (!_keys.ContainsKey(ip))
            {
                return null;
            }

            var key = _keys[ip];
            _keys.Remove(ip);
            return key;
        }

        public static byte[] GetKeyForCurrentClient()
        {
            var ip = _requestHelper.GetClientIp();
            if (!_keys.ContainsKey(ip))
            {
                return null;
            }

            return _keys[ip];
        }
    }
}