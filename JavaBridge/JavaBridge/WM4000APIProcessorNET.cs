using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.ssaglobal.scm.wms.api.util;
namespace JavaBridge
{
    [Guid("9996F907-4968-4F4E-9E0A-EF078FD4199D"),
    ClassInterface(ClassInterfaceType.None),
    ComSourceInterfaces(typeof(WM4000APIProcessorEvents))]
    [ComVisible(true)]
    [ProgId("JavaBridge.WM4000APIProcessorNET")]
    public class WM4000APIProcessorNET : IWM4000APIProcessorNET
    {
        WM4000APIProcessor _processor;

        public string Init()
        {
            try
            {
                _processor = new WM4000APIProcessor();
                return "SUCCESS";
            }
            catch (Exception e)
            {
                return String.Format("ERROR: JavaBridge Instantiation. ({0})." , e.Message);
            }
            
        }

        public string Process(string callerIdentity, string typeofMessage, string action, string xmlFormattedMessage)
        {
            string returnString = string.Empty;
            try
            {
                returnString =  WM4000APIProcessor.sProcess(callerIdentity, typeofMessage, action, xmlFormattedMessage);
            }
            catch (Exception e)
            {
                               
                return String.Format("ERROR: JavaBridge Process Method. ({0}).", "test");
            }
            return returnString;
            
        }

    }
}
