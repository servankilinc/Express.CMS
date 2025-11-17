using AutoMapper;
using Business.Abstract;
using Business.ServiceBase;
using Core.BaseRequestModels;
using Core.Model;
using Core.Utils.CrossCuttingConcerns;
using Core.Utils.Datatable;
using Core.Utils.ExceptionHandle.Exceptions;
using Core.Utils.Pagination;
using DataAccess.Abstract;
using DataAccess.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.User_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class UserService : ServiceBase<User, IUserRepository>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserService(IUnitOfWork unitWork, IUserRepository userRepository, IMapper mapper, UserManager<User> userManager) : base(userRepository, mapper)
        {
            _unitOfWork = unitWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region Get User
        public async Task<User?> GetAsync(Expression<Func<User, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<User>?> GetListAsync(Expression<Func<User, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<User, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<User, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<User, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.FullName }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "FullName");
            return result;
        }

        public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<User>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<User>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<UserDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<UserDto>(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<UserDto>?> GetAllByBasicAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync<UserDto>(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<UserDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<UserDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }
        #endregion

        [Validation(typeof(UserCreateDto))]
        public async Task<UserDto> CreateAsync(UserCreateDto request, CancellationToken cancellationToken = default)
        {
            var userExist = await _userManager.FindByNameAsync(request.UserName);
            if (userExist != null)
                throw new BusinessException("Kullanıcı adı sistemde zaten mevcut.", description: $"Requester user name : {request.UserName}");

            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new GeneralException(string.Join("\n", result.Errors.Select(e => e.Description)), description: $"User cannot be created. Requester user name: {request.UserName}");

            if (request.RoleList != null && request.RoleList.Any())
            {
                var roleResult = await _userManager.AddToRolesAsync(user, request.RoleList);
                if (!roleResult.Succeeded)
                    throw new GeneralException("Failed to assign role.", description: $"Requester user name: {request.UserName}");
            }

            return _mapper.Map<UserDto>(user);
        }

        [Validation(typeof(UserUpdateDto))]
        public async Task<UserDto> UpdateAsync(UserUpdateDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var user = await _unitOfWork.Users.GetAsync(where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
                if (user == null) throw new BusinessException($"Kullanıcı bilgileri bulunamadı.");

                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.OldPassword);
                if (!isPasswordValid) throw new BusinessException("Girdiğiniz şifre doğru değil.", description: $"Requester user name: {request.UserName}");

                var userToUpdate = _mapper.Map(request, user);
                if (!string.IsNullOrEmpty(request.NewPassword))
                {
                    var resultChangePassword = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                    if (!resultChangePassword.Succeeded) throw new GeneralException(string.Join("\n", resultChangePassword.Errors.Select(e => e.Description)), description: $"User password cannot be update. user name: {request.UserName}");
                }

                var resultUpdateUser = await _userManager.UpdateAsync(user);
                if (!resultUpdateUser.Succeeded) throw new GeneralException(string.Join("\n", resultUpdateUser.Errors.Select(e => e.Description)), description: $"User cannot be update. user name: {request.UserName}");

                // Rolleri Güncelle
                var existingRoles = await _userManager.GetRolesAsync(user);
                var requestedRoles = request.RoleList ?? new List<string>();

                var comparer = StringComparer.OrdinalIgnoreCase;
                var existingSet = new HashSet<string>(existingRoles, comparer);
                var requestedSet = new HashSet<string>(requestedRoles.Select(r => r.Trim()), comparer);

                var toAdd = requestedSet.Except(existingSet).ToList();
                var toRemove = existingSet.Except(requestedSet).ToList();
                if (toAdd.Any()) await _userManager.AddToRolesAsync(user, toAdd);
                if (toRemove.Any()) await _userManager.RemoveFromRolesAsync(user, toRemove);


                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return _mapper.Map<UserDto>(userToUpdate);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;    
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<User>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<User>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}