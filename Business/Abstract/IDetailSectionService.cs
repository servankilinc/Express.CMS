using Business.ServiceBase;
using Core.BaseRequestModels;
using Core.Model;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.DetailSection_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IDetailSectionService : IServiceBase<DetailSection>, IServiceBaseAsync<DetailSection>
    {
        Task<DetailSection?> GetAsync(Expression<Func<DetailSection, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<DetailSection>?> GetListAsync(Expression<Func<DetailSection, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<DetailSection, bool>> where, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<DetailSection, bool>>? where = default, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<DetailSection, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<DetailSection?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<DetailSection>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<DetailSection>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<DetailSectionDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<DetailSectionDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<DetailSectionDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<DetailSection>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<DetailSection>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);

        // Create and Update for Related Entities
        Task<DetailSection> CreateProductAsync(ProductDetailSectionCreateDto request, CancellationToken cancellationToken = default);
        Task<DetailSection> UpdateForProductAsync(ProductDetailSectionUpdateDto request, CancellationToken cancellationToken = default);
        Task<ICollection<DetailSectionDto>?> GetListByProdctAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}