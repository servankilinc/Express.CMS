using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using Model.Entities;
using Core.Model;
using Model.Dtos.User_;

namespace Business.Abstract
{
    public interface IUserService : IServiceBase<User>, IServiceBaseAsync<User>
    {
        Task<User?> GetAsync(Expression<Func<User, bool>> where, CancellationToken cancellationToken = default);
        Task<ICollection<User>?> GetListAsync(Expression<Func<User, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>(Expression<Func<User, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<User, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto;
        Task<SelectList> GetSelectListAsync(Expression<Func<User, bool>>? where = default, CancellationToken cancellationToken = default);
        Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<User>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<User>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<UserDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<UserDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default);
        Task<PaginationResponse<UserDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default);
        Task<UserDto> CreateAsync(UserCreateDto request, CancellationToken cancellationToken = default);
        Task<UserDto> UpdateAsync(UserUpdateDto request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseClientSide<User>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<User>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}