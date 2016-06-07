using FlowCOM_EXEC;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using Web.Logging;

namespace Web.FlowCOM
{
    public delegate void TExecuteCallBackEvent(string ActionFID, bool Success);

    internal static class FlowAPIObject
    {
        public static IFloCOM_EXEC FlowCOM_ApplicationInstance { get; private set; }
        public static Exception FlowCOM_ApplicationInstanceException { get; private set; }
        private static string _Path, _DataPath, _ConfigDatabase, _UID, _PWD, _Server;

        private static void InitializeVariables()
        {
            _Path = ConfigurationManager.AppSettings["FlowPath"];
            _DataPath = ConfigurationManager.AppSettings["FlowDataPath"];
            _ConfigDatabase = ConfigurationManager.AppSettings["FlowConfigurationDatabase"];
            _Server = ConfigurationManager.AppSettings["FlowServer"];
            _UID = ConfigurationManager.AppSettings["FlowLogin"];
            _PWD = ConfigurationManager.AppSettings["FlowPass"];
        }
        

        public static void Application_Start(object sender, EventArgs args)
        {
            
            StringBuilder log = new StringBuilder();
            log.AppendLine(String.Format("{0}.FlowCOM_Application_Start", typeof(FlowAPIObject)));
            log.AppendLine("Initializing Variables.");
            InitializeVariables();
            try
            {
                log.AppendLine("");
                log.AppendLine(String.Format("Instantiating {0}", typeof(FloCOM_EXEC)));
                IFloCOM_EXEC floCom = new FloCOM_EXEC();
                log.AppendLine(String.Format("Instantiated {0}", typeof(FloCOM_EXEC)));

                log.AppendLine("Calling FlowComPrefix");
                FlowComPrefix(floCom);
                log.AppendLine("Called FlowComPrefix");

                log.AppendLine("Applying instance to FlowAPIObject.FlowCOM_ApplicationInstance");
                FlowAPIObject.FlowCOM_ApplicationInstance = floCom;
                log.AppendLine("Applied instance to FlowAPIObject.FlowCOM_ApplicationInstance");

                log.AppendLine("Applying null to FlowAPIObject.FlowCOM_ApplicationInstanceException");
                FlowAPIObject.FlowCOM_ApplicationInstanceException = null;
                log.AppendLine("Applied null to FlowAPIObject.FlowCOM_ApplicationInstanceException");

                Log4NetManager.Instance.Info(typeof(FlowAPIObject), log);
            }
            catch (Exception ex)
            {
                log.AppendLine("");
                FlowAPIObject.FlowCOM_ApplicationInstanceException = ex;
                log.Append("Applying null to FlowAPIObject.FlowCOM_ApplicationInstance");
                FlowAPIObject.FlowCOM_ApplicationInstance = null;
                log.Append("Applied null to FlowAPIObject.FlowCOM_ApplicationInstance");

                Log4NetManager.Instance.Error(typeof(FlowAPIObject), log);
                Log4NetManager.Instance.Error(typeof(FlowAPIObject), ex);
            }
        }

        public static void Application_End(object sender, EventArgs args)
        {

            if (FlowAPIObject.FlowCOM_ApplicationInstance == null)
                return;

            StringBuilder log = new StringBuilder();
            log.AppendLine(String.Format("{0}.FlowCOM_Application_End", typeof(FlowAPIObject)));
            try
            {
                log.AppendLine(String.Format("{0} Calling Flo_Logout", typeof(FloCOM_EXEC)));
                bool result = FlowAPIObject.FlowCOM_ApplicationInstance.Flo_Logout();
                if (!result)
                {
                    log.AppendLine(String.Format("{0} FlowAPIObject.FlowCOM_ApplicationInstance.Flo_Logout Failed.", typeof(FloCOM_EXEC)));
                    throw new ApplicationException("FlowAPIObject.FlowCOM_ApplicationInstance.Flo_Logout Failed.");
                }

                log.AppendLine(String.Format("{0} Releasing: FlowAPIObject.FlowCOM_ApplicationInstance.", typeof(FloCOM_EXEC)));
                Marshal.FinalReleaseComObject(FlowAPIObject.FlowCOM_ApplicationInstance);
                log.AppendLine(String.Format("{0} Released: FlowAPIObject.FlowCOM_ApplicationInstance.", typeof(FloCOM_EXEC)));

                log.AppendLine(String.Format("{0} Nulling: FlowAPIObject.FlowCOM_ApplicationInstanceException.", typeof(FloCOM_EXEC)));
                FlowAPIObject.FlowCOM_ApplicationInstanceException = null;
                log.AppendLine(String.Format("{0} Nulled: FlowAPIObject.FlowCOM_ApplicationInstanceException.", typeof(FloCOM_EXEC)));

                Log4NetManager.Instance.Info(typeof(FlowAPIObject), log);
            }
            catch (Exception ex)
            {
                log.AppendLine("");
                FlowAPIObject.FlowCOM_ApplicationInstanceException = ex;
                log.Append("Applying null to FlowAPIObject.FlowCOM_ApplicationInstance");
                FlowAPIObject.FlowCOM_ApplicationInstance = null;
                log.Append("Applied null to FlowAPIObject.FlowCOM_ApplicationInstance");

                log.AppendLine(String.Format("{0} Releasing: FlowAPIObject.FlowCOM_ApplicationInstance.", typeof(FloCOM_EXEC)));
                Marshal.FinalReleaseComObject(FlowAPIObject.FlowCOM_ApplicationInstance);
                log.AppendLine(String.Format("{0} Released: FlowAPIObject.FlowCOM_ApplicationInstance.", typeof(FloCOM_EXEC)));

                log.AppendLine(String.Format("{0} Nulling: FlowAPIObject.FlowCOM_ApplicationInstanceException.", typeof(FloCOM_EXEC)));
                FlowAPIObject.FlowCOM_ApplicationInstanceException = null;
                log.AppendLine(String.Format("{0} Nulled: FlowAPIObject.FlowCOM_ApplicationInstanceException.", typeof(FloCOM_EXEC)));

                Log4NetManager.Instance.Error(typeof(FlowAPIObject), log);
                Log4NetManager.Instance.Error(typeof(FlowAPIObject), ex);
            }
        }

        internal static bool Logout()
        {
            return FlowAPIObject.FlowCOM_ApplicationInstance.Flo_Logout();
        }

        internal static bool Login(string FlowServer, string FlowConfigurationDatabase, string FlowUser, string FlowPass)
        {
            return FlowAPIObject.FlowCOM_ApplicationInstance.Flo_Login(FlowServer, FlowConfigurationDatabase, FlowUser, FlowPass);
        }

        internal static void FlowComPrefix(IFloCOM_EXEC floCom, bool doLogin = true)
        {
            bool result = false;

            result = floCom.Flo_SetApplicationPath(_Path);
            if (!result)
                throw new ApplicationException(String.Format("{0}.FlowComPrefix: floCom.Flo_SetApplicationPath(\"{1}\") failed.", typeof(FlowAPIObject).FullName, _Path));

            result = floCom.Flo_SetApplicationDataPath(_DataPath);
            if (!result)
                throw new ApplicationException(String.Format("{0}.FlowComPrefix: floCom.Flo_SetApplicationPath(\"{1}\") failed.", typeof(FlowAPIObject).FullName, _DataPath));

            result = floCom.Flo_IsLicenseOk();
            if (!result)
                throw new ApplicationException(String.Format("{0}.FlowComPrefix: floCom.Flo_IsLicenseOk() returned false.", typeof(FlowAPIObject).FullName, _DataPath));

            if (doLogin)
            {
                result = floCom.Flo_Login(_Server, _ConfigDatabase, _UID, _PWD);
                if (!result)
                {
                    string passProtected = new String('•', (_PWD != null) ? _PWD.Length : 0);

                    throw new ApplicationException(String.Format("{0}.FlowComPrefix: floCom.Flo_Login(\"{1}\", \"{2}\", \"{3}\", \"{4}\") failed.", typeof(FlowAPIObject).FullName, _Server, _ConfigDatabase, _UID, _PWD));
                }
            }
        }

        internal static bool IsLicenseOk()
        {
            return FlowAPIObject.FlowCOM_ApplicationInstance.Flo_IsLicenseOk();
        }

        internal static bool SetApplicationPath(string FlowPath)
        {
            return FlowAPIObject.FlowCOM_ApplicationInstance.Flo_SetApplicationPath(FlowPath);
        }

        internal static bool SetApplicationDataPath(string FlowAPIObjectPath)
        {
            return FlowAPIObject.FlowCOM_ApplicationInstance.Flo_SetApplicationDataPath(FlowAPIObjectPath);
        }

        internal static bool ExecuteActionSyncWithParams(string actionId, string inputParams, out string logFidAppScope)
        {

            return FlowAPIObject.FlowCOM_ApplicationInstance.Flo_ExecuteActionSyncWithParams(actionId, inputParams, out logFidAppScope);
        }

        internal static bool ExecuteActionResultWithParams(string actionId, string inputParams, out object resultStream)
        {

            string logFidAppScope;
            return FlowAPIObject.FlowCOM_ApplicationInstance.Flo_ExecuteActionResultWithParams(actionId, inputParams, out resultStream, out logFidAppScope);

        }

        internal static bool ExecuteSOAPRequestInWithLocals(string ActionFID, string Request, string InputParams, int apiCapacity, out string response)
        {
            bool result;
            string logFidAppScope;
            result = FlowAPIObject.FlowCOM_ApplicationInstance.Flo_ExecuteSOAPRequestInWithLocals(ActionFID, Request, InputParams, out response, out logFidAppScope);
            return result;

        }

        internal static bool ExecuteSOAPRequestIn(string ActionFID, string aRequest, out string response, out string logFidAppScope)
        {
            bool result;
            result = FlowAPIObject.FlowCOM_ApplicationInstance.Flo_ExecuteSOAPRequestIn(ActionFID, aRequest, out response, out logFidAppScope);
            return result;

        }

        internal static object AboutAPIEx(int apiCapacity, out string description)
        {
            object result;
            result = FlowAPIObject.FlowCOM_ApplicationInstance.Flo_AboutAPIEx(out description);
            return result;
        }

    }
}