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
        byte[] GetEncryptedSessionKey(byte[] exponent, byte[] modulus);

        [OperationContract]
        bool RegisterUser(string name, string username, string password, string email, byte[] iv);

        [OperationContract]
        bool FriendUser(int idFirst, int idSecond);

        [OperationContract]
        bool IsUniqueUsername(string username);

        [OperationContract]
        bool Login(string username, string password, byte[] iv);
    }
}