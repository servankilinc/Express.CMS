using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class PageRepository : RepositoryBase<Page, AppDbContext>, IPageRepository
    {
        public PageRepository(AppDbContext context) : base(context)
        {
        }
    }
}