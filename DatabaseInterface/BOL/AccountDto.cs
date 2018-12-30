using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aldelo.DatabaseInterface.BOL
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string PasswordHash { get; set; }
        public Nullable<System.Guid> PasswordSalt { get; set; }
        public string AuthCode { get; set; }
    }
}
