﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelloWebClient.HelloWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HelloWebService.HelloWebServiceSoap")]
    public interface HelloWebServiceSoap {
        
        // CODEGEN: Generating message contract since element name name from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetMessage", ReplyAction="*")]
        HelloWebClient.HelloWebService.GetMessageResponse GetMessage(HelloWebClient.HelloWebService.GetMessageRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMessageRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetMessage", Namespace="http://tempuri.org/", Order=0)]
        public HelloWebClient.HelloWebService.GetMessageRequestBody Body;
        
        public GetMessageRequest() {
        }
        
        public GetMessageRequest(HelloWebClient.HelloWebService.GetMessageRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetMessageRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string name;
        
        public GetMessageRequestBody() {
        }
        
        public GetMessageRequestBody(string name) {
            this.name = name;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMessageResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetMessageResponse", Namespace="http://tempuri.org/", Order=0)]
        public HelloWebClient.HelloWebService.GetMessageResponseBody Body;
        
        public GetMessageResponse() {
        }
        
        public GetMessageResponse(HelloWebClient.HelloWebService.GetMessageResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetMessageResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetMessageResult;
        
        public GetMessageResponseBody() {
        }
        
        public GetMessageResponseBody(string GetMessageResult) {
            this.GetMessageResult = GetMessageResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface HelloWebServiceSoapChannel : HelloWebClient.HelloWebService.HelloWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HelloWebServiceSoapClient : System.ServiceModel.ClientBase<HelloWebClient.HelloWebService.HelloWebServiceSoap>, HelloWebClient.HelloWebService.HelloWebServiceSoap {
        
        public HelloWebServiceSoapClient() {
        }
        
        public HelloWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HelloWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        HelloWebClient.HelloWebService.GetMessageResponse HelloWebClient.HelloWebService.HelloWebServiceSoap.GetMessage(HelloWebClient.HelloWebService.GetMessageRequest request) {
            return base.Channel.GetMessage(request);
        }
        
        public string GetMessage(string name) {
            HelloWebClient.HelloWebService.GetMessageRequest inValue = new HelloWebClient.HelloWebService.GetMessageRequest();
            inValue.Body = new HelloWebClient.HelloWebService.GetMessageRequestBody();
            inValue.Body.name = name;
            HelloWebClient.HelloWebService.GetMessageResponse retVal = ((HelloWebClient.HelloWebService.HelloWebServiceSoap)(this)).GetMessage(inValue);
            return retVal.Body.GetMessageResult;
        }
    }
}
