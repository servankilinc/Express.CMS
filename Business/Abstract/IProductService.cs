using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Product_;

namespace Business.Abstract
{
    public interface IProductService : IServiceBase<Product>, IServiceBaseAsync<Product>
    {
        Task<Product?> GetAsync(Expression<Func<Product, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Product>?> GetListAsync(Expression<Func<Product, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Product, bool>> where, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Product, bool>>? where = default, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Product, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Product>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Product>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ProductDto?> GetByBasicAsync(string friendlyUrl, CancellationToken cancellationToken = default);
        Task<ICollection<ProductDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ProductDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ProductDto> CreateAsync(ProductCreateDto request, CancellationToken cancellationToken = default);
        Task<ProductDto> UpdateAsync(ProductUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Product>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Product>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}