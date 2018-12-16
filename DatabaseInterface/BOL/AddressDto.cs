using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aldelo.DatabaseInterface.BOL
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }
}
