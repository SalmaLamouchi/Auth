using AutoMapper;
using DAL.Entities;
using DAL.IRepository;
using Services.DTO;
using Services.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Service
{
    public class MenuService : ServiceAsync<Menu, MenuDto>, IMenuService
    {
        private readonly IRepositoryAsync<Menu> menuRepository;
        private readonly IMapper mapper;

        public MenuService(IRepositoryAsync<Menu> _menuRepository, IMapper _mapper)
            : base(_menuRepository, _mapper)
        {
            menuRepository = _menuRepository;
            mapper = _mapper;
        }

        // Récupère les menus associés à un rôle spécifique
        public async Task<IEnumerable<MenuDto>> GetMenuByIdRoleAsync(int roleId)
        {
            var menu = await menuRepository.GetFirstOrDefault(
         predicate: menu => menu.ListRoles.Any(role => role.IdRole == roleId),
         include: menu => menu.Include(m => m.ListRoles),
         disableTracking: true
     );

            return mapper.Map<IEnumerable<MenuDto>>(menu);
        }

        // Récupère un menu spécifique avec ses rôles associés
        public async Task<MenuDto> GetMenuWithRolesById(int menuId)
        {
            var menu = await menuRepository.GetFirstOrDefault(
                predicate: m => m.IdMenu == menuId,
                include: m => m.Include(menu => menu.ListRoles),
                disableTracking: true
            );

            return menu == null ? null : mapper.Map<MenuDto>(menu);
        }
    }
}
