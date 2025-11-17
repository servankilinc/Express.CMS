using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.ReferanceGroup_;

namespace Business.Abstract
{
    public interface IReferanceGroupService : IServiceBase<ReferanceGroup>, IServiceBaseAsync<ReferanceGroup>
    {
        Task<ReferanceGroup?> GetAsync(Expression<Func<ReferanceGroup, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<ReferanceGroup>?> GetListAsync(Expression<Func<ReferanceGroup, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<ReferanceGroup, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<ReferanceGroup, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<ReferanceGroup, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<ReferanceGroup?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<ReferanceGroup>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ReferanceGroup>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ReferanceGroupDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<ReferanceGroupDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ReferanceGroupDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ReferanceGroupDetailDto?> GetByDetailAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<ReferanceGroupDetailDto>?> GetAllByDetailAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ReferanceGroupDetailDto>> GetListByDetailAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ReferanceGroupDto> CreateAsync(ReferanceGroupCreateDto request, CancellationToken cancellationToken = default);
        Task<ReferanceGroupDto> UpdateAsync(ReferanceGroupUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<ReferanceGroup>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<ReferanceGroup>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}