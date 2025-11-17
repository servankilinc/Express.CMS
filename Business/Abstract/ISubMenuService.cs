using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.SubMenu_;

namespace Business.Abstract
{
    public interface ISubMenuService : IServiceBase<SubMenu>, IServiceBaseAsync<SubMenu>
    {
        Task<SubMenu?> GetAsync(Expression<Func<SubMenu, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<SubMenu>?> GetListAsync(Expression<Func<SubMenu, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<SubMenu, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<SubMenu, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<SubMenu, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<SubMenu?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<SubMenu>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<SubMenu>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<SubMenuDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<SubMenuDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<SubMenuDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<SubMenuDto> CreateAsync(SubMenuCreateDto request, CancellationToken cancellationToken = default);
        Task<SubMenuDto> UpdateAsync(SubMenuUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<SubMenu>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<SubMenu>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}