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
using Model.Dtos.Company_;
using Model.Dtos.Product_;
using Model.Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class CompanyService : ServiceBase<Company, ICompanyRepository>, ICompanyService
    {
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper) : base(companyRepository, mapper)
        {
        }

        public async Task<Company?> GetAsync(Expression<Func<Company, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Company>?> GetListAsync(Expression<Func<Company, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<Company, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<Company, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<Company, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Logo }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Logo");
            return result;
        }

        public async Task<Company?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<Company>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<Company>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(CompanyCreateDto))]
        public async Task<Company> CreateAsync(CompanyCreateDto request, CancellationToken cancellationToken = default)
        { 
            var result = await _AddAsync<CompanyCreateDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(CompanyUpdateDto))]
        public async Task<Company> UpdateAsync(CompanyUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<CompanyUpdateDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<Company>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<Company>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }
    }
}