using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    class User
    {

        public string FirstnameAt { get; set; }
        public string LastnameAt { get; set; }
        public string UserNameAt { get; set; }
        public string PasswordAt { get; set; }
        
        public User(string FirstnamePR,string LastnamePR,string UsernamePR,string PasswordPR)
        {
            this.FirstnameAt = FirstnamePR;
            this.LastnameAt = LastnamePR;
            this.UserNameAt = UsernamePR;
            this.PasswordAt = PasswordPR;
        }
    }
}
