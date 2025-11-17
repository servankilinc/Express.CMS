using Business.ServiceBase;
using Core.BaseRequestModels;
using Core.Model;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Project_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IProjectService : IServiceBase<Project>, IServiceBaseAsync<Project>
    {
        Task<Project?> GetAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Project>?> GetListAsync(Expression<Func<Project, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Project, bool>>? where = default, CancellationToken cancellationToken = default) where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Project, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Project>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Project>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<ProjectDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<ProjectDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ProjectDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<Project> CreateAsync(ProjectCreateDto request, CancellationToken cancellationToken = default);
        Task<Project> UpdateAsync(ProjectUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Project>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Project>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}