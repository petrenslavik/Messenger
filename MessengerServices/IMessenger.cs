using System;
using System.Collections.Generic;
using System.ServiceModel;
using CommonLibrary;

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
        string Login(string username, string password, byte[] iv);

        [OperationContract]
        string WriteMessage(string receiverId, string content, byte[] iv);

        [OperationContract]
        List<MessageDTO> GetAllMessages();

        [OperationContract]
        List<UserDTO> GetPossibleUsers(string str);

        [OperationContract]
        List<MessageDTO> GetNewMessages(DateTime updateDate);

        [OperationContract]
        bool IsUniqueUsername(string username);
    }
}