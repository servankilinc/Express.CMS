using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Business.ServiceBase;
using Core.BaseRequestModels;
using Core.Model;
using Core.Utils.CrossCuttingConcerns;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using DataAccess.Abstract;
using DataAccess.UoW;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.Product_;
using Model.Dtos.ProductGroup_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class ProductGroupService : ServiceBase<ProductGroup, IProductGroupRepository>, IProductGroupService
    {
        private readonly IProductService _productService;
        public ProductGroupService(IProductGroupRepository productGroupRepository, IMapper mapper, IProductService productService) : base(productGroupRepository, mapper)
        { 
            _productService = productService;
        }

        public async Task<ProductGroup?> GetAsync(Expression<Func<ProductGroup, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ProductGroup>?> GetListAsync(Expression<Func<ProductGroup, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<ProductGroup, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<ProductGroup, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<ProductGroup, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Name }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Name");
            return result;
        }

        public async Task<ProductGroup?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ProductGroup>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<ProductGroup>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ProductGroupDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<ProductGroupDto>(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ProductGroupDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<ProductGroupDto>(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<ProductGroupDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<ProductGroupDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ProductGroupDetailDto?> GetByDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<ProductGroupDetailDto>(where: (f) => f.Id == id, include: (i) => i.Include(x => x.Products), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ProductGroupDetailDto>?> GetAllByDetailAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<ProductGroupDetailDto>(filter: request?.Filter, sorts: request?.Sorts, include: (i) => i.Include(x => x.Products), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<ProductGroupDetailDto>> GetListByDetailAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<ProductGroupDetailDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Products), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(ProductGroupCreateDto))]
        public async Task<ProductGroupDto> CreateAsync(ProductGroupCreateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _AddAsync<ProductGroupCreateDto, ProductGroupDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(ProductGroupUpdateDto))]
        public async Task<ProductGroupDto> UpdateAsync(ProductGroupUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<ProductGroupUpdateDto, ProductGroupDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<ProductGroup>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<ProductGroup>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }

        public async Task SetProductPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default)
        {
            var productList = await _productService.GetListAsync(where: f => true, cancellationToken: cancellationToken);
            if (productList == null) return;
            productList = productList.Where(f => sortedList.Contains(f.Id)).ToList();

            var index = 1;
            foreach (var productId in sortedList)
            {
                var prod = productList.FirstOrDefault(f => f.Id == productId);
                if (prod == null) continue;
                prod.Priority = index;
                index++;
            }
            await _productService._UpdateListAsync(productList, cancellationToken);
        }
    }
}