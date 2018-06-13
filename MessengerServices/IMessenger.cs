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
        void SetEncryptedSessionKey(byte[] EncryptedKey);

        [OperationContract]
        string RegisterUser(string name, string username, string password, string email, byte[] iv);

        [OperationContract]
        bool FriendUser(int idFirst, int idSecond);

        [OperationContract]
        bool IsUniqueUsername(string username);

        [OperationContract]
        string Login(string username, string password, byte[] iv);
    }
}