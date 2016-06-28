using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CyComObj
{
    [Guid("55DD33FB-DFF6-4384-AD99-4BE9C81EFECC"),
    ClassInterface(ClassInterfaceType.None),
    ComSourceInterfaces(typeof(DBComEvents))]
    [ComVisible(true)]
    [ProgId("CyComObj.DBComObject")]
    public class DBComObject : IDBComObject
    {
        SqlDataReader myReader = null;
        private SqlConnection myConnection = null;
  
        public void Init()
        {
            try
            {
                string conn = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\SRC\github\prototypes.git\trunk\ComInterop\CyComObj\MyComDatabase.mdf;Integrated Security=True";
                myConnection = new SqlConnection(conn);
                myConnection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public bool ExecuteSelectCommand(string selCommand)
        {
            if (myReader != null)
                myReader.Close();

            SqlCommand myCommand = new SqlCommand(selCommand);
            myCommand.Connection = myConnection;
            myCommand.ExecuteNonQuery();
            myReader = myCommand.ExecuteReader();
            return true;
        }

        public bool NextRow()
        {
            if (!myReader.Read())
            {
                myReader.Close();
                return false;
            }
            return true;
        }

        public string GetColumnData(int pos)
        {
            Object obj = myReader.GetValue(pos);
            if (obj == null) return "";
            return obj.ToString();
        }

        public void ExecuteNonSelectCommand(string insCommand)
        {
            SqlCommand myCommand = new SqlCommand(insCommand, myConnection);
            try
            {
                int retRows = myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
    }
}
