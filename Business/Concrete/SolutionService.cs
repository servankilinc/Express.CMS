using AutoMapper;
using Business.Abstract;
using Business.ServiceBase;
using Core.BaseRequestModels;
using Core.Model;
using Core.Utils;
using Core.Utils.CrossCuttingConcerns;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.Solution_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class SolutionService : ServiceBase<Solution, ISolutionRepository>, ISolutionService
    {
        public SolutionService(ISolutionRepository solutionRepository, IMapper mapper) : base(solutionRepository, mapper)
        {
        }

        public async Task<Solution?> GetAsync(Expression<Func<Solution, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Solution>?> GetListAsync(Expression<Func<Solution, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<Solution, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Solution, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<Solution, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Name }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Name");
            return result;
        }

        public async Task<Solution?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Solution>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<Solution>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: i => i.Include(x => x.SolutionGroup), cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SolutionDto?> GetByBasicAsync(string friendlyUrl, CancellationToken cancellationToken = default)
        {
            if (friendlyUrl == default) throw new ArgumentNullException(nameof(friendlyUrl));
            var result = await _GetAsync<SolutionDto>(where: (f) => f.FriendlyUrl == friendlyUrl, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<SolutionDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<SolutionDto>(filter: request?.Filter, sorts: request?.Sorts, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<SolutionDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<SolutionDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Design), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(SolutionCreateDto))]
        public async Task<SolutionDto> CreateAsync(SolutionCreateDto request, CancellationToken cancellationToken = default)
        {
            var priorityList = await _GetListAsync(where: f => f.SolutionGroupId == request.SolutionGroupId, select: s => new PriorityModel { Priority = s.Priority }, tracking: false, cancellationToken: cancellationToken);
            request.Priority = priorityList != null && priorityList.Any() ? priorityList.Max(f => f.Priority) + 1 : 1;

            request.FriendlyUrl = request.Name.ToSeoFriendly();

            var result = await _AddAsync<SolutionCreateDto, SolutionDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(SolutionUpdateDto))]
        public async Task<SolutionDto> UpdateAsync(SolutionUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<SolutionUpdateDto, SolutionDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<Solution>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<Solution>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}