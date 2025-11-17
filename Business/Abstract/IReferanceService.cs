using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Referance_;

namespace Business.Abstract
{
    public interface IReferanceService : IServiceBase<Referance>, IServiceBaseAsync<Referance>
    {
        Task<Referance?> GetAsync(Expression<Func<Referance, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Referance>?> GetListAsync(Expression<Func<Referance, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Referance, bool>> where, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Referance, bool>>? where = default, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Referance, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Referance?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Referance>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Referance>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ReferanceDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<ReferanceDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ReferanceDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ReferanceDto> CreateAsync(ReferanceCreateDto request, CancellationToken cancellationToken = default);
        Task<ReferanceDto> UpdateAsync(ReferanceUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Referance>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Referance>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}