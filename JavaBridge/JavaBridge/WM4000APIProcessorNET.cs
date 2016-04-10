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

        public void Init()
        {
            try
            {
                _processor = new WM4000APIProcessor();
            }
            catch (Exception e)
            {
                MessageBox.Show("Init() Error: " + e.Message);
            }
            
        }

        public string Process(string callerIdentity, string typeofMessage, string action, string xmlFormattedMessage)
        {
            string returnString = string.Empty;
            try
            {
                returnString =  _processor.process(callerIdentity, typeofMessage, action, xmlFormattedMessage);
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Parameters [ caller: {0} | msgtype: {1}  | action: {2} | msg: {3} ] Process() Error: " , callerIdentity, typeofMessage, action, xmlFormattedMessage, e.Message));
            }
            return returnString;
            
        }

    }
}
