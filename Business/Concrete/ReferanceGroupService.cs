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
using Model.Dtos.ReferanceGroup_;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class ReferanceGroupService : ServiceBase<ReferanceGroup, IReferanceGroupRepository>, IReferanceGroupService
    {
        public ReferanceGroupService(IReferanceGroupRepository referanceGroupRepository, IMapper mapper) : base(referanceGroupRepository, mapper)
        {
        }

        public async Task<ReferanceGroup?> GetAsync(Expression<Func<ReferanceGroup, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ReferanceGroup>?> GetListAsync(Expression<Func<ReferanceGroup, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<ReferanceGroup, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<ReferanceGroup, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<ReferanceGroup, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Name }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Name");
            return result;
        }

        public async Task<ReferanceGroup?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ReferanceGroup>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<ReferanceGroup>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ReferanceGroupDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<ReferanceGroupDto>(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ReferanceGroupDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<ReferanceGroupDto>(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<ReferanceGroupDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<ReferanceGroupDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ReferanceGroupDetailDto?> GetByDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<ReferanceGroupDetailDto>(where: (f) => f.Id == id, include: (i) => i.Include(x => x.Referances), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<ReferanceGroupDetailDto>?> GetAllByDetailAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<ReferanceGroupDetailDto>(filter: request?.Filter, sorts: request?.Sorts, include: (i) => i.Include(x => x.Referances), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<ReferanceGroupDetailDto>> GetListByDetailAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<ReferanceGroupDetailDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Referances), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(ReferanceGroupCreateDto))]
        public async Task<ReferanceGroupDto> CreateAsync(ReferanceGroupCreateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _AddAsync<ReferanceGroupCreateDto, ReferanceGroupDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(ReferanceGroupUpdateDto))]
        public async Task<ReferanceGroupDto> UpdateAsync(ReferanceGroupUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<ReferanceGroupUpdateDto, ReferanceGroupDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<ReferanceGroup>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<ReferanceGroup>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}