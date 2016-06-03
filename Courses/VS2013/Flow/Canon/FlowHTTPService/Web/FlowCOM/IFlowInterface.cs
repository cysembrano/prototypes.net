using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.FlowCOM
{
    public interface IFlowInterface
    {
        bool Flo_AboutAPIEx(out string aBuffer);
        bool Flo_ActionList(string Filter, out string aBuffer);
        bool Flo_ExecuteActionResultWithParams(string ActionFID, string InputParams, out object aResultStream, out string aLogFID);
        bool Flo_ExecuteActionSync(string ActionFID, out string aLogFID);
        bool Flo_ExecuteActionSyncWithParams(string ActionFID, string InputParams, out string aLogFID);
        bool Flo_ExecuteSOAPRequestIn(string ActionFID, string aRequest, out string aResponse, out string aLogFID);
        bool Flo_ExecuteSOAPRequestInWithLocals(string ActionFID, string aRequest, string aLocals, out string aResponse, out string aLogFID);
        bool Flo_ExecuteSOAPRequestInWithParams(string ActionFID, string aRequest, string aInputParams, out string aResponse, out string aLogFID);
        bool Flo_ExecuteSOAPRequestOut(string ActionFID, string InputParams, out string aResponse, out string aLogFID);
        bool Flo_FreeOleVariantPointer(object aResultStream);
        bool Flo_IsLicenseOk();
        bool Flo_Login(string Hostname, string Databasename, string Username, string Password);
        bool Flo_Logout();
        bool Flo_SetApplicationDataPath(string Path);
        bool Flo_SetApplicationPath(string Path);
        bool Flo_ShowLog(string aLogFID);
    }
}