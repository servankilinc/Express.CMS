using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class HomeSectionRepository : RepositoryBase<HomeSection, AppDbContext>, IHomeSectionRepository
    {
        public HomeSectionRepository(AppDbContext context) : base(context)
        {
        }
    }
}