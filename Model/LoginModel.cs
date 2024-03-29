using Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class LoginModel : NotifyBase
    {
        private String _userName;

        private String _password;

        public string UserName
        {
            get { return _userName; }  
            set { 
                _userName = value; 
                this.DoNotify();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.DoNotify();
            }
        }
    }
}
