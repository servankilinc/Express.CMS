using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Page_;

namespace Business.Abstract
{
    public interface IPageService : IServiceBase<Page>, IServiceBaseAsync<Page>
    {
        Task<Page?> GetAsync(Expression<Func<Page, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Page>?> GetListAsync(Expression<Func<Page, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Page, bool>> where, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Page, bool>>? where = default, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Page, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Page?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Page>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Page>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<PageDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<PageDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default);
        Task<PaginationResponse<PageDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<PageDto> CreateAsync(PageCreateDto request, CancellationToken cancellationToken = default);
        Task<PageDto> UpdateAsync(PageUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Page>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Page>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
        Task<PageDto?> GetBasicByNameAsync(string pathName, CancellationToken cancellationToken = default);
    }
}