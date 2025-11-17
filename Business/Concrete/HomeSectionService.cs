using AutoMapper;
using Business.Abstract;
using Business.ServiceBase;
using Castle.DynamicProxy;
using Core.BaseRequestModels;
using Core.Model;
using Core.Utils.Caching;
using Core.Utils.CrossCuttingConcerns;
using Core.Utils.Datatable;
using Core.Utils.Pagination;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.Design_;
using Model.Dtos.HomeSection_;
using Model.Entities;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class HomeSectionService : ServiceBase<HomeSection, IHomeSectionRepository>, IHomeSectionService
    {
        private readonly ICacheService _cacheService;
        public HomeSectionService(IHomeSectionRepository homeSectionRepository, IMapper mapper, ICacheService cacheService) : base(homeSectionRepository, mapper)
        {
            _cacheService = cacheService;
        }

        public async Task<HomeSection?> GetAsync(Expression<Func<HomeSection, bool>> where, CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<HomeSection>?> GetListAsync(Expression<Func<HomeSection, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<TResponse?> GetAsync<TResponse>(Expression<Func<HomeSection, bool>> where, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<TResponse>?> GetListAsync<TResponse>(Expression<Func<HomeSection, bool>>? where = default, CancellationToken cancellationToken = default)
            where TResponse : IDto
        {
            var result = await _GetListAsync<TResponse>(where: where, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<SelectList> GetSelectListAsync(Expression<Func<HomeSection, bool>>? where = default, CancellationToken cancellationToken = default)
        {
            var result = new SelectList(await _GetListAsync(select: s => new { s.Id, s.Name }, where: where, tracking: false, cancellationToken: cancellationToken), "Id", "Name");
            return result;
        }

        public async Task<HomeSection?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync(where: (f) => f.Id == id, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<HomeSection>?> GetAllAsync(DynamicRequest? request, CancellationToken cancellationToken = default)
        {
            var result = await _GetListAsync(filter: request?.Filter, sorts: request?.Sorts, tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<PaginationResponse<HomeSection>> GetListAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<HomeSectionDto?> GetByBasicAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var result = await _GetAsync<HomeSectionDto>(where: (f) => f.Id == id, include: (i) => i.Include(x => x.Design), tracking: false, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<ICollection<HomeSectionDto>?> GetAllByBasicAsync(DynamicRequest? request = default, CancellationToken cancellationToken = default)
        {
            //var resultCache = _cacheService.GetFromCache("HomeSectionList");
            //if (resultCache.IsSuccess && resultCache.Source != null)
            //{
            //    var source = JsonConvert.DeserializeObject<ICollection<HomeSectionDto>>(resultCache.Source!);
            //    if (source != null)
            //    {
            //        return source;
            //    }
            //}

            var result = await _GetListAsync(
                filter: request?.Filter, 
                sorts: request?.Sorts, 
                include: (i) => i.Include(x => x.Design), 
                select: s => new HomeSectionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Priority = s.Priority,
                    DesignId = s.DesignId,
                    DesignModel = new DesignRenderDto
                    {
                        Id = s.Design != null ? s.Design.Id : default,
                        Html = s.Design != null ? s.Design.Html : default,
                        Css = s.Design != null ? s.Design.Css : default,
                        Script = s.Design != null ? s.Design.Script : default
                    }
                },
                tracking: false, 
                cancellationToken: cancellationToken
            );

            //if (result != null)
            //{
            //    _cacheService.AddToCache("HomeSectionList", ["HomeSections"], result);
            //}
            return result;
        }

        public async Task<PaginationResponse<HomeSectionDto>> GetListByBasicAsync(DynamicPaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _PaginationAsync<HomeSectionDto>(paginationRequest: request.PaginationRequest, filter: request.Filter, sorts: request.Sorts, include: (i) => i.Include(x => x.Design), cancellationToken: cancellationToken);
            return result;
        }

        [Validation(typeof(HomeSectionCreateDto))]
        public async Task<HomeSectionDto> CreateAsync(HomeSectionCreateDto request, CancellationToken cancellationToken = default)
        {
            var priorityList = await _GetListAsync(select: s => new PriorityModel { Priority = s.Priority }, tracking: false, cancellationToken: cancellationToken);
            request.Priority = priorityList != null && priorityList.Any() ? priorityList.Max(f => f.Priority) + 1 : 1;

            var result = await _AddAsync<HomeSectionCreateDto, HomeSectionDto>(request, cancellationToken);
            return result;
        }

        [Validation(typeof(HomeSectionUpdateDto))]
        public async Task<HomeSectionDto> UpdateAsync(HomeSectionUpdateDto request, CancellationToken cancellationToken = default)
        {
            var result = await _UpdateAsync<HomeSectionUpdateDto, HomeSectionDto>(updateModel: request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseClientSide<HomeSection>> DatatableClientSideAsync(DynamicRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableClientSideAsync(filter: request.Filter, sorts: request.Sorts, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<DatatableResponseServerSide<HomeSection>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }

        public async Task SetPrioritiesAsync(ICollection<Guid> sortedList, CancellationToken cancellationToken = default)
        {
            var homeSectionList = await _GetListAsync(where: f => true, cancellationToken: cancellationToken);
            if (homeSectionList == null) return;
            homeSectionList = homeSectionList.Where(f => sortedList.Contains(f.Id)).ToList();

            var index = 1;
            foreach (var homeSectionId in sortedList)
            {
                var homeSection = homeSectionList.FirstOrDefault(f => f.Id == homeSectionId);
                if (homeSection == null) continue;
                homeSection.Priority = index;
                index++;
            }
            await _UpdateListAsync(homeSectionList, cancellationToken);
        }
    }
}