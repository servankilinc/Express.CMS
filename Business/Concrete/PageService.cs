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
using Model.Dtos.Page_;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class PageService : ServiceBase<Page, IPageRepository>, IPageService
    {
        public PageService(IPageRepository pageRepository, IMapper mapper) : base(pageRepository, mapper)
        {
        }

        public async Task<Page?> GetAsync(Expression<Func<Page, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Page>?> GetListAsync(Expression<Func<Page, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<Page, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Page, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<Page, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Name }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Name");
            return result;
        }

        public async Task<Page?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Page>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<Page>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PageDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<PageDto>(where: (f) => f.Id == id, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        //[Cache("PatheRenderByName", ["PathGroup"])]
        public async Task<PageDto?> GetBasicByNameAsync(string pathName, CancellationToken cancellationToken = default)
        {
            if (pathName == default) throw new ArgumentNullException(nameof(pathName));
            pathName = pathName.Trim();
            var result = await _GetAsync<PageDto>(where: (f) => f.PathName == pathName, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<PageDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<PageDto>(filter: request?.Filter, sorts: request?.Sorts, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<PageDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<PageDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Design), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(PageCreateDto))]
        public async Task<PageDto> CreateAsync(PageCreateDto request, CancellationToken cancellationToken = default)
        {
            var priorityList = await _GetListAsync(select: s => new PriorityModel { Priority = s.Priority }, tracking: false, cancellationToken: cancellationToken);
            request.Priority = priorityList != null && priorityList.Any() ? priorityList.Max(f => f.Priority) + 1 : 1;

            var result = await _AddAsync<PageCreateDto, PageDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(PageUpdateDto))]
        public async Task<PageDto> UpdateAsync(PageUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<PageUpdateDto, PageDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<Page>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<Page>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}