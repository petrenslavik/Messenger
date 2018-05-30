using System;
using System.ServiceModel;
using System.Threading.Tasks;
using MessengerServiceData.Entities;

namespace MessengerServices
{
    [ServiceContract]
    public interface IMessenger
    {
        [OperationContract]
        Task<bool> RegisterUser(string name, string username, string password, string email);

        [OperationContract]
        bool FriendUser(int idFirst, int idSecond);

        [OperationContract]
        TimeSpan PingToServer(DateTime dateTime);

        [OperationContract]
        bool IsUniqueUsername(string username);
    }
}