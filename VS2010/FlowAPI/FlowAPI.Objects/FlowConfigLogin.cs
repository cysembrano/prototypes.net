using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowAppInterface.Objects
{
    public class FlowConfigLogin
    {
        private readonly string _serverName;
        private readonly string _databaseName;
        private readonly string _userName;
        private readonly string _passWord;

        public string ServerName
        {
            get { return _serverName; }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
        }

        public string Username
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _passWord; }
        }

        public FlowConfigLogin(string servername, string databasename, string username, string password)
        {
            _serverName = servername;
            _databaseName = databasename;
            _userName = username;
            _passWord = password;
        }

    }
}
