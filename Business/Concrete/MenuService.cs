using AutoMapper;
using Business.Abstract;
using Business.ServiceBase;
using Core.BaseRequestModels;
using Core.Model;
using Core.Utils.CrossCuttingConcerns;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System.Linq.Expressions;
using Model.Dtos.Menu_;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class MenuService : ServiceBase<Menu, IMenuRepository>, IMenuService
    {
        private readonly ISubMenuService _subMenuService;
        public MenuService(IMenuRepository menuRepository, IMapper mapper, ISubMenuService subMenuService) : base(menuRepository, mapper)
        {
            _subMenuService = subMenuService;
        }

        public async Task<Menu?> GetAsync(Expression<Func<Menu, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Menu>?> GetListAsync(Expression<Func<Menu, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<Menu, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Menu, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<Menu, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Title }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Title");
            return result;
        }

        public async Task<Menu?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Menu>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<Menu>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<MenuDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<MenuDto>(where: (f) => f.Id == id, include: (i) => i.Include(x => x.SubMenuList), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<MenuDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<MenuDto>(
                filter: request?.Filter, 
                sorts: request?.Sorts, 
                include: (i) => i.Include(x => x.SubMenuList), 
                orderBy: o => o.OrderBy(x => x.Priority),
                tracking: false, 
                cancellationToken: cancellationToken
            );
            if(result != null && result.Any()) 
            { 
                foreach (var item in result)
                {
                    if (item.SubMenuList != null && item.SubMenuList.Any()) 
                        item.SubMenuList = item.SubMenuList.OrderBy(o => o.Priority).ToList();
                }
            }

            return result;
        }

        public async Task<PaginationResponse<MenuDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<MenuDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.SubMenuList), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(MenuCreateDto))]
        public async Task<MenuDto> CreateAsync(MenuCreateDto request, CancellationToken cancellationToken = default)
        {
            var priorityList = await _GetListAsync(select: s => new PriorityModel { Priority = s.Priority }, tracking: false, cancellationToken: cancellationToken);
            request.Priority = priorityList != null && priorityList.Any() ? priorityList.Max(f => f.Priority) + 1 : 1;

            var result = await _AddAsync<MenuCreateDto, MenuDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(MenuUpdateDto))]
        public async Task<MenuDto> UpdateAsync(MenuUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<MenuUpdateDto, MenuDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<Menu>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<Menu>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }

        public async Task SetMenuPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default)
        {
            var menuList = await _GetListAsync(where: f => true, cancellationToken: cancellationToken);
            if (menuList == null) return;
            menuList = menuList.Where(f => sortedList.Contains(f.Id)).ToList();

            var index = 1;
            foreach (var menuId in sortedList)
            {
                var menu = menuList.FirstOrDefault(f => f.Id == menuId);
                if (menu == null) continue;
                menu.Priority = index;
                index++;
            }
            await _UpdateListAsync(menuList, cancellationToken);
        }

        public async Task SetSubMenuPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default)
        {
            var subMenuList = await _subMenuService.GetListAsync(where: f => true, cancellationToken: cancellationToken);
            if (subMenuList == null) return;
            subMenuList = subMenuList.Where(f => sortedList.Contains(f.Id)).ToList();

            var index = 1;
            foreach (var subMenuId in sortedList)
            {
                var subMenu = subMenuList.FirstOrDefault(f => f.Id == subMenuId);
                if (subMenu == null) continue;
                subMenu.Priority = index;
                index++;
            }
            await _subMenuService._UpdateListAsync(subMenuList, cancellationToken);
        }
    }
}