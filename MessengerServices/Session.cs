using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessengerServiceData.Entities;

namespace MessengerServices
{
    public class Session
    {
        public byte[] AesKey { get; set; }
        public User User { get; set; }
    }
}