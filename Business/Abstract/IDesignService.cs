using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Design_;

namespace Business.Abstract;

public interface IDesignService : IServiceBase<Design>, IServiceBaseAsync<Design>
{
    Task<Design?> GetAsync(Expression<Func<Design, bool>> where, CancellationToken cancellationToken = default);
    Task<ICollection<Design>?> GetListAsync(Expression<Func<Design, bool>>? where = default, CancellationToken cancellationToken = default);
    Task<TResponse?> GetAsync<TResponse>(Expression<Func<Design, bool>> where, CancellationToken cancellationToken = default) where TResponse : IDto;
    Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Design, bool>>? where = default, CancellationToken cancellationToken = default) where TResponse : IDto;
    Task<Design?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ICollection<Design>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
    Task<PaginationResponse<Design>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
    Task<DesignDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ICollection<DesignDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
    Task<PaginationResponse<DesignDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
    Task<DesignDto> CreateAsync(DesignCreateDto request, CancellationToken cancellationToken = default);
    Task<DesignDto> UpdateAsync(DesignUpdateDto request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DatatableResponseClientSide<Design>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
    Task<DatatableResponseServerSide<Design>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
}