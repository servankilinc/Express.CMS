using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Blog_;

namespace Business.Abstract
{
    public interface IBlogService : IServiceBase<Blog>, IServiceBaseAsync<Blog>
    {
        Task<Blog?> GetAsync(Expression<Func<Blog, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Blog>?> GetListAsync(Expression<Func<Blog, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Blog, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Blog, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Blog, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Blog?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Blog>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Blog>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<BlogDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<BlogDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default);
        Task<PaginationResponse<BlogDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<BlogDto> CreateAsync(BlogCreateDto request, CancellationToken cancellationToken = default);
        Task<BlogDto> UpdateAsync(BlogUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Blog> UndoDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Blog>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Blog>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}