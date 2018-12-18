using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aldelo.DatabaseInterface.BOL
{
   public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public int AccountId { get; set; }
        public DateTime? PasswordExpireOn { get; set; }
        public string DBFolderPath { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public byte Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual AccountDto AccountDto { get; set; }
        public virtual AddressDto AddressDto { get; set; }
       
    }
}
