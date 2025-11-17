using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.Company_;

namespace Business.Abstract
{
    public interface ICompanyService : IServiceBase<Company>, IServiceBaseAsync<Company>
    {
        Task<Company?> GetAsync(Expression<Func<Company, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<Company>?> GetListAsync(Expression<Func<Company, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<Company, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Company, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<Company, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<Company?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<Company>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<Company>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<Company> CreateAsync(CompanyCreateDto request, CancellationToken cancellationToken = default);
        Task<Company> UpdateAsync(CompanyUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<Company>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<Company>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}