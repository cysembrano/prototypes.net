using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FlowAppInterface.Objects
{

    public delegate void TExecuteCallBackEvent(string ActionFID, bool Success);

    public static class FlowAPI
    {

        public static bool DoLogin(FlowConfigLogin configLogin)
        {
            try
            {
                String BinPath = @"C:\Program Files (x86)\Flow Software\";
                String DataPath = @"C:\ProgramData\Flow\";

                //String BinPath  = @"C:\Program Files\Flow Software\";
                //String DataPath = @"C:\Documents and Settings\All Users\Application Data\Flow\";

                //Set the location that your application will look for the FlowAPI.dll and its associated DLLs
                SetDllDirectory(BinPath);

                //you should first set the application path for the FlowAPI. By default it is the working directory of the application that hosts the FlowAPI.
                //in case of Web Applications this is the folder containing aspnet_wp.exe.  Set it to your FlowAPI application folder
                if (!FlowAPI.Flo_SetApplicationPath(BinPath))
                    throw new Exception("FlowAPI is unable to set its application data path.  Please check the path is correct");

                //you should first set the application data path for the FlowAPI. By default it is the working directory of the application that hosts the FlowAPI.
                //in case of Web Applications this is the folder containing aspnet_wp.exe.  Set it to your FlowAPI data folder
                if (!FlowAPI.Flo_SetApplicationDataPath(DataPath))
                    throw new Exception("FlowAPI is unable to set its application data path.  Please check the path is correct");

                //this license check is a helper.  if you remove it you will still not be 
                //able to use it without a valid license. this is because the API will do its own
                //license check internally.
                if (!FlowAPI.Flo_IsLicenseOk())
                    throw new Exception("FlowAPI is not licensed.  Please obtain a software license using Flow console");

                //change the below Flo_Login call to have the correct details for your configuration database.
                //Param1 = enter the host name of your sql server in the form <computername>\<instancename> (instance is optional) (For SQL Compact - leave Param1 blank)
                //Param2 = name of the database on the sql server (For SQL Compact - set to full file path including .sdf)
                //Param3 = SQL Server username - enter SQL server user name eg sa (Windows Authentication - leave blank to login using current user) 
                //Param4 = Password for above SQL server username - (Windows Authentication - leave blank to login using current user)
                Boolean result = FlowAPI.Flo_Login(configLogin.ServerName, configLogin.DatabaseName, configLogin.Username, configLogin.Password);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DoLogout()
        {
            try
            {
                Boolean result = FlowAPI.Flo_Logout();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);

        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SetApplicationPath(string Path);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SetApplicationDataPath(string Path);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_IsLicenseOk();
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_Login(string Hostname, string Database, string Username, string Password);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_Logout();
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_AboutAPI(string Description);
        [DllImport("FlowAPI.dll")]
        public static extern int Flo_AboutAPIEx(StringBuilder buffer, int length);

        [DllImport("FlowAPI.dll")]
        public static extern int Flo_ActionList(string Filter, StringBuilder buffer, int length);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_ExecuteAction(string ActionFID, TExecuteCallBackEvent CallBack);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_ExecuteActionSync(string ActionFID);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_ExecuteActionWithParams(string ActionFID, string InputParams, TExecuteCallBackEvent CallBack);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_ExecuteActionSyncWithParams(string ActionFID, string InputParams);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_ExecuteActionResultWithParams(string ActionFID, string InputParams, out Object ResultStream);//ResultStream is a OleVariant
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_ExecuteSOAPRequestOut(string ActionFID, string InputParams, StringBuilder aResponse, int aResponseSize);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_ExecuteSOAPRequestIn(string ActionFID, string aRequest, StringBuilder aResponse, int aResponseSize);

        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_TestDBCon(string DBFID);

        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveDBCon(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveFileCon(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveDBDef(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveFileDef(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveReport(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveMap(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveTransport(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveAction(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveTradingPartner(ref string aXML, ref string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_SaveLog(ref string aXML, ref string aXML2);


        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadDBCon(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadFileCon(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadDBDef(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadFileDef(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadReport(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadMap(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadTransport(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadAction(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadTradingPartner(string aFID, out string aXML, out string aXML2);
        [DllImport("FlowAPI.dll")]
        public static extern bool Flo_LoadLog(string aFID, out string aXML, out string aXML2);

    }
}
