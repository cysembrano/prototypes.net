//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Requires RODL file FloBaseTypes (C:\dev\flow\trunk\rodl\FloBaseTypes.rodl) in the same namespace.
// Requires RODL file FloServerActions (C:\dev\flow\trunk\rodl\FloServerActions.rodl) in the same namespace.
namespace FlowMonitor {
    using System;
    using System.Collections.Generic;
    using RemObjects.SDK;
    using RemObjects.SDK.Types;
    using RemObjects.SDK.Server;
    using RemObjects.SDK.Server.ClassFactories;
    using FloBaseTypes;

    
    
    [RemObjects.SDK.Server.Invoker()]
    [System.Reflection.ObfuscationAttribute(Exclude=true)]
    public class MonitorAdmin_Invoker {
        
        public MonitorAdmin_Invoker() : 
                base() {
        }
        
        public static void Invoke_SetActionStatus(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(2);
            try {
                string aServiceId = @__Message.ReadAnsiString("aServiceId");
                int aIndex = @__Message.ReadInt32("aIndex");
                TFloScheduledType aActionType = ((TFloScheduledType)(@__Message.Read("aActionType", typeof(TFloScheduledType), RemObjects.SDK.StreamingFormat.Default)));
                TFloActionSettingType aStatus = ((TFloActionSettingType)(@__Message.Read("aStatus", typeof(TFloActionSettingType), RemObjects.SDK.StreamingFormat.Default)));
                @__ObjectDisposer.Add(aActionType);
                @__ObjectDisposer.Add(aStatus);
                bool Result;
                Result = ((IMonitorAdmin)(@__Instance)).SetActionStatus(aServiceId, aIndex, aActionType, aStatus);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "SetActionStatusResponse");
                @__Message.WriteBoolean("Result", Result);
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_AddTransformError(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aID = @__Message.ReadAnsiString("aID");
            string aURL = @__Message.ReadAnsiString("aURL");
            string aQuestion = @__Message.ReadAnsiString("aQuestion");
            ((IMonitorAdmin)(@__Instance)).AddTransformError(aServiceId, aID, aURL, aQuestion);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "AddTransformErrorResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_HandleTransformError(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aID = @__Message.ReadAnsiString("aID");
            ((IMonitorAdmin)(@__Instance)).HandleTransformError(aServiceId, aID);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "HandleTransformErrorResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_RemoveTransformError(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aID = @__Message.ReadAnsiString("aID");
            ((IMonitorAdmin)(@__Instance)).RemoveTransformError(aServiceId, aID);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "RemoveTransformErrorResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_IsServerStarted(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).IsServerStarted(aServiceId);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "IsServerStartedResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_IsMonitorStarted(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).IsMonitorStarted();
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "IsMonitorStartedResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_RefreshActions(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).RefreshActions(aServiceId);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "RefreshActionsResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_StartFlowServer(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).StartFlowServer(aServiceId);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "StartFlowServerResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_StopFlowServer(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).StopFlowServer(aServiceId);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "StopFlowServerResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_StopFlowServerEx(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceID = @__Message.ReadAnsiString("aServiceID");
            string aUserID = @__Message.ReadAnsiString("aUserID");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).StopFlowServerEx(aServiceID, aUserID);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "StopFlowServerExResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_KillFlowServer(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).KillFlowServer(aServiceId);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "KillFlowServerResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_Login(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aUserId = @__Message.ReadAnsiString("aUserId");
            string Result;
            Result = ((IMonitorAdmin)(@__Instance)).Login(aUserId);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "LoginResponse");
            @__Message.WriteAnsiString("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_Logout(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            ((IMonitorAdmin)(@__Instance)).Logout();
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "LogoutResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_ShowServerInfo(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aMessage = @__Message.ReadAnsiString("aMessage");
            ((IMonitorAdmin)(@__Instance)).ShowServerInfo(aServiceId, aMessage);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "ShowServerInfoResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_ShowServerError(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aMessage = @__Message.ReadAnsiString("aMessage");
            ((IMonitorAdmin)(@__Instance)).ShowServerError(aServiceId, aMessage);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "ShowServerErrorResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_ShowLicenseError(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aMessage = @__Message.ReadAnsiString("aMessage");
            ((IMonitorAdmin)(@__Instance)).ShowLicenseError(aServiceId, aMessage);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "ShowLicenseErrorResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_SetServerMenuOptions(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            ((IMonitorAdmin)(@__Instance)).SetServerMenuOptions(aServiceId);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "SetServerMenuOptionsResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_GetScheduleActions(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                string aServiceId = @__Message.ReadAnsiString("aServiceId");
                bool aShowDisabled = @__Message.ReadBoolean("aShowDisabled");
                string[][] Result;
                Result = ((IMonitorAdmin)(@__Instance)).GetScheduleActions(aServiceId, aShowDisabled);
                @__ObjectDisposer.Add(Result);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "GetScheduleActionsResponse");
                @__Message.Write("Result", Result, typeof(string[][]), RemObjects.SDK.StreamingFormat.Default);
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_GetMonitorActions(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                string aServiceId = @__Message.ReadAnsiString("aServiceId");
                bool aShowDisabled = @__Message.ReadBoolean("aShowDisabled");
                string[][] Result;
                Result = ((IMonitorAdmin)(@__Instance)).GetMonitorActions(aServiceId, aShowDisabled);
                @__ObjectDisposer.Add(Result);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "GetMonitorActionsResponse");
                @__Message.Write("Result", Result, typeof(string[][]), RemObjects.SDK.StreamingFormat.Default);
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_GetExecutingActions(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                string aServiceId = @__Message.ReadAnsiString("aServiceId");
                bool aShowDisabled = @__Message.ReadBoolean("aShowDisabled");
                string[][] Result;
                Result = ((IMonitorAdmin)(@__Instance)).GetExecutingActions(aServiceId, aShowDisabled);
                @__ObjectDisposer.Add(Result);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "GetExecutingActionsResponse");
                @__Message.Write("Result", Result, typeof(string[][]), RemObjects.SDK.StreamingFormat.Default);
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_GetTransportActions(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                string aServiceID = @__Message.ReadAnsiString("aServiceID");
                bool aShowDisabled = @__Message.ReadBoolean("aShowDisabled");
                string[][] Result;
                Result = ((IMonitorAdmin)(@__Instance)).GetTransportActions(aServiceID, aShowDisabled);
                @__ObjectDisposer.Add(Result);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "GetTransportActionsResponse");
                @__Message.Write("Result", Result, typeof(string[][]), RemObjects.SDK.StreamingFormat.Default);
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_GetPendingFiles(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                string aServiceID = @__Message.ReadAnsiString("aServiceID");
                string[][] Result;
                Result = ((IMonitorAdmin)(@__Instance)).GetPendingFiles(aServiceID);
                @__ObjectDisposer.Add(Result);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "GetPendingFilesResponse");
                @__Message.Write("Result", Result, typeof(string[][]), RemObjects.SDK.StreamingFormat.Default);
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_GetServices(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                FloBaseTypes.ServiceInfo[] Result;
                Result = ((IMonitorAdmin)(@__Instance)).GetServices();
                @__ObjectDisposer.Add(Result);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "GetServicesResponse");
                @__Message.Write("Result", Result, typeof(FloBaseTypes.ServiceInfo[]), RemObjects.SDK.StreamingFormat.Default);
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_RegisterService(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                string aServiceId = @__Message.ReadAnsiString("aServiceId");
                string aDisplayName = @__Message.ReadAnsiString("aDisplayName");
                string aPort = @__Message.ReadAnsiString("aPort");
                ServiceConnection aCon = ((ServiceConnection)(@__Message.Read("aCon", typeof(ServiceConnection), RemObjects.SDK.StreamingFormat.Default)));
                string aServiceUser = @__Message.ReadAnsiString("aServiceUser");
                string aServiceUserPass = @__Message.ReadAnsiString("aServiceUserPass");
                @__ObjectDisposer.Add(aCon);
                ((IMonitorAdmin)(@__Instance)).RegisterService(aServiceId, aDisplayName, aPort, aCon, aServiceUser, aServiceUserPass);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "RegisterServiceResponse");
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_UnRegisterService(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).UnRegisterService(aServiceId);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "UnRegisterServiceResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_ChangeService(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aDisplayName = @__Message.ReadAnsiString("aDisplayName");
            string aPort = @__Message.ReadAnsiString("aPort");
            string aServiceUser = @__Message.ReadAnsiString("aServiceUser");
            string aServiceUserPass = @__Message.ReadAnsiString("aServiceUserPass");
            ((IMonitorAdmin)(@__Instance)).ChangeService(aServiceId, aDisplayName, aPort, aServiceUser, aServiceUserPass);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "ChangeServiceResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_ChangeServiceUser(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aDisplayName = @__Message.ReadAnsiString("aDisplayName");
            string aPort = @__Message.ReadAnsiString("aPort");
            string aServiceUser = @__Message.ReadAnsiString("aServiceUser");
            string aServiceUserPass = @__Message.ReadAnsiString("aServiceUserPass");
            ((IMonitorAdmin)(@__Instance)).ChangeServiceUser(aServiceId, aDisplayName, aPort, aServiceUser, aServiceUserPass);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "ChangeServiceUserResponse");
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
        }
        
        public static void Invoke_ChangeServiceConnection(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                string aServiceId = @__Message.ReadAnsiString("aServiceId");
                ServiceConnection aCon = ((ServiceConnection)(@__Message.Read("aCon", typeof(ServiceConnection), RemObjects.SDK.StreamingFormat.Default)));
                @__ObjectDisposer.Add(aCon);
                ((IMonitorAdmin)(@__Instance)).ChangeServiceConnection(aServiceId, aCon);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "ChangeServiceConnectionResponse");
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roNoResponse;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_CheckServiceConnection(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            RemObjects.SDK.ObjectDisposer @__ObjectDisposer = new RemObjects.SDK.ObjectDisposer(1);
            try {
                string aServiceId = @__Message.ReadAnsiString("aServiceId");
                ServiceConnection aCon = ((ServiceConnection)(@__Message.Read("aCon", typeof(ServiceConnection), RemObjects.SDK.StreamingFormat.Default)));
                @__ObjectDisposer.Add(aCon);
                bool Result;
                Result = ((IMonitorAdmin)(@__Instance)).CheckServiceConnection(aServiceId, aCon);
                @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "CheckServiceConnectionResponse");
                @__Message.WriteBoolean("Result", Result);
                @__Message.FinalizeMessage();
                @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
            }
            finally {
                @__ObjectDisposer.Dispose();
            }
        }
        
        public static void Invoke_CheckServiceUser(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aServiceUser = @__Message.ReadAnsiString("aServiceUser");
            string aServiceUserPass = @__Message.ReadAnsiString("aServiceUserPass");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).CheckServiceUser(aServiceId, aServiceUser, aServiceUserPass);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "CheckServiceUserResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_CheckServicePort(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceId = @__Message.ReadAnsiString("aServiceId");
            string aPort = @__Message.ReadAnsiString("aPort");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).CheckServicePort(aServiceId, aPort);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "CheckServicePortResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_CheckUserLogonServiceRight(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceUser = @__Message.ReadAnsiString("aServiceUser");
            string aServiceUserPass = @__Message.ReadAnsiString("aServiceUserPass");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).CheckUserLogonServiceRight(aServiceUser, aServiceUserPass);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "CheckUserLogonServiceRightResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
        
        public static void Invoke_SetUserLogonServiceRight(RemObjects.SDK.IROService @__Instance, RemObjects.SDK.IMessage @__Message, RemObjects.SDK.Server.IServerChannelInfo @__ServerChannelInfo, out RemObjects.SDK.Server.ResponseOptions @__oResponseOptions) {
            string aServiceUser = @__Message.ReadAnsiString("aServiceUser");
            string aServiceUserPass = @__Message.ReadAnsiString("aServiceUserPass");
            bool Result;
            Result = ((IMonitorAdmin)(@__Instance)).SetUserLogonServiceRight(aServiceUser, aServiceUserPass);
            @__Message.InitializeResponseMessage(@__ServerChannelInfo, "FlowMonitor", "MonitorAdmin", "SetUserLogonServiceRightResponse");
            @__Message.WriteBoolean("Result", Result);
            @__Message.FinalizeMessage();
            @__oResponseOptions = RemObjects.SDK.Server.ResponseOptions.roDefault;
        }
    }
    
    [RemObjects.SDK.Activator()]
    [System.Reflection.ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    public class MonitorAdmin_Activator : object, RemObjects.SDK.Server.IServiceActivator {
        
        public MonitorAdmin_Activator() {
        }
        
        public RemObjects.SDK.IROService CreateInstance() {
            return new MonitorAdmin();
        }
    }
}