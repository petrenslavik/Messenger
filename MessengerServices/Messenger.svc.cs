using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using MessengerServiceData.DbContexts;
using MessengerServiceData.Entities;

namespace MessengerServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class MessengerService : IMessenger
    {

        public bool FriendUser(int idFirst, int idSecond)
        {
            using (var db = new UserContext())
            {
                var first = db.Users.Find(idFirst);
                var second = db.Users.Find(idSecond);
                var friendship = new Friendship {FirstUser = first,SecondUser = second,Status = 2};
                db.Friendships.Add(friendship);
                db.SaveChanges();
            }

            return true;
        }

        public bool IsUniqueUsername(string username)
        {
            using (var db = new UserContext())
            {
                var userRes = db.Users.FirstOrDefault(user => user.Username == username);
                return userRes == null;
            }
        }

        public TimeSpan PingToServer(DateTime dateTime)
        {
            return DateTime.Now - dateTime;
        }

        public async Task<bool> RegisterUser(string name, string username, string password, string email)
        {
            if (!IsUniqueUsername(username))
                return false;

            using (var db = new UserContext())
            {
                db.Users.Add(new User()
                {
                    Email = email,
                    EmailConfirmed = false,
                    Username = username,
                    PasswordHash = password,
                    Name = name
                });
                await db.SaveChangesAsync();
            }

            return true;
        }
    }
}
