using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Announcement_;

namespace Business.Abstract
{
    public interface IAnnouncementService : IServiceBase<Announcement>, IServiceBaseAsync<Announcement>
    {
        Task<Announcement?> GetAsync(Expression<Func<Announcement, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Announcement>?> GetListAsync(Expression<Func<Announcement, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Announcement, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Announcement, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Announcement, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Announcement?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Announcement>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Announcement>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<Announcement> CreateAsync(AnnouncementCreateDto request, CancellationToken cancellationToken = default);
        Task<Announcement> UpdateAsync(AnnouncementUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Announcement>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Announcement>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}