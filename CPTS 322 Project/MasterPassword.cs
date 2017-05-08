using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_322_Project
{
    public class MasterPassword
    {
        MasterPassword()
        {

        }

        public bool VerifyPassword(string pass)
        {
            if(pass == password)
            {
                return true;
            }
            return false;
        }

        public string Password { set { password = value; } }

        private string password;
    }
}
