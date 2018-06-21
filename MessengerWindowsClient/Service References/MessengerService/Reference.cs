﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MessengerWindowsClient.MessengerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MessengerService.IMessenger")]
    public interface IMessenger {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/SetEncryptedSessionKey", ReplyAction="http://tempuri.org/IMessenger/SetEncryptedSessionKeyResponse")]
        void SetEncryptedSessionKey(byte[] encryptedKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/SetEncryptedSessionKey", ReplyAction="http://tempuri.org/IMessenger/SetEncryptedSessionKeyResponse")]
        System.Threading.Tasks.Task SetEncryptedSessionKeyAsync(byte[] encryptedKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/RegisterUser", ReplyAction="http://tempuri.org/IMessenger/RegisterUserResponse")]
        string RegisterUser(string name, string username, string password, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/RegisterUser", ReplyAction="http://tempuri.org/IMessenger/RegisterUserResponse")]
        System.Threading.Tasks.Task<string> RegisterUserAsync(string name, string username, string password, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/Login", ReplyAction="http://tempuri.org/IMessenger/LoginResponse")]
        string Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/Login", ReplyAction="http://tempuri.org/IMessenger/LoginResponse")]
        System.Threading.Tasks.Task<string> LoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/WriteMessage", ReplyAction="http://tempuri.org/IMessenger/WriteMessageResponse")]
        string WriteMessage(string receiverId, string content);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/WriteMessage", ReplyAction="http://tempuri.org/IMessenger/WriteMessageResponse")]
        System.Threading.Tasks.Task<string> WriteMessageAsync(string receiverId, string content);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/GetAllMessages", ReplyAction="http://tempuri.org/IMessenger/GetAllMessagesResponse")]
        CommonLibrary.MessageDTO[] GetAllMessages();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/GetAllMessages", ReplyAction="http://tempuri.org/IMessenger/GetAllMessagesResponse")]
        System.Threading.Tasks.Task<CommonLibrary.MessageDTO[]> GetAllMessagesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/GetPossibleUsers", ReplyAction="http://tempuri.org/IMessenger/GetPossibleUsersResponse")]
        CommonLibrary.UserDTO[] GetPossibleUsers(string str);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/GetPossibleUsers", ReplyAction="http://tempuri.org/IMessenger/GetPossibleUsersResponse")]
        System.Threading.Tasks.Task<CommonLibrary.UserDTO[]> GetPossibleUsersAsync(string str);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/GetNewMessages", ReplyAction="http://tempuri.org/IMessenger/GetNewMessagesResponse")]
        CommonLibrary.MessageDTO[] GetNewMessages(System.DateTime updateDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/GetNewMessages", ReplyAction="http://tempuri.org/IMessenger/GetNewMessagesResponse")]
        System.Threading.Tasks.Task<CommonLibrary.MessageDTO[]> GetNewMessagesAsync(System.DateTime updateDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/IsUniqueUsername", ReplyAction="http://tempuri.org/IMessenger/IsUniqueUsernameResponse")]
        bool IsUniqueUsername(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessenger/IsUniqueUsername", ReplyAction="http://tempuri.org/IMessenger/IsUniqueUsernameResponse")]
        System.Threading.Tasks.Task<bool> IsUniqueUsernameAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessengerChannel : MessengerWindowsClient.MessengerService.IMessenger, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessengerClient : System.ServiceModel.ClientBase<MessengerWindowsClient.MessengerService.IMessenger>, MessengerWindowsClient.MessengerService.IMessenger {
        
        public MessengerClient() {
        }
        
        public MessengerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MessengerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessengerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessengerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SetEncryptedSessionKey(byte[] encryptedKey) {
            base.Channel.SetEncryptedSessionKey(encryptedKey);
        }
        
        public System.Threading.Tasks.Task SetEncryptedSessionKeyAsync(byte[] encryptedKey) {
            return base.Channel.SetEncryptedSessionKeyAsync(encryptedKey);
        }
        
        public string RegisterUser(string name, string username, string password, string email) {
            return base.Channel.RegisterUser(name, username, password, email);
        }
        
        public System.Threading.Tasks.Task<string> RegisterUserAsync(string name, string username, string password, string email) {
            return base.Channel.RegisterUserAsync(name, username, password, email);
        }
        
        public string Login(string username, string password) {
            return base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task<string> LoginAsync(string username, string password) {
            return base.Channel.LoginAsync(username, password);
        }
        
        public string WriteMessage(string receiverId, string content) {
            return base.Channel.WriteMessage(receiverId, content);
        }
        
        public System.Threading.Tasks.Task<string> WriteMessageAsync(string receiverId, string content) {
            return base.Channel.WriteMessageAsync(receiverId, content);
        }
        
        public CommonLibrary.MessageDTO[] GetAllMessages() {
            return base.Channel.GetAllMessages();
        }
        
        public System.Threading.Tasks.Task<CommonLibrary.MessageDTO[]> GetAllMessagesAsync() {
            return base.Channel.GetAllMessagesAsync();
        }
        
        public CommonLibrary.UserDTO[] GetPossibleUsers(string str) {
            return base.Channel.GetPossibleUsers(str);
        }
        
        public System.Threading.Tasks.Task<CommonLibrary.UserDTO[]> GetPossibleUsersAsync(string str) {
            return base.Channel.GetPossibleUsersAsync(str);
        }
        
        public CommonLibrary.MessageDTO[] GetNewMessages(System.DateTime updateDate) {
            return base.Channel.GetNewMessages(updateDate);
        }
        
        public System.Threading.Tasks.Task<CommonLibrary.MessageDTO[]> GetNewMessagesAsync(System.DateTime updateDate) {
            return base.Channel.GetNewMessagesAsync(updateDate);
        }
        
        public bool IsUniqueUsername(string username) {
            return base.Channel.IsUniqueUsername(username);
        }
        
        public System.Threading.Tasks.Task<bool> IsUniqueUsernameAsync(string username) {
            return base.Channel.IsUniqueUsernameAsync(username);
        }
    }
}
