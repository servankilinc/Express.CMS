using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Solution_;

namespace Business.Abstract
{
    public interface ISolutionService : IServiceBase<Solution>, IServiceBaseAsync<Solution>
    {
        Task<Solution?> GetAsync(Expression<Func<Solution, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Solution>?> GetListAsync(Expression<Func<Solution, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Solution, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Solution, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Solution, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Solution?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Solution>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Solution>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<SolutionDto?> GetByBasicAsync(string friendlyUrl, CancellationToken cancellationToken = default);
        Task<ICollection<SolutionDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<SolutionDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<SolutionDto> CreateAsync(SolutionCreateDto request, CancellationToken cancellationToken = default);
        Task<SolutionDto> UpdateAsync(SolutionUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Solution>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Solution>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}