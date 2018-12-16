using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aldelo.DatabaseInterface.BOL
{
   public class MenuDto
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Logo { get; set; }
        public string Style { get; set; }
        public byte Status { get; set; }
    }
}
