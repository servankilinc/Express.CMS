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
using Model.Dtos.Project_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class ProjectService : ServiceBase<Project, IProjectRepository>, IProjectService
    {
        public ProjectService(IProjectRepository projectRepository, IMapper mapper) : base(projectRepository, mapper)
        {
        }

        public async Task<Project?> GetAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Project>?> GetListAsync(Expression<Func<Project, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Project, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<Project, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Name }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Name");
            return result;
        }

        public async Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Project>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<Project>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ProjectDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<ProjectDto>(where: (f) => f.Id == id, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ProjectDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<ProjectDto>(filter: request?.Filter, sorts: request?.Sorts, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<ProjectDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<ProjectDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Design), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(ProjectCreateDto))]
        public async Task<Project> CreateAsync(ProjectCreateDto request, CancellationToken cancellationToken = default)
        {
            var priorityList = await _GetListAsync(select: s => new PriorityModel { Priority = s.Priority }, tracking: false, cancellationToken: cancellationToken);
            request.Priority = priorityList != null && priorityList.Any() ? priorityList.Max(f => f.Priority) + 1 : 1;

            var result = await _AddAsync<ProjectCreateDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(ProjectUpdateDto))]
        public async Task<Project> UpdateAsync(ProjectUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<ProjectUpdateDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<Project>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<Project>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}