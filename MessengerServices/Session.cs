using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessengerServices
{
    public class Session
    {
        public byte[] AesKey { get; set; }
        public string Username { get; set; }
    }
}