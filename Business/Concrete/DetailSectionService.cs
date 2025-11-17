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
using Model.Dtos.Design_;
using Model.Dtos.DetailSection_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Concrete;


[ExceptionHandler]
public class DetailSectionService : ServiceBase<DetailSection, IDetailSectionRepository>, IDetailSectionService
{
    public DetailSectionService(IDetailSectionRepository detailSectionRepository, IMapper mapper) : base(detailSectionRepository, mapper)
    {
    }

    public async Task<DetailSection?> GetAsync(Expression<Func<DetailSection, bool>> where, CancellationToken cancellationToken = default)
    {
        var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<ICollection<DetailSection>?> GetListAsync(Expression<Func<DetailSection, bool>>? where = default, CancellationToken cancellationToken = default)
    {
        var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<DetailSection, bool>> where, CancellationToken cancellationToken = default) where TResponse : IDto
    {
        var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<DetailSection, bool>>? where = default, CancellationToken cancellationToken = default) where TResponse : IDto
    {
        var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    //[Cache("DetailSectionsByProduct", ["GroupDetailSection"])]
    public async Task<ICollection<DetailSectionDto>?> GetListByProdctAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var result = await _GetListAsync(
            where: f => f.ProductId == productId, 
            select: s => new DetailSectionDto
            {
                Id = s.Id,
                Title = s.Title,
                Design = new DesignRenderDto
                {
                    Id = s.Design != null ? s.Design.Id : default,
                    Html = s.Design != null ? s.Design.Html : string.Empty,
                    Css= s.Design != null ? s.Design.Css : string.Empty,
                    Script= s.Design != null ? s.Design.Script : string.Empty,
                }, 
                Priority = s.Priority,
                ProductId = s.ProductId.HasValue ? s.ProductId.Value : default,
            },
            tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<SelectList> GetSelectListAsync(Expression<Func<DetailSection, bool>>? where = default, CancellationToken cancellationToken = default)
    {
        var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Title }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Ttile");
        return result;
    }

    public async Task<DetailSection?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == default)
            throw new ArgumentNullException(nameof(id));
        var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<ICollection<DetailSection>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
    {
        var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<PaginationResponse<DetailSection>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<DetailSectionDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == default)
            throw new ArgumentNullException(nameof(id));
        var result = await _GetAsync<DetailSectionDto>(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<ICollection<DetailSectionDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
    {
        var result = await _GetListAsync<DetailSectionDto>(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<PaginationResponse<DetailSectionDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _PaginationAsync<DetailSectionDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
        return result;
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == default)
            throw new ArgumentNullException(nameof(id));
        await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<DatatableResponseClientSide<DetailSection>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
        return result;
    }

    public async Task<DatatableResponseServerSide<DetailSection>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
        return result;
    }


    [Validation(typeof(ProductDetailSectionCreateDto))]
    public async Task<DetailSection> CreateProductAsync(ProductDetailSectionCreateDto request, CancellationToken cancellationToken = default)
    {
        var priorityList = await _GetListAsync(select: s => new PriorityModel { Priority = s.Priority }, tracking: false, cancellationToken: cancellationToken);
        request.Priority = priorityList != null && priorityList.Any() ? priorityList.Max(f => f.Priority) + 1 : 1;

        var result = await _AddAsync<ProductDetailSectionCreateDto>(request, cancellationToken);
        return result;
    }

    [Validation(typeof(ProductDetailSectionUpdateDto))]
    public async Task<DetailSection> UpdateForProductAsync(ProductDetailSectionUpdateDto request, CancellationToken cancellationToken = default)
    {
        var result = await _UpdateAsync<ProductDetailSectionUpdateDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
        return result;
    }
}