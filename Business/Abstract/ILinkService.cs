using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Link_;

namespace Business.Abstract
{
    public interface ILinkService : IServiceBase<Link>, IServiceBaseAsync<Link>
    {
        Task<Link?> GetAsync(Expression<Func<Link, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Link>?> GetListAsync(Expression<Func<Link, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Link, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Link, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Link, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Link?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Link>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Link>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<Link> CreateAsync(LinkCreateDto request, CancellationToken cancellationToken = default);
        Task<Link> UpdateAsync(LinkUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Link>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Link>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}