using Aldelo.DatabaseInterface.Entity;
using DatabaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aldelo.DatabaseInterface.DAL
{
    public class CompanyMenuListDal : RepositoryBase<CompanyMenuList>
    {
        public bool RemoveAllCompanyMenu(int companyId)
        {
            try
            {
                Delete(x => x.CompanyId == companyId);
            }
            catch(Exception ee)
            {
                return false;
            }
            return true;
        }
    }
}
