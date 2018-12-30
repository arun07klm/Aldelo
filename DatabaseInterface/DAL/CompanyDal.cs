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
                var company = new Company { Account = new Account() ,Address=new Address()};
                CreateLoginInfo(companyDto);
                CopyFromCompanyDto(company, companyDto);
                Add(company);
                Save();
            }
            catch(Exception ee)
            {
                return false;

            }

            return true;
        }

        public CompanyDto IsValidCompany(string username, string password)
        {
            var company = new Company();
            var companyDto = new CompanyDto { AddressDto = new AddressDto() };
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
                destination.AddressDto.State = source.Address.Location;
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
            destination.Account.passwordHash = source.AccountDto.PasswordHash;
            destination.Account.passwordSalt = source.AccountDto.PasswordSalt;
            if (destination.Address != null && source.AddressDto != null)
            {
                destination.Address.AddressLine1 = source.AddressDto.AddressLine1;
                destination.Address.City = source.AddressDto.City;
                destination.Address.Country = source.AddressDto.Country;
                destination.Address.Location = source.AddressDto.Location;
                destination.Address.State = source.AddressDto.Location;
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
