using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Menu_;

namespace Business.Abstract
{
    public interface IMenuService : IServiceBase<Menu>, IServiceBaseAsync<Menu>
    {
        Task<Menu?> GetAsync(Expression<Func<Menu, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Menu>?> GetListAsync(Expression<Func<Menu, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Menu, bool>> where, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Menu, bool>>? where = default, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Menu, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Menu?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Menu>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Menu>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<MenuDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<MenuDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default);
        Task<PaginationResponse<MenuDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<MenuDto> CreateAsync(MenuCreateDto request, CancellationToken cancellationToken = default);
        Task<MenuDto> UpdateAsync(MenuUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Menu>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Menu>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
        Task SetMenuPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default);
        Task SetSubMenuPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default);
    }
}