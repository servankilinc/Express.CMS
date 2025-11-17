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
using Model.Entities;
using System.Linq.Expressions;
using Model.Dtos.SolutionGroup_;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class SolutionGroupService : ServiceBase<SolutionGroup, ISolutionGroupRepository>, ISolutionGroupService
    {
        private readonly ISolutionService _solutionService;
        public SolutionGroupService(ISolutionGroupRepository solutionGroupRepository, IMapper mapper, ISolutionService solutionService) : base(solutionGroupRepository, mapper)
        {
            _solutionService = solutionService;
        }

        public async Task<SolutionGroup?> GetAsync(Expression<Func<SolutionGroup, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<SolutionGroup>?> GetListAsync(Expression<Func<SolutionGroup, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<SolutionGroup, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<SolutionGroup, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<SolutionGroup, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Name }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Name");
            return result;
        }

        public async Task<SolutionGroup?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<SolutionGroup>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<SolutionGroup>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SolutionGroupDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<SolutionGroupDto>(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<SolutionGroupDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<SolutionGroupDto>(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<SolutionGroupDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<SolutionGroupDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SolutionGroupDetailDto?> GetByDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<SolutionGroupDetailDto>(where: (f) => f.Id == id, include: (i) => i.Include(x => x.Solutions), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<SolutionGroupDetailDto>?> GetAllByDetailAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<SolutionGroupDetailDto>(filter: request?.Filter, sorts: request?.Sorts, include: (i) => i.Include(x => x.Solutions), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<SolutionGroupDetailDto>> GetListByDetailAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<SolutionGroupDetailDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Solutions), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(SolutionGroupCreateDto))]
        public async Task<SolutionGroupDto> CreateAsync(SolutionGroupCreateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _AddAsync<SolutionGroupCreateDto, SolutionGroupDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(SolutionGroupUpdateDto))]
        public async Task<SolutionGroupDto> UpdateAsync(SolutionGroupUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<SolutionGroupUpdateDto, SolutionGroupDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<SolutionGroup>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<SolutionGroup>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }

        public async Task SetSolutionPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default)
        {
            var solutionList = await _solutionService.GetListAsync(where: f => true, cancellationToken: cancellationToken);
            if (solutionList == null) return;
            solutionList = solutionList.Where(f => sortedList.Contains(f.Id)).ToList();

            var index = 1;
            foreach (var productId in sortedList)
            {
                var prod = solutionList.FirstOrDefault(f => f.Id == productId);
                if (prod == null) continue;
                prod.Priority = index;
                index++;
            }
            await _solutionService._UpdateListAsync(solutionList, cancellationToken);
        }
    }
}