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
        void SetEncryptedSessionKey(byte[] encryptedKey);

        [OperationContract]
        string RegisterUser(string name, string username, string password, string email);

        [OperationContract]
        string Login(string username, string password);

        [OperationContract]
        string WriteMessage(string receiverId, string content);

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