using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.ProductGroup_;

namespace Business.Abstract
{
    public interface IProductGroupService : IServiceBase<ProductGroup>, IServiceBaseAsync<ProductGroup>
    {
        Task<ProductGroup?> GetAsync(Expression<Func<ProductGroup, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<ProductGroup>?> GetListAsync(Expression<Func<ProductGroup, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<ProductGroup, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<ProductGroup, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<ProductGroup, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<ProductGroup?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<ProductGroup>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ProductGroup>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ProductGroupDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<ProductGroupDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ProductGroupDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ProductGroupDetailDto?> GetByDetailAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<ProductGroupDetailDto>?> GetAllByDetailAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ProductGroupDetailDto>> GetListByDetailAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ProductGroupDto> CreateAsync(ProductGroupCreateDto request, CancellationToken cancellationToken = default);
        Task<ProductGroupDto> UpdateAsync(ProductGroupUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<ProductGroup>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<ProductGroup>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
        Task SetProductPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default);
    }
}