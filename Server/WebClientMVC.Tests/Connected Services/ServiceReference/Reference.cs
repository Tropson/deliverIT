﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebClientMVC.Tests.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ISenderService")]
    public interface ISenderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISenderService/AddSender", ReplyAction="http://tempuri.org/ISenderService/AddSenderResponse")]
        int AddSender(object sender);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISenderService/AddSender", ReplyAction="http://tempuri.org/ISenderService/AddSenderResponse")]
        System.Threading.Tasks.Task<int> AddSenderAsync(object sender);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISenderService/ClearDB", ReplyAction="http://tempuri.org/ISenderService/ClearDBResponse")]
        void ClearDB();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISenderService/ClearDB", ReplyAction="http://tempuri.org/ISenderService/ClearDBResponse")]
        System.Threading.Tasks.Task ClearDBAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISenderServiceChannel : WebClientMVC.Tests.ServiceReference.ISenderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SenderServiceClient : System.ServiceModel.ClientBase<WebClientMVC.Tests.ServiceReference.ISenderService>, WebClientMVC.Tests.ServiceReference.ISenderService {
        
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
        
        public int AddSender(object sender) {
            return base.Channel.AddSender(sender);
        }
        
        public System.Threading.Tasks.Task<int> AddSenderAsync(object sender) {
            return base.Channel.AddSenderAsync(sender);
        }
        
        public void ClearDB() {
            base.Channel.ClearDB();
        }
        
        public System.Threading.Tasks.Task ClearDBAsync() {
            return base.Channel.ClearDBAsync();
        }
    }
}