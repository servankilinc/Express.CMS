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
using Model.Dtos.Blog_;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class BlogService : ServiceBase<Blog, IBlogRepository>, IBlogService
    {
        public BlogService(IBlogRepository blogRepository, IMapper mapper) : base(blogRepository, mapper)
        {
        }

        public async Task<Blog?> GetAsync(Expression<Func<Blog, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Blog>?> GetListAsync(Expression<Func<Blog, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<Blog, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Blog, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<Blog, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Title }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Title");
            return result;
        }

        public async Task<Blog?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, include: i => i.Include(u => u.Author), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Blog>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<Blog>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<BlogDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<BlogDto>(where: (f) => f.Id == id, include: (i) => i.Include(x => x.Author), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<BlogDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<BlogDto>(filter: request?.Filter, sorts: request?.Sorts, include: (i) => i.Include(x => x.Author), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<BlogDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<BlogDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Author), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(BlogCreateDto))]
        public async Task<BlogDto> CreateAsync(BlogCreateDto request, CancellationToken cancellationToken = default)
        {
            var priorityList = await _GetListAsync(select: s => new PriorityModel { Priority = s.Priority }, tracking: false, cancellationToken: cancellationToken);
            request.Priority = priorityList != null && priorityList.Any() ? priorityList.Max(f => f.Priority) + 1 : 1;

            var result = await _AddAsync<BlogCreateDto, BlogDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(BlogUpdateDto))]
        public async Task<BlogDto> UpdateAsync(BlogUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<BlogUpdateDto, BlogDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<Blog> UndoDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));

            var result = await _UndoDeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseClientSide<Blog>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<Blog>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}