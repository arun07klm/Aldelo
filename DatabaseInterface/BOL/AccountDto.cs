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
        public string passwordHash { get; set; }
        public Nullable<System.Guid> passwordSalt { get; set; }
        public string AuthCode { get; set; }
    }
}
