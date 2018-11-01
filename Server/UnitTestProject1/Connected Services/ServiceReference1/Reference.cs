﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnitTestProject1.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ISenderService")]
    public interface ISenderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISenderService/AddSender", ReplyAction="http://tempuri.org/ISenderService/AddSenderResponse")]
        int AddSender(string cpr, string firstName, string lastName, string phoneNumber, string email, string address, string zipCode, string city);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISenderService/AddSender", ReplyAction="http://tempuri.org/ISenderService/AddSenderResponse")]
        System.Threading.Tasks.Task<int> AddSenderAsync(string cpr, string firstName, string lastName, string phoneNumber, string email, string address, string zipCode, string city);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISenderService/ClearDB", ReplyAction="http://tempuri.org/ISenderService/ClearDBResponse")]
        void ClearDB();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISenderService/ClearDB", ReplyAction="http://tempuri.org/ISenderService/ClearDBResponse")]
        System.Threading.Tasks.Task ClearDBAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISenderServiceChannel : UnitTestProject1.ServiceReference1.ISenderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SenderServiceClient : System.ServiceModel.ClientBase<UnitTestProject1.ServiceReference1.ISenderService>, UnitTestProject1.ServiceReference1.ISenderService {
        
        public SenderServiceClient() {
        }
        
        public SenderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SenderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SenderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SenderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int AddSender(string cpr, string firstName, string lastName, string phoneNumber, string email, string address, string zipCode, string city) {
            return base.Channel.AddSender(cpr, firstName, lastName, phoneNumber, email, address, zipCode, city);
        }
        
        public System.Threading.Tasks.Task<int> AddSenderAsync(string cpr, string firstName, string lastName, string phoneNumber, string email, string address, string zipCode, string city) {
            return base.Channel.AddSenderAsync(cpr, firstName, lastName, phoneNumber, email, address, zipCode, city);
        }
        
        public void ClearDB() {
            base.Channel.ClearDB();
        }
        
        public System.Threading.Tasks.Task ClearDBAsync() {
            return base.Channel.ClearDBAsync();
        }
    }
}