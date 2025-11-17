using AutoMapper;
using Business.Abstract;
using Business.ServiceBase;
using Core.BaseRequestModels;
using Core.Model;
using Core.Utils.CrossCuttingConcerns;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using DataAccess.Abstract;
using Model.Dtos.Design_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class DesignService : ServiceBase<Design, IDesignRepository>, IDesignService
    {
        public DesignService(IDesignRepository designRepository, IMapper mapper) : base(designRepository, mapper)
        {
        }

        public async Task<Design?> GetAsync(Expression<Func<Design, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Design>?> GetListAsync(Expression<Func<Design, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<Design, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Design, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<Design?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Design>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<Design>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DesignDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<DesignDto>(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<DesignDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<DesignDto>(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<DesignDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<DesignDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(DesignCreateDto))]
        public async Task<DesignDto> CreateAsync(DesignCreateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _AddAsync<DesignCreateDto, DesignDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(DesignUpdateDto))]
        public async Task<DesignDto> UpdateAsync(DesignUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<DesignUpdateDto, DesignDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<Design>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<Design>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}