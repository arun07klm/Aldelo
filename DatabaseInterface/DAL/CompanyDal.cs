using Aldelo.DatabaseInterface.BOL;
using Aldelo.DatabaseInterface.Entity;
using DatabaseInterface.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    public class CompanyDal : RepositoryBase<Company>
    {
        public List<CompanyDto> GetAllCompanyDal()
        {
            var companyDtoList = new List<CompanyDto>();
            var companyList = Find(x => x.Status == (byte)AldeloEnums.RecordStatus.ACTIVE);
            foreach (var company in companyList)
            {
                var companyDto = new CompanyDto { AddressDto = new AddressDto() };
                CopyTocompanyDto(companyDto, company);
                companyDtoList.Add(companyDto);
            }
            return companyDtoList;
        }
        public bool SaveCompanyDal(CompanyDto companyDto)
        {
            try
            {
                var company = new Company { Account = new Account(), Address = new Address(), CompanyMenuList = new List<CompanyMenuList>() };
                CreateLoginInfo(companyDto);
                CopyFromCompanyDto(company, companyDto);
                Add(company);
                Save();
            }
            catch (Exception ee)
            {
                return false;

            }

            return true;
        }
        public bool EditCompanyDal(CompanyDto companyDto)
        {
            try
            {
                var company = FirstOrDefault(x => x.CompanyId == companyDto.CompanyId);

                CopyFromCompanyDto(company, companyDto);
                Update(company, company.CompanyId);
            }
            catch (Exception ee)
            {
                return false;

            }

            return true;
        }
        public CompanyDto GetCompanyByIdDal(int id)
        {
            var companyDto = new CompanyDto { AddressDto = new AddressDto(), MenuListDto = new List<MenuDto>() };
            var company = FirstOrDefault(x => x.CompanyId == id);
            if (company != null) CopyTocompanyDto(companyDto, company);
            else companyDto = null;
            return companyDto;
        }
        public bool UpdatePasswordDal(int companyId, string password)
        {
            bool bRet = false;
            try
            {
                var company = FirstOrDefault(x => x.CompanyId == companyId);
                LoginInfoCreate loginInfo = PasswordHashHelper.CreatePasswordHash(password);
                company.Account.passwordHash = loginInfo.PasswordHash;
                company.Account.passwordSalt = loginInfo.Salt;
                Update(company, companyId);
                bRet = true;
            }
            catch (Exception ee)
            {
                bRet = false;
            }
            return bRet;
        }
        public CompanyDto IsValidCompany(string username, string password)
        {
            var company = new Company();
            var companyDto = new CompanyDto { AddressDto = new AddressDto(),MenuListDto=new List<MenuDto>() };
            try
            {
                bool bRet = false;
                company = FirstOrDefault(x => x.Username == username);
                if (company != null)
                {
                    var loginInfo = new LoginInfoCreate();
                    loginInfo.PasswordHash = company.Account.passwordHash;
                    loginInfo.SaltVal = company.Account.passwordSalt.ToString();
                    bRet = PasswordHashHelper.ValidatePasswordHash(loginInfo, password);

                    if (bRet)
                    {
                        CopyTocompanyDto(companyDto, company);
                        return companyDto;
                    }
                    else
                    {
                        companyDto = null;
                    }
                }
                else { companyDto = null; }
            }
            catch (Exception e)
            {
                companyDto = null;
            }
            return companyDto;
        }

        private void CopyTocompanyDto(CompanyDto destination, Company source)
        {
            destination.AddressId = source.AddressId;
            destination.CompanyId = source.CompanyId;
            destination.CreatedOn = source.CreatedOn;
            destination.DBFolderPath = source.DBFolderPath;
            destination.Name = source.Name;
            destination.PasswordExpireOn = source.PasswordExpireOn;
            destination.Username = source.Username;
            destination.Status = source.Status;
            if (destination.AddressDto != null && source.Address != null)
            {
                destination.AddressDto.AddressLine1 = source.Address.AddressLine1;
                destination.AddressDto.City = source.Address.City;
                destination.AddressDto.Country = source.Address.Country;
                destination.AddressDto.Location = source.Address.Location;
                destination.AddressDto.State = source.Address.State;
            }
            if (destination.MenuListDto != null && source.CompanyMenuList != null)
            {
                foreach (var menu in source.CompanyMenuList)
                {
                    var menuDto = new MenuDto();
                    menuDto.MenuId = menu.MenuId;
                    menuDto.Logo = menu.Menu.Logo;
                    menuDto.Name = menu.Menu.Name;
                    menuDto.Path = menu.Menu.Path;
                    menuDto.Style = menu.Menu.Style;
                    destination.MenuListDto.Add(menuDto);
                }
            }


        }
        private void CopyFromCompanyDto(Company destination, CompanyDto source)
        {
            destination.AddressId = source.AddressId;
            destination.CompanyId = source.CompanyId;
            destination.CreatedOn = source.CreatedOn;
            destination.DBFolderPath = source.DBFolderPath;
            destination.Name = source.Name;
            destination.PasswordExpireOn = source.PasswordExpireOn;
            destination.Username = source.Username;
            destination.Status = source.Status;
            if (destination.Account != null && source.AccountDto != null)
            {
                destination.Account.passwordHash = source.AccountDto.PasswordHash;
                destination.Account.passwordSalt = source.AccountDto.PasswordSalt;
            }
            if (destination.Address != null && source.AddressDto != null)
            {
                destination.Address.AddressLine1 = source.AddressDto.AddressLine1;
                destination.Address.City = source.AddressDto.City;
                destination.Address.Country = source.AddressDto.Country;
                destination.Address.Location = source.AddressDto.Location;
                destination.Address.State = source.AddressDto.State;
            }
            if (destination.CompanyMenuList != null && source.MenuListDto != null)
            {
                foreach (var menuDto in source.MenuListDto)
                {
                    if (menuDto.IsChecked)
                    {
                        var menu = new CompanyMenuList();
                        menu.MenuId = menuDto.MenuId;
                        destination.CompanyMenuList.Add(menu);
                    }
                }
            }

        }
        public CompanyDto CreateLoginInfo(CompanyDto compnayDto)
        {
            LoginInfoCreate loginInfo = PasswordHashHelper.CreatePasswordHash(compnayDto.Password);
            compnayDto.AccountDto = new AccountDto();
            compnayDto.AccountDto.PasswordSalt = loginInfo.Salt;
            compnayDto.AccountDto.PasswordHash = loginInfo.PasswordHash;
            return compnayDto;
        }
    }
}
