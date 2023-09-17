using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Client.Model
{
    public class UserData
    {
        private string _username;
        private string _password;
        private bool _authed = false;
        private string _status;
        private bool _pass;

        public string UserName 
            {
                set { _username = value; }
                get { return _username; }
            }
        public string Password
        {
            set { _password = value; }
            get => _password;
        }
        public bool Authed
        { 
            set { _authed = value; }
            get { return _authed; } 
        }
        public string Status
        {
            get => _status;
            set { _status = value; }
        }
        public bool Pass
        {
            set { _pass = value; }
            get { return _pass; }
        }
    }
}
