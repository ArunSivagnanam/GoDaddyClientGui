using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace ChatWindowManager.src
{
    public class Client
    {
        private string firstName;
        private string lastName;
        private string userName;
        private string email;
        private SecureString password;

        public string FirstName
        {
            get
            {
                return firstName;
            }
        }

        public Client(string userName)
        {
            this.userName = userName;
        }

        public string UserName
        {
            get
            {
                return userName;
            }
        }

    }
}
