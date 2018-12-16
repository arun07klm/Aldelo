using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aldelo.DatabaseInterface.BOL
{
    public class CompanyMenuListDto
    {
        public int CompanyMenuListId { get; set; }
        public int CompanyId { get; set; }
        public int MenuId { get; set; }
        public byte Status { get; set; }

        public virtual CompanyDto CompanyDto { get; set; }
        public virtual MenuDto Menu { get; set; }
    }
}
