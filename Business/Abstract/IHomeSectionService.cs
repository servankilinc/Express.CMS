using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.HomeSection_;

namespace Business.Abstract
{
    public interface IHomeSectionService : IServiceBase<HomeSection>, IServiceBaseAsync<HomeSection>
    {
        Task<HomeSection?> GetAsync(Expression<Func<HomeSection, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<HomeSection>?> GetListAsync(Expression<Func<HomeSection, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<HomeSection, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<HomeSection, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<HomeSection, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<HomeSection?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<HomeSection>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<HomeSection>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<HomeSectionDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<HomeSectionDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default);
        Task<PaginationResponse<HomeSectionDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<HomeSectionDto> CreateAsync(HomeSectionCreateDto request, CancellationToken cancellationToken = default);
        Task<HomeSectionDto> UpdateAsync(HomeSectionUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<HomeSection>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<HomeSection>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
        Task SetPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default);
    }
}