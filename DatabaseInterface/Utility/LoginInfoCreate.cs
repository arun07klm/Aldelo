using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface.Utility
{
    public class LoginInfoCreate
    {
        public Guid Salt { get; set; }
        public string PasswordHash { get; set; }

        //salt value for validation
        public string SaltVal { get; set; }
    }
}
