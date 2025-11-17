using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.SolutionGroup_;

namespace Business.Abstract
{
    public interface ISolutionGroupService : IServiceBase<SolutionGroup>, IServiceBaseAsync<SolutionGroup>
    {
        Task<SolutionGroup?> GetAsync(Expression<Func<SolutionGroup, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<SolutionGroup>?> GetListAsync(Expression<Func<SolutionGroup, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<SolutionGroup, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<SolutionGroup, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<SolutionGroup, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<SolutionGroup?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<SolutionGroup>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<SolutionGroup>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<SolutionGroupDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<SolutionGroupDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<SolutionGroupDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<SolutionGroupDetailDto?> GetByDetailAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<SolutionGroupDetailDto>?> GetAllByDetailAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<SolutionGroupDetailDto>> GetListByDetailAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<SolutionGroupDto> CreateAsync(SolutionGroupCreateDto request, CancellationToken cancellationToken = default);
        Task<SolutionGroupDto> UpdateAsync(SolutionGroupUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<SolutionGroup>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<SolutionGroup>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
        Task SetSolutionPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default);
    }
}