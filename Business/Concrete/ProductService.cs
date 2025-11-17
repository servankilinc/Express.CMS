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
using Model.Dtos.Product_;
using Model.Dtos.DetailSection_;
using Core.Utils;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class ProductService : ServiceBase<Product, IProductRepository>, IProductService
    {
        private readonly IDetailSectionService _detailSectionService;
        public ProductService(IProductRepository productRepository, IMapper mapper, IDetailSectionService detailSectionService) : base(productRepository, mapper)
        {
            _detailSectionService = detailSectionService;
        }

        public async Task<Product?> GetAsync(Expression<Func<Product, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Product>?> GetListAsync(Expression<Func<Product, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<Product, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Product, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<Product, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Name }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Name");
            return result;
        }

        public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Product>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<Product>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: i => i.Include(x => x.ProductGroup), cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ProductDto?> GetByBasicAsync(string friendlyUrl, CancellationToken cancellationToken = default)
        {
            if (friendlyUrl == default) throw new ArgumentNullException(nameof(friendlyUrl));
            var result = await _GetAsync<ProductDto>(where: (f) => f.FriendlyUrl == friendlyUrl, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);

            var detailSections = await _detailSectionService.GetListByProdctAsync(productId: result!.Id, cancellationToken: cancellationToken);
            if (result != null) result.DetailSections = detailSections;
            
            return result;
        }

        public async Task<ICollection<ProductDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<ProductDto>(filter: request?.Filter, sorts: request?.Sorts, include: (i) => i.Include(x => x.Design).Include(x => x.DetailSections), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<ProductDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<ProductDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Design).Include(x => x.DetailSections), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(ProductCreateDto))]
        public async Task<ProductDto> CreateAsync(ProductCreateDto request, CancellationToken cancellationToken = default)
        {
            var priorityList = await _GetListAsync(where: f => f.ProductGroupId == request.ProductGroupId, select: s => new PriorityModel { Priority = s.Priority }, tracking: false, cancellationToken: cancellationToken);
            request.Priority = priorityList != null && priorityList.Any() ? priorityList.Max(f => f.Priority) + 1 : 1;

            request.FriendlyUrl = request.Name.ToSeoFriendly();

            var result = await _AddAsync<ProductCreateDto, ProductDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(ProductUpdateDto))]
        public async Task<ProductDto> UpdateAsync(ProductUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<ProductUpdateDto, ProductDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<Product>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<Product>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}