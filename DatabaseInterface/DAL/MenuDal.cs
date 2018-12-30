using Aldelo.DatabaseInterface.BOL;
using Aldelo.DatabaseInterface.Entity;
using DatabaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aldelo.DatabaseInterface.DAL
{
    public class MenuDal : RepositoryBase<Menu>
    {
        public List<MenuDto> GetAllMenuDal()
        {
            var menuDtoList = new List<MenuDto>();
            var menuList = GetAll();
            foreach (var menu in menuList)
            {
                var menuDto = new MenuDto();
                menuDto.Logo = menu.Logo;
                menuDto.Name = menu.Name;
                menuDto.Path = menu.Path;
                menuDto.Style = menu.Style;
                menuDto.MenuId = menu.MenuId;
                menuDtoList.Add(menuDto);
            }
            return menuDtoList;
        }
        public bool SaveMenuListDal(List<MenuDto> menuListDto)
        {
            try
            {
                foreach (var menuDto in menuListDto)
                {
                    if (menuDto.IsChecked)
                    {
                        var menu = new Menu();
                        menu.Logo = menuDto.Logo;
                        menu.Name = menuDto.Name;
                        menu.Path = menuDto.Path;
                        menu.Style = menuDto.Style;
                        Add(menu);
                    }
                }
                Save();
            }
            catch (Exception ee)
            {
                return false;

            }
            return true;
        }

    }
}
